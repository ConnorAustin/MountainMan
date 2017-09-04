using UnityEngine;
using System.Collections;

public class TextureScroller : MonoBehaviour 
{
	public float uScroll;
	public float vScroll;
	
	public float width = 1.0f;
	public float height = 1.0f;

	void Update () 
	{
		Vector2 offset = GetComponent<Renderer>().material.GetTextureOffset("_MainTex");

		offset.x += uScroll * Time.deltaTime;
		offset.y += vScroll * Time.deltaTime;

		GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
		GetComponent<Renderer>().material.SetTextureScale("_MainTex", new Vector2(width, height));
	}
}
