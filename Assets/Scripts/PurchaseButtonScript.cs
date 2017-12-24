using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseButtonScript : MonoBehaviour 
{
	
	public TowerType tower;


	private BuyingSystem shop;


	void Awake()
	{
		shop = FindObjectOfType<BuyingSystem>();
	}


	public void PurchaseTower()
	{
		shop.Purchase(tower);
	}
}
