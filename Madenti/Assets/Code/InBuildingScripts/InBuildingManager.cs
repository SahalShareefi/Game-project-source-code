using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InBuildingManager : MonoBehaviour {
	public enum Construct_Type
	{
		database, non_database
	}


	public Dictionary<string, Machine> Machines = new Dictionary<string, Machine>();
	public List<Block_Logic> blocksUsed = new List<Block_Logic>();// temp data for debugging database
	public GameObject blockPrefab;
	public GameObject anchor;
	public GameObject ghostMachine;
	public Machine currentMachine;
	public Block_Logic currentBlock;
	public LayerMask gridLayer;
	public LayerMask machineLayer;
	public orientation currentOrientation = orientation.Vertical;
	public List<MachineData> currentMachines = new List<MachineData>();
	public static InBuildingManager instance;
	public float yGrid;
	public float xGrid;
	public float scale;
	public string database;
	public GameObject rails;
	public List<GameObject> grid = new List<GameObject>();
	
	// Use this for initialization
	void Start () {
		if (instance == null)
			instance = this;

		yGrid = transform.localScale.z * scale;
		xGrid = transform.localScale.x * scale;
		populateMachinesList();
		gridGeneration();

	}

	private void populateMachinesList()
	{
		foreach(Machine i in Resources.FindObjectsOfTypeAll<Machine>())
		{
			Machines.Add(i.name, i);
		}
		print(Machines.Count);
	}

	bool gridGeneration()
	{
		try
		{
			for (int x = 0; x < scale; x++)
			{
				for (int y = 0; y < scale; y++)
				{
					int xx = x;
					int yy = y;
					GameObject block = Instantiate(blockPrefab, anchor.transform);
					block.transform.name = "Grid" + x + "x" + y;
					block.transform.position = new Vector3(x + anchor.transform.GetChild(0).transform.position.x,
						0,
						y + anchor.transform.GetChild(0).transform.position.z);
					grid.Insert(0, block);
				}
			}
			return true;// if generator is successful 
		}
		catch
		{
			print("grid Generation error");
			return false; // if generator failed
		}
	}


	// Update is called once per frame
	void Update ()
	{
		if (Input.GetMouseButton(0) && currentBlock != null && ghostMachine != null)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit,gridLayer))
			{
				constructMachine(currentBlock.gameObject,ghostMachine.transform.rotation,currentMachine.name,false);
				print(hit.collider.name);
			}
		}

		
	}
	public void saveData()
	{
		if(currentMachines.Count > 0)
		database = JsonConvert.SerializeObject(currentMachines);
	}
	public void loadData()
	{
		blocksUsed.Clear();
		if (!database.Equals(""))
		{
			List<MachineData> _data = JsonConvert.DeserializeObject<List<MachineData>>(database);
			foreach (MachineData i in _data)
			{
				constructMachine(anchor.transform.Find(i.blockName).gameObject, i.rotation, i.machineType, true);
			}
		}
	}
	public void clearMap()
	{
		foreach (Block_Logic i in blocksUsed)
		{
			Destroy(i.holder);
			i.machine = null;
		}
	}

	public void setMachine(Machine type)
	{
		if (ghostMachine != null)
			Destroy(ghostMachine);
		ghostMachine = Instantiate(type.preFab, this.transform);
		currentMachine = type;
	}

	public void rotateMachine()
	{
		if (ghostMachine == null)
			return;
		ghostMachine.transform.Rotate(new Vector3(0, 90, 0));
	}
	public void constructMachine(GameObject _block,Quaternion _rotation,string _machineName,bool database)
	{
		Block_Logic _BlockLogic = _block.GetComponent<Block_Logic>();
		if (_BlockLogic.holder != null)
			return;
		if (!Machines[_machineName])
			return;
		Machine _machine = Machines[_machineName];
	
		if (!database)
			currentMachines.Add(new MachineData(_block.transform.name, _machine.name,_rotation));

		blocksUsed.Add(_BlockLogic);

		_BlockLogic.holder = Instantiate(_machine.preFab, _block.transform);
		_BlockLogic.holder.transform.position = new Vector3(_block.transform.position.x, 0.5f, _block.transform.position.z);
		_BlockLogic.holder.transform.rotation = _rotation;
		_BlockLogic.machine = _machine;

	}
}
