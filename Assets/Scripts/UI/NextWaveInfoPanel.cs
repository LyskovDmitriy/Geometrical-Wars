using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveInfoPanel : MonoBehaviour 
{

	public Transform placeForEnemy;
	public Text enemyNumber;


	public bool isActive;


	void Awake()
	{
		isActive = false;
		enemyNumber.text = "";
	}


	public void SetInfo(GameObject enemyToSpawn, int number)
	{
		isActive = true;
		GameObject enemy = Instantiate(enemyToSpawn, placeForEnemy) as GameObject;
		enemy.transform.localPosition = Vector3.zero;
		enemy.GetComponent<EnemyMovement>().enabled = false;
		enemy.GetComponent<CircleCollider2D>().enabled = false;
		enemyNumber.text = number.ToString();

		if (enemy.tag == "Boss")
		{
			placeForEnemy.localScale /= 1.5f;
		}
	}

//
//	public void Erase()
//	{
//		if (isActive)
//		{
//			isActive = false;
//			enemyNumber.text = "";
//			Destroy(placeForEnemy.GetChild(0).gameObject);
//		}
//	}
}
