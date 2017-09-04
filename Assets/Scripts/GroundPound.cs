using UnityEngine;
using System.Collections;

public class GroundPound : MonoBehaviour 
{
	public float scaleSpeed;
		
	public Vector3 maxScale;
	
	private Vector3 startScale;
	
	private float scaleLerp;

	private Vector3 playerOffset;
	
	void Start () 
	{
		startScale = transform.localScale;
		scaleLerp = 0;
		GetComponent<Renderer>().enabled = false;

		playerOffset = transform.position - GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	public void BeginPound()
	{
		scaleLerp = 0;
		GetComponent<Renderer>().enabled = true;
		transform.position = GameObject.FindGameObjectWithTag("Player").transform.position + playerOffset;
	}
	
	void Update () 
	{
		if(GetComponent<Renderer>().enabled)
		{
			scaleLerp += Time.deltaTime * scaleSpeed;

			transform.localScale = Vector3.Lerp(startScale, maxScale, scaleLerp);
			
			GetComponent<Renderer>().material.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), scaleLerp - 0.3f);

			if(GetComponent<Renderer>().material.color.a == 0)
				GetComponent<Renderer>().enabled = false;
		}
	}
}
