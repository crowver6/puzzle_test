using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {
	
	public GameObject src;
	
	const int table_width = 8;
	const int table_height = 6;
	
	GameObject[,] block_tbl = new GameObject[table_width,table_height];

	// Use this for initialization
	void Start () {
		Layout();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void Layout(){
		int i,j;
		int w = table_width;
		int h = table_height;
		for( i = 0; i < w; i++ ){
			for( j = 0; j < h; j++ ){
				Instantiate(src,new Vector3((float)(i-(w/2)),0,(float)(j-(h/2))),Quaternion.identity);
				block_tbl[i,j] = (GameObject)Instantiate(src,new Vector3((float)(i-(w/2)),0,(float)(j-(h/2))),Quaternion.identity);
			}
		}
	}
	
	void SerchErase(){
		
	}
}
