using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTowerAttack : MissileTowerAttack 
{

	protected float freezingRate;
	protected float freezingTime;

	
	public void UpdateAttackParameters(int damage, int piercing, float speed, float range, float explosionRange, float rate, float time)
	{
		UpdateAttackParameters(damage, piercing, speed, range, explosionRange);
		freezingRate = rate;
		freezingTime = time;
	}


	override protected void Shoot()
	{
		IceBall missile = Instantiate(missilePrefab, shootingPosition.position, Quaternion.identity).GetComponent<IceBall>();
		missile.targetPosition = currentEnemy.position;
		missile.explosionRange = missileExplosionRange;
		missile.damage = towerDamage;
		missile.armorPiercing = armorPiercing;
		missile.freezingRate = freezingRate;
		missile.freezingTime = freezingTime;
	}
}
