using System.Collections;
using UnityEngine;

public class LaunchGameTransition : MonoBehaviour
{
    public Camera uiCamera;
    public Transform playerCameraTransform;
    public GameObject player;

    public GameObject windowsImage;     // Image actuelle (écran Windows)
    public GameObject nextStepImage;    // Image suivante (suite du tuto)

    public float transitionDuration = 2f;
    public float startFOV = 40f;
    public float endFOV = 60f;

    public void OnPlayClicked()
    {
        StartCoroutine(TransitionToPlayer());
    }

    private IEnumerator TransitionToPlayer()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 startPos = uiCamera.transform.position;
        Quaternion startRot = uiCamera.transform.rotation;
        float initialFOV = startFOV;
        float targetFOV = endFOV;

        uiCamera.fieldOfView = initialFOV;

        player.SetActive(false);

        Vector3 endPos = playerCameraTransform.position;
        Quaternion endRot = playerCameraTransform.rotation;

        float elapsed = 0f;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;

            uiCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            uiCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            uiCamera.fieldOfView = Mathf.Lerp(initialFOV, targetFOV, t);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fin de transition
        uiCamera.transform.position = endPos;
        uiCamera.transform.rotation = endRot;
        uiCamera.fieldOfView = targetFOV;

        // Changement des images
        if (windowsImage != null) windowsImage.SetActive(false);
        if (nextStepImage != null) nextStepImage.SetActive(true);

        // Activer le joueur et désactiver la caméra UI
        player.SetActive(true);
        uiCamera.gameObject.SetActive(false);
    }
}