using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForTower : MonoBehaviour 
{

	public static Transform towerWithActiveInfo; //position of the last tower with activated info


	public GameObject availableSprite;
	public TowerAttack thisTower;
	public bool isOccupied;
	public bool showInfoBelow;


	private bool infoIsActive;
	private BuyingSystem shop;
	private TowerInfo towerInfo;


	public void DeleteTower()
	{
		isOccupied = false;
		if (infoIsActive)
		{
			DeactivateInfo();
		}
		towerInfo = null;
		Destroy(thisTower.parentObject);
	}


	public void ChangeTowerTransparency(bool isTransparent)
	{
		SpriteRenderer renderer = thisTower.GetComponent<TowerAttack>().GetComponent<SpriteRenderer>();
		Color newColor = renderer.color;
		if (isTransparent)
		{
			newColor.a = 1;
		}
		else
		{
			newColor.a = 0.5f;
		}
		renderer.color = newColor;
	}


	public void ConfirmTowerPlacement()
	{
		ChangeTowerTransparency(true);
		thisTower.ShowAttackRange(false);
		thisTower.isActive = true;
		towerInfo = thisTower.transform.parent.GetComponentInChildren<TowerInfo>();
		towerInfo.towerHolder = this;
		shop.ConfirmPurchase(thisTower.transform.position + new Vector3(0.0f, 1.0f, 0.0f));
		PlacesForTowerManager.currentlyUsedPlace = null;
	}


	void ActivateInfo()
	{
		infoIsActive = true;
		towerInfo.ShowInfo(true);
		towerInfo.ShowBelowTower(showInfoBelow);
		thisTower.ShowAttackRange(true);
		towerWithActiveInfo = transform;
	}


	void DeactivateInfo()
	{
		infoIsActive = false;
		towerInfo.ShowInfo(false);
		thisTower.ShowAttackRange(false);
		if (towerWithActiveInfo == transform)
		{
			towerWithActiveInfo = null;
		}
	}


	void OnMouseDown()
	{
		if (isOccupied && thisTower.isActive)
		{
			if (!infoIsActive)
			{
				ActivateInfo();
			}
			else
			{
				DeactivateInfo();
			}
		}
	}


	public void Highlight(bool activate)
	{
		if (isOccupied)
		{
			availableSprite.SetActive(false);
			return;
		}
		availableSprite.SetActive(activate);
	}


	void Awake()
	{
		shop = FindObjectOfType<BuyingSystem>();
	}

	
	void Start () 
	{
		isOccupied = false;
		infoIsActive = false;
	}


	void Update()
	{
		if (infoIsActive && Input.GetButtonDown("Fire2")) //when use "Fire1" it works the second info activates
		{
			DeactivateInfo();
		}
		else if (infoIsActive && towerWithActiveInfo != transform) //when the other tower's info is activated
		{
			DeactivateInfo();
		}
	}
}
