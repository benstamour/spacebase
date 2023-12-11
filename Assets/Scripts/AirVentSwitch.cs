using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirVentSwitch : MonoBehaviour
{
	[SerializeField] float period = 1f;
	[SerializeField] bool on = true;
	private float lastTime = -1f;
	AirVent airVentScript;
	
    // Start is called before the first frame update
    void Start()
    {
        this.airVentScript = gameObject.transform.Find("Air Vent").transform.Find("particle glow master").GetComponent<AirVent>();
		if(this.on == false)
		{
			this.airVentScript.toggleVent();
		}
    }

    // Update is called once per frame
    void Update()
    {
        if((Time.time - this.lastTime) >= this.period)
		{
			this.airVentScript.toggleVent();
			this.on = !this.on;
			this.lastTime = Time.time;
		}
    }
}
