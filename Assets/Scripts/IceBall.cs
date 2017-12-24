using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBall : Missile 
{

	public float freezingRate;
	public float freezingTime;


	protected override void Explode()
	{
		Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, explosionRange, enemyLayer);
		foreach (Collider2D enemy in enemies)
		{
			EnemyMovement movementScript = enemy.GetComponent<EnemyMovement>();
			movementScript.Freeze(freezingRate, freezingTime);
			if (armorPiercing > 0)
			{
				enemy.GetComponent<EnemyHealthManager>().ChangeHealth(-damage, armorPiercing);
			}
			else
			{
				enemy.GetComponent<EnemyHealthManager>().ChangeHealth(-damage);
			}
		}
		if (explosionParticles != null)
		{
			GameObject particles = Instantiate(explosionParticles, transform.position, Quaternion.identity) as GameObject;
			particles.transform.localScale = new Vector3(explosionRange, explosionRange, 1.0f);
		}
	}
}
