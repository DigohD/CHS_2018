using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class StartSceneUI : MonoBehaviour {

    public void onStartGameClick()
    {
        SceneManager.LoadScene("test");
    }

    public void onHighscoresClick()
    {

    }

    public void onExitGameClick()
    {
        if (!Application.isEditor)
        {
            Application.Quit();
        }
    }

}
