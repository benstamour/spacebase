using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    //private InputAction leverAction;
	private bool activated = false;
	
	private Animator anim;
	
	// NearView()
    private float distance;
    private float angleView;
    private Vector3 direction;
	
	private GameManager gameManagerScript;
	private GameObject character;
	
	// first button
	[SerializeField] private GameObject[] holonums;
	
	// second button
	[SerializeField] private float risingPlatformDistance = 3.5f;
	private GameObject risingPlatform;
	private bool platformIsRising = false;
	private float risingPlatformSpeed = 1f;
	private float risingPlatformProgress = 0f;
	
	[SerializeField] private int id = 1;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.character = GameObject.FindWithTag("Character");
    }
	
	void Update()
    {
		// if the player presses E while near the lever, activate it
        if (Input.GetKeyDown(KeyCode.E) && NearView())
		{
			activate();
		}
		
		if(this.platformIsRising)
		{
			float change = this.risingPlatformSpeed*Time.deltaTime;
			this.risingPlatform.transform.position = this.risingPlatform.transform.position + new Vector3(0, change, 0);
			this.risingPlatformProgress += change;
			if(this.risingPlatformProgress >= this.risingPlatformDistance)
			{
				this.platformIsRising = false;
			}
		}
    }
	
	public void activate()
	{
		this.anim.SetBool("ButtonTrigger", true); // triggers button animation
		if(this.activated == false)
		{
			//this.activated = true;
			
			//this.gameManagerScript.PlayLeverClip();
		}
		StartCoroutine(CheckAnim()); // waits for lever animation to complete before triggering effects
	}
	
	public bool getActivated()
	{
		return activated;
	}
	
	// is the player close enough to the lever to activate it?
	private bool NearView()
    {
        distance = Vector3.Distance(transform.position, this.character.transform.position);
		if(distance <= 3f)
		{
			return true;
		}
        else
		{
			return false;
		}
    }
	
	IEnumerator CheckAnim()
	{
		// wait for lever animation to complete
		yield return new WaitForSeconds(0.1f);
		while(this.anim.GetCurrentAnimatorStateInfo(0).length >
            this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		//this.anim.ResetTrigger("ButtonPress");
		//Debug.Log(this.anim.GetBool("ButtonTrigger"));
		this.anim.SetBool("ButtonTrigger", false);
		
		if(this.activated == false)
		{
			if(this.id == 1)
			{
				Vector3 numLoc = new Vector3(-1.5f,0,0);
				int code = this.gameManagerScript.getIntroCode();
				int length = code.ToString().Length;
				for(int i = 0; i < length; i++)
				{
					Instantiate(holonums[code%10], numLoc, Quaternion.identity);
					numLoc += new Vector3(2.5f,0,0);
					code = (int)Mathf.Floor(code/10);
					yield return new WaitForSeconds(0.1f);
				}
			}
			else if(this.id == 2)
			{
				this.risingPlatform = GameObject.Find("Rising Platform");
				this.platformIsRising = true;
			}
			
			this.activated = true;
		}
	}
}
