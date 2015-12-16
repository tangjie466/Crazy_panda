using UnityEngine;
using System.Collections;

public class nor_enemy_controller : MonoBehaviour {


	Transform up_check,down_check,left_check,right_check,my_transform,normal_enymy;
	[HideInInspector]
	public int move_direction = 0; //1:上 2:下 3:左 4:右 0:静止 －1:死亡
	public int stand_direction = 1; //1:上 2:下 3:左 4:右

	private Vector3 dead_position;

	public float x_dead_speed = 0.5f;
	public float y_dead_speed = 0.2f;

	public GameObject explode;
	bool is_compress = false;

	int last_move = 0;
	float dead_time=0;
	const string DEAD_MOVE = "dead_move";

	Animator player;
    public bool is_stick = false;

	public AnimationCurve x_dead_offset,y_dead_offset;

    AudioSource audio;
    enum SOUND_TYPE { hit,dead};

	void Awake(){
        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;
		my_transform = this.gameObject.GetComponent<Transform> ();
		normal_enymy = my_transform.Find ("normal_enymy");
		player = my_transform.Find ("normal_enymy").gameObject.GetComponent<Animator> ();

		up_check = my_transform.Find ("up_check");
		down_check = my_transform.Find ("down_check");
		left_check = my_transform.Find ("left_check");
		right_check = my_transform.Find ("right_check");


		if (stand_direction == 1) {
			player.CrossFade ("stand_", 0);
		} else if (stand_direction == 2) {
			player.CrossFade ("down",0);
		}else if (stand_direction == 3) {
			player.CrossFade ("left_stand",0);
		}else if (stand_direction == 4) {
			player.CrossFade ("right_stand",0);
		}
		compress ();

	}

    void load_sound(SOUND_TYPE t) {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
        AudioClip clip = null;
        switch (t) {

            case SOUND_TYPE.hit:
                clip = Resources.Load<AudioClip>("sound/enemy_hit");
                break;
            case SOUND_TYPE.dead:
                clip = Resources.Load<AudioClip>("sound/enemy_dead_1");
                break;
                

        }
        audio.clip = clip;
        audio.Play();
    }

	// Use this for initialization
	void Start () {
	
	}


