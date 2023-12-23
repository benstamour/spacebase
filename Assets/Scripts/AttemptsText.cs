using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// displays total number of attempts player has taken on end screen
public class AttemptsText : MonoBehaviour
{
    private GameManager gameManagerScript;
	private int attempts = 0; // total number of attempts
	
	public TextMeshProUGUI textComponent;
	
    // Start is called before the first frame update
    void Start()
    {
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.attempts = gameManagerScript.getAttempts();
		UpdateAttempts(this.attempts);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateAttempts(int attempts)
	{
		textComponent.text = "Attempts Taken: " + attempts.ToString();
	}
}