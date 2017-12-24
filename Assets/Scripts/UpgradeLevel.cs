using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class UpgradeLevel : MonoBehaviour 
{

	public int levelPrice;
	public string towerName;
	public bool isUpgraded;
	public IceTowerUpgrade[] upgradeSelection;
	public Button[] upgradeButtons;
	public UpgradeLevel requirement;


	private static StarsCounter starsCounter;
	private static event UpdateStars updateState;
	private static event UpdateStars deleteUpgrades;
	private static Color disabledButtonColor;

	private delegate void UpdateStars();

	private static UpgradeMenuTowerInfo towerInfoMenu;
	private bool subscribedToUpdateState = false;
	private bool subscribedToDeleteUpgrades = false;


	public static void DeleteTowerUpgrades()
	{
		if (deleteUpgrades != null)
		{
			deleteUpgrades.Invoke();
			UpdateInfo();
			towerInfoMenu.UpdateTowerInfo();
		}
	}


	public static void UpdateInfo()
	{
		starsCounter.Calculate();
		updateState.Invoke();
	}


	public void UpgradeTower(int choice)
	{
		if (PlayerPrefs.HasKey("Used stars"))
		{
			int currentlyUsedStars = PlayerPrefs.GetInt("Used stars");
			PlayerPrefs.SetInt("Used stars", currentlyUsedStars + levelPrice);
		}
		else
		{
			PlayerPrefs.SetInt("Used stars", levelPrice);
		}
		PlayerPrefs.SetInt(gameObject.name, 1);
		starsCounter.Calculate();
		SetButtonsInteractable(false);
		PlayerPrefs.SetInt(gameObject.name + " choice", choice);

		IceTowerUpgrade previousTowerUpgrades = SaveLoad.Load(towerName);
		SaveLoad.Save(previousTowerUpgrades + upgradeSelection[choice]);
		if (!subscribedToDeleteUpgrades)
		{
			subscribedToDeleteUpgrades = true;
			deleteUpgrades += DeleteUpgrades;
		}
		isUpgraded = true;
		UpdateInfo();
		towerInfoMenu.UpdateTowerInfo();
	}
		

	public void DeleteUpgrades()
	{
		if (PlayerPrefs.HasKey(gameObject.name))
		{
			PlayerPrefs.DeleteKey(gameObject.name);
		}
		if (PlayerPrefs.HasKey(gameObject.name + " choice"))
		{
			HighlightChosenUpgrade(PlayerPrefs.GetInt(gameObject.name + " choice"), false);
			PlayerPrefs.DeleteKey(gameObject.name + " choice");
		}
		SaveLoad.Delete(towerName);
		deleteUpgrades -= DeleteUpgrades;
		subscribedToDeleteUpgrades = false;
		isUpgraded = false;
	}


	void Awake()
	{
		if (starsCounter == null)
		{
			starsCounter = FindObjectOfType<StarsCounter>();
		}
		if (towerInfoMenu == null)
		{
			towerInfoMenu = FindObjectOfType<UpgradeMenuTowerInfo>();
		}
		if (!subscribedToUpdateState)
		{
			subscribedToUpdateState = true;
			updateState += UpdateLocalInfo;
		}
		gameObject.SetActive(false);
		if (disabledButtonColor != new Color())
		{
			disabledButtonColor = new Color();
		}
	}

	
	void UpdateLocalInfo () 
	{	
		if (levelPrice > starsCounter.TotalStars || PlayerPrefs.HasKey(gameObject.name) || (requirement != null && !requirement.isUpgraded))
		{
			SetButtonsInteractable(false);

			if (PlayerPrefs.HasKey(gameObject.name))
			{
				isUpgraded = true;
				if (PlayerPrefs.HasKey(gameObject.name + " choice"))
				{
					HighlightChosenUpgrade(PlayerPrefs.GetInt(gameObject.name + " choice"), true);
				}
				if (!subscribedToDeleteUpgrades)
				{
					subscribedToDeleteUpgrades = true;
					deleteUpgrades += DeleteUpgrades;
				}
			}
			else
			{
				isUpgraded = false;
			}
		}
		else
		{
			SetButtonsInteractable(true);
			isUpgraded = false;
			foreach (IceTowerUpgrade upgrade in upgradeSelection)
			{
				upgrade.towerName = towerName;
				upgrade.starsPrice = levelPrice;
			}
		}
	}


	void SetButtonsInteractable(bool interactable)
	{
		foreach (Button button in upgradeButtons)
		{
			button.interactable = interactable;
		}
	}


	void HighlightChosenUpgrade(int choice, bool highlight)
	{		
		ColorBlock colors = upgradeButtons[choice].colors;
		if (disabledButtonColor == new Color())
		{
			disabledButtonColor = colors.disabledColor;
		}

		if (highlight)
		{
			colors.disabledColor = colors.highlightedColor;
		}
		else
		{
			colors.disabledColor = disabledButtonColor;
		}
		upgradeButtons[choice].colors = colors;
	}


	void OnDestroy()
	{
		if (subscribedToUpdateState)
		{
			updateState -= UpdateLocalInfo;
		}
		if (subscribedToDeleteUpgrades)
		{
			deleteUpgrades -= DeleteUpgrades;
		}
	}
}
