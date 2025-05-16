using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void Quit()
    {
        EditorApplication.Exit(0);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
