using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloSplitTrigger : MonoBehaviour
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
			HoloTile holoTileScript = gameObject.GetComponent<HoloTile>();
			holoTileScript.Split();
		}
	}
}
