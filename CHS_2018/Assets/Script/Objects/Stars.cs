using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour {

    public GameObject P_BGStar;

	// Use this for initialization
	void Start () {

        for (int i=0; i < 30; i++){
            GameObject newstar = Instantiate(P_BGStar);
            float x = Random.Range(-800, 800);
            float z = Random.Range(-800, 800);
            float scale = Random.Range(0.5f,8f);
            float rot = Random.Range(0, 360);

            newstar.transform.position = new Vector3(x, -180, z);
            newstar.transform.localScale = Vector3.one * scale;
            newstar.transform.Rotate(0,rot,0);
        }
        
        	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
