using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// storage + skill tree الي موجود
/// <summary>
/// هنا راح نحفظ جمييع القيم وجميع العناصر فاللعبه راح تطلب من هذا الكلاس
/// </summary>
public class GameManager : MonoBehaviour {

	public string jsonStorage;
	//مثل الاب للعنصر هذا راح نستخدم الاوبجيكت هذا مره وحده فهذي طريقه عشان نسوي له رفرنس سريع
	public static GameManager instance;//singleton pattern
    public GroundNode ins;

    public GameObject BuildingTemplate;                                          // ايش الهدف منها؟


	private string Map_Save_Data;

	// هذا المستودع الفعلي نقدر نسحب ونعدل الاغراض
	//وبهذي الطريقه نرفع عدد الحديد الخام فالمستودع بشكل سهل جدا
	public Dictionary<string, Item> Storage = new Dictionary<string, Item>();
	//public Dictionary<string, Skill_Node> Skills = new Dictionary<string, Skill_Node>();


	[SerializeField]
	public List<Item> items = new List<Item>();


	private string gameDataFileName = "data.json";

	// Use this for initialization
	void Awake ()
	{
		if (instance != null)
			Destroy(this);

		instance = this;

		//المفروض نغير العدد للمخزن فالذاكره , فالوقت الحالي كل مابدينا اللعبه راح نعطي نفسنا 5 خام حديد
		foreach (Item item in items)
		{
			print("Storage => ADDED " + item.name);
			Storage.Add(item.name, item);
		}

	}

	// Update is called once per frame
	void Update () {


	}
	void OnEnable()
	{
		SceneManager.sceneLoaded += loadScene;
	}
	void OnDisable()
	{
		SceneManager.sceneLoaded -= loadScene;
	}
    
	public void loadScene(Scene scene , LoadSceneMode mode)
	{
        /*
		if (scene.name == "ONLINE_SCENE")
		{
			Debug.Log("======Starting Loading Scene=====" + Buildings.Count);
			GameObject cam = GameObject.Find("Camera");
			cam.SetActive(false);
			foreach (Building buildingData in Buildings)
			{
				print(buildingData.BuildingModel.name);
				Debug.Log("LOADING GAME =>" + buildingData.name);
				GameObject node = GameObject.Find(buildingData.Node);
				GameObject b = Instantiate(BuildingTemplate, node.transform);
				//b.GetComponent<Building_Logic>().LoadToScene(buildingData);
			}
			cam.SetActive(true);
        }
        */

    }
   
    public void SaveGameData(string dataToJson)
	{

	}

	

	
}
