using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	public static Player player;

	[HideInInspector]
	public bool canFall;

	private bool lookingRight;

	private bool dead;

	private SpriteRenderer spriteRenderer;

	private Sprite playerSprite;
	public Sprite fallSprite;
	
	public AudioClip[] jumpSounds;
	public AudioClip[] landSounds;
	public AudioClip deathSound;

	public float blinkTime;

	public int blinkChance;

	private bool canBlink;

	private Animator animator;
	
	void Start() 
	{
		player = this;

		spriteRenderer = GetComponent<SpriteRenderer>(); 
		animator = GetComponent<Animator>();

		canFall = true;

		canBlink = true;

		dead = false;

		lookingRight = false;

		playerSprite = spriteRenderer.sprite;
		
		//Make an empty gameobject and make it the parent of the player
		//Used for animation
		GameObject parent = new GameObject("PlayerParent");
		transform.parent = parent.transform;

		Time.timeScale = 0.2f;
	}
	
	public void Died()
	{
		Camera.main.GetComponent<AudioSource>().PlayOneShot(deathSound, 0.5f);

		tag = "PlayerDead";
		dead = true;

		gameObject.AddComponent<SpinOffScreen>();
	}
	
	void Flip()
	{
		Vector3 scale = transform.localScale;
		scale.x *=-1;
		transform.localScale = scale;

		lookingRight = !lookingRight;
	}
	
	void FallRight()
	{
		Fall();
		GetComponent<Animation>().Stop();
		GetComponent<Animation>().Play("Player_FallRight");
		
		if(!lookingRight)
			Flip();
		
		GameManager.manager.RightTransition();
	}
	
	void FallLeft()
	{
		//animation.Stop();
		//animation.Play("Player_FallLeft");
		
		Fall();
		animator.SetTrigger("FallingLeft");

		
		if(lookingRight) 
			Flip();
		
		GameManager.manager.LeftTransition();
	}
	
	void Fall()
	{
		AudioClip jumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];
		Camera.main.GetComponent<AudioSource>().PlayOneShot(jumpSound);

		canFall = false;

		spriteRenderer.sprite = fallSprite;
	}
	
	//Landed on ground, falling animation done
	void Landed()
	{
		//Play animation wih respect to the parent, so the parent to player's position
		//transform.parent.transform.position = transform.position;

		GameObject ground = GameObject.FindGameObjectWithTag("GroundMiddle");
		ground.SendMessage("LandedOn");
		
		GroundPound pound = GameObject.FindGameObjectWithTag("GroundPound").GetComponent<GroundPound>();
		pound.BeginPound();
		pound.transform.parent = ground.transform;

		AudioClip landSound = landSounds[Random.Range(0, landSounds.Length)];
		Camera.main.GetComponent<AudioSource>().PlayOneShot(landSound, 0.7f);

		canFall = true;

		spriteRenderer.sprite = playerSprite;
	}

	void CanBlinkAgain()
	{
		canBlink = true;
	}

	void Update()
	{
		if(dead || !GameManager.manager.canStart && Random.Range(0, blinkChance) == 0)
			return;

		if(canBlink && canFall)
		{
			canBlink = false;
			//animation.Play("Player_Blink");

			Invoke("CanBlinkAgain", blinkTime);
		}
		
		if(canFall)
		{
			if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Vector2 touchpos = Input.GetTouch(0).position;
				
				//Normalize touch positions
				touchpos.x /= Screen.width;
				touchpos.y /= Screen.height;
				
				if(touchpos.x <= 0.5f)
					FallLeft();
				else
					FallRight();
			}
			if(Input.GetKey(KeyCode.A))
			{
				FallLeft();
			}
			else if(Input.GetKey(KeyCode.D))
			{
				FallRight();
			}
		}
	}
}