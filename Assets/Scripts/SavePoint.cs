using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for save points
public class SavePoint : MonoBehaviour
{
	[SerializeField] private int id; // unique ID of save point
	[SerializeField] private float yRot; // rotation of character when spawning at save point location
	private GameManager gameManagerScript;
	private ArenaManager arenaManagerScript;
	private int rotation = 0;
	private float sum = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.arenaManagerScript = GameObject.Find("ArenaManager").GetComponent<ArenaManager>();
    }

    // Update is called once per frame
    void Update()
    {
		
	}
	
	void FixedUpdate()
	{
        // handles rotation of the orb; repeats a sequence of two slow rotations followed by two fast rotations
		if(this.rotation == 0)
		{
			this.transform.parent.Rotate(15*Time.fixedDeltaTime,60*Time.fixedDeltaTime,15*Time.fixedDeltaTime, Space.World);
			this.sum += 60*Time.fixedDeltaTime;
			if(this.sum >= 720)
			{
				this.rotation = 1;
				this.sum = 0;
			}
		}
		else
		{
			this.transform.parent.Rotate(45*Time.fixedDeltaTime,180*Time.fixedDeltaTime,45*Time.fixedDeltaTime, Space.World);
			this.sum += 180*Time.fixedDeltaTime;
			if(this.sum >= 720)
			{
				this.rotation = 0;
				this.sum = 0;
			}
		}
    }
	
	// when character collides with save point, set new spawn point
	void OnTriggerEnter(Collider other)
	{
		// when player reaches save point, set this to be their new spawn location and remove save point
		if(other.gameObject.tag == "Character")
		{
			try
			{
				this.gameManagerScript.PlayOrbClip();
			}
			catch
			{
			}
			//Debug.Log("SavePoint " + this.id.ToString() + " " + this.yRot.ToString());
			this.gameManagerScript.setSpawnPoint(this.id);
			this.gameManagerScript.setSpawnRotation(this.yRot);
			this.gameManagerScript.setOrbsCollected(this.arenaManagerScript.getOrbsCollected());
			this.transform.parent.gameObject.SetActive(false);
		}
	}
}
