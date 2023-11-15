using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField] private int dir = 1;
	[SerializeField] private Vector3 move;
	//[SerializeField] private float distance = 5.5f;
	private Vector3 startPos = Vector3.zero;
	private Vector3 dir0 = Vector3.zero;
	private Vector3 dir1 = Vector3.zero;
	[SerializeField] private float speed = 2f;
	
	//[SerializeField] float rotateSpeed = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        this.dir0 = new Vector3(-this.move.x, -this.move.y, -this.move.z);
		this.dir1 = this.move;//new Vector3(0, 0, this.distance);
		this.startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // handles movement of spiked walls
        if(this.dir == 0)
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir0, speed*Time.deltaTime);
			if((gameObject.transform.position - (this.startPos + this.dir0)).magnitude <= 0.01)
			{
				gameObject.transform.position = this.startPos + this.dir0;
			}
			if(gameObject.transform.position == this.startPos + this.dir0)
			{
				this.dir = 1;
				this.startPos = gameObject.transform.position;
			}
		}
		else
		{
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir1, speed*Time.deltaTime);
			if((gameObject.transform.position - (this.startPos + this.dir1)).magnitude <= 0.01)
			{
				gameObject.transform.position = this.startPos + this.dir1;
			}
			if(gameObject.transform.position == this.startPos + this.dir1)
			{
				this.dir = 0;
				this.startPos = gameObject.transform.position;
			}
		}
		
		// rotate spiked balls
		//float rotateAmount = this.rotateSpeed*Time.deltaTime;
		//gameObject.transform.Rotate(new Vector3(0f, rotateAmount, 0f));
    }
}
