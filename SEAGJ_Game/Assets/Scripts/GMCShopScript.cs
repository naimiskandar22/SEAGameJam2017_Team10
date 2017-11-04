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
	BLACK,
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
	public Canvas GMCStoreCanvas;

	public Text moneyDisplay;


	// Use this for initialization
	void Start () {
		UpdateMoney();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateMoney()
	{
		moneyDisplay.text = myMoney.ToString();
	}
}
