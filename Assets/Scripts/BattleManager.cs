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
	
	// FINAL PLATFORM
	[SerializeField] GameObject portalPlatform;
	[SerializeField] float portalSpeed;
	[SerializeField] Vector3 portalDist = new Vector3(0, 4, 0);
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
		int[] attackRams = new int[numSmashes];
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
		int[] circleSpawns = new int[numSmashes];
		for(int i = 0; i < numSmashes; i++)
		{
			circleSpawns[i] = UnityEngine.Random.Range(0, laserCircles.Length);
			if(i > 0)
			{
				while(circleSpawns[i] == circleSpawns[i-1])
				{
					circleSpawns[i] = UnityEngine.Random.Range(0, laserCircles.Length);
				}
			}
		}
		return circleSpawns;
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
		
		robotSphereScript.Play(0);
		
		yield return new WaitForSeconds(1.5f);
		
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
		
		StartCoroutine(ramStab(generateRamSequence(), 0));
	}
	
	IEnumerator robotSphereAnnounce(int attacknum)
	{
		robotSphereClosed.SetActive(true);
		
		RobotSphere robotSphereScript = robotSphereClosed.GetComponent<RobotSphere>();
		
		Vector3 startPos = robotSphereClosed.transform.position;
		while(true)
		{
			robotSphereClosed.transform.position = Vector3.MoveTowards(robotSphereClosed.transform.position, startPos - robotMidCeilingDist, robotSpeed*Time.deltaTime);
			if((robotSphereClosed.transform.position - (startPos - robotMidCeilingDist)).magnitude <= 0.01)
			{
				robotSphereClosed.transform.position = startPos - robotMidCeilingDist;
			}
			if(robotSphereClosed.transform.position == startPos - robotMidCeilingDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		
		robotSphereScript.Play(attacknum);
		
		yield return new WaitForSeconds(1.5f);
		
		if(attacknum == 5)
		{
			StartCoroutine(raisePlatform());
		}
		
		startPos = robotSphereClosed.transform.position;
		while(true)
		{
			robotSphereClosed.transform.position = Vector3.MoveTowards(robotSphereClosed.transform.position, startPos + robotMidCeilingDist, robotSpeed*Time.deltaTime);
			if((robotSphereClosed.transform.position - (startPos + robotMidCeilingDist)).magnitude <= 0.01)
			{
				robotSphereClosed.transform.position = startPos + robotMidCeilingDist;
			}
			if(robotSphereClosed.transform.position == startPos + robotMidCeilingDist)
			{
				break;
			}
			else
			{
				yield return null;
			}
		}
		robotSphereClosed.SetActive(false);
		
		if(attacknum == 1)
		{
			StartCoroutine(laserAttack1(attacknum));
		}
		else if(attacknum == 2)
		{
			StartCoroutine(colourSpikeDrop(attacknum));
		}
		else if(attacknum == 3)
		{
			StartCoroutine(laserAttack3(attacknum));
		}
		else if(attacknum == 4)
		{
			StartCoroutine(laserCircleSpawn(generateLaserCircleSequence(), 0, attacknum));
		}
	}
	
	IEnumerator colourSpikeDrop(int attacknum)
	{
		string[] arr = {"R", "R", "B", "B", "G", "G"};
		for(int i = 0; i < arr.Length; i++)
		{
			int num = UnityEngine.Random.Range(i,arr.Length);
			if(i > 0)
			{
				while(arr[num] == arr[i-1])
				{
					num = UnityEngine.Random.Range(i,arr.Length);
				}
			}
			string temp = arr[i];
			arr[i] = arr[num];
			arr[num] = temp;
		}
		string dropSequence = arr[0] + arr[1] + arr[2] + arr[3] + arr[4];
		Debug.Log(dropSequence);
		
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
			yield return new WaitForSeconds(0.5f);
		}
		foreach(Transform child in colourLights.transform)
		{
			Light light = child.gameObject.GetComponent<Light>();
			light.color = Color.white;
			light.intensity = 0.5f;
		}
		
		yield return new WaitForSeconds(1);
		
		for(int i = 0; i < dropSequence.Length; i++)
		{
			char colour = dropSequence[i];
			GameObject spikes;
			if(colour == 'R')
			{
				spikes = redSpikes;
			}
			else if(colour == 'B')
			{
				spikes = blueSpikes;
			}
			else
			{
				spikes = greenSpikes;
			}
			
			Vector3 startPos = spikes.transform.position;
			while(true)
			{
				spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, startPos + spikeDropDist, spikeDropSpeed*Time.deltaTime);
				if((spikes.transform.position - (startPos + spikeDropDist)).magnitude <= 0.01)
				{
					spikes.transform.position = startPos + spikeDropDist;
				}
				if(spikes.transform.position == startPos + spikeDropDist)
				{
					break;
				}
				else
				{
					yield return null;
				}
			}
			
			startPos = spikes.transform.position;
			while(true)
			{
				spikes.transform.position = Vector3.MoveTowards(spikes.transform.position, startPos - spikeDropDist, spikeDropSpeed*Time.deltaTime);
				if((spikes.transform.position - (startPos - spikeDropDist)).magnitude <= 0.01)
				{
					spikes.transform.position = startPos - spikeDropDist;
				}
				if(spikes.transform.position == startPos - spikeDropDist)
				{
					break;
				}
				else
				{
					yield return null;
				}
			}
		}
		yield return new WaitForSeconds(0.5f);
		colourLights.SetActive(false);
		
		StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserCircleSpawn(int[] circleOrder, int orderNum, int attacknum)
	{
		GameObject laserCircle = laserCircles[circleOrder[orderNum]];
		laserCircle.SetActive(true);
		foreach(Transform child in laserCircle.transform)
		{
			yield return new WaitForSeconds(2);
			/*while(child.eulerAngles.x > 0 && child.eulerAngles.x <= 90)
			{
				Debug.Log(child.eulerAngles.x);
				yield return null;
			}*/
			if(orderNum < circleOrder.Length-1)
			{
				StartCoroutine(laserCircleSpawn(circleOrder, orderNum+1, attacknum));
			}
			
			yield return new WaitForSeconds(5);
			laserCircle.SetActive(false);
			break;
		}
		
		if(orderNum >= circleOrder.Length-1)
		{
			StartCoroutine(robotSphereAnnounce(attacknum+1));
		}
	}
	
	IEnumerator laserAttack1(int attacknum)
	{
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
				break;
			}
			else
			{
				yield return null;
			}
		}
		sweepingLasers1.SetActive(false);
		
		StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserAttack2(int attacknum)
	{
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
		
		StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator laserAttack3(int attacknum)
	{
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
		
		StartCoroutine(robotSphereAnnounce(attacknum+1));
	}
	
	IEnumerator ramStab(int[] attackOrder, int orderNum)
	{
		GameObject ram = batteringRams[attackOrder[orderNum]];
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
		
		if(orderNum < attackOrder.Length-1)
		{
			StartCoroutine(ramStab(attackOrder, orderNum+1));
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
		
		if(orderNum >= attackOrder.Length-1)
		{
			StartCoroutine(robotSphereAnnounce(1));
		}
	}
	
	IEnumerator raisePlatform()
	{
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
	}
}
