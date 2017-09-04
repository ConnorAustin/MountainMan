using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour 
{
	public Vector3 axis;
	
	public float speed;
	
	void Update () 
	{
		transform.Rotate(axis * speed * Time.deltaTime);
	}
}
