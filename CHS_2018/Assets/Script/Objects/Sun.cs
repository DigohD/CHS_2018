using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public static float gravity = 0.9f;
    public static float gravityRange = 22f;

    public ArrayList planets = new ArrayList();

    protected float velocity;

    public GameObject G_Visuals;

	void Start () {
        
	}
	
	void Update () {
        if (MouseInput.isChanneling)
        {
            G_Visuals.GetComponent<Animator>().SetInteger("State", 1);
        }
        else
        {
            G_Visuals.GetComponent<Animator>().SetInteger("State", 0);

        }
    }
}
