using UnityEngine;
using System.Collections;

public class NoMoreSnow : PassPoint 
{
	protected override void PassedPoint()
	{
		foreach(GameObject snow in GameObject.FindGameObjectsWithTag("SnowParticle"))
		{
			snow.GetComponent<ParticleSystem>().Stop();
		}
	}
}
