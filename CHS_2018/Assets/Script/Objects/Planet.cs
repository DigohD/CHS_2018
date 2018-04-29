using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    protected GameObject player;

    protected float velocity;
    public float mass;

    public GameObject P_PlanetPart;

    public GameObject planet;
    public GameObject G_HoveredCircle;
    public GameObject G_InputCol;

    public SpriteRenderer orbitCircle;

    public bool isHovered;

    protected float maxMass;
    protected float inertia;

    protected float inputColScale;

    public Material[] mats;

    bool isdead;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        G_InputCol.GetComponent<InputCol>().parent = gameObject;
    }

    public void init(int orderInSystem)
    {
        planet.GetComponent<MeshRenderer>().materials = new Material[1] { mats[Random.Range(0, 8)] };

        G_InputCol.transform.SetParent(null);

        float distance = orderInSystem * 1.5f + Random.Range(-0.25f, 0.25f);
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one * distance;
        
        transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));

        float distancePercent = (1f - (distance / 17f));

        orbitCircle.color = new Color(1, 1, 1,distancePercent * 0.75f);

        velocity = distancePercent * distancePercent * 5f;

        mass = Random.Range(1f + (orderInSystem * 0.5f), 1f + orderInSystem + (orderInSystem * 2f));
        maxMass = 1f + 11 + (11 * 2f);
        inertia = 1 - (mass / maxMass);

        adjustPlanet();
    }

    void Update () {
        transform.Rotate(0, velocity * Time.deltaTime * 80f, 0);

        G_InputCol.transform.position = planet.transform.position;

        G_HoveredCircle.SetActive(isHovered);
    }

    public void boost()
    {
        if (transform.localScale.x >= 16.5f)
            return;

        transform.localScale += Vector3.one * Time.deltaTime * 5f * inertia;

        adjustPlanet();
    }

    public void retract()
    {
        if (transform.localScale.x < 1.5f)
            return;

        transform.localScale -= Vector3.one * Time.deltaTime * 5f * inertia;

        adjustPlanet();
    }

    private void adjustPlanet()
    {
        float distance = transform.localScale.x;
        float distancePercent = (1f - (distance / 17f));

        orbitCircle.color = new Color(1, 1, 1, distancePercent * 0.75f);

        velocity = distancePercent * distancePercent * 5f;

        planet.transform.SetParent(null);
        planet.transform.localScale = Vector3.one * 9f * ((mass / 33f * 18f) + 6f);
        planet.transform.SetParent(transform);
    }

    private void LateUpdate()
    {
        isHovered = false;
    }

    public Vector3 getPlanetPosition()
    {
        return planet.transform.position;
    }

    public void destroy()
    {
        if (isdead)
            return;

        for(int i = 0; i < (int) mass; i++)
        {
            Instantiate(P_PlanetPart, planet.transform.position, Quaternion.identity);
        }

        foreach (Collider c in GetComponents<Collider>())
            c.enabled = false;

        GameControl.mayWarp = true;

        int combo = GameControl.combo > 1 ? GameControl.combo : 1;

        GameControl.score += (int) mass * 1000 * combo;

        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().planetDied();

        isdead = true;

        Destroy(G_InputCol);
        Destroy(gameObject);
    }
}
