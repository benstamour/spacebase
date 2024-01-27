using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicTrigger : MonoBehaviour
{
	private bool activated = false;
	
	[SerializeField] private GameObject[] unhideRooms;
	[SerializeField] private GameObject[] hideRooms;
	
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
			
			for(int i = 0; i < this.unhideRooms.Length; i++)
			{
				unhideRooms[i].SetActive(true);
			}
			for(int i = 0; i < this.hideRooms.Length; i++)
			{
				hideRooms[i].SetActive(false);
			}
			
			RisingAcid acidScript = GameObject.Find("Acid Sphere").GetComponent<RisingAcid>();
			acidScript.triggerAcid();
		}
	}
}
