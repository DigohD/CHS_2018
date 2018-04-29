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
    public Material voidMaterial;

    public Mesh satelliteMesh;
    public Material satelliteMaterial;

    bool isdead;

    private enum PlanetType { NORMAL, VOID, SPACE_BASE };
    PlanetType planetType = PlanetType.NORMAL;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        G_InputCol.GetComponent<InputCol>().parent = gameObject;
    }

    public void init(int orderInSystem, bool canBseSpecial)
    {
        if(canBseSpecial && Random.Range(0, 16) == 0)
        {
            planetType = PlanetType.VOID;
        }else if (canBseSpecial && Random.Range(0, 16) == 0)
        {
            planetType = PlanetType.SPACE_BASE;
        }

        G_InputCol.transform.SetParent(null);

        float distance = orderInSystem * 1.5f + Random.Range(-0.25f, 0.25f);
        transform.position = Vector3.zero;
        transform.localScale = Vector3.one * distance;

        transform.Rotate(new Vector3(0, Random.Range(0f, 360f), 0));

        float distancePercent = (1f - (distance / 17f));

        velocity = distancePercent * distancePercent * 5f;

        mass = Random.Range(1f + (orderInSystem * 0.5f), 1f + orderInSystem + (orderInSystem * 2f));
        if(planetType == PlanetType.SPACE_BASE)
        {
            mass = 33;
        }

        maxMass = 1f + 11 + (11 * 2f);
        inertia = 1 - (mass / maxMass);

        switch (planetType)
        {
            case PlanetType.NORMAL:
                planet.GetComponent<MeshRenderer>().materials = new Material[1] { mats[Random.Range(0, 8)] };
                orbitCircle.color = new Color(1, 1, 1, distancePercent * 0.75f);
                break;
            case PlanetType.VOID:
                planet.GetComponent<MeshRenderer>().materials = new Material[1] { voidMaterial };
                orbitCircle.color = new Color(1, 0, 0, distancePercent * 0.75f);
                break;
            case PlanetType.SPACE_BASE:
                Debug.LogWarning("SPACE BASE");
                planet.GetComponent<MeshFilter>().mesh = satelliteMesh;
                planet.GetComponent<MeshRenderer>().materials = new Material[1] { satelliteMaterial };
                orbitCircle.color = new Color(0, 0, 1f, distancePercent * 0.75f);
                break;
        }

        adjustPlanet();
    }

    void Update () {
        transform.Rotate(0, velocity * Time.deltaTime * 80f, 0);

        G_InputCol.transform.position = planet.transform.position;

        G_HoveredCircle.SetActive(isHovered);

        switch (planetType)
        {
            case PlanetType.SPACE_BASE:
                planet.transform.Rotate(Vector3.up, 360f * Time.deltaTime);
                break;
        }
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

        switch (planetType)
        {
            case PlanetType.NORMAL:
                orbitCircle.color = new Color(1, 1, 1, distancePercent * 0.75f);
                break;
            case PlanetType.VOID:
                orbitCircle.color = new Color(1, 0, 0, distancePercent * 0.75f);
                break;
            case PlanetType.SPACE_BASE:
                orbitCircle.color = new Color(0, 0, 1f, distancePercent * 0.75f);
                break;
        }

        velocity = distancePercent * distancePercent * 5f;

        G_HoveredCircle.transform.SetParent(null);
        G_HoveredCircle.transform.localScale = Vector3.one * 8f;
        G_HoveredCircle.transform.SetParent(transform);

        planet.transform.SetParent(null);
        planet.transform.localScale = Vector3.one * 7f * ((mass / 33f * 18f) + 4f);
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

        for (int i = 0; i < (int)mass; i++)
        {
            GameObject part = Instantiate(P_PlanetPart, planet.transform.position, Quaternion.identity);
            part.transform.GetChild(0).GetComponent<MeshRenderer>().materials = planet.GetComponent<MeshRenderer>().materials;
        }

        foreach (Collider c in GetComponents<Collider>())
            c.enabled = false;

        switch (planetType)
        {
            case PlanetType.NORMAL:
                GameControl.mayWarp = true;

                int combo = GameControl.combo > 1 ? GameControl.combo : 1;
                GameControl.score += (int)mass * 1000 * combo;

                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().planetDied();
                break;
            case PlanetType.VOID:
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().voidDied();
                break;
            case PlanetType.SPACE_BASE:
                GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControl>().satelliteDied();
                break;
        }

        isdead = true;

        Destroy(G_InputCol);
        Destroy(gameObject);
    }
}
