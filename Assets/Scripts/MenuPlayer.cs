using UnityEngine;
using System.Collections;

public class MenuPlayer : MonoBehaviour 
{
	public void AnimationDone()
	{
		GetComponent<Animator>().enabled = false;
		GameObject.FindGameObjectWithTag("Finish").GetComponent<Animation>().Play(PlayMode.StopAll);
		
		Invoke("GoToGame", 3);
	}
	
	public void GoToGame()
	{
		Application.LoadLevel("game");
	}
}
