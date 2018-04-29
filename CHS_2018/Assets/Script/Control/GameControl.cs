using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static long score;

    public GameObject P_Planet;

    public GameObject P_BG;
    public GameObject G_BG;

    private ArrayList planetList = new ArrayList();

    public static int time = 180;

    public static bool transitioning;
    public static bool endingGame;

    public static bool mayWarp;

    private enum ShiftState { FADE_OUT, GENERATE_LEVEL, FADE_IN };
    private enum EndGameState { END_GAME_FLASH, FADE_OUT, SWITCH_SCENE};
    private ShiftState shiftState;
    private EndGameState endGameState;

    public static int combo;
    private float comboTimer;

    void Start () {
        time = 180;
        transitioning = false;
        endingGame = false;
        mayWarp = false;
        score = 0;
        combo = 0;
        comboTimer = 0;

        generateLevel();
	}

    float timer;
    float timeTimer;
    void Update () {
        timer += Time.deltaTime;
        timeTimer += Time.deltaTime;

        if(combo > 1)
        {
            comboTimer += Time.deltaTime;
            if(comboTimer > 5)
            {
                comboTimer = 0;
                combo = 0;
            }
        }
        else
        {
            comboTimer = 0;
        }

        if (!endingGame && time <= 0)
        {
            timer = 0;
            timeTimer = 0;
            endingGame = true;
            endGameState = EndGameState.END_GAME_FLASH;
        }

        if (endingGame)
        {
            switch (endGameState)
            {
                case EndGameState.END_GAME_FLASH:
                    if(timer > 4f)
                    {
                        endGameState = EndGameState.FADE_OUT;
                        timer = 0;
                    }
                    break;
                case EndGameState.FADE_OUT:
                    if (timer > 0.55f)
                    {
                        endGameState = EndGameState.SWITCH_SCENE;
                    }
                    break;
                case EndGameState.SWITCH_SCENE:
                    SceneManager.LoadScene("HighscoreScreen");
                    break;
            }
            return;
        }

        if (timeTimer >= 1)
        {
            time -= 1;
            timeTimer -= 1;
        }

        if (transitioning)
        {
            switch (shiftState)
            {
                case ShiftState.FADE_OUT:
                    UI.fadeOut = true;
                    if (timer > 0.55f){
                        shiftState = ShiftState.GENERATE_LEVEL;
                        timer = 0;
                    }
                    break;
                case ShiftState.GENERATE_LEVEL:
                    foreach (GameObject go in GameObject.FindGameObjectsWithTag("PlanetAnchor"))
                        Destroy(go);

                    generateLevel();

                    GameObject tmp = Instantiate(P_BG, G_BG.transform.position, Quaternion.identity);
                    Destroy(G_BG);
                    G_BG = tmp;

                    mayWarp = false;

                    shiftState = ShiftState.FADE_IN;
                    timer = 0;
                    break;
                case ShiftState.FADE_IN:
                    if (timer > 0.05f)
                        UI.fadeOut = false;

                    if(timer > 0.55f)
                    {
                        transitioning = false;
                    }
                    break;
            }
            
            return;
        }

        if (mayWarp && Input.GetKeyDown(KeyCode.Space))
        {
            transitioning = true;
            timer = 0;
            shiftState = ShiftState.FADE_OUT;
            return;
        }
	}

    void generateLevel()
    {
        for (int i = 2; i < 12; i++)
        {
            if (Random.Range(0, 100) < 33f)
                continue;
            GameObject newPlanet = Instantiate(P_Planet);
            newPlanet.GetComponent<Planet>().init(i);
            planetList.Add(newPlanet);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Sun>().planets = planetList;
    }

    public static bool isInputFreezed()
    {
        return transitioning || endingGame;
    }

    public void planetDied()
    {
        AudioManager.playExplosion = true;

        combo += 1;
        comboTimer = 0;
        if(combo > 2)
        {
            AudioManager.playCombo = true;
            AudioManager.comboType = 0;

            if(combo > 4)
                AudioManager.comboType = 1;

            if (combo > 6)
                AudioManager.comboType = 2;

            if (combo >8)
                AudioManager.comboType = 3;

            if (combo > 10)
                AudioManager.comboType = 4;
        }
        
    }
}
