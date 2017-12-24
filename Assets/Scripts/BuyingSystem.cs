using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuyingSystem : MonoBehaviour 
{

	public PriceList[] prices;


	private GoldManager goldManager;
	private PlacesForTowerManager placesManager;
	private int currentTowerPrice;
	private bool isHighlighted;


	public void Purchase(TowerType towerToBuy)
	{
		if (isHighlighted)
		{
			if (PlacesForTowerManager.currentlyUsedPlace != null)
			{
				PlacesForTowerManager.currentlyUsedPlace.DeleteTower();
			}
			placesManager.HighlightAll(false);
			isHighlighted = false;
			currentTowerPrice = 0;
		}
		else
		{
			foreach (PriceList tower in prices)
			{
				if (towerToBuy == tower.towerType) //find data about the tower
				{
					if (goldManager.canBuy(tower.price))
					{
						currentTowerPrice = tower.price;
						placesManager.HighlightAll(true, tower.towerPrefab);
						isHighlighted = true;
					}
					else
					{
						return;
					}
				}
			}
		}
	}


	public void ConfirmPurchase(Vector3 positionToSpawnGold)
	{
		goldManager.ChangeGold(-currentTowerPrice, positionToSpawnGold);
		placesManager.HighlightAll(false);
		isHighlighted = false;
	}

	
	void Awake () 
	{
		goldManager = FindObjectOfType<GoldManager>();
		placesManager = FindObjectOfType<PlacesForTowerManager>();
	}


	void Start()
	{
		for (int i = 0; i < prices.Length; i++)
		{
			TowerMarketInfoBase tower = Instantiate(prices[i].towerPrefab, new Vector3(-100.0f, 0.0f, 0.0f), Quaternion.identity).GetComponentInChildren<TowerMarketInfoBase>();
			prices[i].price = tower.GetPrice();
			prices[i].priceText.text = prices[i].price.ToString();
			Destroy(tower.gameObject);
		}
		isHighlighted = false;
		currentTowerPrice = 0;
	}


	void Update()
	{
		if (isHighlighted && Input.GetButton("Fire2"))
		{
			Purchase(TowerType.None);
		}
	}
}
