using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	[SerializeField] private float depth = -20f;
	[SerializeField] private float speed = 1f;
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
		if(!this.activated)
		{
			this.activated = true;
		
			GameObject door = GameObject.Find("Seventh Door");
			Animator anim = door.GetComponent<Animator>();
			anim.SetBool("character_nearby", false);
			
			StartCoroutine(Descend());
		}
	}
	
	IEnumerator Descend()
	{
		yield return new WaitForSeconds(1.5f);
		
		GameObject floor = GameObject.Find("Elevator Tile");
		Vector3 startPos = floor.transform.position;
		Vector3 curPos = startPos;
		while(startPos.y + depth < curPos.y)
		{
			floor.transform.position = new Vector3(curPos.x, curPos.y + depth*Time.deltaTime*speed, curPos.z);
			curPos = floor.transform.position;
			yield return null;
		}
	}
}
