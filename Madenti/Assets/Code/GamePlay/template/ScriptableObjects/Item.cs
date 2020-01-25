using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// الكلاس هذا راح يكون النموذج لجميع الاغراض فاللعبه 
/// راح يكون له درجه الجوده
/// واسم للغرض 
/// والكميه 
/// </summary>
[CreateAssetMenu(fileName ="New Item",menuName ="new Item")]
public class Item : ScriptableObject {

	/// <summary>
	/// اسم الغرض
	/// </summary>
	public string itemName;


	/// <summary>
	/// جوده الغرض
	/// </summary>
	public Quality quality;

	public float requirement;

	/// <summary>
	///كميه الغرض 
	/// </summary>
	public float amount;
}
