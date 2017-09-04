using UnityEngine;
using System.Collections;

public class SpinOffScreen : MonoBehaviour 
{
	private float angularSpinVelocity;
	private float yVelocity;
	private float xVelocity;
	private float zVelocity;	
	
	void Start() 
	{
		yVelocity = Random.Range(15.0f, 18.0f);
		xVelocity = Random.Range(-6.0f, 6.0f);
		zVelocity = 1.5f;
		angularSpinVelocity = Random.Range(200.0f, 260.0f);
		if(Random.Range(0, 2) == 1)
			angularSpinVelocity *= -1;
	}
	
	void Update() 
	{
		Vector3 position = transform.position;
		position.x += xVelocity * Time.deltaTime;
		position.y += yVelocity * Time.deltaTime;
		position.z -= zVelocity * Time.deltaTime;
		transform.position = position;
		
		//Gravity, crazy stuff
		yVelocity -= 17 * Time.deltaTime;
		
		transform.Rotate(Vector3.forward * angularSpinVelocity * Time.deltaTime);
	}
}
