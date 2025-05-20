using UnityEngine;

public class TrashManager : MonoBehaviour
{
    [SerializeField] private int requiredObjects = 4;
    [SerializeField] private LightManager lightManager;

    private int currentPlacedObjects = 0;
    private bool lightTriggered = false;

    public void RegisterObjectPlacement()
    {
        if (lightTriggered) return;

        currentPlacedObjects++;

        if (currentPlacedObjects >= requiredObjects)
        {
            lightManager.ActivateFirstLight();
            lightTriggered = true;
        }
    }
}