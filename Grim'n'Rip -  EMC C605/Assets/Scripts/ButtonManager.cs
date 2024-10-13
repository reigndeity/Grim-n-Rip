using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    public void OnClickStart(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
