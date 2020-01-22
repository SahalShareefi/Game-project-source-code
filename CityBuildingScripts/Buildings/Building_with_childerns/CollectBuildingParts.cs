using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBuildingParts : MonoBehaviour {

    public Transform[] buildingParts;
    public Renderer[] renderers;

    public static CollectBuildingParts buildingPartsInstance;//singleton pattern

    private void Start()
    {
      //  buildingParts = GetComponentsInChildren<GameObject>();
    }
    void Awake()//singleton pattern
    {
      //  buildingParts = GetComponentsInChildren<GameObject>();
     //   if (buildingPartsInstance != null) { DestroyObject(this); }     to make each building_Pref_Holder get progress colors 
        buildingPartsInstance = this;// reference to BuildManager
    }

    public Transform[] getbuildingParts()
    {
        return buildingParts = GetComponentsInChildren<Transform>();  
    }

    public Renderer[] getbuildingRenderers()
    {
        return renderers = GetComponentsInChildren<Renderer>();
    }    

}//endScript
