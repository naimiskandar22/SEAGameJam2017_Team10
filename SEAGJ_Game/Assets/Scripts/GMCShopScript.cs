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
	public float lifespan;

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
	public int totalSatisfaction;
	public int totalCustomers;

	public Text reputationStatus;
	public List<GMCWeeklyLogScript> weeklyLogList;
	public GameObject weeklyLogGO;
	public GameObject GameOverGO;
	public Text WinLoseText;


	// Use this for initialization
	void Start () {
        reputation = PlayerPrefs.GetInt(LocalDataManager.saveDataReputationKey,1);
        myMoney = PlayerPrefs.GetInt(LocalDataManager.saveDataCashKey,1000);
        UpdateMoney();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateRep()
	{
		float average = totalSatisfaction / totalCustomers;

		reputation = (int)average;

		if(reputation < 0)
		{
			reputation = 0;
		}

		reputationStatus.text = reputation.ToString();
        PlayerPrefs.SetInt(LocalDataManager.saveDataReputationKey,reputation);
    }

	public void ItemExpire()
	{
		for(int i = 0; i < myStocks.Count; i++)
		{
			StockItem item = myStocks[i];

			item.stock = (int)(item.stock * myStocks[i].lifespan);

			myStocks[i] = item; 
		}
	}

	public void UpdateMoney()
	{
		moneyDisplay.text = myMoney.ToString();
        PlayerPrefs.SetInt(LocalDataManager.saveDataCashKey,myMoney);
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
            if(TutorialManager.instance)
            {
                TutorialManager.instance.OpenedGMC();
            }
		}
	}

	public void CloseGMCUI()
	{
		weeklyLogGO.SetActive(false);
		GMCUi.SetActive(false);

		for(int i = 0; i < DayOperationManagerScript.instance.customerScripts.Count; i++)
		{
			DayOperationManagerScript.instance.customerScripts[i].CheckPurchase();
		}
	}

	public void OpenWeeklyLog()
	{
		if(!weeklyLogGO.activeSelf)
		{
			weeklyLogGO.SetActive(true);

			for(int i = 0; i < weeklyLogList.Count; i++)
			{
				weeklyLogList[i].UpdateWeeklyLog();
			}
            if(TutorialManager.instance)
            {
                TutorialManager.instance.DailyCheck();
            }
		}
		else
		{
			weeklyLogGO.SetActive(false);
		}
	}

	public void WinLoseCondition()
	{
		bool EndGame = false;
		bool Win = false;

		if(myMoney >= 10000)
		{
			EndGame = true;
			Win = true;
		}

		if(reputation <= 1)
		{
			EndGame = true;
			Win = false;
		}

		if(EndGame)
		{
			Time.timeScale = 0f;
			GameOverGO.SetActive(true);

			if(Win)
			{
				WinLoseText.text = "The Cun Cun App was a success!";
			}
			else
			{
				WinLoseText.text = "The Cun Cun App didn't make it";
			}
		}
	}
}
