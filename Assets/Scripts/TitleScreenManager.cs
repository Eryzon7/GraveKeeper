using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    public string mainGameScene = "MainGame"; // name of your main gameplay scene

    public void StartGame()
    {
        SceneManager.LoadScene(mainGameScene);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}