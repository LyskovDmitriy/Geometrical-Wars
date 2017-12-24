using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Missile : MonoBehaviour 
{

	public float speed;
	public float explosionRange;
	public int damage;
	public int armorPiercing;
	public Vector3 targetPosition;
	public GameObject explosionParticles;
	public LayerMask enemyLayer;


	abstract protected void Explode();

	
	void Update () 
	{
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

		if (transform.position == targetPosition)
		{
			Explode();
			Destroy(gameObject);
		}
	}
}
