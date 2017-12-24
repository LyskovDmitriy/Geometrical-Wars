using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfo : MonoBehaviour 
{

	public GameObject infoCanvas;
	public Button upgradeButton;
	public Text towerName;
	public Text attackText;
	public Text attackSpeedText;
	public Text upgradePriceText;
	public Text sellingPriceText;
	public TowerMarketInfoBase marketScript;
	public PlaceForTower towerHolder;
	public bool isBuyingInfo;


	private static float sellingMultiplier = 0.5f;


	private GoldManager goldManager;
	private BoxCollider collider;

	
	public void ShowInfo (bool activate) 
	{
		infoCanvas.SetActive(activate);
		if (collider != null)
		{
			collider.enabled = activate;
		}
	}


	public void ShowBelowTower(bool isBelow)
	{
		if (isBelow)
		{
			Vector3 pos = transform.localPosition;
			if (pos.y > 0)
			{
				pos.y = -pos.y;
				transform.localPosition = pos;
			}
		}
	}


	public void Sell()
	{
		goldManager.ChangeGold((int)(marketScript.GetPrice() * sellingMultiplier), marketScript.transform.position);
		towerHolder.DeleteTower();
	}


	public void Upgrade()
	{
		int upgradePrice = marketScript.GetUpgradePrice();
		if (goldManager.canBuy(upgradePrice))
		{
			goldManager.ChangeGold(-upgradePrice, Vector3.zero);
			bool canUpgradeMore = marketScript.Upgrade();
			UpdateInfo();
			if (!canUpgradeMore)
			{
				upgradeButton.interactable = false;
				upgradePriceText.text = "";
			}
		}
	}


	public void UpdateInfo()
	{
		attackText.text = marketScript.currentDamage.ToString();
		attackSpeedText.text = marketScript.currentAttackSpeed.ToString();
		towerName.text = marketScript.towerName;
		if (upgradePriceText != null && sellingPriceText != null)
		{
			upgradePriceText.text = "-" + marketScript.GetUpgradePrice();
			sellingPriceText.text = "+" + (marketScript.GetPrice() * sellingMultiplier);
		}
	}


	void Awake()
	{
		goldManager = FindObjectOfType<GoldManager>();
		collider = GetComponent<BoxCollider>();
	}


	void Start()
	{
		ShowInfo(false);
		if (isBuyingInfo)
		{
			StartCoroutine(BuyingInfoUpdate());
		}
		else
		{
			UpdateInfo();
		}
	}


	IEnumerator BuyingInfoUpdate()
	{
		GameObject towerObject = Instantiate(marketScript.transform.parent.gameObject, new Vector3(100.0f, 0.0f, 0.0f), Quaternion.identity);
		towerObject.transform.SetParent(transform);
		marketScript = towerObject.GetComponentInChildren<TowerMarketInfoBase>();
		yield return new WaitForSeconds(0.1f);
		UpdateInfo();
	}
}
