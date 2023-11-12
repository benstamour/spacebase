using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// script for when player is on a special object
public class CharacterPlatform : MonoBehaviour
{	
    // Start is called before the first frame update
    void Start()
    {
        
    }
	
	void Update()
	{
		RaycastHit hit;
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.5f))//Mathf.Infinity))
		{
			if(hit.collider.gameObject.name == "Cylinder" && hit.distance <= 0.5f)
			{
				CharacterController characterController = gameObject.GetComponent<CharacterController>();
				Vector3 slide = new Vector3(1.75f, -0.981f, 0);
				characterController.Move(slide*Time.deltaTime);
				//characterController.Move(Physics.gravity*Time.deltaTime);
			}
			/*else if(hit.collider.gameObject.tag == "OrbitingTile" && hit.distance <= 0.1)
			{
				gameObject.transform.parent = hit.collider.gameObject.transform;
				Debug.Log("Gavaan: " + gameObject.transform.position);
			}*/
			/*else
			{
				gameObject.transform.parent = null;
				
				float[] z = {0f, 0.5f, -0.5f};
			
				for(int i = 0; i < z.Length; i++)
				{
					Vector3 transl = new Vector3(0, 0, z[i]);
					
					Debug.DrawRay(gameObject.transform.position + transl,
						gameObject.transform.position + transl + Vector3.down*200f, Color.cyan, 200);
					
					RaycastHit hitInfo;
					Physics.Linecast(
						gameObject.transform.position + transl,
						gameObject.transform.position + transl + Vector3.down*0.5f,
						out hitInfo,
						//m_currentProperties.m_layerMask
						~0
					);
				
					if(hitInfo.collider)
					{
						if((hit.collider.gameObject.name == "Cube (13)" || hit.collider.gameObject.name == "Cylinder") && hit.distance <= 0.5f)
						{
							Debug.Log("B");
							CharacterController characterController = gameObject.GetComponent<CharacterController>();
							Vector3 slide = new Vector3(0, 0, -1f);
							characterController.Move(slide*Time.fixedDeltaTime);
							break;
						}
					}
				}
			}*/
		}
		else
		{
			gameObject.transform.parent = null;
		}
	}
}
