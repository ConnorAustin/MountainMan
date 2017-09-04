using UnityEngine;
using System.Collections;

public class CameraCenterize : MonoBehaviour 
{
	public float yOffset;
	
	//The starting position when lerping
	private Vector3 lerpStartPos;
	
	//Current lerp value
	private float lerpToGround;
	
	//The Y of the middle ground we are centerizing the view to
	private float groundY;	
	
	//The speed of the lerp
	private static float lerpToGroundSpeed = 1.3f;
	
	void Start () 
	{
		lerpStartPos = Vector3.zero;
		lerpToGround = 0.0f;
		
		groundY = -9001;
	}
	
	void Update () 
	{
		GameObject groundMiddle = GameObject.FindGameObjectWithTag("GroundMiddle");
		if(groundMiddle)
		{
			Vector3 groundPos = groundMiddle.transform.position;
			if(groundPos.y != groundY)
			{
				groundY = groundMiddle.transform.position.y;
				lerpToGround = 0;
				lerpStartPos = transform.position;
			}
			
			//Don't want the camera zooming in
			groundPos.z = transform.position.z;
				
			//Offset y from center
			groundPos.y += yOffset;
			
			if(groundPos != transform.position)
			{
				lerpToGround += lerpToGroundSpeed * Time.smoothDeltaTime;
				
				transform.position = Vector3.Lerp(lerpStartPos, groundPos, lerpToGround);
			}
		}
	}
}
