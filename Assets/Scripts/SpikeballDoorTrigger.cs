using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeballDoorTrigger : MonoBehaviour
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
			GameObject door = GameObject.Find("Second Door");
			Animator anim = door.GetComponent<Animator>();
			anim.SetBool("character_nearby", false);
		}
	}
}
