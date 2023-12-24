using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private bool activated = false;
	
	// DOOR
	[SerializeField] GameObject door;
	
	// ENTRY SINK
	[SerializeField] GameObject entryTiles;
	[SerializeField] float sinkSpeed = 1;
	[SerializeField] Vector3 sinkDist = new Vector3(0, -0.5f, 0);
	
	// BATTERING RAM ATTACK
	[SerializeField] GameObject[] batteringRams;
	[SerializeField] int numSmashes = 8;
	[SerializeField] float ramSlowSpeed;
	[SerializeField] float ramFastSpeed;
	[SerializeField] Vector3 ramSlowDist = new Vector3(0, 0, 3);
	[SerializeField] Vector3 ramFastDist = new Vector3(0, 0, 10.5f);
	
	// SWEEPING LASERS ATTACK 1
	[SerializeField] GameObject sweepingLasers1;
	[SerializeField] float lasers1Speed;
	[SerializeField] Vector3 lasers1Dist = new Vector3(0, 0, 42);
	
	// SWEEPING LASERS ATTACK 2
	[SerializeField] GameObject sweepingLasers2;
	[SerializeField] float lasers2Speed;
	[SerializeField] Vector3 lasers2Dist = new Vector3(0, 0, 61);
	
	// SWEEPING LASERS ATTACK 3
	[SerializeField] GameObject sweepingLasers3;
	[SerializeField] float lasers3Speed;
	[SerializeField] Vector3 lasers3Dist = new Vector3(0, 0, 48.25f);
	[SerializeField] GameObject sideLaser;
	[SerializeField] float sideLaserSpeed;
	[SerializeField] Vector3 sideLaserDist = new Vector3(21, 0, 0);
	
	// LASER CIRCLE ATTACK
	[SerializeField] GameObject[] laserCircles;
	[SerializeField] int numCircles = 6;
	
	// COLOUR-CODED SPIKE DROP ATTACK
	[SerializeField] GameObject redSpikes;
	[SerializeField] GameObject blueSpikes;
	[SerializeField] GameObject greenSpikes;
	[SerializeField] float spikeDropSpeed;
	[SerializeField] Vector3 spikeDropDist = new Vector3(0, -7.875f, 0);
	[SerializeField] GameObject colourLights;
	
	// ROBOT SPHERE
	[SerializeField] GameObject robotSphere;
	[SerializeField] GameObject robotSphereClosed;
	[SerializeField] float robotSpeed;
	[SerializeField] Vector3 robotFloorMidDist = new Vector3(0, 1f, 0);
	[SerializeField] Vector3 robotMidCeilingDist = new Vector3(0, 8f, 0);
	[SerializeField] Vector3 robotDisappearDist = new Vector3(0, 5.25f, 0);
	[SerializeField] Vector3 robotClosedDist = new Vector3(0, 4.75f, 0);
	
	// FINAL PLATFORM
	[SerializeField] GameObject portalPlatform;
	[SerializeField] float portalSpeed;
	[SerializeField] Vector3 portalDist = new Vector3(0, 4, 0);
	
	private GameManager gameManagerScript;
	private WaitForSeconds shortWait = new WaitForSeconds(0.5f);
	private WaitForSeconds announceWait = new WaitForSeconds(1.5f);
	private int attackNum = -1;
	private bool attacking = false;
	
	/*private GameObject objectToMove;
	private Vector3 objStartPos;
	private Vector3 objDistance;
	private float objSpeed;
	private bool isMoving = false;
	private bool toBeReversed = false;
	private int orderNum;*/
	
	private int[] attackRams;
	private int[] circleSpawns;
	private string dropSequence;
	
    // Start is called before the first frame update
    void Start()
    {
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		
		generateRamSequence();
		generateLaserCircleSequence();
		generateSpikeDropSequence();
    }

    // Update is called once per frame
    void Update()
    {
        if(!attacking && attackNum != -1)
		{
			StopAllCoroutines();
			Debug.Log("W " + attackNum);
			attacking = true;
			if(attackNum > 0)
			{
				this.gameManagerScript.robotVoicePlay(attackNum);
			}
			if(attackNum == 0)
			{
				StartCoroutine(ramStab(0));
			}
			else if(attackNum == 1)
			{
				Debug.Log("P");
				StartCoroutine(laserAttack1());
			}
			else if(attackNum == 2)
			{
				Debug.Log("Y");
				StartCoroutine(flashLights());
				//StartCoroutine(colourSpikeDrop());
			}
			else if(attackNum == 3)
			{
				StartCoroutine(laserAttack3());
			}
			else if(attackNum == 4)
			{
				StartCoroutine(laserCircleSpawn(0));
			}
			else if(attackNum == 5)
			{
				StartCoroutine(raisePlatform());
			}
		}
		
		/*if(isMoving)
		{
			objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, objStartPos + objDistance, objSpeed*Time.deltaTime);
			
			if((objectToMove.transform.position - (objStartPos + objDistance)).magnitude <= 0.01)
			{
				objectToMove.transform.position = objStartPos + objDistance;
			}
			if(objectToMove.transform.position == objStartPos + objDistance)
			{
				if(toBeReversed)
				{
					objDistance *= -1;
					objStartPos = objectToMove.transform.position;
					toBeReversed = false;
				}
				else
				{
					if(attackNum == 0 && orderNum < numSmashes-1)
					{
						objectToMove = attackRams[orderNum+1];
						objStartPos = objectToMove.transform.position;
						isMoving = true;
						toBeReversed = true;
					}
					else if(attackNum == 2)
					{
						
					}
					else
					{
						isMoving = false;
					}
				}
			}
		}*/
    }
	
	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Character" && activated == false)
		{
			activated = true;
			
			StartCoroutine(CloseDoor());
			StartCoroutine(sinkEntry());
			
			//StartCoroutine(colourSpikeDrop());
			
			/*int[] circleSpawns = new int[numCircles];
			for(int i = 0; i < numCircles; i++)
			{
				int index = UnityEngine.Random.Range(0, laserCircles.Length);
				if(i > 0)
				{
					while(System.Array.IndexOf(circleSpawns, index) > -1)
					{
						index = UnityEngine.Random.Range(0, laserCircles.Length);
					}
				}
				circleSpawns[i] = index;
			}
			
			StartCoroutine(laserCircleSpawn(circleSpawns, 0));*/
			
			//StartCoroutine(laserAttack3());
			
			/*int[] attackRams = new int[numSmashes];
			for(int i = 0; i < numSmashes; i++)
			{
				attackRams[i] = UnityEngine.Random.Range(0, batteringRams.Length);
				if(i > 0)
				{
					while(attackRams[i] == attackRams[i-1])
					{
						attackRams[i] = UnityEngine.Random.Range(0, batteringRams.Length);
					}
				}
			}
			
			StartCoroutine(ramStab(attackRams, 0));*/
		}
	}
	
	private int[] generateRamSequence()
	{
		attackRams = new int[numSmashes];
		for(int i = 0; i < numSmashes; i++)
		{
			attackRams[i] = UnityEngine.Random.Range(0, batteringRams.Length);
			if(i > 0)
			{
				while(attackRams[i] == attackRams[i-1])
				{
					attackRams[i] = UnityEngine.Random.Range(0, batteringRams.Length);
				}
			}
		}
		return attackRams;
	}
	
	private int[] generateLaserCircleSequence()
	{
		circleSpawns = new int[numCircles];
		for(int i = 0; i < numCircles; i++)
		{
			int index = UnityEngine.Random.Range(0, laserCircles.Length);
			if(i > 0)
			{
				while(System.Array.IndexOf(circleSpawns, index) > -1)
				{
					index = UnityEngine.Random.Range(0, laserCircles.Length);
				}
			}
			circleSpawns[i] = index;
		}
		return circleSpawns;
	}
	
	private string generateSpikeDropSequence()
	{
		Debug.Log("I");
		string[] arr = {"R", "R", "B", "B", "G", "G"};
		for(int i = 0; i < arr.Length - 1; i++)
		{
			int num = UnityEngine.Random.Range(i,arr.Length);
			if(i > 0)
			{
				while(arr[num] == arr[i-1])
				{
					Debug.Log("H");
					num = UnityEngine.Random.Range(i,arr.Length);
				}
			}
			string temp = arr[i];
			arr[i] = arr[num];
			arr[num] = temp;
		}
		dropSequence = arr[0] + arr[1] + arr[2] + arr[3] + arr[4];
		Debug.Log("HH");
		return dropSequence;
	}
	
	IEnumerator sinkEntry()
	{
		Vector3 startPos = entryTiles.transform.position;
		StartCoroutine(robotSphereOpen());
		while(true)
		{
			entryTiles.transform.position = Vector3.MoveTowards(entryTiles.transform.position, startPos + sinkDist, sinkSpeed*Time.deltaTime);
			if((entryTiles.transform.position - (startPos + sinkDist)).magnitude <= 0.01)
			{
				entryTiles.transform.position = startPos + sinkDist;
			}
			if(entryTiles.transform.position == startPos + sinkDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		entryTiles.SetActive(false);
	}
	
	IEnumerator CloseDoor()
	{
		float doorMove = -1.5f;
		float doorSpeed = 2f;
		Vector3 doorPos = door.transform.position;
		Vector3 curDoorPos = doorPos;
		while(curDoorPos.x > doorPos.x + doorMove)
		{
			curDoorPos = door.transform.position;
			
			if(curDoorPos.x + doorMove*Time.deltaTime*doorSpeed < doorPos.x + doorMove)
			{
				door.transform.position = new Vector3(doorPos.x + doorMove, doorPos.y, doorPos.z);
			}
			else
			{
				door.transform.position = new Vector3(curDoorPos.x + doorMove*Time.deltaTime*doorSpeed, curDoorPos.y, curDoorPos.z);
			}
			yield return null;
		}
	}
	
	IEnumerator robotSphereOpen()
	{
		RobotSphere robotSphereScript = robotSphere.GetComponent<RobotSphere>();
		robotSphereScript.Activate();
		yield return new WaitForSeconds(2f);
		
		Vector3 startPos = robotSphere.transform.position;
		while(true)
		{
			robotSphere.transform.position = Vector3.MoveTowards(robotSphere.transform.position, startPos + robotFloorMidDist, robotSpeed*Time.deltaTime);
			if((robotSphere.transform.position - (startPos + robotFloorMidDist)).magnitude <= 0.01)
			{
				robotSphere.transform.position = startPos + robotFloorMidDist;
			}
			if(robotSphere.transform.position == startPos + robotFloorMidDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		//robotSphereScript.Play(0);
		this.gameManagerScript.robotVoicePlay(0);
		
		yield return announceWait;
		
		startPos = robotSphere.transform.position;
		while(true)
		{
			robotSphere.transform.position = Vector3.MoveTowards(robotSphere.transform.position, startPos + robotMidCeilingDist, robotSpeed*Time.deltaTime);
			if((robotSphere.transform.position - (startPos + robotMidCeilingDist)).magnitude <= 0.01)
			{
				robotSphere.transform.position = startPos + robotMidCeilingDist;
			}
			if(robotSphere.transform.position == startPos + robotMidCeilingDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		robotSphere.SetActive(false);
		
		robotSphereClosed.SetActive(true);
		
		startPos = robotSphereClosed.transform.position;
		while(true)
		{
			robotSphereClosed.transform.position = Vector3.MoveTowards(robotSphereClosed.transform.position, startPos - robotClosedDist, robotSpeed*Time.deltaTime);
			if((robotSphereClosed.transform.position - (startPos - robotClosedDist)).magnitude <= 0.01)
			{
				robotSphereClosed.transform.position = startPos - robotClosedDist;
				break;
			}
			if(robotSphereClosed.transform.position.y <= startPos.y - robotClosedDist.y)
			{
				robotSphereClosed.transform.position = startPos - robotClosedDist;
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		GameManager gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameManagerScript.triggerBattleMusic();
		
		attackNum = 0;
		
		//StartCoroutine(ramStab(generateRamSequence(), 0));
	}
	
	IEnumerator robotSphereAnnounce(int attacknum)
	{
		Debug.Log("A");
		//RobotSphere robotSphereScript = robotSphereClosed.GetComponent<RobotSphere>();
		
		/*robotSphereClosed.SetActive(true);
		
		RobotSphere robotSphereScript = robotSphereClosed.GetComponent<RobotSphere>();
		
		Vector3 startPos = robotSphereClosed.transform.position;
		Debug.Log(startPos);
		while(true)
		{
			robotSphereClosed.transform.position = Vector3.MoveTowards(robotSphereClosed.transform.position, startPos - robotMidCeilingDist, robotSpeed*Time.deltaTime);
			if((robotSphereClosed.transform.position - (startPos - robotMidCeilingDist)).magnitude <= 0.01)
			{
				robotSphereClosed.transform.position = startPos - robotMidCeilingDist;
				break;
			}
			if(robotSphereClosed.transform.position.y <= startPos.y - robotMidCeilingDist.y)
			{
				robotSphereClosed.transform.position = startPos - robotMidCeilingDist;
				break;
			}
			else
			{
				yield return null;
			}
		}*/
		
		yield return announceWait;
		Debug.Log("B");
		//robotSphereScript.Play(attacknum);
		this.gameManagerScript.robotVoicePlay(attacknum);
		Debug.Log("C");
		yield return announceWait;
		Debug.Log("D");
		if(attacknum == 5)
		{
			StartCoroutine(raisePlatform());
		}
		
		/*Debug.Log("K");
		startPos = robotSphereClosed.transform.position;
		int i = 0;
		while(true)
		{
			i += 1;
			robotSphereClosed.transform.position = Vector3.MoveTowards(robotSphereClosed.transform.position, startPos + robotMidCeilingDist, robotSpeed*Time.deltaTime);
			if((robotSphereClosed.transform.position - (startPos + robotMidCeilingDist)).magnitude <= (robotMidCeilingDist - robotDisappearDist).magnitude) // 0.01)
			{
				robotSphereClosed.transform.position = startPos + robotMidCeilingDist;
				break;
			}
			if(robotSphereClosed.transform.position.y >= startPos.y + robotMidCeilingDist.y)
			{
				robotSphereClosed.transform.position = startPos + robotMidCeilingDist;
				Debug.Log("M " + i);
				break;
			}
			else
			{
				Debug.Log("L " + i);
				yield return null;
			}
		}
		Debug.Log("J");
		robotSphereClosed.SetActive(false);*/
		
		Debug.Log("X");
		if(attacknum == 1)
		{
			StartCoroutine(laserAttack1());
		}
		else if(attacknum == 2)
		{
			Debug.Log("Y");
			StartCoroutine(colourSpikeDrop(0));
		}
		else if(attacknum == 3)
		{
			StartCoroutine(laserAttack3());
		}
		else if(attacknum == 4)
		{
			StartCoroutine(laserCircleSpawn(0));
		}
		Debug.Log("Z");
	}
	
	IEnumerator flashLights()
	{
		Debug.Log("N");
		yield return announceWait;
		
		for(int i = 0; i < dropSequence.Length; i++)
		{
			char colour = dropSequence[i];
			foreach(Transform child in colourLights.transform)
			{
				Light light = child.gameObject.GetComponent<Light>();
				if(colour == 'R')
				{
					light.color = Color.red;
				}
				else if(colour == 'B')
				{
					light.color = Color.blue;
				}
				else
				{
					light.color = Color.green;
				}
			}
			yield return shortWait;
		}
		Debug.Log("E");
		foreach(Transform child in colourLights.transform)
		{
			Light light = child.gameObject.GetComponent<Light>();
			light.color = Color.white;
			light.intensity = 0.5f;
		}
		
		yield return new WaitForSeconds(1);
		
		StartCoroutine(colourSpikeDrop(0));
	}
	
	IEnumerator colourSpikeDrop(int orderNum)
	{
		Debug.Log("D");
		//string dropSequence = generateSpikeDropSequence();
		
		/*for(int i = 0; i < dropSequence.Length; i++)
		{
			char colour = dropSequence[i];
			foreach(Transform child in colourLights.transform)
			{
				Light light = child.gameObject.GetComponent<Light>();
				if(colour == 'R')
				{
					light.color = Color.red;
				}
				else if(colour == 'B')
				{
					light.color = Color.blue;
				}
				else
				{
					light.color = Color.green;
				}
			}
			yield return shortWait;
		}
		Debug.Log("E");
		foreach(Transform child in colourLights.transform)
		{
			Light light = child.gameObject.GetComponent<Light>();
			light.color = Color.white;
			light.intensity = 0.5f;
		}
		
		yield return new WaitForSeconds(1);*/
		
		char colour = dropSequence[orderNum];
		GameObject spikes1;
		GameObject spikes2;
		if(colour == 'R')
		{
			spikes1 = greenSpikes;
			spikes2 = blueSpikes;
		}
		else if(colour == 'B')
		{
			spikes1 = redSpikes;
			spikes2 = greenSpikes;
		}
		else
		{
			spikes1 = redSpikes;
			spikes2 = blueSpikes;
		}
		
		Vector3 startPos = spikes1.transform.position;
		while(true)
		{
			Debug.Log("G");
			spikes1.transform.position = Vector3.MoveTowards(spikes1.transform.position, startPos + spikeDropDist, spikeDropSpeed*Time.deltaTime);
			spikes2.transform.position = Vector3.MoveTowards(spikes2.transform.position, startPos + spikeDropDist, spikeDropSpeed*Time.deltaTime);
			if((spikes1.transform.position - (startPos + spikeDropDist)).magnitude <= 0.01)
			{
				spikes1.transform.position = startPos + spikeDropDist;
				spikes2.transform.position = startPos + spikeDropDist;
			}
			if(spikes1.transform.position == startPos + spikeDropDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		startPos = spikes1.transform.position;
		while(true)
		{
			spikes1.transform.position = Vector3.MoveTowards(spikes1.transform.position, startPos - spikeDropDist, spikeDropSpeed*Time.deltaTime);
			spikes2.transform.position = Vector3.MoveTowards(spikes2.transform.position, startPos - spikeDropDist, spikeDropSpeed*Time.deltaTime);
			if((spikes1.transform.position - (startPos - spikeDropDist)).magnitude <= 0.01)
			{
				spikes1.transform.position = startPos - spikeDropDist;
				spikes2.transform.position = startPos - spikeDropDist;
			}
			if(spikes1.transform.position == startPos - spikeDropDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		/*for(int i = 0; i < dropSequence.Length; i++)
		{
			Debug.Log("F");
			char colour = dropSequence[i];
			GameObject spikes1;
			GameObject spikes2;
			if(colour == 'R')
			{
				spikes1 = greenSpikes;
				spikes2 = blueSpikes;
			}
			else if(colour == 'B')
			{
				spikes1 = redSpikes;
				spikes2 = greenSpikes;
			}
			else
			{
				spikes1 = redSpikes;
				spikes2 = blueSpikes;
			}
			
			Vector3 startPos = spikes1.transform.position;
			while(true)
			{
				Debug.Log("G");
				spikes1.transform.position = Vector3.MoveTowards(spikes1.transform.position, startPos + spikeDropDist, spikeDropSpeed*Time.deltaTime);
				spikes2.transform.position = Vector3.MoveTowards(spikes2.transform.position, startPos + spikeDropDist, spikeDropSpeed*Time.deltaTime);
				if((spikes1.transform.position - (startPos + spikeDropDist)).magnitude <= 0.01)
				{
					spikes1.transform.position = startPos + spikeDropDist;
					spikes2.transform.position = startPos + spikeDropDist;
				}
				if(spikes1.transform.position == startPos + spikeDropDist)
				{
					break;
				}
				else
				{
					yield return null;
				}
			}
			
			startPos = spikes1.transform.position;
			while(true)
			{
				spikes1.transform.position = Vector3.MoveTowards(spikes1.transform.position, startPos - spikeDropDist, spikeDropSpeed*Time.deltaTime);
				spikes2.transform.position = Vector3.MoveTowards(spikes2.transform.position, startPos - spikeDropDist, spikeDropSpeed*Time.deltaTime);
				if((spikes1.transform.position - (startPos - spikeDropDist)).magnitude <= 0.01)
				{
					spikes1.transform.position = startPos - spikeDropDist;
					spikes2.transform.position = startPos - spikeDropDist;
				}
				if(spikes1.transform.position == startPos - spikeDropDist)
				{
					break;
				}
				else
				{
					yield return null;
				}
			}
		}
		yield return shortWait;
		colourLights.SetActive(false);*/
		
		if(orderNum >= dropSequence.Length-1)
		{
			colourLights.SetActive(false);
			attackNum++;
			attacking = false;
			yield return announceWait;
			//StartCoroutine(robotSphereAnnounce(attacknum+1));
		}
		else
		{
			StartCoroutine(colourSpikeDrop(orderNum+1));
		}
		
		//attackNum++;
		//attacking = false;
		//StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserCircleSpawn(int orderNum)
	{
		if(orderNum == 0)
		{
			yield return announceWait;
		}
		GameObject laserCircle = laserCircles[circleSpawns[orderNum]];
		laserCircle.SetActive(true);
		foreach(Transform child in laserCircle.transform)
		{
			yield return new WaitForSeconds(2);
			/*while(child.eulerAngles.x > 0 && child.eulerAngles.x <= 90)
			{
				Debug.Log(child.eulerAngles.x);
				yield return null;
			}*/
			if(orderNum < circleSpawns.Length-1)
			{
				StartCoroutine(laserCircleSpawn(orderNum+1));
			}
			
			yield return new WaitForSeconds(5);
			laserCircle.SetActive(false);
			break;
		}
		
		if(orderNum >= circleSpawns.Length-1)
		{
			attackNum++;
			attacking = false;
			yield return announceWait;
			//StartCoroutine(robotSphereAnnounce(attacknum+1));
		}
	}
	
	IEnumerator laserAttack1()
	{
		yield return announceWait;
		Debug.Log("V");
		sweepingLasers1.SetActive(true);
		Vector3 startPos = sweepingLasers1.transform.position;
		while(true)
		{
			sweepingLasers1.transform.position = Vector3.MoveTowards(sweepingLasers1.transform.position, startPos + lasers1Dist, lasers1Speed*Time.deltaTime);
			if((sweepingLasers1.transform.position - (startPos + lasers1Dist)).magnitude <= 0.01)
			{
				sweepingLasers1.transform.position = startPos + lasers1Dist;
			}
			if(sweepingLasers1.transform.position == startPos + lasers1Dist)
			{
				Debug.Log("T");
				break;
			}
			else
			{
				Debug.Log("U");
				yield return null;
			}
		}
		Debug.Log("S");
		sweepingLasers1.SetActive(false);
		Debug.Log("R");
		attackNum++;
		attacking = false;
		yield return announceWait;
		Debug.Log("Q");
		//StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserAttack2()
	{
		yield return announceWait;
		sweepingLasers2.SetActive(true);
		Vector3 startPos = sweepingLasers2.transform.position;
		while(true)
		{
			sweepingLasers2.transform.position = Vector3.MoveTowards(sweepingLasers2.transform.position, startPos + lasers2Dist, lasers2Speed*Time.deltaTime);
			if((sweepingLasers2.transform.position - (startPos + lasers2Dist)).magnitude <= 0.01)
			{
				sweepingLasers2.transform.position = startPos + lasers2Dist;
			}
			if(sweepingLasers2.transform.position == startPos + lasers2Dist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		sweepingLasers2.SetActive(false);
		
		attackNum++;
		attacking = false;
		yield return announceWait;
		//StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserAttack3()
	{
		yield return announceWait;
		sweepingLasers3.SetActive(true);
		Vector3 startPos = sweepingLasers3.transform.position;
		while(true)
		{
			sweepingLasers3.transform.position = Vector3.MoveTowards(sweepingLasers3.transform.position, startPos + lasers3Dist, lasers3Speed*Time.deltaTime);
			if((sweepingLasers3.transform.position - (startPos + lasers3Dist)).magnitude <= 0.01)
			{
				sweepingLasers3.transform.position = startPos + lasers3Dist;
			}
			if(sweepingLasers3.transform.position == startPos + lasers3Dist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		sweepingLasers3.SetActive(false);
		sideLaser.SetActive(true);
		
		startPos = sideLaser.transform.position;
		while(true)
		{
			sideLaser.transform.position = Vector3.MoveTowards(sideLaser.transform.position, startPos + sideLaserDist, sideLaserSpeed*Time.deltaTime);
			if((sideLaser.transform.position - (startPos + sideLaserDist)).magnitude <= 0.01)
			{
				sideLaser.transform.position = startPos + sideLaserDist;
			}
			if(sideLaser.transform.position == startPos + sideLaserDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		sideLaser.SetActive(true);
		
		//StartCoroutine(robotSphereAnnounce(attacknum+1));
		attackNum++;
		attacking = false;
		yield return announceWait;
	}
	
	IEnumerator ramStab(int orderNum)
	{
		GameObject ram = batteringRams[attackRams[orderNum]];
		Vector3 startPos = ram.transform.position;
		
		// battering ram emerges slowly
		while(true)
		{
			ram.transform.position = Vector3.MoveTowards(ram.transform.position, startPos + ramSlowDist, ramSlowSpeed*Time.deltaTime);
			if((ram.transform.position - (startPos + ramSlowDist)).magnitude <= 0.01)
			{
				ram.transform.position = startPos + ramSlowDist;
			}
			if(ram.transform.position == startPos + ramSlowDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		startPos = ram.transform.position;
		// battering ram goes forward quickly
		while(true)
		{
			ram.transform.position = Vector3.MoveTowards(ram.transform.position, startPos + ramFastDist, ramFastSpeed*Time.deltaTime);
			if((ram.transform.position - (startPos + ramFastDist)).magnitude <= 0.01)
			{
				ram.transform.position = startPos + ramFastDist;
			}
			if(ram.transform.position == startPos + ramFastDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		if(orderNum < attackRams.Length-1)
		{
			StartCoroutine(ramStab(orderNum+1));
		}
		
		startPos = ram.transform.position;
		// battering ram retreats
		Vector3 totalDist = -1*(ramSlowDist + ramFastDist);
		while(true)
		{
			ram.transform.position = Vector3.MoveTowards(ram.transform.position, startPos + totalDist, ramFastSpeed*Time.deltaTime);
			if((ram.transform.position - (startPos + totalDist)).magnitude <= 0.01)
			{
				ram.transform.position = startPos + totalDist;
			}
			if(ram.transform.position == startPos + totalDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		if(orderNum >= attackRams.Length-1)
		{
			//StartCoroutine(robotSphereAnnounce(1));
			attackNum++;
			attacking = false;
			yield return announceWait;
		}
	}
	
	IEnumerator raisePlatform()
	{
		yield return announceWait;
		portalPlatform.SetActive(true);
		
		Vector3 startPos = portalPlatform.transform.position;
		
		// raise portal platform
		while(true)
		{
			portalPlatform.transform.position = Vector3.MoveTowards(portalPlatform.transform.position, startPos + portalDist, portalSpeed*Time.deltaTime);
			if((portalPlatform.transform.position - (startPos + portalDist)).magnitude <= 0.01)
			{
				portalPlatform.transform.position = startPos + portalDist;
			}
			if(portalPlatform.transform.position == startPos + portalDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		attackNum = -1;
		attacking = false;
	}
}
