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
	
	public enum STATE{
		NORMAL,
		DELETE,
		STATE_MAX
	}

	public TYPE type;
	public STATE state;
	Material mat;
	MeshRenderer mesh_renderer;
	
	
	
	
	// Use this for initialization
	void Start () {
		type = (TYPE)(Random.Range((int)TYPE.TYPE_1,(int)TYPE.TYPE_NORMAL_NUM));
		
		mesh_renderer = GetComponent<MeshRenderer>();
		mat = GetComponent<MeshRenderer>().material;// マテリアルを取得しておく
		
		SetTypeColor();
	}
	
	public void Delete(){
		 SetState(STATE.DELETE);
	}
	
	// Update is called once per frame
	void Update () {
		
		// 自身を消去
		switch(GetState()){
		case STATE.NORMAL:
			break;
		case STATE.DELETE:
			Destroy(this.gameObject);
			break;
		}
	}
	
	
	void SetTypeColor(){
		Color set_color;
		
		switch(type){
		case TYPE.TYPE_1:	set_color = Color.red;		break;
		case TYPE.TYPE_2:	set_color = Color.blue;		break;
		case TYPE.TYPE_3:	set_color = Color.cyan;		break;
		case TYPE.TYPE_4:	set_color = Color.yellow;	break;
		case TYPE.TYPE_5:	set_color = Color.green;	break;
		case TYPE.TYPE_6:	set_color = Color.white;	break;
		default:			set_color = Color.black;	break;
		}
		mat.SetColor("_Color",set_color);
	}
	public void SetMaterial( Material mat ){
		mesh_renderer.material = mat;
	}
	
	// ブロックの種類　
	public void SetBlockType( TYPE _set ){
		type = _set;
	}
	public TYPE GetBlockType(){
		return type;
	}

	// ブロックの状態　
	public STATE GetState(){
		return state;
	}
	public void SetState( STATE _set ){
		state = _set;
	}
}
