using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
	private bool activated = false;
	
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
			
			StartCoroutine(laserAttack1());
			
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
	
	IEnumerator laserAttack1()
	{
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
	}
	
	IEnumerator laserAttack2()
	{
		Vector3 startPos = sweepingLasers2.transform.position;
		while(true)
		{
			sweepingLasers2.transform.position = Vector3.MoveTowards(sweepingLasers2.transform.position, startPos + lasers2Dist, lasers1Speed*Time.deltaTime);
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
		
		StartCoroutine(ramStab(attackOrder, orderNum+1));
		
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
	}
}
