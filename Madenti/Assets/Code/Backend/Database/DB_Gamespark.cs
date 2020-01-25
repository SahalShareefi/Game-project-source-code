using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using GameSparks.Core;

public class DB_Gamespark : MonoBehaviour
{

	public static DB_Gamespark instance = null;

	// Use this for initialization
	void Start()
	{
		if (instance == null)
		{
			instance = this;
			StartCoroutine(connectToGamespark());
		}
		else
			Destroy(this.gameObject);
	}

	private IEnumerator connectToGamespark()
	{

        
		yield return new WaitForSecondsRealtime(3f);
		new GameSparks.Api.Requests.DeviceAuthenticationRequest().Send((response) =>
		{
			if (!response.HasErrors)
			{
				Debug.Log("Device Authenticated...");
				if ((bool)response.NewPlayer)
					createPlayerRecords();
				else
					LoadPlayerData();
					
				StopCoroutine(connectToGamespark());
			}
			else
			{
				StartCoroutine(connectToGamespark());
				Debug.Log("Error Authenticating Device... "  + response.Errors.ToString() );
			
			}
		});
	}

	private void LoadPlayerData()
	{
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GetPlayerData").Send((response) => {
			if (!response.HasErrors)
			{
				print(" LOAD PLAYER DATA = >  \n  " + response.ScriptData.JSON);
				print("Decoding Storage = > " + response.ScriptData.GetGSDataList("Storage").ToString());
				if(response.ScriptData.GetGSDataList("Storage") != null)
					foreach (GSData _item in response.ScriptData.GetGSDataList("Storage"))
					{
						if (GameManager.instance.Storage.ContainsKey(_item.GetString("Key")))
						{
							GameManager.instance.Storage
							[_item.GetGSData("Value").GetString("name")] = JsonConvert.DeserializeObject<Item>
																						(_item.GetGSData("Value").JSON);
						}
					}

				print("Decoding buildings =>" + response.ScriptData.GetGSDataList("Buildings").ToString());
				if (response.ScriptData.GetGSDataList("Buildings") != null)
					foreach (GSData _Buildingdata in response.ScriptData.GetGSDataList("Buildings"))
					{
						foreach (Building x in Resources.FindObjectsOfTypeAll<Building>())
						{
							if (_Buildingdata.GetString("name").Equals(x.name))
							{
								
								print("Adding building_Pref_Holder");
							//	city_Build_Manager.instance.Buildings.Add(x.BuildingModel);
							}
						}
					}

				print("LOADED everything now loading scene");
                SceneManager.LoadScene("ONLINE_SCENE");
			}
			else
				Debug.Log("Items have not been saved");
		});
	}
	

	private void createPlayerRecords()
	{
		Debug.Log("STORAGE => " + JsonConvert.SerializeObject(GameManager.instance.Storage) + "\n");
		Debug.Log("BUILDINGS => " + JsonConvert.SerializeObject(city_Build_Manager.instance.Buildings)+"\n");
		var Storage = JsonConvert.SerializeObject(GameManager.instance.Storage);
		var Buildings = JsonConvert.SerializeObject(city_Build_Manager.instance.Buildings);

        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("Player_Creation")
			.SetEventAttribute("Storage", Storage)
			.SetEventAttribute("Buildings", Buildings)
			.Send((response) => {
			if (!response.HasErrors)
			{
					print(response.ScriptData.JSON);
					Debug.Log("Player created!!");
					LoadPlayerData();
			}

			else
				Debug.Log("Items have not been saved");

		});
	}

	public void post_storage()
	{
		string Storage = JsonConvert.SerializeObject(GameManager.instance.Storage);
        new GameSparks.Api.Requests.LogEventRequest().SetEventKey("PostStorage")
			.SetEventAttribute("Storage", Storage)
			.Send((response) => {
			if (!response.HasErrors)
			{
				Debug.Log("Storage Posted!!");
				get_storage();

			}

			else
				Debug.Log("Items have not been saved");

		});
	}



	public void post_building(List<BuildingData> buildings)
	{
		
		string _Buildings = JsonConvert.SerializeObject(buildings);
		print(_Buildings);

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("PostBuildings")
			.SetEventAttribute("Buildings", _Buildings)
			.Send((response) => {
				if (!response.HasErrors)
				{
					Debug.Log("Buildings Posted!!");
					get_buildings();

				}

				else
					Debug.Log("Building have not been saved");

			});

	}
	public void UpdateAll()
	{
		Debug.Log("STORAGE => " + JsonConvert.SerializeObject(GameManager.instance.Storage) + "\n");
		Debug.Log("BUILDINGS => " + JsonConvert.SerializeObject(city_Build_Manager.instance.Buildings) + "\n");
		var Storage = JsonConvert.SerializeObject(GameManager.instance.Storage);
		var Buildings = JsonConvert.SerializeObject(city_Build_Manager.instance.Buildings);
		print(Storage);
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("Player_Creation")
			.SetEventAttribute("Storage", Storage)
			.SetEventAttribute("Buildings", Buildings)
			.Send((response) => {
				if (!response.HasErrors)
				{
					print(response.ScriptData.JSON);
					Debug.Log("Player Updated!!");
					LoadPlayerData();
				}

				else
					Debug.Log("Have not been updated");

			});
	}
	private void get_storage()
	{

		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GetStorage").Send((response) => {
				if (!response.HasErrors)
				{
				Debug.Log("Items saved ");
					foreach(GSData _item in response.ScriptData.GetGSDataList("Storage"))
						{
							if (GameManager.instance.Storage.ContainsKey(_item.GetString("Key")))
							{
								GameManager.instance.Storage[_item.GetGSData("Value").GetString("itemName")] = JsonConvert.DeserializeObject<Item>
																							(_item.GetGSData("Value").JSON);
							}
						}
				General_UI.GeneralUI_instance.updateData();
				}
				else
					Debug.Log("Items have not been saved");
			});
		}
	private void get_buildings()
	{
		new GameSparks.Api.Requests.LogEventRequest().SetEventKey("GetBuildings").Send((response) => {
			if (!response.HasErrors)
			{
				print("Decoding buildings");
				List<BuildingData> buildings = JsonConvert
				.DeserializeObject<List<BuildingData>>(response.ScriptData.GetGSDataList("Buildings").ToString());
			}
			else
				Debug.Log("Items have not been saved");
		});
	}
	}
