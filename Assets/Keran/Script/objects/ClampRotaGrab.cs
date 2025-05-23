using UnityEngine;

public class ClampRotaGrab : MonoBehaviour
{
    [SerializeField] private Grab _grab;

    // Update is called once per frame
    void Update()
    {
        if (_grab.isGrab)
        {
            gameObject.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
        }    
    }
}
