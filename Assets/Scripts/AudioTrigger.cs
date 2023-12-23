using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
	private bool activated = false;
	[SerializeField] int id;
	
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
			
			GameManager gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
			if(this.id == 2)
			{
				gameManagerScript.triggerHoloMusic();
			}
			else if(this.id == 3)
			{
				gameManagerScript.triggerCircuitMusic();
			}
		}
	}
}
