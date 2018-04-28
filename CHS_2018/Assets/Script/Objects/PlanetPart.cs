using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetPart : MonoBehaviour {

    public GameObject G_Visuals;

    Vector3 velocity;


    GameObject player;
	void Start () {
        G_Visuals.transform.localScale = Vector3.one * Random.Range(2f, 5f);

        velocity = new Vector3(
            Random.Range(1f, 5f),
            0,
            Random.Range(1f, 5f)
        );

        if(Random.Range(0, 2) == 0)
        {
            velocity = -velocity;
        }

        player = GameObject.FindGameObjectWithTag("Player");
	}

    float timer;
    void Update()
    {
        timer += Time.deltaTime;

        if(timer < 1f)
        {
            velocity -= ((velocity * 0.8f) * Time.deltaTime);
        }
        else
        {
            velocity -= ((velocity * 0.8f) * Time.deltaTime);
            velocity += (player.transform.position - transform.position.normalized) * Time.deltaTime * 5f;
        }

        transform.position += velocity * Time.deltaTime * 80f;
    }
}
