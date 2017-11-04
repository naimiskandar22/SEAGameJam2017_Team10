using System.Collections;
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

		timeCheck += Time.deltaTime * 10;

		if(timeCheck >= 60.0f)
		{
			CreateCustomers(1);
			timeCheck = 0f;
			currTime++;

			if(currTime >= 24)
			{
				currTime = 0;

				currDay++;

				if(currDay >= 7)
				{
					currDay = 0;
				}
			}

			UpdateDay();
		}
	}

	void UpdateDay()
	{
		timeDisplay.text = currTime + " : 00";

		calendarDisplay.sprite = calendarImage[currDay];
	}

	public void CreateCustomers(int createNum)
	{
		if(idleCustomers.Count == 0)
		{
			GameObject newCustomer = Instantiate(customer,Vector3.zero,Quaternion.identity);
			idleCustomers.Add(newCustomer);
			customerScripts.Add(newCustomer.GetComponent<CustomerScript>());
			customerScripts[0].CreateGroceryList();
		}

		idleCustomers[0].SetActive(true);
		activeCustomers.Add(idleCustomers[0]);
		idleCustomers.RemoveAt(0);
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

	public void WeeklyLog()
	{
		
	}
}
