using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class IceTowerMarketInfo : TowerMarketInfoBase  
{

	public float currentExplosionRange;
	public float currentFreezingRate;
	public float currentFreezingTime;
	public IceTowerLevel[] iceTowerLevels;


	private IceTowerAttack iceTowerAttack;
	private IceTowerUpgrade upgradeBonuses;


	public override bool Upgrade() //return value indicates whether the tower can be further upgraded or not
	{
		currentLevel++;
		currentPrice += iceTowerLevels[currentLevel].upgradePrice;
		ApplyLevelParameters();

		if (currentLevel + 1 >= iceTowerLevels.Length)
		{
			return false;
		}
		else
		{
			return true;
		}
	}


	public override int GetPrice()
	{
		return currentPrice - upgradeBonuses.basePriceDecrease;
	}


	public override void ApplyLevelParameters()
	{
		if (currentLevel != -1)
		{
			currentDamage = iceTowerLevels[currentLevel].towerDamage + upgradeBonuses.additionalDamage;
			currentAttackSpeed = iceTowerLevels[currentLevel].attackSpeed + upgradeBonuses.additionalAttackSpeed;
			currentAttackRange = iceTowerLevels[currentLevel].towerRange + upgradeBonuses.additionalRange;
			currentExplosionRange = iceTowerLevels[currentLevel].explosionRange + upgradeBonuses.additionalExplosionRange;
			currentFreezingRate = iceTowerLevels[currentLevel].freezingRate - upgradeBonuses.additionalFreezingRate;
			currentFreezingTime = (float)Math.Round(iceTowerLevels[currentLevel].freezingTime + upgradeBonuses.additionalFreezingTime, 2);

			switch (currentLevel)
			{
				case 0:
					towerName = startingName + "+";
					break;
				case 1:
					towerName = startingName + "++";
					break;
				case 2:
					towerName = startingName + "#";
					break;
			}
		}
		else
		{
			currentDamage += upgradeBonuses.additionalDamage;
			currentAttackSpeed += upgradeBonuses.additionalAttackSpeed;
			currentAttackRange += upgradeBonuses.additionalRange;
			currentExplosionRange += upgradeBonuses.additionalExplosionRange;
			currentFreezingRate = (float)Math.Round(currentFreezingRate - upgradeBonuses.additionalFreezingRate, 2);
			currentFreezingTime += upgradeBonuses.additionalFreezingTime;
			armorPiercing = upgradeBonuses.armorPiercing;
		}
		iceTowerAttack.UpdateAttackParameters(currentDamage, armorPiercing, currentAttackSpeed, currentAttackRange, currentExplosionRange, currentFreezingRate, currentFreezingTime);
	}


	public override int GetUpgradePrice()
	{
		if (currentLevel + 1 >= iceTowerLevels.Length)
		{
			return 0;
		}
		else
		{
			return iceTowerLevels[currentLevel + 1].upgradePrice - upgradeBonuses.upgradePriceDecrease;
		}
	}


	void Awake()
	{
		iceTowerAttack = gameObject.GetComponent<IceTowerAttack>();
		upgradeBonuses = SaveLoad.Load(towerName);
	}
}
