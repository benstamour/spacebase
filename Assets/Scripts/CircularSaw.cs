using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularSaw : MonoBehaviour
{
	[SerializeField] float speed = 10f;
	//[SerializeField] bool rotateAround = false;
	//[SerializeField] float rotateAroundSpeed = 10f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		float rotateAmount = this.speed*Time.deltaTime;
        gameObject.transform.Rotate(new Vector3(0f, -rotateAmount, 0f));
		
		/*Vector3 center = new Vector3(-2.5f, 8f, -37.75f);
		if(this.rotateAround)
		{
			transform.RotateAround(center, Vector3.up, -rotateAroundSpeed * Time.deltaTime);
		}*/
    }
}
