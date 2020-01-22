using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unfitable_place_Detector : GroundNode {

    private Renderer rend;
    private Color startColor;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnTriggerStay(Collider x)
    {
        if ( x.tag == "Building" )
        {
            print("triggered");
            this.gameObject.GetComponent<Renderer>().material.color = Color.red;
           // foreach (Transform parts in CollectBuildingParts.buildingPartsInstance.getbuildingParts())
           // gameObject.GetComponent<Renderer>().material.color = Color.red; 
            correct_place = false;
        }
    }
    private void OnTriggerExit(Collider x)
    {
        if (x.tag == "Building")
        {
            this.gameObject.GetComponent<Renderer>().material.color = startColor;
            // foreach (Transform parts in CollectBuildingParts.buildingPartsInstance.getbuildingParts())
            //gameObject.GetComponent<Renderer>().material.color = startColor; 
            correct_place = true;
        }
    }

}
