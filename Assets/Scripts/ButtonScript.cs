using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// script for buttons on GUI screens
public class ButtonScript : MonoBehaviour
{
	private GameManager gameManagerScript;
	//public AudioClip buttonClip;
	//AudioSource audioSource;
	
    // Start is called before the first frame update
    void Start()
    {
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		//this.audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	// these functions load the corresponding scene
	public void LoadCharSelection()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("CharSelectScreen");
	}
	
	public void LoadCharSelection2()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("CharSelectScreen2");
	}
	
	public void LoadArena()
	{
		this.gameManagerScript.PlayButtonClip();
		this.gameManagerScript.StartGame();
		//SceneManager.LoadScene("Arena");
	}
	
	public void LoadControls()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("ControlsScreen");
	}
	
	public void LoadStartScreen()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("StartScreen");
	}
	
	public void LoadEndScreen()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("EndScreen");
	}
	
	public void LoadInstructionScreen()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("InstructionsScreen");
	}
	
	public void LoadSettingsScreen()
	{
		this.gameManagerScript.PlayButtonClip();
		SceneManager.LoadScene("SettingsScreen");
	}
	
	public void Quit()
	{
		this.gameManagerScript.PlayButtonClip();
		Application.Quit();
	}
	
	// triggered when player selects a character
	public void SelectCharacter(int charID)
	{
		this.gameManagerScript.PlayButtonClip();
		this.gameManagerScript.SetCharacter(charID);
		this.gameManagerScript.StartGame();
	}
	
	// triggered when player reaches end zone and goes to main menu; the menu soundtrack should start playing
	public void GameToStartScreen()
	{
		this.gameManagerScript.PlayButtonClip();
		this.gameManagerScript.LoadStartScreen();
	}
	
	// toggles sound on and off
	public void ToggleVolume()
	{
		this.gameManagerScript.ToggleVolume();
		TextMeshProUGUI textComponent = GameObject.Find("SoundButton").GetComponentInChildren<TextMeshProUGUI>();
		if(textComponent.text == "SOUND ON")
		{
			textComponent.text = "SOUND OFF";
		}
		else
		{
			textComponent.text = "SOUND ON";
		}
	}
	
	// toggles save points on and off
	public void ToggleSavePoints()
	{
		this.gameManagerScript.ToggleSavePoints();
		TextMeshProUGUI textComponent = GameObject.Find("SavePointButton").GetComponentInChildren<TextMeshProUGUI>();
		if(textComponent.text == "SAVE POINTS ON")
		{
			textComponent.text = "SAVE POINTS OFF";
		}
		else
		{
			textComponent.text = "SAVE POINTS ON";
		}
	}
}
