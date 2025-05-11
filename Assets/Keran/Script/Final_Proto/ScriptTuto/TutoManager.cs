using TMPro.Examples;
using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private Controller _controller;
    [SerializeField] private GameObject ecranCodeTuto;
    [SerializeField] private LightManager lightManager;
   public void Interact()
    {
        if (_codeManager.isCorrect)
        {
            _controller.isLock = false;
            _controller.isInTuto = false;
            ecranCodeTuto.SetActive(false);
        }
    }
}