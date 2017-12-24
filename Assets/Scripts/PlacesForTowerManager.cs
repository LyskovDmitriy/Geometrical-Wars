using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacesForTowerManager : MonoBehaviour 
{

	public PlaceForTower[] places;
	public float borderToShowInfoBelow;
	public static GameObject towerToBuy;
	public static PlaceForTower currentlyUsedPlace;


	public void HighlightAll (bool activate, GameObject tower = null) 
	{
		towerToBuy = tower;
		foreach (PlaceForTower place in places)
		{
			place.Highlight(activate);
		}
	}


	void Start()
	{
		for (int i = 0; i < places.Length; i++)
		{
			places[i].showInfoBelow = places[i].transform.position.y > borderToShowInfoBelow;
		}
	}
}
