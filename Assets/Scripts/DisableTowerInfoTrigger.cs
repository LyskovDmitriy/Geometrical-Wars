using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTowerInfoTrigger : MonoBehaviour 
{
	void OnMouseDown()
	{
		if (PlaceForTower.towerWithActiveInfo != null)
		{
			PlaceForTower.towerWithActiveInfo = null;
		}
	}
}
