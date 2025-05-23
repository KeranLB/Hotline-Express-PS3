using UnityEngine;

public class LightManagerFinal : MonoBehaviour
{
    [SerializeField] private PanelFinalManager _panelFinalManager;
    [SerializeField] private FinalEnigmeManager _finalEnigmeManager;

    [SerializeField] private Light _lightPanel;
    [SerializeField] private Light _lightBibliotheque;
    [SerializeField] private Light _lightAscenseur;

    private void Start()
    {
        _lightAscenseur.enabled = false;
        _lightPanel.enabled = false;
        _lightBibliotheque.enabled = false;
    }

    void Update()
    {
        if (_finalEnigmeManager.isFinish)
        {
            _lightAscenseur.enabled = true;
            _lightPanel.enabled = false;
            _lightBibliotheque.enabled = false;
        }
        else if (_panelFinalManager.isComplet)
        {
            _lightPanel.enabled = true;
            _lightBibliotheque.enabled = true;
        } 
    }
}
