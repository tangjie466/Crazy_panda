using UnityEngine;
using System.Collections;

public class street_line : MonoBehaviour {

	public int line_num = 0;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool is_run(int diretion){

		if(diretion == 1){
			if((line_num&8)==8){
				return true;
			}
		}else if(diretion == 2){
			if((line_num&4)==4){
				return true;
			}
		}else if(diretion == 3){
			if((line_num&2)==2){
				return true;
			}

		}else if(diretion == 4){

			if((line_num&1)==1){
				return true;
			}
		}

		return false;
	}
}
