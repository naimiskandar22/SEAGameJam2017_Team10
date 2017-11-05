using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMCWeeklyLogScript : MonoBehaviour {

	public Items stockItems;

	public Text stockBoughtText;
	public Text stockSoldText;
	public Text stockSales;

	public Image salesIncline;
	public Sprite[] arrows;

	public int previousSale;
	public int currSale;
	public int currStock;

	public bool isUpdated = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateWeeklyLog()
	{
		if(!isUpdated)
		{
			float sale = currSale / previousSale;

			if(sale >= 1)
			{
				salesIncline.sprite = arrows[0];
			}
			else
			{
				salesIncline.sprite = arrows[1];
			}

			stockSales.text = sale.ToString("F");

			stockBoughtText.text = currStock.ToString();
			stockSoldText.text = currSale.ToString();

			previousSale = currSale;
			currSale = 0;
			currStock = 0;

			isUpdated = true;
		}
	}
}
