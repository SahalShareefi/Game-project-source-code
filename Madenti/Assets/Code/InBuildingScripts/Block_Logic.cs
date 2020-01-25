using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Block_Logic : MonoBehaviour {

	public Machine machine;
	public GameObject holder;
	public int mX;
	public int mY;

	void Start()
	{
		if (gameObject.name.Contains("Clone"))
			return;


		Match reg = Regex.Match(gameObject.name, "Grid([0-9]+)x([0-9]+)");
		if (reg.Success)
		{
			mX =int.Parse(reg.Groups[1].Value);
			mY =int.Parse(reg.Groups[2].Value);
		}
	}

	
	void OnMouseOver()
	{
		InBuildingManager.instance.currentBlock = this;
		if (InBuildingManager.instance.ghostMachine == null)
			return;
		InBuildingManager.instance.ghostMachine.transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
	}


}
