using UnityEngine;
using System.Collections;

public class GUIGame : MonoBehaviour 
{
	public float retryW = 120;
	public float retryH = 60;
	
	public float timerWidth;
	public float timerHeight;
	
	public GUISkin gameGUISkin;
	
	public Texture2D gameOverTexture;
	
	public Texture2D timerTexture;
	
	public Texture2D fadeInTexture;
	
	public float fadeSpeed;
	
	private float curFadeAlpha;
	
	void Update()
	{
		curFadeAlpha -= Time.deltaTime * fadeSpeed;
		if(curFadeAlpha <= 0)
		{
			GameManager.manager.canStart = true;
			curFadeAlpha = 0;
		}
	}
	
	void Start()
	{
		if(GameManager.manager.firstGamePlayed)
			curFadeAlpha = 1;
		else curFadeAlpha = 0;
		
		StartCoroutine(WaitForFrameEnd());
	}
	
	IEnumerator WaitForFrameEnd()
	{
		while(true)
		{
			int prevCullMask = Camera.main.cullingMask;
			Camera.main.cullingMask = 1 << LayerMask.NameToLayer("PlayerLayer");
			
			CameraClearFlags prevClearFlags = Camera.main.clearFlags;
			Camera.main.clearFlags = CameraClearFlags.Nothing;
			
			Camera.main.Render();
			
			//Restore previous camera state
			Camera.main.cullingMask = prevCullMask;
			Camera.main.clearFlags = prevClearFlags;
			
			yield return new WaitForEndOfFrame();
		}
	}
	
	void OnGUI()
	{
		GUI.skin = gameGUISkin;
		
		if(GameManager.manager.playerDead)
		{
			//Retry button
			if(GUI.Button(new Rect((Screen.width / 2) - (retryW / 2.0f), 
						           (Screen.height / 2) - (retryH / 2.0f) + 90, retryW, retryH), ""))
			{
				Application.LoadLevel("game");
			}
		}
		//Timer
		else
		{ 
			//Rounds it to the nearest .25
			float fallRound = Mathf.Ceil(GameManager.manager.fallTime * 4) / 4;
			
			//Move up since it's the top coord, not the bottom
			fallRound -= 0.25f;
			
			//Can't have it going past
			fallRound = Mathf.Clamp01(fallRound);
			
			GUI.DrawTextureWithTexCoords(
				new Rect((Screen.width / 2.0f) - (timerWidth / 2.0f), 40, timerWidth,  timerHeight),
				 		  timerTexture, 
						  //The tex-coords are normalized at the bottom-left
			       		  new Rect(0.0f, fallRound, 1.0f, 0.25f));
			
		}
		
		GUI.color = new Color(1.0f, 1.0f, 1.0f, curFadeAlpha);
		
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeInTexture);
		
		GUI.color = Color.white;
	}
}
