using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Items
{
	BLUE,
	RED,
	GREEN,
	YELLOW,
	TOTAL,
}

[System.Serializable]
public struct StockItem
{
	public Items myItem;
	public Sprite myImage;
	public int stock;
	public int demand;
	public int cost;
	public int sellingPrice;
}

public class GMCShopScript : MonoBehaviour {

	public static GMCShopScript instance;

	void Awake()
	{
		if(instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
		}
	}

	public int myMoney;
	public float currTime;
	public List<StockItem> myStocks;
	public GameObject GMCUi;
	public int reputation = 1;

	public Text moneyDisplay;
	public List<GMCDataScript> itemList;


	// Use this for initialization
	void Start () {
		UpdateMoney();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateMoney()
	{
		moneyDisplay.text = myMoney.ToString();
	}

	public void UpdateGMCUI()
	{
		for(int i = 0; i < itemList.Count; i++)
		{
			itemList[i].itemData = myStocks[i];

			itemList[i].itemImage.sprite = myStocks[i].myImage;
			itemList[i].itemCost.text = myStocks[i].cost.ToString();
			itemList[i].itemSellingPrice.text = myStocks[i].sellingPrice.ToString();

			float fillAmount = (myStocks[i].stock  * 1.0f) / ((myStocks[i].stock + myStocks[i].demand) * 1.0f);

			itemList[i].supplyFill.fillAmount = fillAmount;
		}
	}

	public void OpenGMCUI()
	{
		if(GMCUi.activeSelf == false)
		{
			GMCUi.SetActive(true);

			UpdateGMCUI();
		}
	}

	public void CloseGMCUI()
	{
		GMCUi.SetActive(false);

		for(int i = 0; i < DayOperationManagerScript.instance.customerScripts.Count; i++)
		{
			DayOperationManagerScript.instance.customerScripts[i].CheckPurchase();
		}
	}
}
