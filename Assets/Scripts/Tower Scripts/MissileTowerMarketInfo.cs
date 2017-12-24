using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTowerMarketInfo : TowerMarketInfoBase
{

	public float currentExplosionRange;
	public MissileTowerLevel[] missileTowerLevels;


	private MissileTowerAttack missileTowerAttack;
	private IceTowerUpgrade upgradeBonuses;


	public override bool Upgrade() //return value indicates whether the tower can be further upgraded or not
	{
		currentLevel++;
		currentPrice += missileTowerLevels[currentLevel].upgradePrice;
		ApplyLevelParameters();

		if (currentLevel + 1 >= missileTowerLevels.Length)
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
			currentDamage = missileTowerLevels[currentLevel].towerDamage + upgradeBonuses.additionalDamage;
			currentAttackSpeed = missileTowerLevels[currentLevel].attackSpeed + upgradeBonuses.additionalAttackSpeed;
			currentAttackRange = missileTowerLevels[currentLevel].towerRange + upgradeBonuses.additionalRange;
			currentExplosionRange = missileTowerLevels[currentLevel].explosionRange + upgradeBonuses.additionalExplosionRange;

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
			armorPiercing = upgradeBonuses.armorPiercing;
		}
		missileTowerAttack.UpdateAttackParameters(currentDamage, armorPiercing, currentAttackSpeed, currentAttackRange, currentExplosionRange);
	}


	public override int GetUpgradePrice()
	{
		if (currentLevel + 1 >= missileTowerLevels.Length)
		{
			return 0;
		}
		else
		{
			return missileTowerLevels[currentLevel + 1].upgradePrice - upgradeBonuses.upgradePriceDecrease;
		}
	}


	void Awake()
	{
		missileTowerAttack = gameObject.GetComponent<MissileTowerAttack>();
		upgradeBonuses = SaveLoad.Load(towerName);
	}
}
