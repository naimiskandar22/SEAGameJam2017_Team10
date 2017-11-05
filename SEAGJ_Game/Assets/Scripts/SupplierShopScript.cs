using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SupplierShopScript : MonoBehaviour {

	public List<StockItem> myStocks;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenShop()
	{

		if(!CanvasManagerScript.instance.shopOpened)
		{
			UpdateStat();

			CanvasManagerScript.instance.shopScript.shopUI.SetActive(true);
			CanvasManagerScript.instance.shopScript.currShop = this;
			CanvasManagerScript.instance.shopOpened = true;

			CanvasManagerScript.instance.shopScript.itemImage.sprite = myStocks[0].myImage;
			CanvasManagerScript.instance.shopScript.currStock.text = myStocks[0].stock.ToString();
			CanvasManagerScript.instance.shopScript.currCost.text = myStocks[0].cost.ToString();
			CanvasManagerScript.instance.shopScript.currDemand.text = myStocks[0].demand.ToString();
			CanvasManagerScript.instance.shopScript.TotalCost.text = (myStocks[0].demand * myStocks[0].cost).ToString();
		}

        if(TutorialManager.instance)
        {
            TutorialManager.instance.ShopClicked();
        }
	}

	public void CloseShop()
	{
		StockItem item = myStocks[0];

		item.demand = 0;

		myStocks[0] = item;
	}

	public void UpdateStat()
	{
		for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
		{
			if(myStocks[0].myItem == GMCShopScript.instance.myStocks[i].myItem)
			{
				StockItem item = GMCShopScript.instance.myStocks[i];

				item.demand = 0;

				myStocks[0] = item;
			}
		}
	}

	public void CheckPurchase(ShopCanvasScript shop)
	{
		if((myStocks[0].demand * myStocks[0].cost) <= GMCShopScript.instance.myMoney)
		{
			GMCShopScript.instance.myMoney -= (myStocks[0].demand * myStocks[0].cost);

			GMCShopScript.instance.moneyDisplay.text = GMCShopScript.instance.myMoney.ToString();

			bool inList = false;

			StockItem item = myStocks[0];

			for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
			{
				if(GMCShopScript.instance.myStocks[i].myItem == myStocks[0].myItem)
				{
					inList = true;

					int demandPurchase = myStocks[0].demand;

					item = GMCShopScript.instance.myStocks[i];

					item.stock += demandPurchase;

					GMCShopScript.instance.myStocks[i] = item;

					for(int j = 0; j < GMCShopScript.instance.weeklyLogList.Count; j++)
					{
						if(myStocks[0].myItem == GMCShopScript.instance.weeklyLogList[j].stockItems)
						{
							GMCShopScript.instance.weeklyLogList[j].currStock += demandPurchase;
						}
					}
				}
			}

			if(!inList)
			{
				item.stock = item.demand;

				GMCShopScript.instance.myStocks.Add(item);
			}

			item.demand = 0;

			shop.currShop.myStocks[0] = item;

			shop.currDemand.text = shop.currShop.myStocks[0].demand.ToString();
		}

	}

}
