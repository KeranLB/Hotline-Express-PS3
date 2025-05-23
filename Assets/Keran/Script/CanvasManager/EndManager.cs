using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    [Header("Set Script :")]
    [SerializeField] private Controller _controller;
    [SerializeField] private Prompteur _prompteur;
    [Header("Set UI :")]
    [SerializeField] private Image _curseur;
    [SerializeField] private Image _endImage;
    [SerializeField] private Text _time;
    private void End()
    {
        _controller.canMove = false;
        _curseur.gameObject.SetActive(false);
        _time.text = _prompteur.textMeshPro.text;
        _endImage.gameObject.SetActive(true);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            End();
        }
    }
}
