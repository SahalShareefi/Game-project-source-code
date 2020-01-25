using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketPost_Logic : MonoBehaviour
{
	[SerializeField]
	GameObject Textfield, icon;

	public string itemID;

	public GameObject initlize(MarketPost thePost)
	{
		this.itemID = thePost.postID;
		Textfield.GetComponent<Text>().text = thePost.postName + " " + thePost.postTotalAmount;
		return gameObject;
	}

	public void __BuyButton()
	{

	}
}
