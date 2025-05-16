using System.Collections.Generic;
using UnityEngine;

public class FinalEnigmeManager : MonoBehaviour
{
    [Header("Set PanelManager :")]
    [SerializeField] private PanelFinalManager panelFinalManager;
    [SerializeField] private MeshManager _meshManager;

    [Header("Code :")]
    [SerializeField] private List<InteractRouage> _codes;
    private bool _isComplet = false;

    public bool isFinish = false;

    public void Interact()
    {
        if (panelFinalManager.isComplet)
        {
            if (!_isComplet)
            {
                _isComplet = true;
                foreach (var code in _codes)
                {
                    if (!code.isLock)
                    {
                        Debug.Log(code.isLock);
                        _isComplet = false;
                    }
                }
                isFinish = true;
                _meshManager.Open();
            }
        }
    }
}
