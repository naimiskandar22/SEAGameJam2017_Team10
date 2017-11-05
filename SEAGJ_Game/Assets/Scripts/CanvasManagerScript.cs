using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManagerScript : MonoBehaviour {

	public static CanvasManagerScript instance;

	public ShopCanvasScript shopScript;
	public bool shopOpened = false;

    [SerializeField]
    Text nameText;
    [SerializeField]
    Image genderImage;
    [SerializeField]
    Sprite maleSprite;
    [SerializeField]
    Sprite femaleSprite;

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
        nameText.text = LocalDataManager.instance.GetName();
        genderImage.sprite = LocalDataManager.instance.GetGender() == 0 ? maleSprite : femaleSprite;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
