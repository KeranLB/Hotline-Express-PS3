using System.Collections;
using UnityEngine;


public class InteractRouage : MonoBehaviour
{
    [SerializeField] private float _rotationValue;
    [SerializeField,Range(0f, 359.99f)] private float _target;
    [SerializeField] private float _speedRotaion;
    public bool isLock = false;
    private bool _isRotating;

    public float currentRotation;
    private Vector3 baseRota;

    private void Start()
    {
        baseRota = transform.localEulerAngles;
        currentRotation = baseRota.x;
    }

    private void Verif()
    {
        if (currentRotation == _target)
        {
            isLock = true;
        }
        else
        {
            isLock = false;
        }
    }

    public void Interact()
    {
        if (!_isRotating)
        {
            StartCoroutine(Rotation(currentRotation + _rotationValue));
        }
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
        Verif();
        _isRotating = false;
    }
}
