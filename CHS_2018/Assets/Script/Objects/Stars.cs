using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public GameObject P_BGStar;

    public int stars;

	// Use this for initialization
	void Start () {

        for (int i=0; i < stars; i++){
            GameObject newstar = Instantiate(P_BGStar);
            float x = Random.Range(-600, 600);
            float z = Random.Range(-600, 600);
            float scale = Random.Range(0.5f,4f);
            float rot = Random.Range(0, 360);

            newstar.transform.position = new Vector3(x, -180, z);
            newstar.transform.localScale = Vector3.one * scale;
            newstar.transform.Rotate(0,rot,0);

            newstar.transform.SetParent(transform);
        }
        
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
