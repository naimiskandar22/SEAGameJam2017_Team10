using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvasScript : MonoBehaviour {

	public Canvas shopCanvas;

	public Image itemImage;
	public Text currStock;
	public Text currCost;
	public Text currDemand;
	public Text TotalCost;

	public SupplierShopScript currShop;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void IncrementAmount()
	{
		StockItem item = currShop.myStocks[0];

		item.demand++;

		currShop.myStocks[0] = item;

		currDemand.text = currShop.myStocks[0].demand.ToString();
		TotalCost.text = (currShop.myStocks[0].demand * currShop.myStocks[0].cost).ToString();
	}

	public void DecrementAmount()
	{
		StockItem item = currShop.myStocks[0];

		item.demand--;

		currShop.myStocks[0] = item;

		if(currShop.myStocks[0].demand <= 0)
		{
			item.demand = 0;

			currShop.myStocks[0] = item;
		}

		currDemand.text = currShop.myStocks[0].demand.ToString();
		TotalCost.text = (currShop.myStocks[0].demand * currShop.myStocks[0].cost).ToString();
	}

	public void CheckPurchase()
	{
		if((currShop.myStocks[0].demand * currShop.myStocks[0].cost) <= GMCShopScript.instance.myMoney)
		{
			GMCShopScript.instance.myMoney -= (currShop.myStocks[0].demand * currShop.myStocks[0].cost);

			GMCShopScript.instance.moneyDisplay.text = GMCShopScript.instance.myMoney.ToString();

			bool inList = false;

			StockItem item = currShop.myStocks[0];

			for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
			{
				if(GMCShopScript.instance.myStocks[i].myItem == currShop.myStocks[0].myItem)
				{
					inList = true;

					int demandPurchase = currShop.myStocks[0].demand;

					item = GMCShopScript.instance.myStocks[i];

					item.stock += demandPurchase;

					GMCShopScript.instance.myStocks[i] = item;
				}
			}

			if(!inList)
			{
				item.stock = item.demand;

				GMCShopScript.instance.myStocks.Add(item);
			}

			item.demand = 0;

			currShop.myStocks[0] = item;

			currDemand.text = currShop.myStocks[0].demand.ToString();
		}

	}

	public void CloseShop()
	{
		shopCanvas.enabled = false;

		StockItem item = currShop.myStocks[0];

		item.demand = 0;

		currShop.myStocks[0] = item;

		currShop = null;
	}
}
