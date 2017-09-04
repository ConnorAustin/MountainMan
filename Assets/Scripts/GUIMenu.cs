using UnityEngine;
using System.Collections;

public class GUIMenu : MonoBehaviour 
{
	public GUISkin menuSkin;
	
	public int virtualScreenWidth;
	public int virtualScreenHeight;
	
	public Texture2D startButtonTexture;
	
	public Texture2D bubblePop;
	
	private bool startPressed = false;
	
	
	void PopBubbles()
	{
		foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("Bubble"))
		{
			bubble.GetComponent<Renderer>().material.SetTexture("_MainTex", bubblePop);
			
			GameObject.Destroy(bubble.transform.Find("DreamWorld").gameObject);
			
			Invoke("KillBubbles", 0.17f);
		}
	}
	
	void KillBubbles()
	{
		foreach(GameObject bubble in GameObject.FindGameObjectsWithTag("Bubble"))
		{
			GameObject.Destroy(bubble);
		}
	}
	
	void OnGUI()
	{
		GUI.skin = menuSkin;
		
		//Start button
		if(!startPressed)
		{
			int width = startButtonTexture.width * 2;
			int height = startButtonTexture.height * 2;
			
			startPressed = GUI.Button(new Rect((Screen.width / 2.0f) - (width / 2), 
											   (Screen.height / 2.0f) - (height / 2), 
				            		           width, height), "");
			if(startPressed)
			{
				GameObject player = GameObject.FindGameObjectWithTag("Player");
				player.GetComponent<Animator>().SetBool("PlayerAwake", true);
				
				PopBubbles();
			}
		}
	}
}
