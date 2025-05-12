using UnityEngine;

public class LedManager : MonoBehaviour
{
    [SerializeField] private Material _ledMaterial;

    [SerializeField] private InteractRouage _interactRouage;


    // Update is called once per frame
    void Update()
    {
        if (_interactRouage.isLock)
        {
            _ledMaterial.color = Color.green;
        }
        else
        {
            _ledMaterial.color = Color.red;
        }
    }
}
