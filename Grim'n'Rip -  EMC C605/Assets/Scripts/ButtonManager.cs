using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [Header("Main Menu Properties")]
    [SerializeField] GameObject shopPanelObj;
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
