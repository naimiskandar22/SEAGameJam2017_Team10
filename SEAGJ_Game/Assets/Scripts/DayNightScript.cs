using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DayNightScript : MonoBehaviour {

	public List<Sprite> calendarImage;
	public float currTime;
	public int currDay;
	public Image calendarDisplay;
	public Text timeDisplay;

	// Use this for initialization
	void Start () {
		currDay = 0;
		calendarDisplay.sprite = calendarImage[currDay];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void UpdateDay()
	{
		calendarDisplay.sprite = calendarImage[currDay];
	}
}
