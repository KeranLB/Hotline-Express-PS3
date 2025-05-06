using UnityEngine;

public class LightButtonInteraction : MonoBehaviour
{
    [SerializeField] private LightManager _lightManager;
    [SerializeField] private bool hasBeenActivated = false;

    public void Interact()
    {
        if (hasBeenActivated) return; // Empêche de relancer l'action plusieurs fois
        Debug.Log("LightButtonInteraction : Interaction triggered");

        _lightManager.SwitchToSecondLight();
        hasBeenActivated = true;
    }
}