using System;
using System.Collections;
using System.Collections.Generic;
using GameSparks.Core;
using UnityEngine;
using UnityEngine.UI;
using GameSparks.Api.Responses;
using System.Linq;

public class General_UI : MonoBehaviour {

	public GameObject Listing;
	public GameObject BuyingContent;
	public UI_BuildingInfoWindow buildingInfoWindow;
	public GameObject SellItem;
	public GameObject SellContent;

    // do building from selection attributes
    public bool selected = false;
    public bool readyToBuild = false;

    public static General_UI GeneralUI_instance = null;

	// Use this for initialization
	void Start()
	{
		if (GeneralUI_instance == null)
		{
            GeneralUI_instance = this;
		}
		else
			Destroy(this.gameObject);


		List<MarketPost> x = new List<MarketPost>();
		if (DB_Gamespark.instance != null)
		{

		}
		StartCoroutine(_Update());
    }

    private void PopulateSellBoard()
	{
		Debug.Log("Received Market Data From GameSparks...");

		foreach (Item item in GameManager.instance.Storage.Values)
		{

			Instantiate(SellItem, SellContent.transform)
				.GetComponent<SellBoard_listing_logic>().initlize(item);
		}
	}

	public void ShowPanel(CanvasGroup panel)
	{
		if (panel.alpha > 0)
		{
			Exist_button(panel);
			return;
		}
		else
		{
			StartCoroutine(FadeImage(false, panel));
			panel.blocksRaycasts = true;
        }


	}
	public IEnumerator _Update()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.5f);
			foreach (Item item in GameManager.instance.Storage.Values)
			{

				if (GameObject.Find(item.itemName + "_txt") != null)
					GameObject.Find(item.itemName + "_txt").GetComponent<Text>().text = item.amount.ToString();
			}
        }
	}
	IEnumerator FadeImage(bool fadeAway,CanvasGroup panel)
	{
		// fade from opaque to transparent
		if (fadeAway)
		{
			// loop over 1 second backwards
			for (float i = 1; i >= 0; i -= Time.deltaTime*5)
			{
				// set color with i as alpha
				panel.alpha = i;
				yield return null;
			}
			panel.alpha = 0;

		}
		// fade from transparent to opaque
		else
		{
			// loop over 1 second
			for (float i = 0; i <= 1; i += Time.deltaTime*5)
			{
				// set color with i as alpha
				panel.alpha = i;
				yield return null;
			}
			panel.alpha = 1;

		}
	}
	public void Exist_button(CanvasGroup panel)
	{
		if (panel.alpha < 0.5)
		{
			ShowPanel(panel);
			return;
		}
		else
		{
			StartCoroutine(FadeImage(true, panel));
			panel.blocksRaycasts = false;
        }
	}

	public void populateMarketBoard(LogEventResponse response)
	{
		List<GSData> posts = response.ScriptData.GetGSDataList("posts");
		Debug.Log("Received Market Data From GameSparks..." + response.ScriptData.GetGSDataList("posts").Count);
		List<GSData> pureList = posts.Distinct().ToList();

		foreach (GSData post in pureList)
		{
			string postid	 = post.GetString("itemID");
			string postname  = post.GetGSData("itemInfo").GetString("itemName");
			float postcount  = (float)post.GetGSData("itemInfo").GetInt("itemCount");
			Instantiate(Listing, BuyingContent.transform)
				.GetComponent<MarketPost_Logic>().initlize(new MarketPost(postid,postname,postcount));
		}
	}

	public void updateData()
	{
		PopulateSellBoard();
		StartCoroutine(_Update());
	}
	public void SetBuildingTypeTemp(Building buildingType)//this is to use the new UI elements
	{
		buildingInfoWindow.buildingDescription.text = buildingType.name;
		buildingInfoWindow.buildingName.text = buildingType.name;
		buildingInfoWindow.buildingPrice.text = buildingType.price;
		ShowPanel(buildingInfoWindow.GetComponent<CanvasGroup>());
		city_Build_Manager.instance.SetCurrentBuilding (buildingType);
	}
    /*
	public void PurchaseBuilding()	
	{
		SetBuildingType(city_Build_Manager.instance.chosenBuilding);
	}*/
    public void SetBuildingType(Building buildingPref)
    {
        
        //accessor.GetComponent<GroundNode> ().building_Pref_Holder.tag == ...  , before inheritance solution 
      //  if (city_Build_Manager.instance.ghostingBuilding != null)// to check is there nuilding or it is all ready destroyed (to avoid error message)
            //    Destroy(city_Build_Manager.instance.ghostingBuilding);

        if(/*Check_Requirements_Availability()*/true)
        {
            if (GroundNode.building_Pref_Holder != null)
                if (GroundNode.building_Pref_Holder.tag == "BeforeBuilding")
                    Destroy(GroundNode.building_Pref_Holder); 

            city_Build_Manager.instance.SetCurrentBuilding(buildingPref);
            city_Build_Manager.instance.SetBuildingTagAndLayer();
            
           selected = true;
            readyToBuild = true;
        } else
            print("u can't build this starage");



        // old method 

        /* if (building_Pref_Holder != null)// to check is there nuilding or it is all ready destroyed (to avoid error message)
            if (readyToBuild == true && building_Pref_Holder.tag == "BeforeBuilding")
                Destroy(building_Pref_Holder);

        if(Check_Requirements_Availability()
        true)
        {
            city_Build_Manager.instance.SetCurrentBuilding(city_Build_Manager.instance.building_Types_Array[city_Build_Manager.instance.building_Index]);
            // gameManagerInstance.SetCurrentBuilding(gameManagerInstance.building_Types_Array[gameManagerInstance.building_Index]);
            selected = true;
            readyToBuild = true;
        } else
            print("u can't build this starage"); */
    }

    public bool Check_Requirements_Availability()                    // need to fix the logic 
    {
        // if (BuildManager.instance.building_Types_Array.GetComponent<Building_Logic>().Choosed_BuildingInfo.RequiredSkill.Skill_State == Skill_Enum.Active) ;

        // check money Reqs
        if (city_Build_Manager.instance.chosenBuilding.Building_Type == BuildingType.Storage)
            if (city_Build_Manager.instance.chosenBuilding.input.amount >= 500)
            {
                return true;
     
            }
            else
                return false;
        else
            return false;
    }

} //end script

