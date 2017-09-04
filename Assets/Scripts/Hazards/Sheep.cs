using UnityEngine;
using System.Collections;

public class Sheep : Hazard 
{
	protected override void PlayerLandedOn()
	{
		GetComponent<Animation>().Play();
	}
}
