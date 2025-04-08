using System.Collections;
using Autodesk.Fbx;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;

public class InteractRouage : MonoBehaviour
{
    [SerializeField] private float _rotationValue;
    [SerializeField,Range(0f,360f)] private float _target;
    [SerializeField] private float _speedRotaion;
    public bool isLock = false;
    public bool test = false;
    private bool _isRotating;

    [SerializeField] public float goTo;

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
        if (transform.eulerAngles.x == 360f)
        {
            transform.eulerAngles = new Vector3(0f,transform.eulerAngles.y,transform.eulerAngles.z);
        }
    }

    private void Verif()
    {
        if (transform.eulerAngles.x == _target)
        {
            isLock = true;
        }
    }

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

        //Stats.lasterWidth
    }
}
