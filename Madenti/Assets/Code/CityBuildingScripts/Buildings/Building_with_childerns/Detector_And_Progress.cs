using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector_And_Progress : MonoBehaviour {

    public Renderer[] rends;
    public Color[] StartColor;
    public Color[] ghostingColor;
    public float transparentValue;

    public void Start()
    {
        rends = CollectBuildingParts.buildingPartsInstance.getbuildingRenderers();
        StartColor = new Color[CollectBuildingParts.buildingPartsInstance.getbuildingParts().Length];// length for all building_Pref_Holder parts
        ghostingColor = new Color[CollectBuildingParts.buildingPartsInstance.getbuildingParts().Length];// length for all building_Pref_Holder parts

        // save origin colors 
        for (int i = 0; i < StartColor.Length; i++)
           StartColor[i] = rends[i].material.color;

        // save ghosting colors
        transparentValue = 0.2f;
        for (int i = 0; i < ghostingColor.Length; i++)
            ghostingColor[i] = new Color(0.5f, 0.5f, 1, transparentValue); // blue color


        //   foreach (Renderer parts in CollectBuildingParts.buildingPartsInstance.getbuildingRenderers())
        //  { parts.material.color = new Color(0.5f, 0.5f, 1, 1); } // blue color
        //  foreach (Transform parts in CollectBuildingParts.buildingPartsInstance.getbuildingParts())
        //{ StartColor = parts.gameObject.GetComponent<Renderer>().material.color }
        //startColor = rend.material.color;
    }

    

    //unfitable_place_Detector_Mparts
    private void OnTriggerStay(Collider x)
    {
        if (x.tag == "Building")
        {
            InCorrectPlaceColoring();
        }
    }// unfitable_place_Detector_Mparts
    private void OnTriggerExit(Collider x)
    {
        if (x.tag == "Building")
        {
            CorrectPlaceColoring();
        }
    }

    
    public void InCorrectPlaceColoring()
    {
            print("triggered");
            foreach (Renderer parts in rends)
            { parts.material.color = Color.red; }
    }

    public void CorrectPlaceColoring()
    {
            for (int i = 0; i < ghostingColor.Length; i++)
            {
                rends[i].material.EnableKeyword("_ALPHAPREMULTIPLY_ON");// _ALPHAPREMULTIPLY_ON keyword used to “Transparent” Transparency Rendering Mode
                rends[i].material.color = ghostingColor[i];
            }
    }

    public void GhostingColoring()
    {
        for (int i = 0; i < ghostingColor.Length; i++)
        {
            rends[i].material.EnableKeyword("_ALPHAPREMULTIPLY_ON");// _ALPHAPREMULTIPLY_ON keyword used to “Transparent” Transparency Rendering Mode
            rends[i].material.color = ghostingColor[i];
        }
    }
    
    public void coloring_Build_progress()
    {
        foreach (Renderer parts in CollectBuildingParts.buildingPartsInstance.getbuildingRenderers())
        { parts.material.color = Color.green; }
    }

    public void Finish_Build_progress()
    {
        for (int i = 0; i < StartColor.Length; i++)
            rends[i].material.color = StartColor[i];
    }
}

