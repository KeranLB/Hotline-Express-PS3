using UnityEngine;

public class LightButtonInteraction : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    private bool hasBeenActivated = false;

    public void Interact()
    {
        if (hasBeenActivated) return;

        lightManager.SwitchToSecondLight();
        hasBeenActivated = true;
    }
}