using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicTrigger : MonoBehaviour
{
	private bool activated = false;
	
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
			RisingAcid acidScript = GameObject.Find("Acid Sphere").GetComponent<RisingAcid>();
			acidScript.triggerAcid();
		}
	}
}
