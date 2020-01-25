using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketPost {

	public string postID;
	public string postName;
	public float postTotalAmount;
	public float postPriceEach;


	public MarketPost (string postID,string postName,float postTotalAmount)
	{
		this.postID = postID;
		this.postName = postName;
		this.postTotalAmount = postTotalAmount;
	}
}
