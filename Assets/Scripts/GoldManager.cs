using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour 
{

	public Text goldText;
	public int currentGold;
	public GameObject goldObject;


	public void ChangeGold(int gold, Vector3 positionToSpawn)
	{
		currentGold += gold;
		goldText.text = currentGold.ToString();
		if (positionToSpawn != Vector3.zero)
		{
			GameObject goldInstance = Instantiate(goldObject, positionToSpawn, Quaternion.identity);
			if (gold > 0)
			{
				goldInstance.GetComponentInChildren<Text>().text = "+" + gold;
			}
			else
			{
				goldInstance.GetComponentInChildren<Text>().text = gold.ToString(); //in that case gold already has a minus
			}
			Destroy(goldInstance, 1.5f);
		}
	}


	public bool canBuy(int gold)
	{
		return gold <= currentGold;
			
	}


	void Start () 
	{
		goldText.text = currentGold.ToString();
	}
}
