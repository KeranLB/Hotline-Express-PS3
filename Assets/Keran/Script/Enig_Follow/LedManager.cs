using UnityEngine;

public class LedManager : MonoBehaviour
{
    [SerializeField] private Renderer _ledMaterialA;
    [SerializeField] private Renderer _ledMaterialB;
    [SerializeField] private Renderer _ledMaterialC;

    [SerializeField] private InteractRouage _interactRouageA;
    [SerializeField] private InteractRouage _interactRouageB;
    [SerializeField] private InteractRouage _interactRouageC;

    // Update is called once per frame
    void Update()
    {
        
        if (_interactRouageA.isLock && _interactRouageB.isLock && _interactRouageC.isLock)
        {
            _ledMaterialA.material.color = Color.green;
            _ledMaterialB.material.color = Color.green;
            _ledMaterialC.material.color = Color.green;
        }
        else
        {
            _ledMaterialA.material.color = Color.red;
            _ledMaterialB.material.color = Color.red;
            _ledMaterialC.material.color = Color.red;
        }
        
    }
}
