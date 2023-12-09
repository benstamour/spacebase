using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingAcid : MonoBehaviour
{
	private bool activated = false;
	private Vector3 startPos;
	[SerializeField] private float riseAmount;
	[SerializeField] private float speed;
	
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
		this.activated = true;
	}
}
