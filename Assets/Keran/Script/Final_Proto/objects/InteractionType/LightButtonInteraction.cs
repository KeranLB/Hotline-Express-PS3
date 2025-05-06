using UnityEngine;

public class LightButtonInteraction : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    [SerializeField] private bool isFirstButton = true;
    private bool hasBeenUsed = false;

    public void Interact()
    {
        if (hasBeenUsed || lightManager == null) return;

        if (isFirstButton)
        {
            lightManager.SwitchToLight2();
        }
        else
        {
            lightManager.StartSequentialLights();
        }

        hasBeenUsed = true;
    }
}