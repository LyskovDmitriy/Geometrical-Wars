using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenuTowerButton : MonoBehaviour 
{

	public TowerType type;


	private UpgradeMenuTowerInfo info;


	public void ChangeType () 
	{
		info.ChangeType(type);
	}

	
	void Awake () 
	{
		info = FindObjectOfType<UpgradeMenuTowerInfo>();
	}
}
