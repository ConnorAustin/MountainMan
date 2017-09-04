using UnityEngine;
using System.Collections;

public class TextureAnimator : MonoBehaviour 
{	
	public bool loopAnimation;
	
	public bool playAutomatically = true;
	
	public float frameDelay;
	
	public Texture2D[] textures;
	
	//Forgive me.
	private uint _curFrame;
	private uint curFrame
	{
		get { return _curFrame; }
		set 
		{
			_curFrame = value;
			gameObject.GetComponent<Renderer>().material.SetTexture("_MainTex", textures[_curFrame]);
		}
	}
	
	public void StopAnim()
	{
		if(IsInvoking("FrameUpdate"))
			CancelInvoke("FrameUpdate");
	}
	
	public void StartAnim()
	{
		if(!IsInvoking("FrameUpdate"))
			Invoke("FrameUpdate", frameDelay);
	}
	
	public void FrameUpdate()
	{
		if(curFrame + 1 == textures.Length)
		{
			if(loopAnimation)
				curFrame = 0;
			//Stop at last frame if not looping
			else 
			{
				curFrame--;
				
				//Don't call Invoke
				return;
			}
		}
		else curFrame++;
		
		Invoke("FrameUpdate", frameDelay);
	}
	
	void Start () 
	{
		if(playAutomatically)
		{
			Invoke("FrameUpdate", frameDelay);
		}
	}
}
