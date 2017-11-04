using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GMCDataScript : MonoBehaviour {

	public StockItem itemData;
	public Image supplyFill;
	public Image itemImage;
	public Text itemCost;
	public Text itemSellingPrice;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpSellingPrice()
	{
		for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
		{
			if(itemData.myItem == GMCShopScript.instance.myStocks[i].myItem)
			{

				itemData.sellingPrice += 10;

				if(itemData.sellingPrice > (GMCShopScript.instance.myStocks[i].cost * 2))
				{
					itemData.sellingPrice = GMCShopScript.instance.myStocks[i].cost * 2;
				}

				itemSellingPrice.text = itemData.sellingPrice.ToString();
				GMCShopScript.instance.myStocks[i] = itemData;
			}
		}
	}

	public void DownSellingPrice()
	{
		for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
		{
			if(itemData.myItem == GMCShopScript.instance.myStocks[i].myItem)
			{

				itemData.sellingPrice -= 10;

				if(itemData.sellingPrice < (GMCShopScript.instance.myStocks[i].cost / 2))
				{
					itemData.sellingPrice = GMCShopScript.instance.myStocks[i].cost / 2;
				}

				itemSellingPrice.text = itemData.sellingPrice.ToString();
				GMCShopScript.instance.myStocks[i] = itemData;
			}
		}
	}
}
