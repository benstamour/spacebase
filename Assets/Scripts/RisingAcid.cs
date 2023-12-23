using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingAcid : MonoBehaviour
{
	private bool activated = false;
	private Vector3 startPos;
	[SerializeField] private float riseAmount;
	[SerializeField] private float speed;
	[SerializeField] private GameObject door;
	
    // Start is called before the first frame update
    void Start()
    {
        this.startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.activated)
		{
			Vector3 curPos = gameObject.transform.position;
			gameObject.transform.position = new Vector3(startPos.x, curPos.y + Time.deltaTime*speed, startPos.z);
			if(gameObject.transform.position.y >= startPos.y+riseAmount)
			{
				activated = false;
			}
		}
    }
	
	public void triggerAcid()
	{
		StartCoroutine(CloseDoor());
		//this.activated = true;
	}
	
	IEnumerator CloseDoor()
	{
		if(this.activated == false)
		{
			this.activated = true; // trigger acid
			float doorMove = 1.5f;
			float doorSpeed = 2f;
			Vector3 doorPos = door.transform.position;
			Vector3 curDoorPos = doorPos;
			while(curDoorPos.z < doorPos.z + doorMove)
			{
				curDoorPos = door.transform.position;
				
				if(curDoorPos.z + doorMove*Time.deltaTime*doorSpeed > doorPos.z + doorMove)
				{
					door.transform.position = new Vector3(doorPos.x, doorPos.y, doorPos.z + doorMove);
				}
				else
				{
					door.transform.position = new Vector3(curDoorPos.x, curDoorPos.y, curDoorPos.z + doorMove*Time.deltaTime*doorSpeed);
				}
				yield return null;
			}
		}
	}
}
