using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

    public Text scoreText;
    public Text timeText;
    public Image fade;
    public Text gameOverText;

    public GameObject G_Warp;
    public GameObject G_Combo;

    public static bool fadeOut;

    float fadeOpacity = 0;

    void Start () {
        scoreText.text = "Time: 3:00";
	}

    float endGameTimer = 0;
	void FixedUpdate () {
        scoreText.text = "Score: " + GameControl.score;
        scoreText.transform.GetChild(0).GetComponent<Text>().text = "Score: " + GameControl.score;

        int minutes = GameControl.time / 60;
        int seconds = GameControl.time % 60;

        if(seconds < 10)
        {
            timeText.text = "Time: " + minutes + ":0" + seconds;
            timeText.transform.GetChild(0).GetComponent<Text>().text = "Time: " + minutes + ":0" + seconds;
        }
        else
        {
            timeText.text = "Time: " + minutes + ":" + seconds;
            timeText.transform.GetChild(0).GetComponent<Text>().text = "Time: " + minutes + ":" + seconds;
        }

        if (!fadeOut)
        {
            fadeOpacity -= Time.fixedDeltaTime * 2;
            if (fadeOpacity < 0)
                fadeOpacity = 0;
        }
        else
        {
            fadeOpacity += Time.fixedDeltaTime * 2;
            if (fadeOpacity > 1)
                fadeOpacity = 1;
        }

        if (GameControl.endingGame)
        {
            endGameTimer += Time.deltaTime;

            if(endGameTimer > 0.3f)
            {
                endGameTimer = 0;
                gameOverText.gameObject.SetActive(!gameOverText.gameObject.activeInHierarchy);
            }
        }

        G_Warp.SetActive(GameControl.mayWarp);

        if(GameControl.combo > 1)
        {
            G_Combo.GetComponent<Text>().text = GameControl.combo + "x";
            G_Combo.transform.GetChild(0).GetComponent<Text>().text = GameControl.combo + "x";
            G_Combo.SetActive(true);
        }
        else
        {
            G_Combo.SetActive(false);
        }

        fade.color = new Color(0, 0, 0, fadeOpacity);
    }
}
