using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollission : MonoBehaviour {

	void Start () {
		
	}
	
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Planet")
        {
            Destroy(transform.parent.gameObject);
            Destroy(coll.gameObject.transform.parent.gameObject);
        }
    }
}
