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
    }
	
	public void activate()
	{
		if(activated == false)
		{
			//this.anim.SetTrigger("ButtonPress"); // triggers button animation
			this.anim.SetBool("ButtonTrigger", true); // triggers button animation
			//activated = true;
			
			//this.gameManagerScript.PlayLeverClip();
			
			StartCoroutine(CheckAnim()); // waits for lever animation to complete before triggering effects
		}
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
		Debug.Log(this.anim.GetBool("ButtonTrigger"));
		this.anim.SetBool("ButtonTrigger", false);
	}
}
