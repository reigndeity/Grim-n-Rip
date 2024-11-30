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
    public AudioManager _audioManager;

    void Start()
    {
        _audioManager = FindObjectOfType<AudioManager>();  
    }
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
            // ===============================================================================

        }
    }
    // MAIN MENU SCENE ==================================================================================
    public void OnClickStart()
    {
       LoadScene(1);
       _audioManager.PlayButtonClickSound();
    }
    public void OnClickQuit()
    {
        quitPanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickQuitNo()
    {
        quitPanelObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickQuitYes()
    {
        Application.Quit();
        _audioManager.PlayButtonClickSound();
    }

    // SHOP PANEL
    public void OnClickShop()
    {
        menuPanelObj.SetActive(false);
        shopPanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickShopBack()
    {
        menuPanelObj.SetActive(true);
        shopPanelObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickDodgeRateUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[0].SetActive(true);
        _shopManager.selectedUpgrade = 0;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickHealthUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[1].SetActive(true);
        _shopManager.selectedUpgrade = 1;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickMovementSpeedUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[2].SetActive(true);
        _shopManager.selectedUpgrade = 2;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickProjectileFireRateUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[3].SetActive(true);
        _shopManager.selectedUpgrade = 3;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickProjectileDamageUpgrade()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[4].SetActive(true);
        _shopManager.selectedUpgrade = 4;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickProjectileSpeed()
    {
        _shopManager.confirmationUpgradePanel.SetActive(true);
        _shopManager.upgradeImages[5].SetActive(true);
        _shopManager.selectedUpgrade = 5;
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickYesConfirmation()
    {
        _audioManager.PlayButtonClickSound();
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
        _audioManager.PlayButtonClickSound();
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
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickBackSetting()
    {
        settingPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPostProcessHigh()
    {
        PlayerPrefs.SetInt("postProcess", 1);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPostProcessLow()
    {
        PlayerPrefs.SetInt("postProcess", 0);
        _audioManager.PlayButtonClickSound();
    }

    // HELP PANEL
    public void OnClickHelp()
    {
        helpPanelObj.SetActive(true);
        menuPanelObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickBackHelp()
    {
        helpPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    // ABOUT PANEL
    public void OnClickAbout()
    {
        aboutPanelObj.SetActive(true);
        menuPanelObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickBackAbout()
    {
        aboutPanelObj.SetActive(false);
        menuPanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    // GAME SCENE ==================================================================================
    public void OnClickGameOverRestart()
    {
        LoadScene(1);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickGameOverMenu()
    {
        gameOverMenuObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickGameOverMenuNo()
    {
        gameOverMenuObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickGameOverMenuYes()
    {
        LoadScene(0);
        _audioManager.PlayButtonClickSound();
    }

    public void OnClickPause()
    {
        Time.timeScale = 0;
        pausePanelObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPauseResume()
    {
        Time.timeScale = 1;
        pausePanelObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }

    public void OnClickPauseMenuYes()
    {
        LoadScene(0);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPauseMenuNo()
    {
        pauseMenuObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }

    public void OnClickPauseSetting()
    {
        pauseSettingObj.SetActive(true);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPauseSettingBack()
    {
        pauseSettingObj.SetActive(false);
        _audioManager.PlayButtonClickSound();
    }
    public void OnClickPlayerIcon()
    {
        _audioManager.PlayButtonClickSound();
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
        _audioManager.PlayButtonClickSound();
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
