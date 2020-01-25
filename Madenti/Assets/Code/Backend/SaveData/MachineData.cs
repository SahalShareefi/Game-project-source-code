using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineData {

	public string blockName;
	public string machineType;
	public Quaternion rotation;

	public MachineData(string blockName,string machineType,Quaternion rotation)
	{
		this.blockName = blockName;
		this.machineType = machineType;
		this.rotation = rotation;
	}
}
