using UnityEngine;
using System.Collections;


public class BlockManager : MonoBehaviour {
	
	// リソース管理-
	public GameObject[] src = new GameObject[(int)Block.TYPE.TYPE_NORMAL_NUM];
	
	// 定数-
	const int erase_num = 3;
	
	const int table_width = 8;
	const int table_height = 6;
	
	
	public float wait_time = 0;
	
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
	public Block[,] block_tbl = new Block[table_width,table_height];
	
	public PHASE phase;
	
	

	// Use this for initialization
	void Start () {
		SetPhase(PHASE.LAYOUT);
	}
	
	// Update is called once per frame
	void Update () {
		if( WaitPhase() ){
			switch(phase)
			{
			case PHASE.LAYOUT:
				Layout();
				SetPhase(PHASE.SEARCH_ERASE);
				SetWaitTime(1.0f);
				break;
			case PHASE.WAIT:
				break;
			case PHASE.TOUCH_MOVE:
				break;
			case PHASE.SEARCH_ERASE:
				SerchErase();
				SetPhase(PHASE.ERASE);
				SetWaitTime(1.0f);
				break;
			case PHASE.ERASE:
				SetPhase(PHASE.ADD);
				SetWaitTime(1.0f);
				break;
			case PHASE.ADD:
				Layout();
				SetPhase(PHASE.FALL);
				SetWaitTime(1.0f);
				break;
			case PHASE.FALL:
				Move();
				SetPhase(PHASE.WAIT);
				SetWaitTime(1.0f);
				break;
			}
		}
	}
	
	bool WaitPhase(){
		if( wait_time > 0 ){
			wait_time -= Time.deltaTime;
			if( wait_time < 0){
				wait_time = 0;
				return true;
			}
			return false;
		}
		return true;
	}
	void SetWaitTime( float _time ){
		wait_time = _time;
	}
	
	
	void SetPhase( PHASE _phase ){
		phase = _phase;
	}
	void GetBlockData( int x, int y ){
	}
	
	Vector3 GetTransBlock( int x, int y ){
		return new Vector3((float)(x-(table_width/2)),0,(float)((table_height/2)-y));
	}
	
	Block.TYPE GetBlockTypeRandom(){
		return (Block.TYPE)Random.Range((int)Block.TYPE.TYPE_1,(int)Block.TYPE.TYPE_NORMAL_NUM);
	}
	
	void Layout(){
		int i,j;
		int w = table_width;
		int h = table_height;
		for( i = 0; i < w; i++ ){
			for( j = 0; j < h; j++ ){
				if( block_tbl[i,j] == null ){
					Block.TYPE tmp_type = GetBlockTypeRandom();
					GameObject tmp = Instantiate(src[(int)tmp_type],GetTransBlock(i,j),Quaternion.identity) as GameObject;
					block_tbl[i,j] = (Block)tmp.GetComponent<Block>();
					block_tbl[i,j].SetBlockType(tmp_type);
				}
			}
		}
	}
	
	void SerchErase(){
		Block.TYPE tmp_type = Block.TYPE.TYPE_1;
		int i,j;
		int w = table_width;
		int h = table_height;
		
		int count_line = 0;
		
		// ーー縦方向を調べるーー 
		for( i = 0; i < w; i++ ){
			if( block_tbl[i,0] ){
				count_line = 0;
				tmp_type = block_tbl[0,0].GetBlockType();
				for( j = 0; j < h; j++ ){
					if( block_tbl[i,j] ){
						if( tmp_type == block_tbl[i,j].GetBlockType() ){
							count_line++;
						}
						else
						{
							if( tmp_type != block_tbl[i,j].GetBlockType() || h >= j+1 )// １個前のブロックと違う、列最後のブロック 
							{
								// 消去処理
								// 連続して３個以上同色か？ 
								if( count_line >= erase_num ){
									// さかのぼって連続した同色のブロックを消去状態にする 
									for( ; count_line > 0; count_line-- ){
										block_tbl[i,j-count_line].Delete();
										block_tbl[i,j-count_line] = null;
									}
								}
							}
							// チェックするブロックを変更
							if( block_tbl[i,j] ){
								tmp_type = block_tbl[i,j].GetBlockType();
							}
							count_line = 1;
						}
					}
				}
			}
		}

		// ーー横方向を調べるーー
		for( j = 0; j < h; j++ ){
			if( block_tbl[0,j] ){
				count_line = 0;
				tmp_type = block_tbl[0,0].GetBlockType();
				for( i = 0; i < w; i++ ){
					if( block_tbl[i,j] ){
						if( tmp_type == block_tbl[i,j].GetBlockType() ){
							count_line++;
						}
						else
						{
							if( tmp_type != block_tbl[i,j].GetBlockType() || w >= i+1 ){// １個前のブロックと違う、行最後のブロック 
								// 消去処理
								// 連続して３個以上同色か？ 
								if( count_line >= erase_num ){
									// さかのぼって連続した同色のブロックを消去状態にする 
									for( ; count_line > 0; count_line-- ){
										block_tbl[i-count_line,j].Delete();
										block_tbl[i-count_line,j] = null;
									}
								}
							}
							// チェックするブロックを変更
							if( block_tbl[i,j] ){
								tmp_type = block_tbl[i,j].GetBlockType();
							}
							count_line = 1;
						}
					}
				}
			}
		}
	}
	
	void Move(){
		int i,j;
		int w = table_width;
		int h = table_height;
		
		// ーー横方向を調べるーー
		for( j = h-2; j >= 0; j-- ){
			for( i = 0; i < w; i++ ){
				if( block_tbl[i,j] != null ){
					if( block_tbl[i,j+1] == null ){
						block_tbl[i,j+1] = block_tbl[i,j];
						block_tbl[i,j] = null;
					}
				}
			}
		}
	}
}
