using UnityEngine;

[RequireComponent(typeof(ObjectClass))]
public class LightSequenceTrigger : MonoBehaviour
{
    [SerializeField] private LightManager _lightManager;
    private bool hasBeenActivated = false;

    private void Awake()
    {
        GetComponent<ObjectClass>().interactType = ObjectType.Interactable;
    }

    public void Interact()
    {
        if (hasBeenActivated) return;

        _lightManager.ActivateSequentialLights();
        hasBeenActivated = true;
    }
}