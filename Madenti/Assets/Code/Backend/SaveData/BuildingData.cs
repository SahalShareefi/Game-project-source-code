using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingData {

    public string nodeName;
    public string buildingname;
  //  public Vector3 nodePosition;

    public BuildingData(string _NodeName,string _buildingname)
    {
        this.nodeName = _NodeName;
        this.buildingname = _buildingname;
     //   this.nodePosition = _nodePosition;
    }
}
