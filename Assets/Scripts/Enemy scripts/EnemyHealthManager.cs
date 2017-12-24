using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour 
{

	public int maxHealth;
	public int goldForDefeat;
	public int armor;
	public float goldObjectOffsetY; //position to spawn gold after death
	public Slider healthBar;
	public GameObject bloodParticle;
	public GameObject missText;
	public bool canBeAttacked; //for invisible enemy


	protected int currentHealth;


	private static int evasionPerArmor = 20; //percents of evasion per armor if it is greater than tower attack


	public virtual void ChangeHealth(int healthToAdd)
	{
		if (healthToAdd < 0)
		{
			healthToAdd += armor;
			if (healthToAdd >= 0)
			{
				bool evaded = TryEvade(healthToAdd * evasionPerArmor);
				if (evaded)
				{
					return;
				}
				else
				{
					healthToAdd = -1;
				}
			}
		}
		currentHealth += healthToAdd;

		if (healthToAdd < 0)
		{
			for (int i = 0; i < -healthToAdd; i++) //creates one blood particle per damage unit
			{
				Instantiate(bloodParticle, transform.position, Quaternion.identity);
			}
		}

		if (currentHealth != maxHealth && !healthBar.gameObject.activeSelf) //appears only if enemy is injured
		{
			healthBar.gameObject.SetActive(true);
		}

		if (currentHealth <= 0)
		{
			FindObjectOfType<GoldManager>().ChangeGold(goldForDefeat, transform.position + new Vector3(0.0f, goldObjectOffsetY, 0.0f));
			Destroy(gameObject);
		}
		healthBar.value = currentHealth;
	}


	public void ChangeHealth(int healthToAdd, int armorPiercing) //for towers with armor piercing
	{
		if (armor == 0)
		{
			ChangeHealth(healthToAdd);
		}
		else
		{
			if (armor >= armorPiercing)
			{
				ChangeHealth(healthToAdd - armorPiercing);
			}
			else
			{
				ChangeHealth(healthToAdd - armor);
			}
		}
	}


	protected bool TryEvade(int chanceToEvade)
	{
		if (Random.Range(0, 100) < chanceToEvade)
		{
			Instantiate(missText, transform.position + new Vector3(0.0f, goldObjectOffsetY, 0.0f), Quaternion.identity);
			return true;
		}
		return false;
	}

	
	protected virtual void Start () 
	{
		currentHealth = maxHealth;
		healthBar.maxValue = maxHealth;
		healthBar.value = currentHealth;
		healthBar.gameObject.SetActive(false); //health bar is not active by default
		canBeAttacked = true;
	}
}
