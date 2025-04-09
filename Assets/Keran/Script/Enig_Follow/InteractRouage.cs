using System.Collections;
using Autodesk.Fbx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class InteractRouage : MonoBehaviour
{
    [SerializeField] private float _rotationValue;
    [SerializeField,Range(-180f,179.99f)] private float _target;
    [SerializeField] private float _speedRotaion;
    public bool isLock = false;
    public bool test = false;
    private bool _isRotating;

    public float currentRotation;
    private Vector3 baseRota;
    private void Start()
    {
        baseRota = transform.localEulerAngles;
        currentRotation = baseRota.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            if (!_isRotating)
            {
                Interact();
            }
            test = false;
        }
        Verif();
    }

    private void Verif()
    {
        Debug.Log(true);
        if (currentRotation == _target)
        {
            Debug.Log(false);
            isLock = true;
        }
    }

    /*
    public void Interact()
    {
        StartCoroutine(Rotation(transform.eulerAngles.x + _rotationValue));
        //transform.eulerAngles.x + _rotationValue
    }

    IEnumerator Rotation(float Target)
    {
        _isRotating = true;
        
        var start = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
        var end = Quaternion.Euler(transform.eulerAngles.x + Target, transform.eulerAngles.y, transform.eulerAngles.z);
        var t = 0f;
        while (t < 1)
        {
            t += _speedRotaion * Time.deltaTime;
            t = Mathf.Clamp01(t);
            //transform.eulerAngles = new Vector3(Mathf.Lerp(start, end, t),-90f,0f);
            transform.localRotation = Quaternion.Lerp(start, end, t);
            yield return null;
        }
        _isRotating = false;
    }
    */

    public void Interact()
    {
        StartCoroutine(Rotation(currentRotation + _rotationValue));

        //currentRotation += _rotationValue;
        //transform.localEulerAngles = new Vector3(currentRotation, baseRota.y, baseRota.z);
    }

    IEnumerator Rotation(float target)
    {
        _isRotating = true;

        float start = currentRotation;
        
        var t = 0f;
        while (t < 1)
        {
            t += _speedRotaion * Time.deltaTime;
            t = Mathf.Clamp01(t);
            currentRotation = Mathf.Lerp(start, target ,t);
            if (currentRotation >= 360f)
            {
                currentRotation -= 360;
            }
            transform.localEulerAngles = new Vector3(currentRotation, baseRota.y, baseRota.z);

            yield return null;
        }

        _isRotating = false;
    }
}
