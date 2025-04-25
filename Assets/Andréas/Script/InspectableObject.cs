using UnityEngine;
using UnityEngine.UI;

public class InspectableObject : MonoBehaviour
{
    [Header("Inspection System")]
    public Camera inspectionCamera;
    public RawImage inspectionDisplayImage;
    public LayerMask interactableLayer;
    public Transform playerCamera;
    public float inspectDistance = 4f;

    [Header("UI")]
    public GameObject interactionPrompt;

    private bool isInspecting = false;
    private GameObject currentInspectableClone;
    private float rotateSpeed = 1000f;

    void Update()
    {
        if (!isInspecting)
        {
            HandleInteractionRaycast();

            if (Input.GetKeyDown(KeyCode.E))
            {
                TryInspect();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                StopInspect();
            }

            if (currentInspectableClone != null)
            {
                RotateObject();
            }
        }
    }

    void HandleInteractionRaycast()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, inspectDistance, interactableLayer))
        {
            if (interactionPrompt != null)
                interactionPrompt.SetActive(true);
        }
        else
        {
            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    void TryInspect()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, inspectDistance, interactableLayer))
        {
            GameObject original = hit.collider.gameObject;

            currentInspectableClone = Instantiate(original);
            SetLayerRecursively(currentInspectableClone.transform, LayerMask.NameToLayer("InspectableOnly"));

            currentInspectableClone.transform.SetParent(inspectionCamera.transform);
            currentInspectableClone.transform.localPosition = new Vector3(0, 0, 5f);
            currentInspectableClone.transform.localRotation = Quaternion.identity;
            currentInspectableClone.transform.localScale = Vector3.one;

            inspectionCamera.gameObject.SetActive(true);
            inspectionDisplayImage.gameObject.SetActive(true);
            inspectionCamera.Render();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            Time.timeScale = 0f;
            isInspecting = true;

            if (interactionPrompt != null)
                interactionPrompt.SetActive(false);
        }
    }

    void StopInspect()
    {
        if (currentInspectableClone != null)
        {
            Destroy(currentInspectableClone);
        }

        inspectionCamera.gameObject.SetActive(false);
        inspectionDisplayImage.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        isInspecting = false;
    }

    void RotateObject()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        currentInspectableClone.transform.Rotate(Camera.main.transform.up, -mouseX * rotateSpeed * Time.unscaledDeltaTime, Space.World);
        currentInspectableClone.transform.Rotate(Camera.main.transform.right, -mouseY * rotateSpeed * Time.unscaledDeltaTime, Space.World);
    }

    void SetLayerRecursively(Transform obj, int newLayer)
    {
        obj.gameObject.layer = newLayer;
        foreach (Transform child in obj)
        {
            SetLayerRecursively(child, newLayer);
        }
    }
}