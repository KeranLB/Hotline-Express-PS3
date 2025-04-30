using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuTransitionManager : MonoBehaviour
{
    [Header("Cameras & Player")]
    [SerializeField] private Camera uiCamera;
    [SerializeField] private GameObject playerObject; // Objet parent désactivé au départ (avec la PlayerCamera dedans)

    [Header("Transition Settings")]
    [SerializeField] private Transform transitionStartPoint; // Position actuelle de la UICamera
    [SerializeField] private Transform transitionEndPoint;   // Position finale du mouvement "je me lève"
    [SerializeField] private float transitionDuration = 2f;

    [Header("UI Elements")]
    [SerializeField] private Canvas menuCanvas;

    private void Start()
    {
        uiCamera.enabled = true;
        menuCanvas.enabled = true;

        if (playerObject != null)
            playerObject.SetActive(false); // Le joueur est désactivé par défaut
    }

    public void OnPlayButtonClicked()
    {
        StartCoroutine(TransitionFromUICameraToPlayer());
    }

    private IEnumerator TransitionFromUICameraToPlayer()
    {
        menuCanvas.enabled = false;

        float elapsed = 0f;
        Vector3 startPos = transitionStartPoint.position;
        Vector3 endPos = transitionEndPoint.position;
        Quaternion startRot = transitionStartPoint.rotation;
        Quaternion endRot = transitionEndPoint.rotation;

        while (elapsed < transitionDuration)
        {
            float t = elapsed / transitionDuration;
            uiCamera.transform.position = Vector3.Lerp(startPos, endPos, t);
            uiCamera.transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Fin de transition : désactiver la UICamera, activer le joueur
        playerObject.SetActive(true);
        uiCamera.enabled = false;
    }
}