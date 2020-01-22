using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// هذا باختصار راح يحافظ على عنصر اللاعب بحيث 
/// لو بغينا قيم معينه تكون محفوظه مهما كان السين 
/// شغال 
/// </summary>
public class PlayerManager : MonoBehaviour {


	//هذا لو بغينا نستدعيها فاوبجيكتس مختلفه 
	//مثلا لو بغينا نستدعي كم حديد موجود فنستخدم العنصر هذا
	public static PlayerManager instance;


	// Use this for initialization
	void Start () {
		//كذا راح يحفظ العنصر هذا فماراح يكون فيه الا اوبجكت واحد للكلاس هذا دائما
		instance = this;

		DontDestroyOnLoad(this);
	}
}
