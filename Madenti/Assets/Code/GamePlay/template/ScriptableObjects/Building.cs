using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// هذا النموذج للمباني بحيث انه يكون سهل علينا نظيف مباني
/// حاليا راح يكون الاساس انه ياخذ مدخل ويخرج مخرج معين
/// </summary>

[CreateAssetMenu(fileName = "New Building", menuName = "new building")]
public class Building : ScriptableObject {

	public string price;
	public BuildingType Building_Type;
	public Building_Status Building_Status = Building_Status.NotUsed;

	public GameObject BuildingModel;

	public Vector3 Position;

	public int HoursToBuild = 0;
	public int MinutesToBuild = 0;
	public int SecondsToBuild = 0;

	public DateTime Construction_Starting_Time;

	// هذا راح يحفظ الاوت بوت مثلا في المناجم راح يكون حديد خام
	public Item output;
	//المدخل هنا راح يحفظ نوع المنتج المخرج فالوقت الحالي المخرج راح يكون نوع واحد مستقبلا نقدر ننوع
	//فالمثال الحالي راح يكون قالب حديد
	//المدخل هنا راح يحفظ نوع المنتج المخرج فالوقت الحالي المخرج راح يكون نوع واحد مستقبلا نقدر ننوع
	public Item input;

    public Boolean isConstruction_Finished()
	{

		TimeSpan Differance = Construction_Starting_Time.Subtract(DateTime.Now);                                        //? what is TimeSpan
        TimeSpan TimeToConstruct = new TimeSpan(HoursToBuild, MinutesToBuild, SecondsToBuild);
		if (Differance + TimeToConstruct <= new TimeSpan(0, 0, 0))
		{
			Building_Status = Building_Status.Functional;
			return true;
		}
		else
		{
			Building_Status = Building_Status.UnderConstruction;
			return false;
		} 
	}
	public TimeSpan timeNeededToCunstruct()
	{
		return Construction_Starting_Time.Subtract(DateTime.Now) + new TimeSpan(HoursToBuild, MinutesToBuild, SecondsToBuild);                   //?
    }
    
	public void StartConstructing(GroundNode node,GameObject currentbuilding)            //? ليه ما تخلي اسمها nodeSaver او شي ثاني
    {
		Construction_Starting_Time = DateTime.Now;
        currentbuilding.GetComponent<Building_Logic>().Node = node.gameObject.name;
    }
}
