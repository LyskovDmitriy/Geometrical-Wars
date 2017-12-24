using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTowerAttack : TowerAttack 
{

	public GameObject missilePrefab;
	public Transform shootingPosition;


	protected float missileExplosionRange;


	public void UpdateAttackParameters(int damage, int piercing, float speed, float range, float explosionRange)
	{
		UpdateAttackParameters(damage, piercing, speed, range);
		missileExplosionRange = explosionRange;
	}


	override protected void Shoot()
	{
		Missile missile = Instantiate(missilePrefab, shootingPosition.position, Quaternion.identity).GetComponent<Missile>();
		missile.targetPosition = currentEnemy.position;
		missile.explosionRange = missileExplosionRange;
		missile.damage = towerDamage;
		missile.armorPiercing = armorPiercing;
	}
}
