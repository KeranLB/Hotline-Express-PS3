using UnityEngine;

public class EnterCode : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private int _EnterNumber;

    public void Interact()
    {
        _codeManager.AddToCode(_EnterNumber);
    }
}
