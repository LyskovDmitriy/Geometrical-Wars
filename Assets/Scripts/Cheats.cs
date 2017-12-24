using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour 
{

	private GoldManager goldManager;

	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.K) && Input.GetKey(KeyCode.LeftControl))
		{
			EnemyHealthManager[] enemies = FindObjectsOfType<EnemyHealthManager>();
			foreach (EnemyHealthManager enemy in enemies)
			{
				enemy.ChangeHealth(-500);
			}
		}
		if (Input.GetKeyDown(KeyCode.G) && Input.GetKey(KeyCode.LeftControl)) //allows spawn gold
		{
			goldManager.ChangeGold(500, Vector3.zero);
		}
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Time.timeScale += 0.5f;
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			Time.timeScale -= 0.5f;
		}
	}


	void Awake()
	{
		goldManager = FindObjectOfType<GoldManager>();
	}
}
