using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourButton : MonoBehaviour
{
	[SerializeField] private Camera camera;
	bool isPressed = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.camera.isActiveAndEnabled == true && Input.GetMouseButtonDown(0) && !this.isPressed) // if left mouse button clicked
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit))
			{
				if(hit.transform.gameObject.name == gameObject.name)
				{
					Debug.Log(hit.transform.gameObject.name);
					StartCoroutine(ButtonPress());
				}
			}
		}
    }
	
	IEnumerator ButtonPress()
	{
		this.isPressed = true;
		Vector3 startPos = gameObject.transform.position;
		Vector3 curPos = startPos;
		float change = 0.05f;
		float speed = 4f;
		
		while(startPos.z + change > curPos.z)
		{
			gameObject.transform.position += new Vector3(0,0,change*speed*Time.deltaTime);
			curPos = gameObject.transform.position;
			yield return null;
		}
		ColourCode colourCodeScript = GameObject.Find("Button Keypad").GetComponent<ColourCode>();
		colourCodeScript.addInput(gameObject.name[0]);
		while(startPos.z < curPos.z)
		{
			gameObject.transform.position -= new Vector3(0,0,change*speed*Time.deltaTime);
			curPos = gameObject.transform.position;
			yield return null;
		}
		this.isPressed = false;
	}
}