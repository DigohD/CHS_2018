using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControl : MonoBehaviour {

    public GameObject P_Planet;

    private ArrayList planetList = new ArrayList();

	void Start () {
        generateLevel();
	}
	
	void Update () {
		
	}

    void generateLevel()
    {
        for (int i = 1; i < 12; i++)
        {
            if (Random.Range(0, 100) < 33f)
                continue;
            GameObject newPlanet = Instantiate(P_Planet);
            newPlanet.GetComponent<Planet>().init(i);
            planetList.Add(newPlanet);
        }

        GameObject.FindGameObjectWithTag("Player").GetComponent<Sun>().planets = planetList;
    }
}
