using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    // MAIN MENU SCENE ==================================================================================
    public void OnClickStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // GAME SCENE ==================================================================================
    public void OnClickRestart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnClickMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
