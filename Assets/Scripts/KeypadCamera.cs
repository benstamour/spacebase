using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeypadCamera : MonoBehaviour
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
	//private GameObject charCamera;
	
	[SerializeField] private GameObject keypadCamera;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		this.character = GameObject.FindWithTag("Character");
		/*foreach(Transform t in this.character.GetComponentsInChildren<Transform>())
		{
			if(t.CompareTag("MainCamera"))
			{
				this.charCamera = t.gameObject;
			}
		}*/
		
		/*foreach(Transform t in gameObject.GetComponentsInChildren<Transform>())
		{
			if(t.CompareTag("MainCamera"))
			{
				this.keypadCamera = t.gameObject;
				this.keypadCamera.SetActive(false);
			}
		}*/
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
			this.character.SetActive(false);
			this.keypadCamera.SetActive(true);
			activated = true;
		}
		else
		{
			this.keypadCamera.SetActive(false);
			this.character.SetActive(true);
			activated = false;
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
		if(distance <= 2f)
		{
			return true;
		}
        else
		{
			return false;
		}
    }
}
