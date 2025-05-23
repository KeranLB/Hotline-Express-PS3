using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    [Header("Lights")]
    [SerializeField] private Light firstLight;
    [SerializeField] private Light secondLight;
    [SerializeField] private Light _lightStart;
    [SerializeField] private List<Light> sequentialLights;

    [Header("Settings")]
    [SerializeField] private float delayBetweenLights = 1.5f;
    private bool _isOff = false;

    [Header("srcipts :")]
    [SerializeField] private PanelFinalManager _panelFinalManager;

    private void Update()
    {
        if (!_isOff && _panelFinalManager.isComplet)
        {
            firstLight.enabled = false;
            secondLight.enabled = false;
            foreach (Light light in sequentialLights)
            {
                light.enabled = false;
            }
            _isOff = true;
        }
    }
    public void ActivateFirstLight()
    {
        if (firstLight != null)
        {
            firstLight.enabled = true;
        }
    }

    public void SwitchToSecondLight()
    {
        if (firstLight != null)
            firstLight.enabled = false;

        if (secondLight != null)
        {
            secondLight.enabled = true;
        }
    }

    public void ActivateSequentialLights()
    {
        StartCoroutine(ActivateLightsSequentially());
        _lightStart.enabled = false;
    }

    private IEnumerator ActivateLightsSequentially()
    {
        foreach (Light l in sequentialLights)
        {
            if (l != null)
            {
                l.enabled = true;
                yield return new WaitForSeconds(delayBetweenLights);
            }
        }
    }
}