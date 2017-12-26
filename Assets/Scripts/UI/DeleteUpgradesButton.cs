using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUpgradesButton : MonoBehaviour 
{
	public void Delete()
	{
		UpgradeLevel.DeleteTowerUpgrades();
	}
}
