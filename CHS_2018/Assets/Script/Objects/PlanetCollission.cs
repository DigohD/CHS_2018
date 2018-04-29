using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetCollission : MonoBehaviour {

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Planet")
        {
            Debug.LogWarning("COLLLLL");
            transform.parent.gameObject.GetComponent<Planet>().destroy();
            coll.gameObject.transform.parent.gameObject.GetComponent<Planet>().destroy();
        }
    }
}
