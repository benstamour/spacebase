using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for the collectible orbs
public class Collectible : MonoBehaviour
{
	[SerializeField] private int id = -1; // unique ID of collectible
	private int rotation = 0;
	private float sum = 0f;
	private GameManager gameManagerScript;
	private ArenaManager arenaManagerScript;
	//private int id = -1;
	
	// Start is called before the first frame update
    void Start()
    {
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.arenaManagerScript = GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
		
		for(int i = 0; i < this.arenaManagerScript.orbs.Length; i++)
		{
			if(gameObject.transform.parent.position == this.arenaManagerScript.orbs[i].transform.position)
			{
				this.id = i;
				break;
			}
		}
		//Debug.Log(this.id);
		
		//this.id = this.arenaManagerScript.curOrbID; // unique ID to identify which orbs have been collected
		//this.arenaManagerScript.incrementCurOrbID(); // ensure no other orb has same ID
		if(this.gameManagerScript.getOrbCollected(this.id)) // if orb has already been collected by player, remove it
		{
			gameObject.transform.parent.gameObject.SetActive(false);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		// handles rotation of the orb; repeats a sequence of two slow rotations followed by two fast rotations
		if(rotation == 0)
		{
			this.transform.parent.Rotate(15*Time.fixedDeltaTime,60*Time.fixedDeltaTime,15*Time.fixedDeltaTime, Space.World);
			this.sum += 60*Time.fixedDeltaTime;
			if(this.sum >= 720)
			{
				rotation = 1;
				this.sum = 0;
			}
		}
		else
		{
			this.transform.parent.Rotate(45*Time.fixedDeltaTime,180*Time.fixedDeltaTime,45*Time.fixedDeltaTime, Space.World);
			this.sum += 180*Time.fixedDeltaTime;
			if(this.sum >= 720)
			{
				rotation = 0;
				this.sum = 0;
			}
		}
		
	}
	
	// when character collides with orbs, increment their score and set the orb object to inactive
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Character")
		{
			try
			{
				this.gameManagerScript.PlayOrbClip();
			}
			catch
			{
			}
			Character characterScript = other.gameObject.GetComponent<Character>();
			characterScript.IncrementScore();
			this.arenaManagerScript.setOrbCollected(this.id, true);
			this.transform.parent.gameObject.SetActive(false);
		}
	}
	
	public void setID(int id)
	{
		this.id = id;
	}
	public int getID()
	{
		return this.id;
	}
}
