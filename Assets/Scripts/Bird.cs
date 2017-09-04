using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour 
{
	public Vector3 spawnOffset;

	public bool flyingAway;
	
	private int lookingLeft;
	
	const float birdSpeed = 15;

	void Start()
	{
		transform.position += spawnOffset;

		lookingLeft = 1;
		
		if(Random.Range(0, 2) == 0)
			Flip();
	}
	
	void Update()
	{
		Vector3 pos = transform.position;
		if(flyingAway)
		{
			pos.x -= birdSpeed * lookingLeft * Time.deltaTime;
			pos.y += birdSpeed * Time.deltaTime;
			pos.z -= birdSpeed * Time.deltaTime;
		}
		transform.position = pos;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(other.tag != "Player")
			return;

		flyingAway = true;

		//Always fly away from the player
		if(other.transform.position.x < transform.position.x && lookingLeft == 1)
			Flip();
		else if(other.transform.position.x > transform.position.x && lookingLeft == -1)
			Flip();

		GetComponent<Animator>().SetBool("FlyingAway", true);

		Invoke("Kill", 2);
	}

	void Kill()
	{
		Destroy(gameObject);
	}
	
	void Flip()
	{
		lookingLeft *= -1;
		
		Vector3 scale = transform.localScale;
		
		scale.x *= -1;
		
		transform.localScale = scale;

		GetComponent<Animator>().SetBool("FlyingAway", false);
	}
}
