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
        transform.localEulerAngles = new Vector3(-Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + _adjustement + _parent.eulerAngles.y + _parent.position.x + _parent.position.z, 0f, 0f);
    }
}
