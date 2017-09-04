using UnityEngine;
using System.Collections;

public class StatController : MonoBehaviour 
{
	public float timeUntilTransition;
	
	private Vector3 lerpStart;
	private float lerp;
	
	void Start() 
	{
		Invoke("StartTransition", timeUntilTransition);
	}
	
	void StartTransition()
	{
		transform.Find("Text").GetComponent<TextMesh>().text = "You fell less than the height of a four-story building.";
		GetComponent<Animation>().Play(PlayMode.StopAll);
	}
}
