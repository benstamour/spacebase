using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotSphere : MonoBehaviour
{
	//[SerializeField] private AudioClip[] clips;
	[SerializeField] bool isAnimated;
	
	// Start is called before the first frame update
    void Start()
    {
		if(this.isAnimated)
		{
			Animator anim = gameObject.GetComponent<Animator>();
			anim.speed = 0;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Activate()
	{
		Animator anim = gameObject.GetComponent<Animator>();
		anim.speed = 1.5f;
	}
	
	/*public void Play(int i)
	{
		AudioSource audioSource = gameObject.GetComponent<AudioSource>();
		audioSource.Stop();
		audioSource.clip = clips[i];
		audioSource.Play();
	}*/
}
