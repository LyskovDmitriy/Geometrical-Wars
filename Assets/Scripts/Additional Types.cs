using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//for waves manager
[System.Serializable]
public struct EnemyGroup
{
	public GameObject enemyType;
	public int numberOfEnemies;
	public int trailNumber;
}

//for waves manager
[System.Serializable]
public struct EnemyWave
{
	public EnemyGroup[] enemyGroups;
	public float delayBetweenGroups;
	public int waveCompletedBonus;
}
	
//for buying system
[System.Serializable]
public enum TowerType {None, MachineGun, Cannon, IceTower, FireTower};

//for buying system
[System.Serializable]
public struct PriceList
{
	public TowerType towerType;
	public GameObject towerPrefab;
	public int price;
	public Text priceText;
}

//for tower market info
[System.Serializable]
public struct TowerLevel
{
	public int upgradePrice;
	public float attackSpeed;
	public int towerDamage;
	public float towerRange;
}

[System.Serializable]
public struct MissileTowerLevel
{
	public int upgradePrice;
	public float attackSpeed;
	public int towerDamage;
	public float towerRange;
	public float explosionRange;
}

[System.Serializable]
public struct IceTowerLevel
{
	public int upgradePrice;
	public float attackSpeed;
	public int towerDamage;
	public float towerRange;
	public float explosionRange;
	public float freezingRate;
	public float freezingTime;
}

[System.Serializable]
public struct Trail
{
	public Transform[] points;
}

//for upgrade menu
[System.Serializable]
public struct TypeToInfo
{
	public TowerType type;
	public GameObject towerPrefab;
	public GameObject[] upgrades;
}

[System.Serializable]
public class TowerUpgradeBase
{
	public int starsPrice;
	public string towerName;
	public int additionalDamage;
	public int armorPiercing;
	public int basePriceDecrease;
	public int upgradePriceDecrease;
	public float additionalAttackSpeed;
	public float additionalRange;


	public TowerUpgradeBase()
	{
		towerName = "";
		additionalDamage = 0;
		armorPiercing = 0;
		basePriceDecrease = 0;
		upgradePriceDecrease = 0;
		additionalAttackSpeed = 0.0f;
		additionalRange = 0.0f;
	}


	public TowerUpgradeBase(string name)
	{
		towerName = name;
		additionalDamage = 0;
		basePriceDecrease = 0;
		additionalAttackSpeed = 0.0f;
		additionalRange = 0.0f;
	}
}


[System.Serializable]
public class IceTowerUpgrade: TowerUpgradeBase
{
	public float additionalExplosionRange;
	public float additionalFreezingRate;
	public float additionalFreezingTime;


	public IceTowerUpgrade(): base()
	{
		additionalFreezingRate = 0.0f;
		additionalFreezingTime = 0.0f;
	}


	public IceTowerUpgrade(string name): base(name)
	{
		additionalFreezingRate = 0.0f;
		additionalFreezingTime = 0.0f;
	}


	public static IceTowerUpgrade operator +(IceTowerUpgrade tower1, IceTowerUpgrade tower2)
	{
		IceTowerUpgrade newTower = new IceTowerUpgrade();
		newTower.towerName = tower1.towerName;

		newTower.starsPrice = tower1.starsPrice + tower2.starsPrice;
		newTower.additionalDamage = tower1.additionalDamage + tower2.additionalDamage;
		newTower.armorPiercing = tower1.armorPiercing + tower2.armorPiercing;
		newTower.additionalAttackSpeed = tower1.additionalAttackSpeed + tower2.additionalAttackSpeed;
		newTower.basePriceDecrease = tower1.basePriceDecrease + tower2.basePriceDecrease;
		newTower.upgradePriceDecrease = tower1.upgradePriceDecrease + tower2.upgradePriceDecrease;
		newTower.additionalRange = tower1.additionalRange + tower2.additionalRange;
		newTower.additionalExplosionRange = tower1.additionalExplosionRange + tower2.additionalExplosionRange;
		newTower.additionalFreezingRate = tower1.additionalFreezingRate + tower2.additionalFreezingRate;
		newTower.additionalFreezingTime = tower1.additionalFreezingTime + tower2.additionalFreezingTime;

		return newTower;
	}
}