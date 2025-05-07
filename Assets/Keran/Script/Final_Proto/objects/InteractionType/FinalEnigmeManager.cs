using System.Collections.Generic;
using UnityEngine;

public class FinalEnigmeManager : MonoBehaviour
{
    [Header("Rouages crypté :")]
    [SerializeField] private List<InteractRouage> _rouages;

    [Header("Code :")]
    [SerializeField] private List<InteractRouage> _codes;
    private bool _isComplet = false;

    public bool isFinish = false;

    public void Interact()
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
            if (!_isComplet)
            {
                foreach (var rouage in _rouages)
                {
                    rouage.Interact();
                }
            }
            else
            {
                isFinish = true;
            }
        }
    }
}
