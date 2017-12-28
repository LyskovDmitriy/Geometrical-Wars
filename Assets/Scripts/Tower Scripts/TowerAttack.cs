using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttack : MonoBehaviour
{

	public float rotationSpeed;
	public float roughness; //if tower hasn't rotated, it can't shoot. Roughness makes this rule less strict
	public bool isActive;
	public bool canRotate; //if tower can't rotate it shoots without checking rotation
	public GameObject attackRangeSprite;
	public GameObject parentObject;


	protected Transform currentEnemy; //currently attacked enemy
	protected int towerDamage;
	protected int armorPiercing;


	private float attackSpeed; //shots per second
	private float attackRange;
	private float betweenShotsCounter; 
	private bool hasRotated; 
	private List<Transform> enemiesToFollow; //all the enemies in the attack range
	private EnemyHealthManager enemyHealth;
	private CircleCollider2D attackTriggerCircle; //physical representation of attack range


	public void ShowAttackRange(bool activate)
	{
		attackRangeSprite.SetActive(activate);
	}


	public void UpdateAttackParameters(int damage, int piercing, float speed, float range)
	{
		towerDamage = damage;
		armorPiercing = piercing;
		attackSpeed = speed;
		attackRange = range;
		attackTriggerCircle.radius = attackRange * 2 / transform.localScale.x;
		attackRangeSprite.transform.localScale = new Vector3(attackRange * 2, attackRange * 2, attackRangeSprite.transform.localScale.z);
	}


	protected virtual void Shoot()
	{				
		if (armorPiercing <= 0)
		{
			enemyHealth.ChangeHealth(-towerDamage);
		}
		else
		{
			enemyHealth.ChangeHealth(-towerDamage, armorPiercing);
		}
	}


	void RotateTowardsEnemy()
	{
		float rotationAngle = Mathf.Atan2(currentEnemy.position.y - transform.position.y, currentEnemy.position.x - transform.position.x) * Mathf.Rad2Deg;
		if (Mathf.Abs(transform.eulerAngles.z - rotationAngle) > 180)
		{
			rotationAngle = 360 + rotationAngle;
		}
		Vector3 rotationToEnemy = new Vector3(0.0f, 0.0f, rotationAngle);
		transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, rotationToEnemy, rotationSpeed * Time.deltaTime);
		if (transform.eulerAngles.z - roughness < rotationAngle && transform.eulerAngles.z + roughness > rotationAngle)
		{
			hasRotated = true;
		}
		else
		{
			hasRotated = false;
		}
	}


	void FindEnemy()
	{
		if (enemiesToFollow.Count > 0 && currentEnemy == null)
		{
			Transform farthestEnemy = null; //TODO find closest enemy to the finish
			int lastPoint = -1;
			foreach (Transform enemy in enemiesToFollow)
			{
				EnemyMovement enemyMovement = enemy.GetComponent<EnemyMovement>();
				if ((enemyMovement.currentTargetPoint > lastPoint) && (enemy.GetComponent<EnemyHealthManager>().canBeAttacked))
				{
					lastPoint = enemyMovement.currentTargetPoint;
					farthestEnemy = enemy;
				}
			}

			currentEnemy = farthestEnemy;
			if (currentEnemy != null)
			{
				enemyHealth = currentEnemy.GetComponent<EnemyHealthManager>();
			}
		}
	}


	void Awake()
	{
		attackTriggerCircle = GetComponent<CircleCollider2D>();
	}


	void Start()
	{
		enemiesToFollow = new List<Transform>();
	}


	void Update () 
	{
		if (!isActive)
		{
			return;
		}

		if (currentEnemy != null && enemyHealth.canBeAttacked)
		{
			if (canRotate)
			{
				RotateTowardsEnemy();
			}
			else
			{
				hasRotated = true;
			}

			if (betweenShotsCounter <= 0 && hasRotated) //if tower hasn't rotated, it can't shoot
			{
				Shoot();
				betweenShotsCounter = 1.0f / attackSpeed;
			}
		}
		else
		{
			if (currentEnemy != null)
			{
				currentEnemy = null;
			}
			FindEnemy();
		}

		betweenShotsCounter -= Time.deltaTime;
	}


	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Enemy" || collider.tag == "Boss")
		{
			enemiesToFollow.Add(collider.transform);

			if (currentEnemy == null)
			{
				FindEnemy();
			}
			//FindEnemy();
		}
	}


	void OnTriggerExit2D(Collider2D collider)
	{
		if (collider.tag == "Enemy" || collider.tag == "Boss")
		{
			enemiesToFollow.Remove(collider.transform);

			if (currentEnemy == collider.transform)
			{
				currentEnemy = null;
				enemyHealth = null;
				FindEnemy();
			}
		}
	}
}
