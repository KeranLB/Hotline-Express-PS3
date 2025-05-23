using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private GameObject _target;
    private Vector2 _direction;
    [SerializeField, Range(-180f,180f)] private float _adjustement;
    [SerializeField] private InteractRouage _interactionRouageA;
    [SerializeField] private InteractRouage _interactionRouageB;
    [SerializeField] private InteractRouage _interactionRouageC;
    [SerializeField] private List<GameObject> _buttons;

    private bool asSwitch = false;
    // Update is called once per frame
    void Update()
    {
        if (_interactionRouageA.isLock && _interactionRouageB.isLock && _interactionRouageC.isLock)
        {
            if (!asSwitch)
            {
                SwitchType();
            }
            _direction = new Vector2(
            transform.position.x - _target.transform.position.x,
            transform.position.z - _target.transform.position.z);
            transform.eulerAngles = new Vector3(0f, -Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg + _adjustement, 0f);
        }
    }

    private void SwitchType()
    {
        foreach (GameObject button in _buttons)
        {
            button.GetComponent<ObjectClass>().interactType = ObjectType.None;
        }
        _target.GetComponent<ObjectClass>().interactType = ObjectType.Movable;
        
        _target.AddComponent<Rigidbody>();
        _target.GetComponent<Grab>().rb = _target.GetComponent<Rigidbody>(); 
        asSwitch = true;
    }
}