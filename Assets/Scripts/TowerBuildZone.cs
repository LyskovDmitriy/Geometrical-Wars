using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuildZone : MonoBehaviour 
{

	public PlaceForTower place;


	void OnMouseEnter()
	{
		if (!place.isOccupied && PlacesForTowerManager.towerToBuy != null)
		{
			TowerAttack tower = Instantiate(PlacesForTowerManager.towerToBuy, transform.position, Quaternion.identity).GetComponentInChildren<TowerAttack>();
			tower.parentObject.transform.SetParent(place.transform);
			tower.ShowAttackRange(true);
			tower.isActive = false;
			place.thisTower = tower;
			place.ChangeTowerTransparency(false);
			place.isOccupied = true;
			PlacesForTowerManager.currentlyUsedPlace = place;
		}
	}


	void OnMouseExit()
	{
		if (place.isOccupied && !place.thisTower.isActive)
		{
			place.DeleteTower();
			PlacesForTowerManager.currentlyUsedPlace = null;
		}
	}

	 
	void OnMouseDown()
	{
		place.ConfirmTowerPlacement();
	}
}
