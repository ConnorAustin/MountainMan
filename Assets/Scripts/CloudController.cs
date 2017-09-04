using UnityEngine;
using System.Collections;

public class CloudController : MonoBehaviour 
{
	private float speed;

	
	void Start()
	{
		speed = Random.Range(1, 3);
		
		//Direction
		if(Random.Range(0, 2) == 0)
			speed *= -1;
	}
	
	void Update () 
	{
		Vector3 pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;
	}
	
}
