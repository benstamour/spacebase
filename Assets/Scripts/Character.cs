using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// script for controlling character
public class Character : MonoBehaviour
{
	// for character movement
	private CharacterController characterController;
	private CapsuleCollider capsuleCollider;
	private float speed = 5f;
	[SerializeField] private Vector3 playerVelocity;
	[SerializeField] private bool groundedPlayer;
	private float jumpHeight = 0.5f;
	public float gravityValue = -9.81f;
	private float turnSpeed = 1.5f;
	[SerializeField] private Vector3 rotation;
	[SerializeField] private bool isActive = true; // should the character be allowed to move? (false when player reaches end zone)
	[SerializeField] private float scaleVal = 1.5f; // different character prefabs have different collider values due to scaling
	private float multiplier; // scaled height of character relative to scale of 1.5
	[SerializeField] public bool onAirVent = false;
	
	// game data
	[SerializeField] private int score = 0; // number of orbs collected since last save point
	[SerializeField] private float startTime; // time passed since last save point
	private GameManager gameManagerScript;
	
	// data for animations
	public Animator animator;
	private bool isJumping = false;
	private bool isWalkingForward = false;
	private bool isWalkingBackward = false;
	private bool isTurningLeft = false;
	private bool isTurningRight = false;
	
	//private int changingController = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
		capsuleCollider = GetComponent<CapsuleCollider>();
		this.startTime = Time.time;
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.gameManagerScript.incrementAttempts();
		this.multiplier = this.scaleVal/1.5f;
    }

    // Update is called once per frame
    void Update()
    {
		groundedPlayer = characterController.isGrounded;
        if(groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
		
		if(this.isActive)
		{
			// character X/Z movement
			this.rotation = new Vector3(0, Input.GetAxisRaw("Horizontal") * turnSpeed*100 * Time.deltaTime, 0);
 
			Vector3 move = new Vector3(0, 0, Input.GetAxisRaw("Vertical") * Time.deltaTime);
			
			move = this.transform.TransformDirection(move);
			characterController.Move(move * speed);
			this.transform.Rotate(this.rotation);
			
			// character Y movement/jumping
			/*if(animator.IsInTransition(0))
			{
				Debug.Log(AnimPlaying("Idle2").ToString() + AnimPlaying("Walking2").ToString() + AnimPlayingTag("jump").ToString() + animator.GetBool("isJumping").ToString());
			}*/
			if(Input.GetButton("Jump") && groundedPlayer)// && !animator.GetBool("isJumping"))// && !(animator.IsInTransition(0) && AnimPlaying("Jumping")))
			{
				playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
				
				if(jumpHeight * -3.0f * gravityValue >= 0)
				{
					this.isJumping = true; // if character is moving upwards, show jump animation
					//animator.SetBool("isIdle", false);
				}
				//anim.CrossFade("Jump");
			}
			// setting variables for animations
			else if(groundedPlayer)
			{
				this.isJumping = false;
			}
			
			if(Input.GetAxisRaw("Horizontal") * turnSpeed*100 * Time.deltaTime < 0)
			{
				this.isTurningLeft = true;
				this.isTurningRight = false;
			}
			else if(Input.GetAxisRaw("Horizontal") * turnSpeed*100 * Time.deltaTime > 0)
			{
				this.isTurningLeft = false;
				this.isTurningRight = true;
			}
			else
			{
				this.isTurningLeft = false;
				this.isTurningRight = false;
			}
			
			if(Input.GetAxisRaw("Vertical") > 0)
			{
				this.isWalkingForward = true;
				this.isWalkingBackward = false;
			}
			else if(Input.GetAxisRaw("Vertical") < 0)
			{
				this.isWalkingForward = false;
				this.isWalkingBackward = true;
			}
			else
			{
				this.isWalkingForward = false;
				this.isWalkingBackward = false;
			}
			
			animator.SetBool("isJumping", false);
			animator.SetBool("isWalkingForward", false);
			animator.SetBool("isWalkingBackward", false);
			animator.SetBool("isTurningLeft", false);
			animator.SetBool("isTurningRight", false);
			animator.SetBool("isIdle", false);
			// deciding which animation will be played
			// jumping animation takes priority, followed by walking forward/backward,
			// followed by turning left/right, followed by idle
			if(this.isJumping)
			{
				animator.SetBool("isJumping", true);
			}
			else if(this.isWalkingForward)
			{
				animator.SetBool("isWalkingForward", true);
			}
			else if(this.isWalkingBackward)
			{
				animator.SetBool("isWalkingBackward", true);
			}
			else if(this.isTurningLeft)
			{
				animator.SetBool("isTurningLeft", true);
			}
			else if(this.isTurningRight)
			{
				animator.SetBool("isTurningRight", true);
			}
			else
			{
				animator.SetBool("isIdle", true);
			}
			
			playerVelocity.y += gravityValue*Time.deltaTime;
			if(onAirVent)
			{
				playerVelocity = Vector3.zero;
				if(this.isJumping)
				{
					this.isJumping = false;
					animator.SetBool("isJumping", false);
					animator.SetBool("isIdle", true);
					StartCoroutine(ChangeCharController(0));
				}
			}
			if(this.isJumping && playerVelocity.y < 0)
			{
				animator.SetBool("isJumping", false);
				animator.SetBool("isIdle", true);
				StartCoroutine(ChangeCharController(0));
				//this.characterController.height = 1.6f;
				//this.characterController.center = new Vector3(0f, 0.8f, 0f);
			}
			else if(this.isJumping)
			{
				StartCoroutine(ChangeCharController(1));
				//this.characterController.height = 1f;
				//this.characterController.center = new Vector3(0f, 1f, 0f);
			}
			characterController.Move(playerVelocity*Time.deltaTime);
		}
		else
		{
			characterController.Move(Vector3.zero);
		}
    }
	
	// triggered when player enters end zone or save point
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "EndZone")
		{
			this.EndZone();
		}
		else if(other.gameObject.tag == "SavePoint")
		{
			// save time and score data
			this.gameManagerScript.addSavePointTime(Time.time - this.startTime);
			this.gameManagerScript.addSavePointScore(this.score);
			this.score = 0;
			this.startTime = Time.time;
		}
	}
	
	// when player reaches end zone, they are taken to the finish screen
	private void EndZone()
	{
		this.isActive = false;
		
		// if save points are enabled, add score and time from this run to saved data
		if(gameManagerScript.getSavePointsEnabled())
		{
			this.gameManagerScript.addSavePointScore(this.score);
			this.gameManagerScript.addSavePointTime(Time.time - this.startTime);
			//ArenaManager arenaManagerScript = GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
			//this.gameManagerScript.setOrbsCollected(arenaManagerScript.getOrbsCollected());
		}
		else
		{
			float totalTime = Time.time - this.startTime;
			this.gameManagerScript.setScore(this.score);
			//this.gameManagerScript.setTime(totalTime);
		}
		
		//SceneManager.LoadScene("EndScreen");
		this.gameManagerScript.EndZone();
	}
	
	public void IncrementScore()
	{
		this.score++;
	}
	
	private bool AnimPlaying()
	{
		return this.animator.GetCurrentAnimatorStateInfo(0).length > this.animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
	}
	
	// is the character going through a specific animation?
	private bool AnimPlaying(string stateName)
	{
		return AnimPlaying() && this.animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
	}
	private bool AnimPlayingTag(string tagName)
	{
		return AnimPlaying() && this.animator.GetCurrentAnimatorStateInfo(0).IsTag(tagName);
	}
	
	// is the player jumping?
	public bool getJumping()
	{
		return animator.GetBool("isJumping");
	}
	
	// temporarily change character controller and capsule collider size when jumping so that it doesn't block the
	// character from jumping onto higher surfaces
	IEnumerator ChangeCharController(int dir)
	{
		// if character is coming down from a jump
		if(dir == 0)
		{
			float timeToChange = 4f;
			/*float centerChange = -0.2f/timeToChange;
			float heightChange = 0.6f/timeToChange;
			float targetCenter = 0.8f/this.multiplier;
			float targetHeight = 1.6f/this.multiplier;*/
			/*float centerChange = -0.55f/timeToChange;
			float heightChange = 0.4f/timeToChange;
			float targetCenter = 0.8f/this.multiplier;
			float targetHeight = 1.6f/this.multiplier;*/
			float centerChange = -0.45f/timeToChange; // amount to change each time
			float heightChange = 0.35f/timeToChange; // amount to change each time
			float targetCenter = 0.8f/this.multiplier;
			float targetHeight = 1.6f/this.multiplier;
			while(true)
			{
				Vector3 center = this.characterController.center;
				float height = this.characterController.height;
				this.characterController.center = new Vector3(0, this.characterController.center.y + centerChange * Time.deltaTime, 0);
				this.capsuleCollider.center = new Vector3(0, this.capsuleCollider.center.y + centerChange * Time.deltaTime, 0);
				this.characterController.height += heightChange * Time.deltaTime;
				this.capsuleCollider.height += heightChange * Time.deltaTime;
				//Debug.Log("A" + this.characterController.height.ToString() + " " + this.characterController.center.ToString());
				if(Math.Abs(this.characterController.height - targetHeight) <= 0.05 && Math.Abs(this.characterController.center.y - targetCenter) <= 0.05)
				{
					this.characterController.height = targetHeight;
					this.capsuleCollider.height = targetHeight;
					this.characterController.center = new Vector3(0, targetCenter, 0);
					this.capsuleCollider.center = new Vector3(0, targetCenter, 0);
					break;
				}
				yield return null;
			}
		}
		// if character is jumping up
		else
		{
			float timeToChange = 4f;
			/*float centerChange = 0.2f/timeToChange;
			float heightChange = -0.6f/timeToChange;
			float targetCenter = 1.0f/this.multiplier;
			float targetHeight = 1.1f/this.multiplier;*/
			/*float centerChange = 0.55f/timeToChange;
			float heightChange = -0.4f/timeToChange;
			float targetCenter = 1.35f/this.multiplier;
			float targetHeight = 1.2f/this.multiplier;*/
			float centerChange = 0.45f/timeToChange; // amount to change each time
			float heightChange = -0.35f/timeToChange; // amount to change each time
			float targetCenter = 1.25f/this.multiplier;
			float targetHeight = 1.25f/this.multiplier;
			while(true)
			{
				Vector3 center = this.characterController.center;
				float height = this.characterController.height;
				this.characterController.center = new Vector3(0, this.characterController.center.y + centerChange * Time.deltaTime, 0);
				this.capsuleCollider.center = new Vector3(0, this.capsuleCollider.center.y + centerChange * Time.deltaTime, 0);
				this.characterController.height += heightChange * Time.deltaTime;
				this.capsuleCollider.height += heightChange * Time.deltaTime;
				//Debug.Log("B" + this.characterController.height.ToString() + " " + this.characterController.center.ToString());
				if(Math.Abs(this.characterController.height - targetHeight) <= 0.2 && Math.Abs(this.characterController.center.y - targetCenter) <= 0.05)
				{
					this.characterController.height = targetHeight;
					this.capsuleCollider.height = targetHeight;
					this.characterController.center = new Vector3(0, targetCenter, 0);
					this.capsuleCollider.center = new Vector3(0, targetCenter, 0);
					break;
				}
				yield return null;
			}
		}
	}
	
	public void setActive(bool b)
	{
		this.isActive = b;
	}
}
