using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenCloseButtonScript : MonoBehaviour 
{

	public bool isOpen;
	public Sprite openSprite;
	public Sprite closeSprite;
	public GameObject[] buttons;


	private Image buttonImage;


	public void ChangeStatus()
	{
		isOpen = !isOpen;

		foreach (GameObject button in buttons)
		{
			button.SetActive(isOpen);
		}

		if (isOpen)
		{
			buttonImage.sprite = closeSprite;
		}
		else
		{
			buttonImage.sprite = openSprite;
		}
	}


	void Awake()
	{
		buttonImage = GetComponent<Image>();
	}
}
