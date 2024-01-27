using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script to spawn character chosen by player with corresponding spawn point
public class ArenaManager : MonoBehaviour
{
	// character prefabs
	public GameObject[] charPrefabs;
	/*public GameObject serazPrefab;
	public GameObject aestaPrefab;
	public GameObject gavaanPrefab;
	public GameObject xaleriePrefab;*/
	
	// orb prefabs
	public GameObject[] orbPrefabs;
	/*public GameObject blueOrbPrefab;
	public GameObject pinkOrbPrefab;
	public GameObject greenOrbPrefab;
	public GameObject purpleOrbPrefab;*/
	
	private GameManager gameManagerScript;
	[SerializeField] private int character;
	
	// start position
	[SerializeField] private float startX;
	[SerializeField] private float startY;
	[SerializeField] private float startZ;
	[SerializeField] private float startRot;
	
	// start location for testing purposes
	[SerializeField] private bool useTestLoc;
	[SerializeField] private float testX;
	[SerializeField] private float testY;
	[SerializeField] private float testZ;
	[SerializeField] private float testRot;
	
	public GameObject[] savePoints = new GameObject[6]; // save point array
	public GameObject[] orbs = new GameObject[6]; // orb array
	[SerializeField] private Vector3[] orbLocations; // locations of each orb
	[SerializeField] private bool[] orbsCollected = new bool[6]; // which orbs are collected by the player?
	
	public Canvas pauseScreen; // pause screen
	private bool paused = false; // is the game paused?
	
	[SerializeField] private GameObject[] rooms;
	
	//public int curOrbID = 0; // for instantiating orbs to ensure each has unique ID
	
	void Awake()
	{
		Time.timeScale = 1;
		
		this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.gameManagerScript.getOrbsCollected().CopyTo(this.orbsCollected,0); // restore orb data from save points
		/*for(int i=0; i<this.orbsCollected.Length; i++)
		{
			Debug.Log(this.orbsCollected[i]);
		}*/
		
		Vector3 startLoc = Vector3.zero; // player's starting location
		float yRot = 0; // initial rotation of character
		//Debug.Log("ArenaManager: SpawnPoint " + gameManagerScript.getSpawnPoint().ToString());
		
		// if character has not reached a save point
		if(gameManagerScript.getSpawnPoint() == -1)
		{
			// location that character begins at
			if(useTestLoc)
			{
				startLoc = new Vector3(testX, testY, testZ);
				yRot = testRot;
			}
			else
			{
				startLoc = new Vector3(startX, startY, startZ);
				yRot = startRot;
			}
			
			if(gameManagerScript.getSavePointsEnabled() == false) // if save points are turned off, remove them
			{
				GameObject savePoints = GameObject.Find("Save Points");
				savePoints.SetActive(false);
			}
			
			if(gameManagerScript.getCurArenaMusic() > 0)
			{
				gameManagerScript.triggerIntroMusic();
			}
		}
		else
		{
			// set start location according to last save point reached and remove all previous save points
			int spawnID = gameManagerScript.getSpawnPoint();
			startLoc = this.savePoints[spawnID].transform.position + Vector3.down;//*1.995f;
			yRot = gameManagerScript.getSpawnRotation();
			for(int i = 0; i <= spawnID; i++)
			{
				this.savePoints[i].SetActive(false);
			}
			
			if(gameManagerScript.getCurArenaMusic() > 0 && spawnID < 1) // second arena soundtrack starts at save point 1
			{
				gameManagerScript.triggerIntroMusic();
			}
			else if(gameManagerScript.getCurArenaMusic() > 1 && spawnID < 2) // third arena soundtrack starts at save point 2
			{
				gameManagerScript.triggerNeonMusic();
			}
			else if(gameManagerScript.getCurArenaMusic() > 2 && spawnID < 4) // fourth arena soundtrack starts at save point 4
			{
				gameManagerScript.triggerHoloMusic();
			}
			else if(gameManagerScript.getCurArenaMusic() > 3 && spawnID < 5) // fifth arena soundtrack starts at save point 5
			{
				gameManagerScript.triggerCircuitMusic();
			}
			else if(gameManagerScript.getCurArenaMusic() > 4) // sixth arena soundtrack starts after last save point
			{
				gameManagerScript.triggerWarehouseMusic();
			}
		}
		
		// this block should only be executed during testing
		/*var charList = new List<string>{"Gavaan","Xalerie","Seraz","Aesta"};
		int index = Random.Range(0, charList.Count);
		this.character = charList[index];*/
			
        try
		{
			//this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
			
			// gets chosen character from game manager
			this.character = gameManagerScript.getChar();
		}
		catch
		{
			// this block should only be executed during testing
			this.character = Random.Range(0, charPrefabs.Length);
		}
		
		// instantiates chosen character at starting location
		GameObject charPrefab;
		GameObject orbPrefab;
		charPrefab = charPrefabs[this.character];
		orbPrefab = orbPrefabs[this.character];
		/*switch(this.character)
		{
			case "Seraz":
			{
				charPrefab = serazPrefab;
				orbPrefab = greenOrbPrefab;
				break;
			}
			case "Aesta":
			{
				charPrefab = aestaPrefab;
				orbPrefab = purpleOrbPrefab;
				break;
			}
			case "Gavaan":
			{
				charPrefab = gavaanPrefab;
				orbPrefab = blueOrbPrefab;
				break;
			}
			case "Xalerie":
			{
				charPrefab = xaleriePrefab;
				orbPrefab = pinkOrbPrefab;
				break;
			}
			default:
			{
				charPrefab = gavaanPrefab;
				orbPrefab = blueOrbPrefab;
				break;
			}
		}*/
		//foreach(GameObject orb in orbs)
		
		
		for(int i = 0; i < this.orbs.Length; i++)
		{
			GameObject orb = this.orbs[i];
			//Transform transform = orb.transform;
			Vector3 position = orb.transform.position;
			this.orbs[i] = Instantiate(orbPrefab, position, Quaternion.identity);
			//GameObject newOrb = Create(orbPrefab, position, i);
			//Collectible newOrbScript = newOrb.GetComponent<Collectible>();
			//newOrbScript.setID(i);
			orb.SetActive(false);
		}
		/*for(int i = 0; i < this.orbLocations.Length; i++)
		{
			Vector3 position = orbLocations[i];
			this.orbs[i] = Instantiate(orbPrefab, position, Quaternion.identity);
			this.orbs[i].transform.localScale = new Vector3(1,1,1)*0.667f;
		}*/
		Instantiate(charPrefab, startLoc, Quaternion.Euler(0,yRot,0));
	}
	
