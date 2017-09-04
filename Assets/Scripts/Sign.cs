
using UnityEngine;
using System.Collections;

public class Sign : MonoBehaviour
{
	//The distance between the bottom 2 ground
	public const float groundDistance = 12;

	void Start()
	{
		Transform groundParent = transform.parent.parent;

		Transform otherGround;

		if(groundParent.tag == "GroundLeft")
		{
			transform.position += Vector3.right * groundDistance;

			otherGround = GameObject.FindGameObjectWithTag("GroundRight").transform;
		}
		else
		{
			transform.position += Vector3.left * groundDistance;

			otherGround = GameObject.FindGameObjectWithTag("GroundRight").transform;
		}
		transform.parent = otherGround;
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Player")
		{
			GameManager.manager.PlayerDied();
		}
	}
}
