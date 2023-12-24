using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVent : MonoBehaviour
{
	//public ParticleSystem wind;
	//public List<ParticleCollisionEvent> collisionEvents;
	[SerializeField] float maxRayDistance = 6f;
	[SerializeField] float windForce = 20f;
	[SerializeField] float ventBuffer = 0.2f;
	private float lastUpforce;
	private bool pauseForce = false;
	//Coroutine curCoroutine;
	int numCoroutines = 0;
	private bool on = true;
	private ParticleSystem wind;
	
	void Awake()
    {
        this.wind = GetComponent<ParticleSystem>();
		//collisionEvents = new List<ParticleCollisionEvent>();
    }
	
    // Start is called before the first frame update
    /*void Start()
    {
        this.wind = GetComponent<ParticleSystem>();
		//collisionEvents = new List<ParticleCollisionEvent>();
    }*/

    // Update is called once per frame
    void Update()
    {
		//Debug.Log(this.on);
		if(this.on == true)
		{
			float[] x = {0f, 0.5f, 0.5f, -0.5f, -0.5f};
			float[] z = {0f, 0.5f, -0.5f, 0.5f, -0.5f};
			//bool collidedWithPlayer = false;
			
			for(int i = 0; i < x.Length; i++)
			{
				Vector3 transl = new Vector3(x[i], 0, z[i]);
				RaycastHit hitInfo;
				Physics.Linecast(
					gameObject.transform.position + transl,
					gameObject.transform.position + transl + gameObject.transform.forward*this.maxRayDistance,
					out hitInfo,
					//m_currentProperties.m_layerMask
					~0
				);
				
				if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
				{
					Physics.Linecast(
						gameObject.transform.position + Vector3.down,
						gameObject.transform.position + Vector3.down + gameObject.transform.forward*this.maxRayDistance,
						out hitInfo,
						//m_currentProperties.m_layerMask
						~0
					);
					if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
					{
						Physics.Linecast(
							gameObject.transform.position + Vector3.right*0.5f,
							gameObject.transform.position + Vector3.right*0.5f + gameObject.transform.forward*this.maxRayDistance,
							out hitInfo,
							//m_currentProperties.m_layerMask
							~0
						);
						if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
						{
							Physics.Linecast(
								gameObject.transform.position + Vector3.left*0.5f,
								gameObject.transform.position + Vector3.left*0.5f + gameObject.transform.forward*this.maxRayDistance,
								out hitInfo,
								//m_currentProperties.m_layerMask
								~0
							);
						}
					}
				}
			
				/*RaycastHit hitInfo;
				Physics.Linecast(
					gameObject.transform.position,
					gameObject.transform.position + gameObject.transform.forward*this.maxRayDistance,
					out hitInfo,
					//m_currentProperties.m_layerMask
					~0
				);*/
				
				//Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward*this.maxRayDistance, Color.cyan, 200);

				if(hitInfo.collider)
				{
					//gameObject.lineRenderer.SetPosition(1, hitInfo3D.point);

					if(hitInfo.collider.gameObject.tag == "Character")
					{
						GameObject character = hitInfo.collider.gameObject;
						//Debug.Log(gameObject.name);
						float distance = hitInfo.distance; //hitInfo3D.collider.gameObject.transform.position.z - gameObject.transform.position.z;
						//Debug.Log(distance);
						Vector3 force = Vector3.zero;
						if(gameObject.transform.forward.y > 0.8)
						{
							distance = character.transform.position.y - gameObject.transform.position.y + this.ventBuffer;
							if(this.pauseForce == false)
							{
								if(this.numCoroutines == 0)
								{
									StartCoroutine(disableGravity(character));
								}
								if(distance < maxRayDistance*0.95)
								{
									if(distance >= 0)
									{
										force = gameObject.transform.forward*Mathf.Min(10, this.windForce/(distance+0.001f));
										//force = new Vector3(0,0,Mathf.Max(10, distance*this.windForce/(distance+0.001f)));
										this.lastUpforce = Time.time;
									}
									else
									{
										force = gameObject.transform.forward*Mathf.Max(-10, this.windForce/(distance+0.001f));
										//force = new Vector3(0,0,Mathf.Min(-10, distance*this.windForce/(distance+0.001f)));
										this.lastUpforce = Time.time;
									}
								}
							}
						}
						else
						{
							if(distance > 0)
							{
								force = gameObject.transform.forward*Mathf.Min(10, this.windForce/(distance+0.001f));
								//force = new Vector3(0,0,Mathf.Max(10, distance*this.windForce/(distance+0.001f)));
							}
							else
							{
								force = gameObject.transform.forward*Mathf.Max(-10, this.windForce/(distance+0.001f));
								//force = new Vector3(0,0,Mathf.Min(-10, distance*this.windForce/(distance+0.001f)));
							}
						}
						CharacterController characterController = character.GetComponent<CharacterController>();
						//Debug.Log(characterController.velocity);
						characterController.Move(force*Time.deltaTime);
					}
					break;
				}
			}
		}
    }
	
	IEnumerator disableGravity(GameObject character)
	{
		this.numCoroutines++;
		Character charScript = character.GetComponent<Character>();
		//CharacterController charController = character.GetComponent<CharacterController>();
		charScript.gravityValue = 0f;
		charScript.onAirVent = true;
		//charController.velocity = 0;
		while(true)
		{
			if(this.on == false)
			{
				break;
			}
			
			RaycastHit hitInfo;
			Physics.Linecast(
				gameObject.transform.position,
				gameObject.transform.position + gameObject.transform.forward*this.maxRayDistance,
				out hitInfo,
				//m_currentProperties.m_layerMask
				~0
			);
			
			if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
			{
				Physics.Linecast(
					gameObject.transform.position + Vector3.down,
					gameObject.transform.position + Vector3.down + gameObject.transform.forward*this.maxRayDistance,
					out hitInfo,
					//m_currentProperties.m_layerMask
					~0
				);
				if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
				{
					Physics.Linecast(
						gameObject.transform.position + Vector3.right*0.5f,
						gameObject.transform.position + Vector3.right*0.5f + gameObject.transform.forward*this.maxRayDistance,
						out hitInfo,
						//m_currentProperties.m_layerMask
						~0
					);
					if(!hitInfo.collider || hitInfo.collider.gameObject.tag != "Character")
					{
						Physics.Linecast(
							gameObject.transform.position + Vector3.left*0.5f,
							gameObject.transform.position + Vector3.left*0.5f + gameObject.transform.forward*this.maxRayDistance,
							out hitInfo,
							//m_currentProperties.m_layerMask
							~0
						);
					}
				}
			}

			if(hitInfo.collider && hitInfo.collider.gameObject.tag == "Character")
			{
				if(hitInfo.distance <= 0.95*this.maxRayDistance)
				{
					this.pauseForce = false;
				}
				else
				{
					this.pauseForce = true;
				}
				yield return null;
			}
			else
			{
				this.pauseForce = false;
				break;
			}
		}
		charScript.gravityValue = -9.81f;
		charScript.onAirVent = false;
		this.numCoroutines--;
	}
	
	public void toggleVent()
	{
		this.on = !this.on;
		if(this.on)
		{
			wind.Play();
			GameObject fan = gameObject.transform.parent.parent.gameObject;
			Animation anim = fan.GetComponent<Animation>();
			anim.Play();
		}
		else
		{
			wind.Stop();
			GameObject fan = gameObject.transform.parent.parent.gameObject;
			Animation anim = fan.GetComponent<Animation>();
			anim.Stop();
		}
	}
	
	public bool getVentStatus()
	{
		return this.on;
	}
	
	
	/*void OnParticleCollision(GameObject other)
    {
		if(other.tag == "Character")
		{
			int numCollisionEvents = wind.GetCollisionEvents(other, collisionEvents);

			Rigidbody rb = other.GetComponent<Rigidbody>();
			int i = 0;

			while (i < numCollisionEvents)
			{
				if(rb)
				{
					Debug.Log("A");
					Vector3 pos = collisionEvents[i].intersection;
					Vector3 force = collisionEvents[i].velocity * 10;
					//rb.AddForce(force);
					CharacterController characterController = other.GetComponent<CharacterController>();
					characterController.Move(force*Time.deltaTime);
				}
				i++;
			}
		}
	}*/
}
