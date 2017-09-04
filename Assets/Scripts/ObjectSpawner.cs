using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour 
{
	public GameObject objectToSpawn;
	public float yRange;
	
	public float chance;
	public float spawnInterval = 0.5f;
	
	private float time;
	
	void Update () 
	{
		if(time + spawnInterval < Time.time)
		{
			time = Time.time;
			if(Random.Range(0.0f, 1.0f) <= chance)
			{
				GameObject obj = (GameObject)GameObject.Instantiate(objectToSpawn);
				Vector3 pos = transform.position;
				pos.y += Random.Range(-yRange, yRange + 1);
				obj.transform.position = pos;
			}
		}
	}
}
