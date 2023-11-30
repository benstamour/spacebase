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
	private float moveDist = 1.5f;
	private float moveSpeed = 2f;
	private GameObject nextNextTile;
	public static int[] key = {1, 0, 0, 2};
	[SerializeField] private int id = 0;
	
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
			Debug.Log(this.nextTile.name);
		}
		Debug.Log(key[0] + " " + key[1] + " " + key[2] + " " + key[3]);
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
			
			if(key[this.id+1] == 0)
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
