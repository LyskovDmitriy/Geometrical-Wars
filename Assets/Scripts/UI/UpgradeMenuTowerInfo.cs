using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeMenuTowerInfo : MonoBehaviour 
{

	public TypeToInfo[] info;
	public Text towerName;
	public Text upgradeTowerName;
	public Dropdown levelDropdown;
	public Text attackValue;
	public Text piercingValue;
	public Text attackSpeedValue;
	public Text attackRangeValue;
	public GameObject explosionRangeText;
	public Text explosionRangeValue;
	public GameObject freezingRateText;
	public Text freezingRateValue;
	public GameObject freezingTimeText;
	public Text freezingTimeValue;


	private TowerType currentType;


	public void ChangeType(TowerType type)
	{
		if (currentType == type)
		{
			return;
		}
		levelDropdown.value = 0;
		ActivateTowerUpgrades(false);
		currentType = type;
		SetTowerLevel(0);
		ActivateTowerUpgrades(true);
	}


	public void SetTowerLevel(int level)
	{
		GameObject tower = Instantiate(GetTowerObject(), new Vector3(-50.0f, 0.0f, 0.0f), Quaternion.identity) as GameObject;
		TowerMarketInfoBase towerInfo = tower.GetComponent<TowerMarketInfoBase>();
		towerInfo.RollBack();

		for (int i = 0; i < level; i++)
		{
			towerInfo.Upgrade();
		}
		if (level == 0)
		{
			string towerName = towerInfo.name;
			towerName = towerName.Substring(0, towerName.IndexOf("(Clone)")); //Deletes "(Clone)" ending
			upgradeTowerName.text = towerName + " upgrades";
		}
		SetInfo(tower);
		Destroy(tower);
	}


	public void UpdateTowerInfo()
	{
		SetTowerLevel(levelDropdown.value);
	}


	void Start()
	{
		currentType = TowerType.None;
		ChangeType(TowerType.MachineGun);
	}


	GameObject GetTowerObject()
	{
		foreach (TypeToInfo infoType in info)
		{
			if (infoType.type == currentType)
			{
				return infoType.towerPrefab;
			}
		}
		return null;
	}


	void SetInfo(GameObject towerPrefab)
	{
		TowerMarketInfoBase marketInfo = towerPrefab.GetComponent<TowerMarketInfoBase>();

		towerName.text = marketInfo.towerName;
		attackValue.text = marketInfo.currentDamage.ToString();
		piercingValue.text = marketInfo.armorPiercing.ToString();
		attackSpeedValue.text = marketInfo.currentAttackSpeed.ToString();
		attackRangeValue.text = marketInfo.currentAttackRange.ToString();

		if (currentType == TowerType.IceTower)
		{
			IceTowerMarketInfo iceTowerInfo = towerPrefab.GetComponent<IceTowerMarketInfo>();
			explosionRangeText.SetActive(true);
			freezingRateText.SetActive(true);
			freezingTimeText.SetActive(true);
			explosionRangeValue.text = iceTowerInfo.currentExplosionRange.ToString();
			freezingRateValue.text = iceTowerInfo.currentFreezingRate.ToString();
			freezingTimeValue.text = iceTowerInfo.currentFreezingTime.ToString();
		}
		else if (currentType == TowerType.FireTower)
		{
			MissileTowerMarketInfo missileInfo = towerPrefab.GetComponent<MissileTowerMarketInfo>();
			explosionRangeText.SetActive(true);
			freezingRateText.SetActive(false);
			freezingTimeText.SetActive(false);
			explosionRangeValue.text = missileInfo.currentExplosionRange.ToString();
		}
		else
		{
			explosionRangeText.SetActive(false);
			freezingRateText.SetActive(false);
			freezingTimeText.SetActive(false);
		}
	}


	void ActivateTowerUpgrades(bool activate)
	{
		foreach (TypeToInfo infoType in info)
		{
			if (infoType.type == currentType)
			{
				foreach (GameObject upgrade in infoType.upgrades)
				{
					upgrade.SetActive(activate);
				}
			}
		}
		UpgradeLevel.UpdateInfo();
	}
}