	void compress(){

		if (stand_direction == 1) {

			Collider2D col = getCollider (1);
			if (col != null) {
				if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG || col.gameObject.tag == Common_data.ICE_BLOCK_TAG || col.gameObject.tag == "smile_block") {
					
					player.CrossFade ("compress",0);
					is_compress = true;
					return;
				}
			}
			normal_enymy.eulerAngles = new Vector3 (0, 0, 0);	
			player.CrossFade ("stand_", 0);

		} else if (stand_direction == 2) {
			Collider2D col = getCollider (2);
			if (col != null) {
				if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG || col.gameObject.tag == Common_data.ICE_BLOCK_TAG || col.gameObject.tag == "smile_block") {
					normal_enymy.eulerAngles = new Vector3 (0, 0, 180);	
					player.CrossFade ("compress",0);
					is_compress = true;
					return;
				}
			}
			normal_enymy.eulerAngles = new Vector3 (0, 0, 0);	
			player.CrossFade ("down",0);
		} else if (stand_direction == 3) {

			Collider2D col = getCollider (4);
			if (col != null) {
				if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == "smile_block" || col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG || col.gameObject.tag == Common_data.ICE_BLOCK_TAG) {
					normal_enymy.eulerAngles = new Vector3 (0, 0, -90);	
					player.CrossFade ("compress",0);
					is_compress = true;
					return;
				}
			}

			normal_enymy.eulerAngles = new Vector3 (0, 0, 0);	
			player.CrossFade ("left_stand",0);
		}else if(stand_direction == 4){
			Collider2D col = getCollider (3);
			if (col != null) {
				if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == "smile_block" || col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG || col.gameObject.tag == Common_data.ICE_BLOCK_TAG) {
					normal_enymy.eulerAngles = new Vector3 (0, 0, 90);	
					player.CrossFade ("compress",0);
					is_compress = true;
					return;
				}
			}
			normal_enymy.eulerAngles = new Vector3 (0, 0, 0);	

			player.CrossFade ("right_stand",0);
		}
		is_compress = false;
	}


    void Update() {
        if (move_direction == 0) {
            compress();
        }
    }
	// Update is called once per frame
	void FixedUpdate () {

		if (move_direction == 0) {
			
			Collider2D col  = null;
			if(stand_direction == 1){
				col =  getCollider(2);



			}else if(stand_direction == 2){

				col =  getCollider(1);

			}else if(stand_direction == 3){

				col =  getCollider(3);

			}else if(stand_direction == 4){

				col =  getCollider(4);
			}
			if(col == null){
				initActionState();
				if (stand_direction == 1) {
					last_move = 2;
				} else if (stand_direction == 2) {
					last_move = 1;
				} else if (stand_direction == 3) {
					last_move = 4;
				} else if (stand_direction == 4) {
					last_move = 3;
				}
				player.SetInteger (DEAD_MOVE,2);
				move_direction = -1;
				dead_position = my_transform.position;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
                dead();
                Destroy (this.gameObject.GetComponent<BoxCollider2D>());
			}


		} else if (move_direction == 1) {

			normal_enymy.eulerAngles = new Vector3(0,0,90);
			my_transform.Translate (Vector3.up * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 2) {
			
			normal_enymy.eulerAngles = new Vector3(0,0,-90);
			my_transform.Translate (Vector3.down * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 3) {

			normal_enymy.eulerAngles = new Vector3(0,180,0);
			my_transform.Translate (Vector3.left * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 4) {

			my_transform.Translate (Vector3.right * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == -1) {


			if (last_move == 1) {

				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				my_transform.position = dead_position + new Vector3 (x, y, 0);
			} else if (last_move == 2) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				my_transform.position = dead_position + new Vector3 (x, y, 0);
			}else if (last_move == 3) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				my_transform.position = dead_position + new Vector3 (x, y, 0);
			}else if (last_move == 4) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				my_transform.position = dead_position + new Vector3 (x, y, 0);
			}




		}



	}

	void initActionState(){
		player.SetInteger (DEAD_MOVE , 0);
	}

	void OnTriggerEnter2D( Collider2D col ){

		if (move_direction == 0) {

			if (col.gameObject.tag == Common_data.PANDA_TAG) {
                if (is_stick == true) {
                    initActionState();
                    if (stand_direction == 1)
                    {
                        last_move = 2;
                    }
                    else if (stand_direction == 2)
                    {
                        last_move = 1;
                    }
                    else if (stand_direction == 3)
                    {
                        last_move = 4;
                    }
                    else if (stand_direction == 4)
                    {
                        last_move = 3;
                    }
                    player.SetInteger(DEAD_MOVE, 2);
                    move_direction = -1;
                    dead_position = my_transform.position;
                    Instantiate(explode, my_transform.position, explode.transform.rotation);
                    dead();
                    Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                    
                    return;
                }

                initActionState ();
                load_sound(SOUND_TYPE.hit);
                player.SetTrigger ("dead_begin");
				panda_controller p_c = col.gameObject.GetComponent<panda_controller> ();
				move_direction = p_c.move_direction;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
				Debug.Log ("panda move dead move is "+p_c.move_direction);
			}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
				initActionState ();
				player.SetTrigger ("dead_begin");
				barrel_controller b_c = col.gameObject.GetComponent<barrel_controller> ();
				move_direction = b_c.move_direction;
                load_sound(SOUND_TYPE.hit);
                GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
			}else if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG){
				initActionState ();

				if (stand_direction == 1) {
					last_move = 2;
				} else if (stand_direction == 2) {
					last_move = 1;
				} else if (stand_direction == 3) {
					last_move = 4;
				} else if (stand_direction == 4) {
					last_move = 3;
				}
				player.SetInteger (DEAD_MOVE,2);
				move_direction = -1;
				dead_position = my_transform.position;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
                dead();
                Destroy (this.gameObject.GetComponent<BoxCollider2D>());
			}

		} else {
			Debug.Log ("dead tag is "+col.gameObject.tag);
			if(col.tag == Common_data.BARELL_COLLIDER_TAG || col.tag == Common_data.TRAP_COLLIDER_TAG){
				return;
			}
			if (move_direction == 1) {

				if (col.tag == Common_data.UP_BLOCK_TAG || col.tag == Common_data.LEFT_BLOCK_TAG || col.tag == Common_data.RIGHT_BLOCK_TAG || col.tag == Common_data.STREET_TAG) {
					return;
				}

			}else if(move_direction == 2){
				if (col.tag == Common_data.DOWN_BLOCK_TAG || col.tag == Common_data.LEFT_BLOCK_TAG || col.tag == Common_data.RIGHT_BLOCK_TAG || col.tag == Common_data.STREET_TAG) {
					return;
				}
				
			}else if(move_direction == 3){
				if (col.tag == Common_data.UP_BLOCK_TAG || col.tag == Common_data.LEFT_BLOCK_TAG || col.tag == Common_data.DOWN_BLOCK_TAG || col.tag == Common_data.STREET_TAG ) {
					return;
				}

			}else if(move_direction == 4){
				if (col.tag == Common_data.UP_BLOCK_TAG || col.tag == Common_data.DOWN_BLOCK_TAG || col.tag == Common_data.RIGHT_BLOCK_TAG || col.tag == Common_data.STREET_TAG ) {
					return;
				}
			}


			initActionState ();
			player.SetInteger (DEAD_MOVE,2);
			last_move = move_direction;
			move_direction = -1;
			dead_position = my_transform.position;
			GameObject explode_instance  = (GameObject)Instantiate(explode,dead_position,explode.transform.rotation);
            dead();
            Destroy (this.gameObject.GetComponent<BoxCollider2D>());
		} 


	}

	Collider2D getCollider(int direction){

		RaycastHit2D hit = new RaycastHit2D();

		if(direction == 1){
			hit = Physics2D.Linecast(new Vector2(my_transform.position.x,my_transform.position.y),new Vector2(up_check.position.x,up_check.position.y),1<<LayerMask.NameToLayer("block"));
		}else if(direction == 2){
			hit =  Physics2D.Linecast(new Vector2(my_transform.position.x,my_transform.position.y),new Vector2(down_check.position.x,down_check.position.y),1<<LayerMask.NameToLayer("block"));
		}else if(direction == 3){
			hit = Physics2D.Linecast(new Vector2(my_transform.position.x,my_transform.position.y),new Vector2(left_check.position.x,left_check.position.y),1<<LayerMask.NameToLayer("block"));
		}else if(direction == 4){
			hit =  Physics2D.Linecast(new Vector2(my_transform.position.x,my_transform.position.y),new Vector2(right_check.position.x,right_check.position.y),1<<LayerMask.NameToLayer("block"));
		}
		//		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
		return hit.collider;
	}

	public void move(){

		Debug.Log ("enymy move");

	}



	public bool is_cross(int direction){
		Debug.Log ("enymy move");

		if (stand_direction == 1) {

			if (direction == 2) {
				return false;
			}
		} else if (stand_direction == 2) {
			if (direction == 1) {
				return  false;
			}
		} else if (stand_direction == 3) {
			if (direction == 3) {
				return false;
			}
		} else if (stand_direction == 4) {
			if (direction == 4) {
				return false;
			}
		}

		return true;
	}



    public void dead()
    {
        load_sound(SOUND_TYPE.dead);
        StartCoroutine(send_dead_msg());
    }

    IEnumerator send_dead_msg()
    {
        GameObject o = GameObject.Find(Common_data.GATE);
        o.SendMessage("enemy_die");

        yield return null;
        StopCoroutine(send_dead_msg());
    }
}
