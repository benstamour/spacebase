using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitDoor : MonoBehaviour
{
	[SerializeField] private GameObject[] targets;
	private CircuitTile[] circuitTileScripts;
	private int numComplete = 0;
	private bool activated = false;
	[SerializeField] private float speed = 1f;
	[SerializeField] private float move = -3.5f;
	
    // Start is called before the first frame update
    void Start()
    {
		circuitTileScripts = new CircuitTile[3];
        for(int i = 0; i < targets.Length; i++)
		{
			GameObject target = targets[i];
			CircuitTile circuitTileScript = target.GetComponent<CircuitTile>();
			circuitTileScripts[i] = circuitTileScript;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void FixedUpdate()
	{
		if(!this.activated)
		{
			bool complete = true;
			for(int i = 0; i < circuitTileScripts.Length; i++)
			{
				CircuitTile circuitTileScript = circuitTileScripts[i];
				if(!circuitTileScript.getComplete())
				{
					complete = false;
					break;
				}
			}
			if(complete)
			{
				this.activated = true;
				StartCoroutine(slideDoor());
			}
		}
	}
	
	public void changeComplete(int change)
	{
		this.numComplete += change;
	}
	
	IEnumerator slideDoor()
	{
		yield return new WaitForSeconds(2f);
		Vector3 startPos = gameObject.transform.position;
		Vector3 curPos = startPos;
		while(curPos.y > startPos.y + move)
		{
			gameObject.transform.position = new Vector3(curPos.x, curPos.y + move*speed*Time.deltaTime, curPos.z);
			curPos = gameObject.transform.position;
			yield return null;
		}
	}
}
