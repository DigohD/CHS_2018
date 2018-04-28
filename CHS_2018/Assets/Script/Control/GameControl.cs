using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {

    public static long score;

    public GameObject P_Planet;

    private ArrayList planetList = new ArrayList();

    public static int time = 180;

    public static bool transitioning;
    public static bool endingGame;

    private enum ShiftState { FADE_OUT, GENERATE_LEVEL, FADE_IN };
    private enum EndGameState { END_GAME_FLASH, FADE_OUT, SWITCH_SCENE};
    private ShiftState shiftState;
    private EndGameState endGameState;

    void Start () {
        time = 180;
        transitioning = false;
        endingGame = false;
        score = 0;

        generateLevel();
	}

    float timer;
    float timeTimer;
    void Update () {
        timer += Time.deltaTime;
        timeTimer += Time.deltaTime;

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
                    if(timer > 3f)
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

        if (Input.GetKeyDown(KeyCode.Space))
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
}
