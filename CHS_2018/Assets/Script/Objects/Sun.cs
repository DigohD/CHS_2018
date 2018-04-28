using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour {

    public static float gravity = 0.9f;
    public static float gravityRange = 22f;

    public ArrayList planets = new ArrayList();

    protected float velocity;

    public GameObject P_ExplosionEffect;

    public GameObject G_Visuals;
    public GameObject G_Spikes;
    public GameObject G_TractorBeam;
    public GameObject G_BlackHole;
    ParticleSystem tractorParticles;

    private float eatTimer = 1f;
    private float spikeVelocity = 1f;
    private float spikeTargetVelocity = 1f;

	void Start () {
        tractorParticles = G_TractorBeam.GetComponent<ParticleSystem>();
    }

    float explodeTimer;
	void Update () {
        if (MouseInput.isChanneling && !GameControl.endingGame)
        {
            if (eatTimer > 0.1f)
                G_Visuals.GetComponent<Animator>().SetInteger("State", 1);

            spikeTargetVelocity = 5f;

            if (!tractorParticles.isPlaying)
                tractorParticles.Play();

            Vector3 planetPos = MouseInput.currentlySelected.getPlanetPosition();
            G_TractorBeam.transform.position = planetPos;

            Vector3 diff = transform.position - planetPos;
            G_TractorBeam.transform.LookAt(diff.normalized);

            ParticleSystem.MainModule mm = tractorParticles.main;
            ParticleSystem.MinMaxCurve mmc = new ParticleSystem.MinMaxCurve(diff.magnitude / 573.91f);
            mm.startLifetime = mmc;
        }
        else if (GameControl.endingGame)
        {
            G_Visuals.GetComponent<Animator>().SetInteger("State", 3);

            explodeTimer += Time.deltaTime;
            if(explodeTimer > 1f)
            {
                Instantiate(P_ExplosionEffect, transform.position, Quaternion.identity);

                G_BlackHole.SetActive(true);

                Destroy(gameObject);
                return;
            }
        }
        else
        {
            if (eatTimer > 0.1f)
                G_Visuals.GetComponent<Animator>().SetInteger("State", 0);

            spikeTargetVelocity = 1f;

            tractorParticles.Stop();
        }

        eatTimer += Time.deltaTime;
        if(eatTimer <= 0.1f)
        {
            spikeTargetVelocity = 0.3f;

            G_Visuals.GetComponent<Animator>().SetInteger("State", 2);
        }

        spikeVelocity = Mathf.Lerp(spikeVelocity, spikeTargetVelocity, Time.deltaTime * 6);
        G_Spikes.transform.Rotate(0, spikeVelocity * Time.deltaTime * 80f, 0);
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
