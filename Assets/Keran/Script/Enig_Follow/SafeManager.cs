using UnityEngine;

public class SafeManager : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private MeshManager _meshManager;

    void Update()
    {
        if (_codeManager.isCorrect)
        {
            _meshManager.Open();
        }
    }
}
