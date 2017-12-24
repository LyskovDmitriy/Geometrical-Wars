using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithEvasionHealthManager : EnemyHealthManager 
{

	public int evasionChance;


	public override void ChangeHealth(int healthToAdd)
	{
		bool evaded = TryEvade(evasionChance);

		if (!evaded)
		{
			base.ChangeHealth(healthToAdd);
		}
	}
}
