using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidSprinkler : MonoBehaviour
{
	public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;
	
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);

        if(other.tag == "Character")
		{
			Debug.Log(gameObject.name);
		}
    }
}
