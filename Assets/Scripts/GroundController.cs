using UnityEngine;
using System.Collections;

public class GroundController : MonoBehaviour 
{
	//Lerp destination
	Vector3 destination;
	
	Vector3 startPos;
	
	//Current lerp value
	float lerpToDest;
	
	//How fast to lerp
	static float lerpSpeed = 1.5f;
	
	static Vector3 speedToGoOffScreen = new Vector3(40, 0, -16);

	float killTime = 1.5f;
	
	bool goingOffScreen;
	bool movingRight;
	
	
	void Start()
	{
		//Vary the ground's tilt a slight bit
		transform.Rotate(Vector3.forward * Random.Range(-1f, 1f));

		//Vary color a little bit
		Color color = GetComponent<Renderer>().material.GetColor("_Color");
		color.r += Random.Range(-0.03f, 0.03f);
		color.g += Random.Range(-0.03f, 0.03f);
		color.b += Random.Range(-0.03f, 0.03f);

		GetComponent<Renderer>().material.SetColor("_Color", color);

		startPos = transform.position;
		if(tag != "GroundMiddle")
		{
			destination = GameObject.FindGameObjectWithTag("GroundMiddle").transform.position;
			destination.x = transform.position.x;
			destination.y -= 6;
		}
		else 
		{
			destination = transform.position;
		}
		lerpToDest = 0;
	}

	void OnDestroy()
	{
		//Makes sure we don't destroy the ground pound parented to the ground when the ground is destroyed
		Transform groundPound = null;

		if((groundPound = transform.Find("GroundPound")) != null)
		{
			groundPound.parent = null;
		}
	}
	
	void MoveRight()
	{
		lerpToDest = 1;
		goingOffScreen = true;
		movingRight = true;
		tag = "";
	}
	
	void MoveLeft()
	{
		lerpToDest = 1;
		goingOffScreen = true;
		movingRight = false;
		tag = "";
	}
	
	void LandedOn()
	{
		Transform snowPuff = transform.Find("SnowPuff");
		if(snowPuff != null)
		{
			snowPuff.gameObject.GetComponent<ParticleSystem>().Play();
		}
	}
	
	void Update()
	{
		if(lerpToDest < 1.0f)
		{
			lerpToDest += lerpSpeed * Time.deltaTime;

			transform.position = Vector3.Lerp(startPos, destination, lerpToDest);
		}
		Vector3 position = transform.position;

		if(goingOffScreen)
		{
			killTime -= Time.deltaTime;
			if(killTime <= 0)
			{
				DestroyObject(gameObject);
				return;
			}
			
			if(movingRight)
			{
				position.x += speedToGoOffScreen.x * Time.deltaTime;
			}
			else
			{
				position.x -= speedToGoOffScreen.x * Time.deltaTime;
			}
		}
		
		transform.position = position;
	}
}
