using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{

	public RouteManager routeManager;
	public float movespeed;
	public int currentTargetPoint;
	public int trailNumber;


	private Rigidbody2D rBody;
	private Transform targetPoint;
	private float slowDownRate;
	private float freezeCounter;


	public void Freeze(float freezingRate, float freezingTime)
	{
		if (freezeCounter > 0)
		{
			if (freezingRate < slowDownRate) //applies the lowest rate
			{
				slowDownRate = freezingRate;
			}

			if (freezingTime > freezeCounter) //updates freezing time
			{
				freezeCounter = freezingTime;
			}
		}
		else
		{
			freezeCounter = freezingTime;
			slowDownRate = freezingRate;
		}
	}

	
	void Awake () 
	{
		rBody = GetComponent<Rigidbody2D>();
	}


	void Start()
	{
		SetTargetPoint();
		slowDownRate = 1.0f;
	}
	
	
	void Update () 
	{
		if (targetPoint == null)
		{
			return;
		}

		rBody.MovePosition(Vector3.MoveTowards(transform.position, targetPoint.position, movespeed * slowDownRate * Time.deltaTime));

		if (freezeCounter > 0)
		{
			freezeCounter -= Time.deltaTime;

			if (freezeCounter <= 0)
			{
				slowDownRate = 1.0f;
			}
		}

		if (transform.position == targetPoint.position)
		{
			if (targetPoint.name == "Finish")
			{
				if (gameObject.tag == "Enemy")
				{
					FindObjectOfType<LivesManager>().TakeLife();
				}
				else if (gameObject.tag == "Boss")
				{
					FindObjectOfType<LivesManager>().BossAttack();
				}
				Destroy(gameObject);
			}
			else
			{
				currentTargetPoint++;
				SetTargetPoint();
			}
		}
	}


	void SetTargetPoint()
	{
		targetPoint = routeManager.routes[trailNumber].points[currentTargetPoint];
	}
}
