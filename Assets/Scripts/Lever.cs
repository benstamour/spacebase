using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    //private InputAction leverAction;
	private bool activated = false;
	
	private Animator anim;
	
	// NearView()
    private float distance;
    private float angleView;
    private Vector3 direction;
	
	private GameManager gameManagerScript;
	
	[SerializeField] private int id = 1;
	
	[SerializeField] private GameObject cyanLasers;
	[SerializeField] private GameObject magentaLasers;
	[SerializeField] private GameObject yellowLasers;

	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		anim.SetBool("LeverUp", true);
		
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	void Update()
    {
		// if the player presses E while near the lever, activate it
        if(Input.GetKeyDown(KeyCode.E) && NearView())
		{
			activate();
		}
    }
	
	public void activate()
	{
		string[] colours = {"Cyan", "Magenta", "Yellow"};
		if(activated == false && (this.id == 1 || GameObject.Find(colours[this.id-2] + " Blocking Laser") == null))
		{
			anim.SetBool("LeverUp", false); // triggers lever animation
			activated = true;
			
			//this.gameManagerScript.PlayLeverClip();
			
			StartCoroutine(CheckAnim()); // waits for lever animation to complete before triggering effects
		}
		/*else
		{
			anim.SetBool("LeverUp", true);
			activated = false;

			if(gameObject.transform.parent.name == "Lever_Prefab")
			{
				Debug.Log("Lever B");
			}
		}*/
	}
	
	public bool getActivated()
	{
		return activated;
	}
	
	// is the player close enough to the lever to activate it?
	private bool NearView()
    {
		GameObject character = GameObject.FindWithTag("Character");
		if(!character)
		{
			return false;
		}
        distance = Vector3.Distance(transform.position, character.transform.position);
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
		yield return new WaitForSeconds(0.5f);
		while(this.anim.GetCurrentAnimatorStateInfo(0).length >
            this.anim.GetCurrentAnimatorStateInfo(0).normalizedTime)
		{
			yield return null;
		}
		
		string[] colours = {"Cyan", "Magenta", "Yellow"};
		GameObject icons = GameObject.Find("Colour Icons");
		GameObject platforms;
		GameObject line;
		if(this.id >= 1 && this.id <= 3)
		{
			GameObject blockingLaser = GameObject.Find(colours[this.id-1] + " Blocking Laser");
			blockingLaser.SetActive(false);
			
			if(this.id == 3)
			{
				this.magentaLasers.SetActive(false);
			}
			
			if(this.id > 1)
			{
				line = GameObject.Find(colours[this.id-2] + " Line");
				foreach(Transform node in line.transform)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.updateOnTimerSpeed(2.0f);
					LEDNodeScript.updateOffTimerSpeed(0f);
				}
				
				platforms = GameObject.Find(colours[this.id-2] + " LED Platforms");
				foreach(Transform platform in platforms.transform)
				{
					Transform square = platform.Find("LED_Square_Example");
					foreach(Transform node in square)
					{
						LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
						LEDNodeScript.updateOnTimerSpeed(2.0f);
						LEDNodeScript.updateOffTimerSpeed(0f);
					}
				}
				
				foreach(Transform icon in icons.transform)
				{
					if(icon.gameObject.name.Contains(colours[this.id-2]))
					{
						foreach(Transform node in icon.transform)
						{
							LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
							LEDNodeScript.updateOnTimerSpeed(2.0f);
							LEDNodeScript.updateOffTimerSpeed(0f);
						}
					}
				}
			}
			
			line = GameObject.Find(colours[this.id-1] + " Line");
			foreach(Transform node in line.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.toggle(true);
			}
			
			platforms = GameObject.Find(colours[this.id-1] + " LED Platforms");
			foreach(Transform platform in platforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.toggle(true);
				}
			}
			
			foreach(Transform icon in icons.transform)
			{
				if(icon.gameObject.name.Contains(colours[this.id-1]))
				{
					foreach(Transform node in icon.transform)
					{
						LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
						LEDNodeScript.toggle(true);
					}
				}
			}
			
			if(this.id == 2)
			{
				yield return new WaitForSeconds(1.5f);
				this.magentaLasers.SetActive(true);
			}
			else if(this.id == 3)
			{
				yield return new WaitForSeconds(1.5f);
				this.yellowLasers.SetActive(true);
			}
		}
		
		/*if(this.id == 1)
		{
			GameObject cyanLine = GameObject.Find("Cyan Line");
			foreach(Transform node in cyanLine.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.toggle(true);
			}
			
			GameObject cyanPlatforms = GameObject.Find("Cyan LED Platforms");
			foreach(Transform platform in cyanPlatforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.toggle(true);
				}
			}
		}
		else if(this.id == 2)
		{
			GameObject cyanLine = GameObject.Find("Cyan Line");
			foreach(Transform node in cyanLine.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.updateOnTimerSpeed(2.0f);
				LEDNodeScript.updateOffTimerSpeed(0f);
			}
			
			GameObject cyanPlatforms = GameObject.Find("Cyan LED Platforms");
			foreach(Transform platform in cyanPlatforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.updateOnTimerSpeed(2.0f);
					LEDNodeScript.updateOffTimerSpeed(0f);
				}
			}
			
			GameObject magentaLine = GameObject.Find("Magenta Line");
			foreach(Transform node in magentaLine.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.toggle(true);
			}
			
			GameObject magentaPlatforms = GameObject.Find("Magenta LED Platforms");
			foreach(Transform platform in magentaPlatforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.toggle(true);
				}
			}
			yield return new WaitForSeconds(1.5f);
			this.magentaLasers.SetActive(true);
		}
		else if(this.id == 3)
		{
			GameObject magentaLine = GameObject.Find("Magenta Line");
			foreach(Transform node in magentaLine.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.updateOnTimerSpeed(2.0f);
				LEDNodeScript.updateOffTimerSpeed(0f);
			}
			
			GameObject magentaPlatforms = GameObject.Find("Magenta LED Platforms");
			foreach(Transform platform in magentaPlatforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.updateOnTimerSpeed(2.0f);
					LEDNodeScript.updateOffTimerSpeed(0f);
				}
			}
			
			this.magentaLasers.SetActive(false);
			
			GameObject yellowLine = GameObject.Find("Yellow Line");
			foreach(Transform node in yellowLine.transform)
			{
				LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
				LEDNodeScript.toggle(true);
			}
			
			GameObject yellowPlatforms = GameObject.Find("Yellow LED Platforms");
			foreach(Transform platform in yellowPlatforms.transform)
			{
				Transform square = platform.Find("LED_Square_Example");
				foreach(Transform node in square)
				{
					LEDNode LEDNodeScript = node.gameObject.GetComponent<LEDNode>();
					LEDNodeScript.toggle(true);
				}
			}
			yield return new WaitForSeconds(1.5f);
			this.yellowLasers.SetActive(true);
		}*/
	}
}
