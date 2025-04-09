using UnityEngine;

public class InspectableObject : MonoBehaviour
{
    public Transform inspectDisplaySlot; // À assigner dans l’inspecteur via Canvas

    private bool isInspecting = false;
    private GameObject currentInspectable;

    // Sauvegarde pour remettre en place l'objet après inspection
    private Transform originalParent;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Vector3 originalScale;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!isInspecting)
            {
                TryInspect();
            }
            else
            {
                StopInspect();
            }
        }

        if (isInspecting && currentInspectable != null)
        {
            RotateObject();
        }
    }

    void TryInspect()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 3f))
        {
            Interactable interactable = hit.collider.GetComponent<Interactable>();

            if (interactable != null && interactable.interactType == InteractType.Inspectable)
            {
                currentInspectable = hit.collider.gameObject;

                // Sauvegarde des données actuelles
                originalParent = currentInspectable.transform.parent;
                originalPosition = currentInspectable.transform.position;
                originalRotation = currentInspectable.transform.rotation;
                originalScale = currentInspectable.transform.localScale;

                // On déplace l’objet dans le Canvas
                currentInspectable.transform.SetParent(inspectDisplaySlot);
                currentInspectable.transform.localPosition = Vector3.zero;
                currentInspectable.transform.localRotation = Quaternion.identity;
                currentInspectable.transform.localScale = Vector3.one * 100f; // Ajuste selon visuel voulu

                isInspecting = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                // Optionnel : désactive mouvement joueur
                Time.timeScale = 0f;
            }
        }
    }

    void StopInspect()
    {
        if (currentInspectable != null)
        {
            currentInspectable.transform.SetParent(originalParent);
            currentInspectable.transform.position = originalPosition;
            currentInspectable.transform.rotation = originalRotation;
            currentInspectable.transform.localScale = originalScale;

            currentInspectable = null;
        }

        isInspecting = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1f;
    }

    void RotateObject()
    {
        float rotateSpeed = 50f;
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        currentInspectable.transform.Rotate(Camera.main.transform.up, -mouseX * rotateSpeed * Time.unscaledDeltaTime, Space.World);
        currentInspectable.transform.Rotate(Camera.main.transform.right, mouseY * rotateSpeed * Time.unscaledDeltaTime, Space.World);
    }
}