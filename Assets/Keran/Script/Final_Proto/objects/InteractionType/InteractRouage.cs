using System.Collections;
using UnityEngine;


public class InteractRouage : MonoBehaviour
{
    [SerializeField] private float _rotationValue;
    [SerializeField, Range(0f, 359.99f)] private float _target;
    [SerializeField] private float _speedRotaion;
    [HideInInspector] public bool isLock = false;
    private bool _isRotating;
    [SerializeField] private RotationDirection _rotationDirection;
    public float currentRotation;
    private Vector3 baseRota;
    private Vector3 _rotaOrigine;

    private void Start()
    {
        baseRota = transform.localEulerAngles;
        currentRotation = Mathf.RoundToInt(baseRota.x);
        _target = Mathf.RoundToInt(_target);
        _rotaOrigine = baseRota;

    }

    private void Verif()
    {
        Debug.Log(currentRotation);
        Debug.Log(_target);
        Debug.Log(currentRotation == _target);
        if (Mathf.RoundToInt(currentRotation) == Mathf.RoundToInt(_target))
        {
            isLock = true;
            Debug.Log("is Lock");
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
            switch (_rotationDirection)
            {
                case RotationDirection.RotationX:
                    transform.localEulerAngles = new Vector3(currentRotation, baseRota.y, baseRota.z);
                    break;
                case RotationDirection.RotationY: 
                    transform.localEulerAngles = new Vector3(baseRota.x, currentRotation, baseRota.z);
                    break;
                case RotationDirection.RotationZ:
                    transform.localEulerAngles = new Vector3(baseRota.x, baseRota.y, currentRotation);
                    break;
            }

            yield return null;
        }
        Verif();
        _isRotating = false;
    }
}
