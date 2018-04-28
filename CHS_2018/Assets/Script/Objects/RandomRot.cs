using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRot : MonoBehaviour {

    Vector3 velocity;

	void Start () {
        velocity = new Vector3(
            Random.Range(1f, 5f),
            Random.Range(1f, 5f),
            Random.Range(1f, 5f)
        );
	}
	
	void Update () {
        transform.Rotate(velocity * Time.deltaTime * 100f);
	}
}
