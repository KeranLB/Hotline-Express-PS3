using UnityEngine;

public class TutoManager : MonoBehaviour
{
    [SerializeField] private CodeManager _codeManager;
    [SerializeField] private Controller _controller;
    [SerializeField] private GameObject ecranCodeTuto;
    [SerializeField] private LightManager _lightManager;

    public void Interact()
    {
        if (_codeManager.isCorrect)
        {
            _controller.isLock = false;
            ecranCodeTuto.SetActive(false);

            if (_lightManager != null)
                _lightManager.ActivateFirstLight();
            else
                Debug.LogWarning("LightManager n’est pas assigné dans TutoManager !");
        }
    }
}