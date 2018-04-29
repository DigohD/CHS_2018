using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInput : MonoBehaviour {

    public static bool isChanneling;

    public static Planet currentlySelected;

	void Start () {
        isChanneling = false;
        currentlySelected = null;

    }

    float deselectTimer = 0f;
	void Update () {
        RaycastHit hit;
        Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

        isChanneling = false;

        if (!currentlySelected)
            currentlySelected = null;

        if (!Input.GetMouseButton(0) && !Input.GetMouseButton(1) && Physics.Raycast(ray, out hit))
        {
            Transform objectHit = hit.transform;
            if (objectHit.tag.Equals("PlanetInput"))
            {
                currentlySelected = objectHit.GetComponent<InputCol>().parent.GetComponent<Planet>();
            }
            deselectTimer = 0;
        }
        else if(currentlySelected != null && Input.GetMouseButton(0) && !GameControl.isInputFreezed())
        {
            deselectTimer = 0.5f;

            currentlySelected.boost();

            isChanneling = true;
        }
        else if (currentlySelected != null && Input.GetMouseButton(1) && !GameControl.isInputFreezed())
        {
            deselectTimer = 0.5f;

            currentlySelected.retract();

            isChanneling = true;
        }
        else if (currentlySelected != null)
        {
            deselectTimer += Time.deltaTime;
            if(deselectTimer > 0.3f)
            {
                currentlySelected = null;
            }
        }

        if(currentlySelected != null)
        {
            currentlySelected.isHovered = true;
        }
    }
}
