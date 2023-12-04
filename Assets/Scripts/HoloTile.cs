using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloTile : MonoBehaviour
{
	[SerializeField] private GameObject nextTile;
	[SerializeField] private GameObject blueTile;
	[SerializeField] private GameObject pinkTile;
	private Vector3 nextTilePos;
	private bool hasSplit = false;
	private float moveDist = 1f;
	private float moveSpeed = 2f;
	private GameObject nextNextTile;
	private string holocode;
	[SerializeField] private int id = 0;
	
	[SerializeField] private GameObject codeTilePrefab;
	[SerializeField] private GameObject blankTilePrefab;
	
    // Start is called before the first frame update
    void Start()
    {
        if(this.nextTile != null && !this.nextTile.Equals(null))
		{
			this.nextTilePos = nextTile.transform.position;
			HoloTile holoTileScript = this.nextTile.GetComponent<HoloTile>();
			if(holoTileScript.nextTile != null && !holoTileScript.nextTile.Equals(null))
			{
				this.nextNextTile = holoTileScript.nextTile;
			}
		}
		
		GameManager gameManagerScript = GameObject.Find("GameManager").GetComponent<GameManager>();
		this.holocode = gameManagerScript.getHoloCode();
		
		if(this.id == -1)
		{
			Vector3 holoFloorPos = new Vector3(18.5f, 8f, -51.75f);
			for(int i = 0; i < this.holocode.Length; i++)
			{
				if(this.holocode[i] == '0')
				{
					Instantiate(this.codeTilePrefab, holoFloorPos - Vector3.forward, Quaternion.identity);
					Instantiate(this.blankTilePrefab, holoFloorPos + Vector3.forward, Quaternion.identity);
				}
				else
				{
					Instantiate(this.codeTilePrefab, holoFloorPos + Vector3.forward, Quaternion.identity);
					Instantiate(this.blankTilePrefab, holoFloorPos - Vector3.forward, Quaternion.identity);
				}
				holoFloorPos += 2f*Vector3.left;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void Split()
	{
		if(this.hasSplit == false && this.nextTile != null && !this.nextTile.Equals(null))
		{
			StartCoroutine(SplitTiles());
		}
	}
	
	IEnumerator SplitTiles()
	{
		if(this.hasSplit == false)
		{
			this.hasSplit = true;
			GameObject blueTile = Instantiate(this.blueTile, this.nextTilePos, Quaternion.identity);
			GameObject pinkTile = Instantiate(this.pinkTile, this.nextTilePos, Quaternion.identity);
			this.nextTile.SetActive(false);
			
			HoloTile blueScript = blueTile.GetComponent<HoloTile>();
			HoloTile pinkScript = pinkTile.GetComponent<HoloTile>();
			blueScript.setNextTile(null);
			pinkScript.setNextTile(null);
			blueScript.setID(this.id+1);
			pinkScript.setID(this.id+1);
			if(this.nextNextTile != null && !this.nextNextTile.Equals(null))
			{
				blueScript.setNextTile(this.nextNextTile);
				pinkScript.setNextTile(this.nextNextTile);
			}
			
			if(this.holocode[this.id+1] == '0')
			{
				pinkTile.GetComponent<Collider>().enabled = false;
			}
			else
			{
				blueTile.GetComponent<Collider>().enabled = false;
			}
			
			while(blueTile.transform.position.x + this.moveDist > this.nextTilePos.x)
			{
				blueTile.transform.position = blueTile.transform.position + new Vector3(-this.moveSpeed*Time.deltaTime, 0, 0);
				pinkTile.transform.position = pinkTile.transform.position + new Vector3(this.moveSpeed*Time.deltaTime, 0, 0);
				yield return null;
			}
		}
	}
	
	private void setNextTile(GameObject tile)
	{
		this.nextTile = tile;
	}
	
	private void setID(int num)
	{
		this.id = num;
	}
}
