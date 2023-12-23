using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// displays number of orbs collected by player on the finish screen
public class ScoreText : MonoBehaviour
{
	private GameManager gameManagerScript;
	private int score = 0;
	
	public TextMeshProUGUI textComponent;
	
    // Start is called before the first frame update
    void Start()
    {
        this.gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.score = gameManagerScript.getScore();
		UpdateText(this.score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateText(int score)
	{
        textComponent.text = "Orbs Collected: " + this.score.ToString() + "/6";
	}
}