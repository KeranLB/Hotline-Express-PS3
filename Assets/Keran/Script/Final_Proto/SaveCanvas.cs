using Unity.VisualScripting;
using UnityEngine;

public class SaveCanvas : MonoBehaviour
{
    [SerializeField] private Canvas _safeCanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _safeCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.isPlaying)
        {
            Open();
        }
        else
        {
            close();
        }
    }

    // partie à mettre dans un autre script
    private void Open()
    {
        //canMove = false;
        _safeCanvas.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    private void close()
    {
        //canMove = true;
        _safeCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
