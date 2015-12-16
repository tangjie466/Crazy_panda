using UnityEngine;
using System.Collections;

public class fire_controoler : MonoBehaviour {


	public GameObject explode;
	Animator player;
	bool is_compress = false;

	// Use this for initialization
	void Start () {
		is_compress = false;
		player = this.gameObject.GetComponent<Animator>();

	}


	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter2D(Collider2D col){

		if(col.gameObject.tag ==Common_data.PANDA_TAG){
			if (is_compress == true) {
				return;
			}

			panda_controller panda = col.gameObject.GetComponent<panda_controller>();
			GameObject explode_instance  = (GameObject)Instantiate(explode,panda.gameObject.transform.position,explode.transform.rotation);
			panda.dead();
		}else if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
			is_compress = true;
			player.CrossFade("fire_down",0);

		}



		
	}

	void OnTriggerStay2D(Collider2D col){
		if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){

			player.CrossFade("fire_down",0);
			is_compress = true;
		}
	}


	void OnTriggerExit2D(Collider2D col){

		if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
			player.CrossFade("fire_",0);
			is_compress = false;
		}

	}

}
