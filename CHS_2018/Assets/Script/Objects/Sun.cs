using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public static float gravity = 0.9f;
    public static float gravityRange = 22f;

    public ArrayList planets = new ArrayList();

    protected float velocity;

    public GameObject G_Visuals;
    public GameObject G_TractorBeam;
    ParticleSystem tractorParticles;

    private float eatTimer = 1f;

	void Start () {
        tractorParticles = G_TractorBeam.GetComponent<ParticleSystem>();

    }
	
	void Update () {
        if (MouseInput.isChanneling)
        {
            if (eatTimer > 0.1f)
                G_Visuals.GetComponent<Animator>().SetInteger("State", 1);
            if (!tractorParticles.isPlaying)
                tractorParticles.Play();

            Vector3 planetPos = MouseInput.currentlySelected.getPlanetPosition();
            G_TractorBeam.transform.position = planetPos;

            Vector3 diff = transform.position - planetPos;
            G_TractorBeam.transform.LookAt(diff.normalized);

            Debug.LogWarning(planetPos + " PLANET");
            Debug.LogWarning(diff.magnitude + " MAG");

            ParticleSystem.MainModule mm = tractorParticles.main;
            ParticleSystem.MinMaxCurve mmc = new ParticleSystem.MinMaxCurve(diff.magnitude / 573.91f);
            mm.startLifetime = mmc;
        }
        else
        {
            if (eatTimer > 0.1f)
                G_Visuals.GetComponent<Animator>().SetInteger("State", 0);

            tractorParticles.Stop();
        }

        eatTimer += Time.deltaTime;
        if(eatTimer <= 0.1f)
        {
            G_Visuals.GetComponent<Animator>().SetInteger("State", 2);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "PlanetPart")
        {
            eatTimer = 0;
            Destroy(coll.gameObject);
        }
    }
}
