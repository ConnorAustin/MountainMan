using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{	
	protected virtual void PlayerLandedOn()
	{	}
	
	void OnTriggerEnter (Collider col)
	{
		PlayerLandedOn();
		
		if(col.gameObject.tag == "Player") 
			GameManager.manager.PlayerDied();
	}
}
