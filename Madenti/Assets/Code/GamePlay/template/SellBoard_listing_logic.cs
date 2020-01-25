using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellBoard_listing_logic : MonoBehaviour {

	public Item Item;

	[SerializeField]
	Slider AmountSlider;

	[SerializeField]
	Text AmountTxt;

	[SerializeField]
	Text ItemName;

	bool init = false;
	public void initlize(Item item)
	{
		this.Item = item;
		AmountSlider.maxValue = item.amount;
		AmountSlider.minValue = 1;
		ItemName.text = item.itemName;
		init = true;
	}

	void LateUpdate()
	{
		if (!init)
			return;
		AmountSlider.maxValue = GameManager.instance.Storage[Item.itemName].amount;
		AmountTxt.text = AmountSlider.value.ToString();

	}


	public void _SellItem()
	{

		Item.amount -= AmountSlider.value;
	}
}
