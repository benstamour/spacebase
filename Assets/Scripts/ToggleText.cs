using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// script to change text of sound and save point toggle buttons to match settings
// this script runs once a scene loads, ensures correct text is shown
public class ToggleText : MonoBehaviour
{
	private GameManager gameManagerScript;
	public TextMeshProUGUI textComponent;
	
    // Start is called before the first frame update
    void Start()
    {
		// change text to match current settings
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		if(gameObject.name == "SoundButton")
		{
			if(gameManagerScript.getVolume())
			{
				textComponent.text = "SOUND ON";
			}
			else
			{
				textComponent.text = "SOUND OFF";
			}
		}
		else if(gameObject.name == "SavePointButton")
		{
			if(gameManagerScript.getSavePointsEnabled())
			{
				textComponent.text = "SAVE POINTS ON";
			}
			else
			{
				textComponent.text = "SAVE POINTS OFF";
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
