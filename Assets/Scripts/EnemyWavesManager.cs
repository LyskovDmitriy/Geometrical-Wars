using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWavesManager : MonoBehaviour 
{

	public EnemyWave[] waves;
	public Transform[] spawnPoints;
	public Transform positionToSpawnGold;
	public GameObject startWaveButton;
	public Text nextWaveTimeText;
	public Text waveNumberText;
	public float timeBetweenEachEnemySpawn;
	public float timeBetweenWaves;
	public float timeToFirstWave;


	private GoldManager goldManager;
	private RouteManager routeManager;
	private Color baseColor;
	private float betweenWavesCounter;
	private int currentWave;
	private bool waveHasStarted;
	private bool allEnemiesInWaveSpawned;


	public void StartWave()
	{
		if (betweenWavesCounter > 0)
		{
			betweenWavesCounter = 0;
		}
	}


	IEnumerator SpawnWave()
	{
		allEnemiesInWaveSpawned = false;

		foreach (EnemyGroup groupToSpawn in waves[currentWave].enemyGroups)
		{
			for (int i = 0; i < groupToSpawn.numberOfEnemies; i++)
			{
				GameObject enemy = Instantiate(groupToSpawn.enemyType, spawnPoints[groupToSpawn.trailNumber].position, Quaternion.identity) as GameObject;
				enemy.transform.SetParent(transform); //for editor view simplification
				EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
				enemyMovement.routeManager = routeManager;
				enemyMovement.trailNumber = groupToSpawn.trailNumber;
				yield return new WaitForSeconds(timeBetweenEachEnemySpawn);
			}
			yield return new WaitForSeconds(waves[currentWave].delayBetweenGroups);
		}
		allEnemiesInWaveSpawned = true;
	}


	void Awake()
	{
		goldManager = FindObjectOfType<GoldManager>();
		routeManager = FindObjectOfType<RouteManager>();
	}


	void Start () 
	{
		betweenWavesCounter = timeToFirstWave;
		currentWave = 0;
		waveNumberText.text = "Wave: 1";
		waveHasStarted = false;
		allEnemiesInWaveSpawned = false;
		baseColor = nextWaveTimeText.color;
		nextWaveTimeText.text = timeBetweenWaves.ToString();
	}
	
	
	void Update () 
	{
		if (betweenWavesCounter > 0)
		{
			betweenWavesCounter -= Time.deltaTime;
			nextWaveTimeText.text = Mathf.Round(betweenWavesCounter).ToString();
		}
		else
		{
			if (!waveHasStarted)
			{
				nextWaveTimeText.text = "STARTED";
				nextWaveTimeText.color = Color.red;
				StartCoroutine(SpawnWave());
				waveHasStarted = true;
				startWaveButton.SetActive(false);
			}
			else if ((FindObjectOfType<EnemyHealthManager>() == null) && allEnemiesInWaveSpawned) //if the wave is completed
			{
				goldManager.ChangeGold(waves[currentWave].waveCompletedBonus, positionToSpawnGold.position);
				currentWave++;
				if (currentWave == waves.Length)
				{
					enabled = false;
					if (LivesManager.isAlive)
					{
						StartCoroutine(FindObjectOfType<GameOverMenu>().LevelCompleted(2.0f));
					}
				}
				else
				{
					waveNumberText.text = "Wave: " + (currentWave + 1);
					nextWaveTimeText.color = baseColor;
					nextWaveTimeText.text = timeBetweenWaves.ToString();
					betweenWavesCounter = timeBetweenWaves;
					startWaveButton.SetActive(true);
				}
				waveHasStarted = false;
			}
		}
	}
}
