using UnityEngine;
using System.Collections;

public class PreSimulateParticles : MonoBehaviour 
{
	public float amount;
	
	void Start()
	{
		GetComponent<ParticleSystem>().Simulate(Time.time + amount);
		GetComponent<ParticleSystem>().Play();
	}
}
