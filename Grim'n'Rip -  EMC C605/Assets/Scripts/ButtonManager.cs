using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Mathematics;

public class ButtonManager : MonoBehaviour
{
    [Header("Main Menu Properties")]
    [SerializeField] GameObject menuPanelObj;
    [SerializeField] GameObject settingPanelObj;
    [SerializeField] GameObject shopPanelObj;
    [SerializeField] GameObject helpPanelObj;
    [SerializeField] GameObject aboutPanelObj;
    [SerializeField] GameObject quitPanelObj;
    [SerializeField] GameObject lowPostProcessObj;
    [SerializeField] GameObject highPostProcessObj;
    [Header("Game Menu Properties")]
    [SerializeField] GameObject pausePanelObj;
    [SerializeField] Animator statHolderAnimator;
    [SerializeField] GameObject pauseMenuObj;
    [SerializeField] GameObject pauseSettingObj;
    [SerializeField] GameObject gameOverMenuObj;
    [Header("Loading Screen Properties")]
    [SerializeField] GameObject loadingScreenPanel;
    [SerializeField] Image loadingBarFill;

    [Header("References")]
    [SerializeField] ShopManager _shopManager;
    public bool isClick;


    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            // Post Process Setting
            int postProcess = PlayerPrefs.GetInt("postProcess");
            if (postProcess == 0)
            {
                lowPostProcessObj.SetActive(true);
                highPostProcessObj.SetActive(false);
            }
            else
            {
                lowPostProcessObj.SetActive(false);
                highPostProcessObj.SetActive(true);              
            }
            // Max Fire Rate Upgrade
            if (_shopManager.selectedUpgrade == 3 && _shopManager.isMaxFireRate == true)
            {
                _shopManager.weaponFireRateCostTxt.text = "MAXED OUT!";
                _shopManager.fireRateBut.interactable = false;
                _shopManager.confirmButton.interactable = false;
            }
            else
            {
                _shopManager.confirmButton.interactable = true;
            }

        }
    }
    // MAIN MENU SCENE ==================================================================================
    public void OnClickStart()
    {
       LoadScene(1);
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

    // SHOP PANEL
    public void OnClickShop()
    {
        menuPanelObj.SetActive(false);
        shopPanelObj.SetActive(true);
    }
    public void OnClickShopBack()
    {
        menuPanelObj.SetActive(true);
        shopPanelObj.SetActive(false);
    }
    public void OnClickDodgeRateUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[0].SetActive(true);
        _shopManager.selectedUpgrade = 0;
    }
    public void OnClickHealthUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[1].SetActive(true);
        _shopManager.selectedUpgrade = 1;
    }
    public void OnClickMovementSpeedUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[2].SetActive(true);
        _shopManager.selectedUpgrade = 2;
    }
    public void OnClickProjectileFireRateUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[3].SetActive(true);
        _shopManager.selectedUpgrade = 3;
    }
    public void OnClickProjectileDamageUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[4].SetActive(true);
        _shopManager.selectedUpgrade = 4;
    }
    public void OnClickProjectileSpeed()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[5].SetActive(true);
        _shopManager.selectedUpgrade = 5;
    }
    public void OnClickYesConfirmation()
    {
        if (_shopManager.selectedUpgrade == 0)
        {
            _shopManager.DodgeRateUpgrade();
        }
        if (_shopManager.selectedUpgrade == 1)
        {
            _shopManager.HealthUpgrade();
        }
        if (_shopManager.selectedUpgrade == 2)
        {
            _shopManager.MovementSpeedUpgrade();
        }
        if (_shopManager.selectedUpgrade == 3)
        {
            _shopManager.WeaponFireRateUpgrade();
        }
        if (_shopManager.selectedUpgrade == 4)
        {
            _shopManager.ProjectileDamageUpgrade();
        }
        if (_shopManager.selectedUpgrade == 5)
        {
            _shopManager.ProjectileSpeedUpgrade();
        }
    }
    public void OnClickBackConfirmation()
    {
        _shopManager.confirmationUpgradePanel.SetActive(false);
        _shopManager.upgradeImages[0].SetActive(false);
        _shopManager.upgradeImages[1].SetActive(false);
        _shopManager.upgradeImages[2].SetActive(false);
        _shopManager.upgradeImages[3].SetActive(false);
        _shopManager.upgradeImages[4].SetActive(false);
        _shopManager.upgradeImages[5].SetActive(false);
        
    }
    // SETTINGS PANEL
    public void OnClickSetting()
    {
        settingPanelObj.SetActive(true);
        menuPanelObj.SetActive(false);
    }
    public void OnClickBackSetting()
    {
        settingPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
    }
    public void OnClickPostProcessHigh()
    {
        PlayerPrefs.SetInt("postProcess", 1);
    }
    public void OnClickPostProcessLow()
    {
        PlayerPrefs.SetInt("postProcess", 0);
    }

    // HELP PANEL
    public void OnClickHelp()
    {
        helpPanelObj.SetActive(true);
        menuPanelObj.SetActive(false);
    }
    public void OnClickBackHelp()
    {
        helpPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
    }
    // ABOUT PANEL
    public void OnClickAbout()
    {
        aboutPanelObj.SetActive(true);
        menuPanelObj.SetActive(false);
    }
    public void OnClickBackAbout()
    {
        aboutPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
    }
    // GAME SCENE ==================================================================================
    public void OnClickGameOverRestart()
    {
        LoadScene(1);
    }
    public void OnClickGameOverMenu()
    {
        gameOverMenuObj.SetActive(true);
    }
    public void OnClickGameOverMenuNo()
    {
        gameOverMenuObj.SetActive(false);
    }
    public void OnClickGameOverMenuYes()
    {
        LoadScene(0);
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

    public void OnClickPauseMenuYes()
    {
        LoadScene(0);
    }
    public void OnClickPauseMenuNo()
    {
        pauseMenuObj.SetActive(false);
    }

    public void OnClickPauseSetting()
    {
        pauseSettingObj.SetActive(true);
    }
    public void OnClickPauseSettingBack()
    {
        pauseSettingObj.SetActive(false);
    }
    public void OnClickPlayerIcon()
    {
        if (isClick == false)
        {
            statHolderAnimator.SetInteger("animState",1);
            isClick = true;
        }
        else
        {
            statHolderAnimator.SetInteger("animState",2);
            isClick = false;
        }
    }

    // LOADING SCREEN ==================================================================================
    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        loadingScreenPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
