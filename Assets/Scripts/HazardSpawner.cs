using UnityEngine;
using System.Collections;

public class HazardSpawner : MonoBehaviour 
{
	public bool canSpawnHazards;

	public GameObject sheep;
	public GameObject blueBird;

	public GameObject SpawnHazard (Environment e) 
	{
		if(!canSpawnHazards)
			return null;

		if(e == Environment.Snow)
		{
			return (GameObject)GameObject.Instantiate(sheep);
		}
		else if(e == Environment.Grass)
		{
			if(Random.Range(0, 9) == 0)
				return (GameObject)GameObject.Instantiate(blueBird);
			else 
				return (GameObject)GameObject.Instantiate(sheep);
		}


		return null;
	}
}
