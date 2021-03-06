﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayOperationManagerScript : MonoBehaviour {

	public List<Sprite> calendarImage;
	public float currTime;
	public float timeCheck;
	public int currDay;
	public Image calendarDisplay;
	public Text timeDisplay;
	bool isPaused = false;

	public GameObject customer;
	public List<CustomerScript> customerScripts;
	public List<GameObject> activeCustomers = new List<GameObject>();
	public List<GameObject> idleCustomers = new List<GameObject>();

	public static DayOperationManagerScript instance;

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

	// Use this for initialization
	void Start () {
		UpdateDay();
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(!isPaused) timeCheck += Time.deltaTime * 10;

		UpdateDay();

		if(timeCheck >= 60.0f)
		{
			timeCheck = 0f;
			currTime++;

			int count = Random.Range(1, currDay) + Random.Range(0, GMCShopScript.instance.reputation);
			Debug.Log(count);

			for(int i = 0; i < customerScripts.Count; i++)
			{
				customerScripts[i].CheckPurchase();
			}

			CreateCustomers(count);


			if(currTime >= 24)
			{
				GMCShopScript.instance.WinLoseCondition();
				GMCShopScript.instance.ItemExpire();
				GMCShopScript.instance.UpdateGMCUI();

				for(int i = 0; i < GMCShopScript.instance.weeklyLogList.Count; i++)
				{
					GMCShopScript.instance.weeklyLogList[i].isUpdated = false;
					GMCShopScript.instance.weeklyLogList[i].UpdateWeeklyLog();
				}

				currTime = 0;

				currDay++;

				if(currDay >= 7)
				{
					for(int i = customerScripts.Count/2; i < customerScripts.Count; i++)
					{
						DeleteCustomers(customerScripts[i]);
					}

					currDay = 0;
				}
			}
		}
	}

	void UpdateDay()
	{
		timeDisplay.text = currTime.ToString() + " : " + (timeCheck>=10?((int)timeCheck).ToString():"0"+((int)timeCheck).ToString());

		calendarDisplay.sprite = calendarImage[currDay];
	}

	public void CreateCustomers(int createNum)
	{
		for(int i = 0; i < createNum; i++)
		{
			if(idleCustomers.Count == 0)
			{
				GameObject newCustomer = Instantiate(customer,Vector3.zero,Quaternion.identity);
				idleCustomers.Add(newCustomer);
				customerScripts.Add(newCustomer.GetComponent<CustomerScript>());
				customerScripts[0].satisfaction = Random.Range(GMCShopScript.instance.reputation, 11);

				if(GMCShopScript.instance.reputation * 2 > 10)
				{
					customerScripts[0].satisfaction = 10;
				}
				customerScripts[0].CreateGroceryList();
			}

			idleCustomers[0].SetActive(true);
			activeCustomers.Add(idleCustomers[0]);
			idleCustomers.RemoveAt(0);
		}
	}

	public void DeleteCustomers(CustomerScript script)
	{
		customerScripts.Remove(script);

		if(activeCustomers.Count != 0)
		{
			activeCustomers[0].SetActive(false);
			idleCustomers.Add(activeCustomers[0]);
			activeCustomers.RemoveAt(0);
		}
	}

	public void Play()
	{
		isPaused = false;

		Time.timeScale = 1.0f;
	}

	public void Pause()
	{
		isPaused = true;

		Time.timeScale = 0.0f;
	}

	public void SpeedUp()
	{
		Time.timeScale = 2.0f;
	}
}
