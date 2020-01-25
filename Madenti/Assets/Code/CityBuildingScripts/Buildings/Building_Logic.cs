using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// هذا منطق المباني 
/// </summary>
public class Building_Logic : MonoBehaviour {

	[SerializeField]
	public Building Choosed_BuildingInfo;
	[SerializeField]
	public GameObject GFX_Holder;                            //for the building's 3d model
	[SerializeField]                                          
    public float AmountPerSecond =  1f;                      //temp data , will be removed

    //Node info
    public String Node;

    // moving building_Pref_Holder attributes
    private float build_Movement_smooth = 22;
    public static GameObject GroundInstance;
    private Vector3 target;
    public Vector3 Target
    {
        get { return target; }
        set
        {
            target = value;

            StopCoroutine("MoveBuilding");
            StartCoroutine("MoveBuilding", target);
        }
    }

    private IEnumerator LateStart()
	{
		yield return new WaitForSecondsRealtime(0.5f);

			switch (Choosed_BuildingInfo.Building_Type)
			{
				case BuildingType.Collector:
					StartCoroutine(Collect());
					break;

				case BuildingType.Producer:
					StartCoroutine(Produce());
					break;
			}
		
	}

	public void Reinitlize()
	{

			switch (Choosed_BuildingInfo.Building_Type)
			{
				case BuildingType.Collector:
					StartCoroutine(Collect());
					break;

				case BuildingType.Producer:
					StartCoroutine(Produce());
					break;
				//case BuildingType.Storage:
				  //  StartCoroutine(Storage());
					//break;

			}
	}

	internal void LoadScene(Building Data) //called when building data is decoded and viable
    {
		Choosed_BuildingInfo = Data;
		print("Creating the gfx");
		Instantiate(Data.BuildingModel, GFX_Holder.transform);
		//GetComponentInChildren<Detector_And_Progress>().LateStart();
		transform.position = Choosed_BuildingInfo.Position;
		Reinitlize();
	}

	/* private IEnumerator Storage()
	 {
		 while (true)
		 {
			 //وبهذي الطريقه نجيب الرفرينس للمخرج المطلوب والي هو فالمبنى الحالي خام حديد
			 yield return new WaitForSecondsRealtime(1);
			 //اذا فيه متطلب للغرض شيل احذف من المتطلب للجديد
			 if (GameManager.instance.Storage[Choosed_BuildingInfo.input.itemName].amount >= Choosed_BuildingInfo.output.requirement)
			 {
				 float required = Choosed_BuildingInfo.output.requirement;
				 GameManager.instance.Storage[Choosed_BuildingInfo.input.itemName].amount -= required;
				 //انتج المطلوب
				 GameManager.instance.Storage[Choosed_BuildingInfo.output.itemName].amount += AmountPerSecond;
			 }

		 }
	 }*/
	private IEnumerator Produce()
	{
		while (true)
		{
			//وبهذي الطريقه نجيب الرفرينس للمخرج المطلوب والي هو فالمبنى الحالي خام حديد
			yield return new WaitForSecondsRealtime(1);
			//اذا فيه متطلب للغرض شيل احذف من المتطلب للجديد
			if (GameManager.instance.Storage[Choosed_BuildingInfo.input.itemName].amount  >=  Choosed_BuildingInfo.output.requirement)
			{
				float required = Choosed_BuildingInfo.output.requirement;
				GameManager.instance.Storage[Choosed_BuildingInfo.input.itemName].amount  -= required;
				//انتج المطلوب
				GameManager.instance.Storage[Choosed_BuildingInfo.output.itemName].amount += AmountPerSecond;
			}

		}
	}
	private IEnumerator Collect()
	{
		while (true)
		{
			//وبهذي الطريقه نجيب الرفرينس للمخرج المطلوب والي هو فالمبنى الحالي خام حديد
			yield return new WaitForSecondsRealtime(1);
			//انتج المطلوب
			GameManager.instance.Storage[Choosed_BuildingInfo.output.itemName].amount += AmountPerSecond;


		}
	}
    
    public IEnumerator MoveBuilding(Vector3 target)
    {
       if(this.transform.gameObject.tag == "BeforeBuilding")
         while (Vector3.Distance(this.transform.position, target) > 0)
           {
              this.transform.position = Vector3.Lerp(this.transform.position, target , Time.deltaTime * build_Movement_smooth);
              yield return null;
           }
    }

}//end script