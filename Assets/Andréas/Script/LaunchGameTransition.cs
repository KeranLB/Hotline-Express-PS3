using System.Collections;
using UnityEngine;

public class LaunchGameTransition : MonoBehaviour
{
    public Camera uiCamera;
    public Transform playerCameraTransform;
    public GameObject player;

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
        // Sauvegarde de la position, rotation et FOV de départ
        Vector3 startPos = uiCamera.transform.position;
        Quaternion startRot = uiCamera.transform.rotation;
        float initialFOV = startFOV;
        float targetFOV = endFOV;

        // Appliquer FOV de départ si pas déjà fait
        uiCamera.fieldOfView = initialFOV;

        // Le joueur reste désactivé durant la transition
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

        // Position finale
        uiCamera.transform.position = endPos;
        uiCamera.transform.rotation = endRot;
        uiCamera.fieldOfView = targetFOV;

        // Activer joueur et désactiver la UIcamera
        player.SetActive(true);
        uiCamera.gameObject.SetActive(false);
    }
}