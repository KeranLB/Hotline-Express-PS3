using System.Collections;
using UnityEngine;


public class InteractRouage : MonoBehaviour
{
    public float _rotationValue;
    [SerializeField, Range(0f, 359.99f)] private float _target;
    [SerializeField] private float _speedRotaion;
    [HideInInspector] public bool isLock = false;
    private bool _isRotating;
    [SerializeField] private RotationOrientation _rotationDirection;
    public float currentRotation;
    private Vector3 _baseRota;

    private void Start()
    {
        _baseRota = transform.localEulerAngles;
        currentRotation = Mathf.RoundToInt(_baseRota.x);
        _target = Mathf.RoundToInt(_target);
        Verif();
    }

    private void Verif()
    {
        if (Mathf.RoundToInt(currentRotation) == Mathf.RoundToInt(_target))
        {
            isLock = true;
        }
        else
        {
            isLock = false;
        }
    }

    public void Interact(int direction)
    {
        if (!_isRotating)
        {
            StartCoroutine(Rotation(currentRotation + _rotationValue * direction));
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
                case RotationOrientation.RotationX:
                    transform.localEulerAngles = new Vector3(currentRotation, _baseRota.y, _baseRota.z);
                    break;
                case RotationOrientation.RotationY: 
                    transform.localEulerAngles = new Vector3(_baseRota.x, currentRotation, _baseRota.z);
                    break;
                case RotationOrientation.RotationZ:
                    transform.localEulerAngles = new Vector3(_baseRota.x, _baseRota.y, currentRotation);
                    break;
            }

            yield return null;
        }
        Verif();
        _isRotating = false;
    }
}
