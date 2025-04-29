using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuRoot;   // Contient l'image + boutons
    [SerializeField] private GameObject playerRoot; // Contient la cam�ra et tout le joueur

    public void PlayGame()
    {
        if (menuRoot != null)
            menuRoot.SetActive(false);

        if (playerRoot != null)
            playerRoot.SetActive(true);

        // Laisse la souris visible m�me apr�s activation du joueur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}