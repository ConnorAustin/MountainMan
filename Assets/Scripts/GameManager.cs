using UnityEngine;
using System.Collections;

public enum Environment : int
{
	Snow = 0,
	Grass = 1
}

public class GameManager : MonoBehaviour 
{
	public static GameManager manager;

	public static byte timesLoadedLevel;
	
	private static Object groundToSpawn;

	private HazardSpawner hazardSpawner;

	private Environment environment;
	
	[HideInInspector]
	public bool playerDead;
	
	[HideInInspector]
	public float fallTime;
	
	[HideInInspector]
	public bool canStart;
	
	[HideInInspector]
	public bool firstGamePlayed = true;
	
	public float skyMove = 0.1f;
	
	public float difficultyIncrease;
	
	public float currentDifficulty;
	
	private bool playerFellOnce = false;
	
	void Start() 
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		
		playerFellOnce = false;
		playerDead = false;
		manager = this;

		environment = Environment.Snow;
		
		groundToSpawn = Resources.Load("Prefabs/Ground");

		hazardSpawner = GetComponent<HazardSpawner>();
	}
	
	void OnLevelWasLoaded(int level)
	{
		timesLoadedLevel++;
		if(timesLoadedLevel == 2)
			firstGamePlayed = false;
	}
	
	void Update()
	{
		if(playerFellOnce && Player.player.canFall)
		{
			fallTime -= currentDifficulty * Time.deltaTime;
			if(fallTime <= 0.0f)
			{
				fallTime = 0.0f;
				PlayerDied();
			}
		}
	}
	
	public void RightTransition()
	{
		//Tell the ground not being landed on to move off the scene
		GameObject.FindGameObjectWithTag("GroundLeft").SendMessage("MoveLeft");
		GameObject.FindGameObjectWithTag("GroundMiddle").SendMessage("MoveLeft");
		
		//Make the right ground the new middle
		GameObject newMiddleGround = GameObject.FindGameObjectWithTag("GroundRight");
		newMiddleGround.tag = "GroundMiddle";
		
	 	PlayerFall();
		MakeNewGround();
	}
	
	public void ChangeEnvironment()
	{
		environment += 1;

		if(environment == Environment.Grass)
		{
			groundToSpawn = Resources.Load("Prefabs/GroundGrass");
		}
	}
	
	public void LeftTransition()
	{
		//Tell the ground not being landed on to move off the scene
		GameObject.FindGameObjectWithTag("GroundRight").SendMessage("MoveRight");
		GameObject.FindGameObjectWithTag("GroundMiddle").SendMessage("MoveRight");
		
		//Make the left ground the new middle
		GameObject newMiddleGround = GameObject.FindGameObjectWithTag("GroundLeft");
		newMiddleGround.tag = "GroundMiddle";
		
		PlayerFall();
		MakeNewGround();
	}
	
	void PlayerFall()
	{
		currentDifficulty += difficultyIncrease;
		fallTime = 1.0f;
		playerFellOnce = true;
	}
	
	//Makes 2 new ground around the middle ground
	private void MakeNewGround()
	{
		Vector3 groundPos;
		
		GameObject groundMiddle = GameObject.FindGameObjectWithTag("GroundMiddle");
		
		//Make new left ground
		GameObject groundLeft = (GameObject)GameObject.Instantiate(groundToSpawn);
		groundLeft.tag = "GroundLeft";
		
		groundPos = groundLeft.transform.position;
		
		//Offset to the left
		groundPos.x = groundMiddle.transform.position.x - 6;
		groundPos.y = groundMiddle.transform.position.y - 40;
		groundPos.z = 0;

		groundLeft.transform.position = groundPos;
		
		//Make new right ground
		GameObject groundRight = (GameObject)GameObject.Instantiate(groundToSpawn);
		groundRight.tag = "GroundRight";
		
		groundPos = groundRight.transform.position;
		
		//Offset to the right
		groundPos.x = groundMiddle.transform.position.x + 6;
		groundPos.y = groundMiddle.transform.position.y - 40;
		groundPos.z = 0;
		
		groundRight.transform.position = groundPos;
		
		CreateHazard();
	}
	
	public void PlayerDied()
	{
		if(!playerDead)
		{
			playerDead = true;
			Player.player.Died();
			
			Camera.main.transform.Find("StatBackground").gameObject.SetActive(true);
		}
	}

	private void CreateHazard()
	{
		GameObject hazard = hazardSpawner.SpawnHazard(environment);

		if(hazard != null)
		{
			string groundTag = Random.Range(0, 2) == 1 ? "GroundLeft" : "GroundRight";
			
			GameObject ground = GameObject.FindGameObjectWithTag(groundTag);

			//Parent hazard to the ground
			hazard.transform.parent = ground.transform.Find("HazardPlace").transform;
			
			hazard.transform.position = hazard.transform.parent.transform.position;
		}
	}
}
