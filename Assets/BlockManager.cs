using UnityEngine;
using System.Collections;

public class BlockManager : MonoBehaviour {
	
	public GameObject src;

	// Use this for initialization
	void Start () {
		int i,j;
		int w = 8;
		int h = 6;
		for( i = 0; i < w; i++ ){
			for( j = 0; j < h; j++ ){
				Instantiate(src,new Vector3((float)(i-(w/2)),0,(float)(j-(h/2))),Quaternion.identity);
			}
		}
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI() {
		if( GUI.Button(new Rect(0,0,100,50),"push") ){
			
		}
	}
}
