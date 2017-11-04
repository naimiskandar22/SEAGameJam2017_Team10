using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerScript : MonoBehaviour {

	public List<StockItem> groceryList;

	public List<int> needList = new List<int>();
	public List<int> wantList = new List<int>();

	public int budget;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateGroceryList()
	{
		for(int i = 0; i < GMCShopScript.instance.reputation; i++)
		{
			//int rand = Random.Range(0, (int)Items.TOTAL);
			int rand = 0;
			StockItem item = GMCShopScript.instance.myStocks[rand];

			groceryList.Add(item);

			//rand = Random.Range(0, DayOperationManagerScript.instance.currDay);
			rand = 7;

			needList.Add(rand);
		}

		int num = Random.Range(1, 3);

		budget = 100 * num;

		CheckPurchase();
	}

	public void CheckPurchase()
	{
		bool done = true;
		List<int> tempList = new List<int>();

		for(int i = 0; i < groceryList.Count; i++)
		{
			int rand = Random.Range(0, 10);


			int temp = 0;

			if(rand <= needList[i])
			{
				for(int j = 0; j < GMCShopScript.instance.myStocks.Count; j++)
				{
					if(GMCShopScript.instance.myStocks[j].myItem == groceryList[i].myItem)
					{
						temp = j;
						Debug.Log(temp);
					}
				}

				StockItem item = GMCShopScript.instance.myStocks[temp];

				item.demand++;

				GMCShopScript.instance.myStocks[temp] = item;

				if(budget >= groceryList[i].sellingPrice)
				{
					if(GMCShopScript.instance.myStocks[temp].stock > 0)
					{
						budget -= groceryList[i].sellingPrice;

						Items type = groceryList[i].myItem;

						for(int j = 0; j < GMCShopScript.instance.myStocks.Count; j++)
						{
							if(GMCShopScript.instance.myStocks[j].myItem == type)
							{
								item = GMCShopScript.instance.myStocks[j];

								item.stock--;
								item.demand--;

								GMCShopScript.instance.myStocks[j] = item;

								GMCShopScript.instance.myMoney += groceryList[i].sellingPrice;
								GMCShopScript.instance.UpdateMoney();
								Debug.Log("Bought");
							}
						}

						groceryList.RemoveAt(i);

					}
					else
					{
						tempList.Add(groceryList[i].sellingPrice);

					}
				}

			}
		}

		for(int i = 0; i < tempList.Count; i++)
		{
			if(budget >= tempList[i])
			{
				done = false;
			}
		}

		if(done)
		{
			DayOperationManagerScript.instance.DeleteCustomers(this);
		}

	}
}
