using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWaveEnemiesInfo : MonoBehaviour 
{

	public GameObject infoPanelPrefab;
	public Vector3 firstEnemySpawnOffset;
	public Vector3 distanceBetweenInfoPanels;


	private List<GameObject> infoPanels;


	public void ShowInfo(List<EnemyGroup> enemies)
	{
		List<GameObject> enemyGroups = new List<GameObject>(0);
		List<int> numberOfEnemies = new List<int>(0);

		for (int i = 0; i < enemies.Count; i++)
		{
			GameObject enemy = enemies[i].enemyType;
			if (enemyGroups.Contains(enemy))
			{
				int index = enemyGroups.IndexOf(enemy);
				numberOfEnemies[index] += enemies[i].numberOfEnemies;
			}
			else
			{
				enemyGroups.Add(enemy);
				numberOfEnemies.Add(enemies[i].numberOfEnemies);
			}
		}

		infoPanels = new List<GameObject>(0);
		for (int i = 0; i < enemyGroups.Count; i++)
		{
			infoPanels.Add(Instantiate(infoPanelPrefab, transform.position + firstEnemySpawnOffset + distanceBetweenInfoPanels * i, Quaternion.identity) as GameObject);
			infoPanels[i].transform.SetParent(transform);
			infoPanels[i].GetComponent<NextWaveInfoPanel>().SetInfo(enemyGroups[i], numberOfEnemies[i]);
		}
	}


	public void DeleteInfo()
	{
		if (infoPanels != null)
		{
			for (int i = 0; i < infoPanels.Count; i++)
			{
				Destroy(infoPanels[i]);
			}

			infoPanels = null;
		}
	}
}
