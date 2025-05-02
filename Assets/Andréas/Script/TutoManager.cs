using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private Controller _controller;
    [SerializeField] private GameObject ecranCodeTuto;
    [SerializeField] private LightManager _lightManager; // <-- Ajout ici

    public void Interact()
    {
        if (_codeManager.isCorrect)
        {
            _controller.isLock = false;
            ecranCodeTuto.SetActive(false);
            _lightManager.ActivateFirstLight(); // <-- Activation de la première lumière
        }
    }
}