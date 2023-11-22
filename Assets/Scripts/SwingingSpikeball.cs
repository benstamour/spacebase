using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingSpikeball : MonoBehaviour
{
	[SerializeField] private int dir = 0;
	[SerializeField] private float speed = 12f;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(dir == 2)
		{
			gameObject.transform.Rotate(0, speed*Time.deltaTime, 0, Space.World);
		}
		else
		{
			float angle = gameObject.transform.eulerAngles.x;
			if(angle > 180)
			{
				angle -= 360;
			}
			Debug.Log(angle);
			float move = (-9.81f*Mathf.Pow(Mathf.Abs(0.0155f*angle),2) + 5)*Time.deltaTime;	
			
			if(dir == 0)
			{
				gameObject.transform.Rotate(-1*speed*move, 0, 0);
				if(angle <= -45)
				{
					this.dir = 1;
				}
			}
			else
			{
				gameObject.transform.Rotate(speed*move, 0, 0);
				if(angle >= 45)
				{
					this.dir = 0;
				}
			}
		}
    }
}
