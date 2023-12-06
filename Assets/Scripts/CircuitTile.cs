using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lightbug.LaserMachine;

public class CircuitTile : MonoBehaviour
{
	[SerializeField] private GameObject[] lasers;
	[SerializeField] private GameObject[] nodes;
	[SerializeField] private bool on = false;
	private float timer = 0f;
	private string firstNode = null;
	private string colour = null;
	[SerializeField] private LaserData cyanLaserObj;
	[SerializeField] private LaserData magentaLaserObj;
	[SerializeField] private LaserData yellowLaserObj;
	[SerializeField] private float toRotate = 0f;
	[SerializeField] private static float rotateSpeed = 50f;
	[SerializeField] private bool isSource = false;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.timer -= Time.deltaTime;
    }
	
	void FixedUpdate()
	{
		if(this.toRotate > 0)
		{
			float amount = Mathf.Min(this.toRotate, rotateSpeed*Time.deltaTime);
			gameObject.transform.Rotate(new Vector3(0,amount,0));
			this.toRotate -= amount;
		}
	}
	
	void LateUpdate()
	{
		if(this.timer <= 0 && !this.isSource)
		{
			this.on = false;
			this.firstNode = null;
			for(int i = 0; i < nodes.Length; i++)
			{
				LEDNode ledNodeScript = nodes[i].GetComponent<LEDNode>();
				
				ledNodeScript.updateOnTimerSpeed(2.0f);
				ledNodeScript.updateOffTimerSpeed(0f);
			}
			for(int i = 0; i < lasers.Length; i++)
			{
				LaserMachine laserScript = lasers[i].GetComponent<LaserMachine>();
				Transform lineRenderer = lasers[i].transform.Find("lineRenderer_0");
				if(lineRenderer != null)
				{
					Destroy(lineRenderer.gameObject);
					laserScript.RemoveLasers();
				}
				lasers[i].SetActive(false);
			}
		}
	}
	
	public void Activate(GameObject node, Material laserMaterial)
	{
		if((node.name == "Node 1" || node.name == "Node 2") && !this.isSource)
		{
			this.timer = 0.5f;
			
			if(!this.on || node.name != firstNode || laserMaterial.name != this.colour)
			{
				bool changeColour = laserMaterial.name != this.colour;
				Color newColour = Color.clear;
				if(changeColour)
				{
					this.colour = laserMaterial.name;
					newColour = laserMaterial.color;
					for(int i = 0; i < lasers.Length; i++)
					{
						LaserMachine laserScript = lasers[i].GetComponent<LaserMachine>();
						Transform lineRenderer = lasers[i].transform.Find("lineRenderer_0");
						if(lineRenderer != null)
						{
							Destroy(lineRenderer.gameObject);
							laserScript.RemoveLasers();
						}
						
						lasers[i].SetActive(false);
						if(this.colour == "Laser_CYAN")
						{
							laserScript.m_data = cyanLaserObj;
						}
						else if(this.colour == "Laser_PINK")
						{
							laserScript.m_data = magentaLaserObj;
						}
						else if(this.colour == "Laser_YELLOW")
						{
							laserScript.m_data = yellowLaserObj;
						}
					}
				}
				firstNode = node.name;
				
				this.on = true;
				if(node.name == "Node 1")
				{
					for(int i = 0; i < nodes.Length; i++)
					{
						LEDNode ledNodeScript = nodes[i].GetComponent<LEDNode>();
						if(i == 0)
						{
							ledNodeScript.isFirstNode = true;
							ledNodeScript.prevNode = null;
						}
						else
						{
							ledNodeScript.isFirstNode = false;
							ledNodeScript.prevNode = nodes[i-1].GetComponent<LEDNode>();
						}
						ledNodeScript.updateOnTimerSpeed(0f);
						ledNodeScript.updateOffTimerSpeed(20f);
						if(changeColour)
						{
							ledNodeScript.updateColour(newColour);
						}
						ledNodeScript.toggle(true);
					}
					lasers[0].SetActive(false);
					StartCoroutine(laserDelay(lasers[1]));
				}
				else if(node.name == "Node 2")
				{
					for(int i = 0; i < nodes.Length; i++)
					{
						LEDNode ledNodeScript = nodes[i].GetComponent<LEDNode>();
						if(i == nodes.Length - 1)
						{
							ledNodeScript.isFirstNode = true;
							ledNodeScript.prevNode = null;
						}
						else
						{
							ledNodeScript.isFirstNode = false;
							ledNodeScript.prevNode = nodes[i+1].GetComponent<LEDNode>();
						}
						ledNodeScript.updateOnTimerSpeed(0f);
						ledNodeScript.updateOffTimerSpeed(20f);
						if(changeColour)
						{
							ledNodeScript.updateColour(newColour);
						}
						ledNodeScript.toggle(true);
					}
					lasers[1].SetActive(false);
					StartCoroutine(laserDelay(lasers[0]));
				}
			}
		}
	}
	
	IEnumerator laserDelay(GameObject laserOn)
	{
		yield return new WaitForSeconds(1.5f);
		laserOn.SetActive(true);
	}
	
	public void addToRotation(float f)
	{
		this.toRotate += f;
	}
}
