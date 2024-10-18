using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Main Menu Properties")]
    [SerializeField] GameObject shopPanelObj;
    [SerializeField] GameObject quitPanelObj;
    [Header("Game Menu Properties")]
    [SerializeField] GameObject pausePanelObj;
    // MAIN MENU SCENE ==================================================================================
    public void OnClickStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // Shop
    public void OnClickShop()
    {
       shopPanelObj.SetActive(true);
    }
    public void OnClickShopBack()
    {
       shopPanelObj.SetActive(false);
    }
    public void OnClickQuit()
    {
        quitPanelObj.SetActive(true);
    }
    public void OnClickQuitNo()
    {
        quitPanelObj.SetActive(false);
    }
    public void OnClickQuitYes()
    {
        Application.Quit();
    }
    


    // GAME SCENE ==================================================================================
    public void OnClickGameOverRestart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void OnClickGameOverMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        pausePanelObj.SetActive(true);
    }
    public void OnClickPauseResume()
    {
        Time.timeScale = 1;
        pausePanelObj.SetActive(false);
    }

    public void OnClickPauseMenu(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
