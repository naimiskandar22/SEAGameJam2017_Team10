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

	void OnMouseDown()
	{
		if(!EventSystem.current.IsPointerOverGameObject())
		{
			Debug.Log("Click");

			if(CanvasManagerScript.instance.shopScript.shopCanvas.enabled == false)
			{
				CanvasManagerScript.instance.shopScript.shopCanvas.enabled = true;
			}

			CanvasManagerScript.instance.shopScript.currShop = this;

			CanvasManagerScript.instance.shopScript.itemImage.sprite = myStocks[0].myImage;
			CanvasManagerScript.instance.shopScript.currStock.text = myStocks[0].stock.ToString();
			CanvasManagerScript.instance.shopScript.currCost.text = myStocks[0].cost.ToString();
			CanvasManagerScript.instance.shopScript.currDemand.text = myStocks[0].demand.ToString();
			CanvasManagerScript.instance.shopScript.TotalCost.text = (myStocks[0].demand * myStocks[0].cost).ToString();


			//		else
			//		{
			//			CanvasManagerScript.instance.shopCanvas.enabled = false;
			//		}
		}
	}
}
