using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMarketInfo : TowerMarketInfoBase 
{

	public TowerLevel[] levels;


	private TowerAttack towerAttack;
	private TowerUpgradeBase upgradeBonuses;


	public override bool Upgrade() //return value indicates whether the tower can be further upgraded or not
	{
		currentLevel++;
		currentPrice += levels[currentLevel].upgradePrice;
		ApplyLevelParameters();

		if (currentLevel + 1 >= levels.Length)
		{
			return false;
		}
		else
		{
			return true;
		}
	}


	public override int GetUpgradePrice()
	{
		if (currentLevel + 1 >= levels.Length)
		{
			return 0;
		}
		else
		{
			return levels[currentLevel + 1].upgradePrice - upgradeBonuses.upgradePriceDecrease;
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
			currentDamage = levels[currentLevel].towerDamage + upgradeBonuses.additionalDamage;
			currentAttackSpeed = levels[currentLevel].attackSpeed + upgradeBonuses.additionalAttackSpeed;
			currentAttackRange = levels[currentLevel].towerRange + upgradeBonuses.additionalRange;

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
			armorPiercing = upgradeBonuses.armorPiercing;
		}
		towerAttack.UpdateAttackParameters(currentDamage, armorPiercing, currentAttackSpeed, currentAttackRange);
	}


	void Awake()
	{
		towerAttack = gameObject.GetComponent<TowerAttack>();
		upgradeBonuses = (TowerUpgradeBase)SaveLoad.Load(towerName);
	}
}
