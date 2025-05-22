using UnityEngine;

public class FollowCompa : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Vector2 _direction;
    [SerializeField] private Transform _parent;
    [SerializeField] private float _adjustement;


    void Update()
    {
        _direction = new Vector2(
        transform.position.x - _target.transform.position.x,
        transform.position.z - _target.transform.position.z);
        Debug.Log(_parent.eulerAngles);
        float tmpX = _parent.position.x;
        float tmpY = _parent.position.y;
        float tmpZ = _parent.position.z;
        if (tmpY  >= 180 && 360 >= tmpY)
        {
            transform.localEulerAngles = new Vector3(-Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + _adjustement + _parent.eulerAngles.y, 0f, 0f);
        }
        else
        {
            transform.localEulerAngles = new Vector3(-Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + _adjustement - _parent.eulerAngles.y, 0f, 0f);
        }
    }
}
