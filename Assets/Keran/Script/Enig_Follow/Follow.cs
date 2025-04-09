using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Vector2 _direction;
    [SerializeField, Range(-180f,180f)] private float _adjustement;
    [SerializeField] private InteractRouage _interactionRouage;
    // Update is called once per frame
    void Update()
    {
        if (_interactionRouage.isLock)
        {
            _direction = new Vector2(
            transform.position.x - _target.transform.position.x,
            transform.position.z - _target.transform.position.z);
            //transform.Rotate(0.0f, Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg, 0.0f);
            transform.eulerAngles = new Vector3(0f, -Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + _adjustement, 0f);
        }
    }
}
