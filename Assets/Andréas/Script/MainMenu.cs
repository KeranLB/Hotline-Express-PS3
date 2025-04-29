using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;   // Contient l'image + boutons
    [SerializeField] private GameObject playerRoot; // Contient la caméra et tout le joueur

    public void PlayGame()
    {
        if (menuRoot != null)
            menuRoot.SetActive(false);

        if (playerRoot != null)
            playerRoot.SetActive(true);

        // Laisse la souris visible même après activation du joueur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}