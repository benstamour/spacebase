using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingPlatformTrigger : MonoBehaviour
{
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
		if(other.gameObject.tag == "Character")
		{
			GameObject risingPlatforms = GameObject.Find("Rising Platforms");
			foreach(Transform platform in risingPlatforms.transform)
			{
				MovingObject movingObjectScript = platform.gameObject.GetComponent<MovingObject>();
				movingObjectScript.Activate();
			}
		}
	}
}
