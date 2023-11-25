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
	
	//private Vector3 moved = Vector3.zero;
	
	//[SerializeField] float rotateSpeed = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        this.dir0 = new Vector3(-this.move.x, -this.move.y, -this.move.z);
		this.dir1 = this.move;//new Vector3(0, 0, this.distance);
		this.startPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		// handles movement of spiked walls
        if(this.dir == 0)
		{
			//this.moved = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir0, speed*Time.deltaTime) - gameObject.transform.position;
			
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir0, speed*Time.fixedDeltaTime);
			//this.moved = -this.move*this.speed*Time.deltaTime;
			
			//gameObject.transform.position -= this.move*this.speed*Time.deltaTime;
			if((gameObject.transform.position - (this.startPos + this.dir0)).magnitude <= 0.01)
			{
				//this.moved = this.startPos + this.dir0 - gameObject.transform.position;
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
			//this.moved = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir1, speed*Time.deltaTime) - gameObject.transform.position;
			
			gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, this.startPos + this.dir1, speed*Time.fixedDeltaTime);
			//this.moved = this.move*this.speed*Time.deltaTime;
			
			//gameObject.transform.position += this.move*this.speed*Time.deltaTime;
			if((gameObject.transform.position - (this.startPos + this.dir1)).magnitude <= 0.01)
			{
				//this.moved = this.startPos + this.dir1 - gameObject.transform.position;
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
	
	/*public int getDir()
	{
		return this.dir;
	}
	public Vector3 getMove()
	{
		return this.move;
	}
	public float getSpeed()
	{
		return this.speed;
	}
	public Vector3 getMoved()
	{
		return this.moved;
	}*/
}
