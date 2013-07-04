using UnityEngine;
using System.Collections;


public class BlockManager : MonoBehaviour {
	
	// リソース管理-
	public GameObject src;
	public Material[] BlockMat = new Material[(int)Block.TYPE.TYPE_MAX];
	
	// 定数-
	const int erase_num = 3;
	
	const int table_width = 8;
	const int table_height = 6;
	
	public enum PHASE{
		LAYOUT,
		WAIT,
		TOUCH_MOVE,
		SEARCH_ERASE,
		ERASE,
		ADD,
		FALL,
		PHASE_MAX
	}
	
	// 実体を管理-
	Block[,] block_tbl = new Block[table_width,table_height];
	
	public PHASE phase;
	
	

	// Use this for initialization
	void Start () {
		SetPhase(PHASE.LAYOUT);
	}
	
	// Update is called once per frame
	void Update () {
		switch(phase)
		{
		case PHASE.LAYOUT:
			Layout();
			SetPhase(PHASE.SEARCH_ERASE);
			break;
		case PHASE.WAIT:
			break;
		case PHASE.TOUCH_MOVE:
			break;
		case PHASE.SEARCH_ERASE:
			SerchErase();
			SetPhase(PHASE.ERASE);
			break;
		case PHASE.ERASE:
//			yield return new WaitForSeconds(0.5f);
			SetPhase(PHASE.ADD);
			break;
		case PHASE.ADD:
			SetPhase(PHASE.FALL);
			break;
		case PHASE.FALL:
			SetPhase(PHASE.WAIT);
			break;
		}
	}
	
	
	
	
	void SetPhase( PHASE _phase ){
		phase = _phase;
	}
	void GetBlockData( int x, int y ){
	}
	
	Vector3 GetTransBlock( int x, int y ){
		return new Vector3((float)(x-(table_width/2)),0,(float)(y-(table_height/2)));
	}
	
	void Layout(){
		int i,j;
		int w = table_width;
		int h = table_height;
		for( i = 0; i < w; i++ ){
			for( j = 0; j < h; j++ ){
				var tmp = Instantiate(src,GetTransBlock(i,j),Quaternion.identity);
				block_tbl[i,j] = (Block)tmp.GetComponent<Block>();
//				block_tbl[i,j] = (Block)Instantiate(src,GetTransBlock(i,j),Quaternion.identity);
			}
		}
	}
	
	void SerchErase(){
		Block.TYPE tmp_type;
		int i,j,k;
		int w = table_width;
		int h = table_height;
		
		int count_line = 0;
		

		// ーー縦方向を調べるーー
		for( i = 0; i < w; i++ ){
			tmp_type = block_tbl[i,0].GetBlockType();
			count_line = 1;
			for( j = 0; j < h; j++ ){
				if( tmp_type == block_tbl[i,j].GetBlockType() ){
					count_line++;
				}
				else{
					if( count_line >= erase_num ){
						// 3個もまで戻って消去
						for( k = j-1; k >= (j-count_line); k-- ){
							block_tbl[i,k].Delete();
						}
					}
					tmp_type = block_tbl[i,j].GetBlockType();
					count_line = 1;
				}
			}
		}

		// ーー横方向を調べるーー
		for( j = 0; j < h; j++ ){
			tmp_type = block_tbl[0,j].GetBlockType();
			count_line = 1;
			for( i = 0; i < w; i++ ){
				if( tmp_type == block_tbl[i,j].GetBlockType() ){
					count_line++;
				}
				else{
					if( count_line >= erase_num ){
						// 3個もまで戻って消去
						for( k = i-1; k >= (i-count_line); k-- ){
							block_tbl[k,j].Delete();
						}
					}
					tmp_type = block_tbl[i,j].GetBlockType();
					count_line = 1;
				}
			}
		}
	}
}
