using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCanvasScript : MonoBehaviour {

	public GameObject shopUI;
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

        if(TutorialManager.instance)
        {
            TutorialManager.instance.Added();
        }
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

	public void BuyItems()
	{
		currShop.CheckPurchase(this);
        if(TutorialManager.instance)
        {
            TutorialManager.instance.Bought();
        }
    }

	public void CloseShop()
	{
		currShop.CloseShop();

		currShop = null;

		CanvasManagerScript.instance.shopOpened = false;

		CanvasManagerScript.instance.shopScript.shopUI.SetActive(false);

        if(TutorialManager.instance)
        {
            TutorialManager.instance.Closed();
        }
    }
}
