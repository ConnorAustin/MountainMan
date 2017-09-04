using UnityEngine;
using System.Collections;

public class PassPoint : MonoBehaviour
{
	protected virtual void PassedPoint()
	{
		GameObject.DestroyObject(gameObject);
	}
	
	void Update () 
	{
		if(Player.player.transform.position.y < transform.position.y)
		{
			PassedPoint();
		}
	}
}
