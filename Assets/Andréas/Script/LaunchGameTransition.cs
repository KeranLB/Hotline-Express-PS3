using System.Collections;
using UnityEngine;
using TMPro; // Assure-toi d'avoir cette ligne

public class LaunchGameTransition : MonoBehaviour
{
    public Camera uiCamera;
    public Transform playerCameraTransform;
    public GameObject player;

    public GameObject windowsImage;
    public GameObject IntroWindows;
    public GameObject nextStepImage;
    [SerializeField] private GameObject _codeImageTuto;

    public float transitionDuration = 2f;
    public float startFOV = 40f;
    public float endFOV = 60f;

    [Header("Instructions UI")]
    public TextMeshProUGUI controlsText;  // <--- Glisse ici ton TMP dans l'inspecteur
    public float displayDuration = 5f;     // Durée d'affichage du message

    private void Start()
    {
        _codeImageTuto.SetActive(false);
    }

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

        uiCamera.transform.position = endPos;
        uiCamera.transform.rotation = endRot;
        uiCamera.fieldOfView = targetFOV;

        if (windowsImage != null) windowsImage.SetActive(false);
        if (IntroWindows != null) IntroWindows.SetActive(false);
        if (nextStepImage != null) nextStepImage.SetActive(true);
        _codeImageTuto.SetActive(true);

        player.SetActive(true);
        uiCamera.gameObject.SetActive(false);

        // ➕ Active le texte d'instructions
        if (controlsText != null)
        {
            controlsText.gameObject.SetActive(true);
            StartCoroutine(HideControlsTextAfterDelay());
        }
    }

    private IEnumerator HideControlsTextAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (controlsText != null)
        {
            controlsText.gameObject.SetActive(false);
        }
    }
}