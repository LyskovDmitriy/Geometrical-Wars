using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerMarketInfoBase : MonoBehaviour
{

	public string towerName;
	public float currentAttackSpeed;
	public float currentAttackRange;
	public int currentDamage;
	public int armorPiercing; //TODO change enemyhealthmanager to make it useful


	[SerializeField]
	protected int currentPrice;
	protected string startingName; //name that won't be affected by upgrades
	protected int currentLevel;


	public abstract bool Upgrade(); //return value indicates whether the tower can be further upgraded or not
	public abstract int GetUpgradePrice();
	public abstract int GetPrice();
	public abstract void ApplyLevelParameters();



	public void RollBack()
	{
		currentLevel = -1;
		startingName = towerName;
		ApplyLevelParameters();
	}


	void Start () 
	{
		currentLevel = -1;
		startingName = towerName;
		ApplyLevelParameters();
	}
}
