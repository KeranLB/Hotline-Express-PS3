using UnityEngine;
using System.Collections;

public class MenuTransitionManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Camera uiCamera;
    [SerializeField] private Camera playerCamera;

    [Header("Transition Settings")]
    [SerializeField] private float transitionDuration = 2f;

    [Header("UI Elements")]
    [SerializeField] private Canvas menuCanvas;

    public void OnPlayButtonClicked()
    {
        StartCoroutine(PlayTransition());
    }

    private IEnumerator PlayTransition()
    {
        menuCanvas.enabled = false;

        // Start and end transforms
        Transform start = uiCamera.transform;
        Transform end = playerCamera.transform;

        float elapsed = 0f;
        Vector3 startPos = start.position;
        Quaternion startRot = start.rotation;
        Vector3 endPos = end.position;
        Quaternion endRot = end.rotation;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            uiCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            uiCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Snap to final position
        uiCamera.transform.position = endPos;
        uiCamera.transform.rotation = endRot;

        // Disable UICamera so player camera takes over
        uiCamera.enabled = false;
    }
}