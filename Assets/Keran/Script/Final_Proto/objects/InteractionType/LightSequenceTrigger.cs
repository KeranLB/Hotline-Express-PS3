using UnityEngine;

[RequireComponent(typeof(ObjectClass))]
public class LightSequenceTrigger : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    private bool hasBeenActivated = false;

    private void Awake()
    {
        // D�finit cet objet comme Interactable pour le syst�me de d�tection du joueur
        GetComponent<ObjectClass>().interactType = ObjectType.Interactable;
    }

    public void Interact()
    {
        if (hasBeenActivated || lightManager == null) return;

        Debug.Log("Interaction avec LightSequenceTrigger : activation des lumi�res s�quentielles");
        lightManager.StartSequentialLights();
        hasBeenActivated = true;
    }
}