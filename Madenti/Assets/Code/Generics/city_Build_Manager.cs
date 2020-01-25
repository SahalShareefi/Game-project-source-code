using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class city_Build_Manager : MonoBehaviour {

    //مثل الاب للعنصر هذا راح نستخدم الاوبجيكت هذا مره وحده فهذي طريقه عشان نسوي له رفرنس سريع
    public static city_Build_Manager instance;//singleton pattern

    // building info
    public Building chosenBuilding;
    public List<BuildingData> Buildings = new List<BuildingData>();
    public Vector3 building_Offset { get { return new Vector3(0, 2.5f, 0); } }
    public GameObject anchor1;  //  how to use it instead of GameObject.Find(...??

    // dataBase info
    public string database1;


    void Awake()
    {
        if (instance != null)
            Destroy(this);

        instance = this;
    }
    public void SetCurrentBuilding(Building input)
    {
        this.chosenBuilding = input;
    }

    public void saveMap() // put in Update instead (auto saving)
    {
        if (Buildings.Count > 0)
            database1 = JsonConvert.SerializeObject(Buildings);
    }

    public void loadMap()
    {
        if (!database1.Equals(""))
        {
            List<BuildingData> _data = JsonConvert.DeserializeObject<List<BuildingData>>(database1);
            foreach (BuildingData DB_Building in _data)
            {

                GameObject _building = Resources.Load ("buildings/prefab/" + DB_Building.buildingname) as GameObject;
                Transform _node = GameObject.Find(DB_Building.nodeName).gameObject.transform; // use Anchor1.transform.Find instead of GameObject.Find(...

                _building.layer = 0;//defailt (raycast can hit)
                _building.tag = "Building";
                _building.GetComponent<Detector_And_Progress>().Finish_Build_progress();

                Instantiate(_building, _node.position + new Vector3(0, 2.5f, 0), _node.rotation);
            }
        }
    }
    public void ClearMap()
    {
        GameObject[] mapBuildings;
        mapBuildings = GameObject.FindGameObjectsWithTag("Building");
        for(int i=0;i<mapBuildings.Length;i++)
            GameObject.Destroy(mapBuildings[i].gameObject);

    }

    public void SetBuildingTagAndLayer() // to not be destroyed
    {

        chosenBuilding.BuildingModel.layer = 2;//ignore (raycast can not hit)
        chosenBuilding.BuildingModel.tag = "BeforeBuilding";
    }
          
    
    private void Update()
    {
        if(Input.GetMouseButtonDown(1)) // use new ui button to disable building
            if (GroundNode.building_Pref_Holder != null && GroundNode.building_Pref_Holder.tag == "BeforeBuilding")
            {
                Destroy(GroundNode.building_Pref_Holder);
                General_UI.GeneralUI_instance.selected = false;
                General_UI.GeneralUI_instance.readyToBuild = false;
            }
    }

}//end script




//scetches ...

 /*
 if (!database1.Equals(""))
 {
     List<BuildingData> _data = JsonConvert.DeserializeObject<List<BuildingData>>(database1);
     foreach (BuildingData DB_Building in _data)
     {
         for (int i = 0; i < BuildingsTemp.Count; i++)
         {
             if (DB_Building.buildingname==BuildingsTemp[i].name)
             {
                 GameObject _building = anchor1.transform.Find(BuildingsTemp[i].name).gameObject;

                 Transform _node = anchor1.transform.Find(DB_Building.nodeName).gameObject.transform;

                 _building.layer = 0;//defailt (raycast can hit)
                 _building.tag = "Building";
                 _building.GetComponent<Detector_And_Progress>().Finish_Build_progress();

                 Instantiate(_building, _node.position + new Vector3(0, 2.5f, 0), _node.rotation);
             }
         }
     }
 }
 */
