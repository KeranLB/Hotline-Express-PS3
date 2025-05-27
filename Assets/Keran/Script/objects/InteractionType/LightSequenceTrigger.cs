using UnityEngine;

[RequireComponent(typeof(ObjectClass))]
public class LightSequenceTrigger : MonoBehaviour
{
    [SerializeField] private LightManager lightManager;
    private bool hasBeenActivated = false;

    [SerializeField] private MeshFilter _meshFilter;
    [SerializeField] private Mesh _meshOff;
    [SerializeField] private Mesh _meshOn;

    private void Start()
    {
        _meshFilter.mesh = _meshOff;
    }

    public void Interact()
    {
        if (!hasBeenActivated)
        {
            lightManager.ActivateSequentialLights();
            hasBeenActivated = true;
            _meshFilter.mesh = _meshOn;
        }
    }
}