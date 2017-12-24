using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleEnemyHealthManager : EnemyHealthManager 
{

	public float invisibilityTime;
	public GameObject invisibilityParticles;
	

	private SpriteRenderer renderer;
	private bool canEnterInvisibility; //enemy can enter invisibility only once
	private bool isInvisible;
	private float invisibilityCounter;


	public override void ChangeHealth(int healthToAdd)
	{
		base.ChangeHealth(healthToAdd);

		if (canEnterInvisibility)
		{
			BecomeInvisible(true);
			canEnterInvisibility = false;
			invisibilityCounter = invisibilityTime;
		}
	}


	protected override void Start()
	{
		base.Start();
		canEnterInvisibility = true;
	}


	void Awake()
	{
		renderer = GetComponent<SpriteRenderer>();
	}


	void Update()
	{
		if (isInvisible)
		{
			if (invisibilityCounter > 0)
			{
				invisibilityCounter -= Time.deltaTime;
			}
			else
			{
				BecomeInvisible(false);
			}
		}
	}


	void BecomeInvisible(bool invisible = false)
	{
		canBeAttacked = !invisible;
		isInvisible = invisible;
		renderer.enabled = canBeAttacked;
		healthBar.gameObject.SetActive(canBeAttacked);

		if (canBeAttacked)
		{
			healthBar.value = currentHealth;
		}

		Instantiate(invisibilityParticles, transform.position, Quaternion.identity);
	}
}
