using UnityEngine;

[RequireComponent(typeof(ObjectClass))]
public class LightSequenceTrigger : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    private bool hasBeenActivated = false;

    private void Awake()
    {
        // Définit cet objet comme Interactable pour le système de détection du joueur
        GetComponent<ObjectClass>().interactType = ObjectType.Interactable;
    }

    public void Interact()
    {
        if (hasBeenActivated || lightManager == null) return;

        Debug.Log("Interaction avec LightSequenceTrigger : activation des lumières séquentielles");
        lightManager.StartSequentialLights();
        hasBeenActivated = true;
    }
}