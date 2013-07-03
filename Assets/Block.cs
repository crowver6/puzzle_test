using UnityEngine;
using System.Collections;



public class Block : MonoBehaviour {

	public enum TYPE{
		TYPE_1,
		TYPE_2,
		TYPE_3,
		TYPE_4,
		TYPE_5,
		TYPE_6,
		TYPE_SP1,
		TYPE_SP2,
		TYPE_SP3,
		
		TYPE_MAX,
		
		TYPE_NORMAL_NUM = TYPE_SP1,
		TYPE_SP_NUM = TYPE_MAX - TYPE_NORMAL_NUM
	}

	public int type;
	Material mat;
	MeshRenderer mesh_renderer;
	
	
	
	
	// Use this for initialization
	void Start () {
		type = (int)(Random.Range((int)TYPE.TYPE_1,(int)TYPE.TYPE_NORMAL_NUM));
		
		mesh_renderer = GetComponent<MeshRenderer>();
		mat = GetComponent<MeshRenderer>().material;// マテリアルを取得しておく
		
		SetTypeColor();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	void SetTypeColor(){
		Color set_color;
		
		switch(type){
		case (int)TYPE.TYPE_1:	set_color = Color.red;		break;
		case (int)TYPE.TYPE_2:	set_color = Color.blue;		break;
		case (int)TYPE.TYPE_3:	set_color = Color.cyan;		break;
		case (int)TYPE.TYPE_4:	set_color = Color.yellow;	break;
		case (int)TYPE.TYPE_5:	set_color = Color.green;	break;
		case (int)TYPE.TYPE_6:	set_color = Color.white;	break;
		default:				set_color = Color.black;	break;
		}
		mat.SetColor("_Color",set_color);
	}
	
	
	void SetMaterial( Material mat ){
		mesh_renderer.material = mat;
	}
}
