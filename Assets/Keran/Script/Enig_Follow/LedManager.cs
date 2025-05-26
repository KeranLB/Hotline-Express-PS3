using UnityEngine;

public class LedManager : MonoBehaviour
{
    [SerializeField] private Renderer _ledMaterial;

    [SerializeField] private Material _redMaterial;
    [SerializeField] private Material _greenMaterial;

    [SerializeField] private InteractRouage _interactRouageA;
    [SerializeField] private InteractRouage _interactRouageB;
    [SerializeField] private InteractRouage _interactRouageC;

    // Update is called once per frame
    void Update()
    {
        
        if (_interactRouageA.isLock && _interactRouageB.isLock && _interactRouageC.isLock)
        {
            _ledMaterial.material = _greenMaterial;
        }
        else
        {
            _ledMaterial.material = _redMaterial;
        }
        
    }
}
