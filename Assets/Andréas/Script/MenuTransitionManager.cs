using UnityEngine;
using System.Collections;

public class MenuTransitionManager : MonoBehaviour
{
    [Header("Cameras & Player")]
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject playerObject;

    [Header("Transition Settings")]
    [SerializeField] private Transform transitionStartPoint;
    [SerializeField] private Transform transitionEndPoint;
    [SerializeField] private float transitionDuration = 2f;

    [Header("UI Elements")]
    [SerializeField] private Canvas menuCanvas;

    private void Start()
    {
        uiCamera.enabled = true;
        playerCamera.enabled = false;
        playerObject.SetActive(false);
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuCanvas.enabled = false;

        float elapsed = 0f;
        Vector3 startPos = transitionStartPoint.position;
        Vector3 endPos = transitionEndPoint.position;
        Quaternion startRot = transitionStartPoint.rotation;
        Quaternion endRot = transitionEndPoint.rotation;

        // Déplacement fluide de la UI Camera
        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            uiCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            uiCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Une fois terminé : activer le joueur, placer sa caméra exactement où est la UI Camera
        playerObject.SetActive(true);

        playerCamera.transform.position = uiCamera.transform.position;
        playerCamera.transform.rotation = uiCamera.transform.rotation;

        playerCamera.enabled = true;
        uiCamera.enabled = false;
    }
}