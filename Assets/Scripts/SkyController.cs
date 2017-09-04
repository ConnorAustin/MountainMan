using UnityEngine;
using System.Collections;

public class SkyController : MonoBehaviour
{
	public float yScrollScaleOffset = 1.0f;
	private Vector3 lastPlayerPos;
	
	void LateUpdate()
	{
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if(player)
		{
			float playerYDif = lastPlayerPos.y - player.transform.position.y;
			
			//Since the sky is parented to the camera and the camera follows the player
			//To make the sky slowly go up, we get the difference in the player's pos and
			//Then multiply by how much we want it to move up
			Vector3 pos = transform.position;
			pos.y += (playerYDif * yScrollScaleOffset) * GameManager.manager.skyMove;
			transform.position = pos;
			
			lastPlayerPos = player.transform.position;
		}
	}
}