    // Start is called before the first frame update
    void Start()
    {
		int spawnPoint = gameManagerScript.getSpawnPoint();
		if(spawnPoint < 1)
		{
			// open door after spikeball area
			GameObject door = GameObject.Find("Second Door");
			Animator anim = door.GetComponent<Animator>();
			anim.SetBool("character_nearby", true);
		}
		if(spawnPoint == 4) // this also needs to open when elevator shaft is unhidden
		{
			// open door to elevator
			GameObject door = GameObject.Find("Seventh Door");
			Animator anim = door.GetComponent<Animator>();
			anim.SetBool("character_nearby", true);
		}
		
		if(spawnPoint < 2)
		{
			for(int i = 8; i < rooms.Length; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		else if(spawnPoint == 7)
		{
			for(int i = 0; i <= 10; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		else if(spawnPoint == 6)
		{
			for(int i = 0; i <= 9; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		else if(spawnPoint == 5)
		{
			for(int i = 0; i <= 7; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		else if(spawnPoint == 4)
		{
			for(int i = 0; i <= 2; i++)
			{
				rooms[i].SetActive(false);
			}
			for(int i = 10; i < rooms.Length; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		else // spawnPoint == 3
		{
			for(int i = 0; i <= 1; i++)
			{
				rooms[i].SetActive(false);
			}
			for(int i = 10; i < rooms.Length; i++)
			{
				rooms[i].SetActive(false);
			}
		}
		
		// 0: before swinging spikeballs
		// 1: before LED platforms
		// 2: before spinning laser circle/holotiles
		// 3: before gravity switch
		// 4: before circuit tiles
		// 5: before saws
		// 6: before warehouse
		// 7: before final room
		
		
		// disable pause screen and make sure scene is not paused
		this.pauseScreen.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
		// pause/unpause game
        if (Input.GetKeyDown("p"))
        {
			if(this.paused == false)
			{
				Time.timeScale = 0;
				this.pauseScreen.enabled = true;
				this.paused = true;
			}
			else
			{
				Time.timeScale = 1;
				this.pauseScreen.enabled = false;
				this.paused = false;
			}
        }
    }
	
	// resume game after being paused
	public void ResumeButton()
	{
		this.gameManagerScript.PlayButtonClip();
		Time.timeScale = 1;
		this.pauseScreen.enabled = false;
		this.paused = false;
	}
	
	// ensures each orb ID is unique
	/*public void incrementCurOrbID()
	{
		//Debug.Log(this.curOrbID);
		this.curOrbID++;
	}*/
	
	// get/set info on which orbs are collected
	public bool[] getOrbsCollected()
	{
		return this.orbsCollected;
	}
	public void setOrbCollected(int id, bool b)
	{
		this.orbsCollected[id] = b;
	}
}
