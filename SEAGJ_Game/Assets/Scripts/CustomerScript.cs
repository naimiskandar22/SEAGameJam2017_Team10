using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour {

	public StockItem groceryList;

	public int needList;

	public int budget;
	public int satisfaction;
	bool demanded = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateGroceryList()
	{
		GMCShopScript.instance.totalCustomers++;

		//int rand = Random.Range(0, (int)Items.TOTAL);
		int rand = Random.Range(0, 2);

		StockItem item = GMCShopScript.instance.myStocks[rand];

		groceryList = item;

		rand = Random.Range(1, DayOperationManagerScript.instance.currDay) + Random.Range(GMCShopScript.instance.reputation, 11);

		needList = rand;

		if(needList <= 0)
		{
			DayOperationManagerScript.instance.DeleteCustomers(this);
			return;
		}

		int num = Random.Range(1, 3);

		budget = 100 * num;

		CheckPurchase();
	}

	public void CheckPurchase()
	{
		bool firstDemand = false;
		bool done = true;
		int tempList = 0;

		int rand = Random.Range(0, 10);


		int temp = 0;

		if(rand <= needList)
		{
			for(int i = 0; i < GMCShopScript.instance.myStocks.Count; i++)
			{
				if(GMCShopScript.instance.myStocks[i].myItem == groceryList.myItem)
				{
					temp = i;
					Debug.Log(temp);
				}
			}

			StockItem item = GMCShopScript.instance.myStocks[temp];

			firstDemand = !demanded;
			if(!demanded) item.demand++;

			demanded = true;

			GMCShopScript.instance.myStocks[temp] = item;

			if(budget >= groceryList.sellingPrice)
			{
				if(GMCShopScript.instance.myStocks[temp].stock > 0)
				{
					budget -= groceryList.sellingPrice;

					Items type = groceryList.myItem;

					for(int j = 0; j < GMCShopScript.instance.myStocks.Count; j++)
					{
						if(GMCShopScript.instance.myStocks[j].myItem == type)
						{
							item = GMCShopScript.instance.myStocks[j];

							item.stock--;
							item.demand--;

							if(firstDemand) GMCShopScript.instance.totalSatisfaction += 10;
							else GMCShopScript.instance.totalSatisfaction += satisfaction;

							GMCShopScript.instance.myStocks[j] = item;

							GMCShopScript.instance.myMoney += groceryList.sellingPrice;
							GMCShopScript.instance.UpdateMoney();

							for(int k = 0; k < GMCShopScript.instance.weeklyLogList.Count; k++)
							{
								if(GMCShopScript.instance.weeklyLogList[k].stockItems == groceryList.myItem)
								{
									GMCShopScript.instance.weeklyLogList[k].currSale++;
								}
							}
						}
					}

				}
				else
				{
					tempList = groceryList.sellingPrice;
					done = false;
				}
			}
			else
			{
				DayOperationManagerScript.instance.DeleteCustomers(this);
				GMCShopScript.instance.UpdateRep();
				GMCShopScript.instance.UpdateGMCUI();
				return;
			}
		}
		else
		{
			DayOperationManagerScript.instance.DeleteCustomers(this);
			GMCShopScript.instance.UpdateRep();
			GMCShopScript.instance.UpdateGMCUI();
			return;
		}

		if(!done)
		{
			if(budget >= tempList)
			{
				done = false;

				GMCShopScript.instance.totalSatisfaction -= satisfaction;

				satisfaction--;

				GMCShopScript.instance.totalSatisfaction += satisfaction;
				GMCShopScript.instance.UpdateRep();

			}
		}

		if(satisfaction <= 0)
		{
			done = true;
		}

		if(done)
		{
			DayOperationManagerScript.instance.DeleteCustomers(this);

			GMCShopScript.instance.UpdateRep();
			GMCShopScript.instance.UpdateGMCUI();
		}




	}
}
