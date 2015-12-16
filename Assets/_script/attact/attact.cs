using UnityEngine;
using System.Collections;

public class attact : MonoBehaviour {


	Transform my_transform;
	[HideInInspector]
	public int move_direction = 0; //1:上 2:下 3:左 4:右 0:静止 －1:死亡
	public int stand_direction = 1; //1:上 2:下 3:左 4:右
	
	private Vector3 dead_position;
	
	public float x_dead_speed = 0.5f;
	public float y_dead_speed = 0.2f;
	
	public GameObject explode;
	
	int last_move = 0;
	float dead_time=0;

	Transform move_enemy;

	Animator player;
	
	public AnimationCurve x_dead_offset,y_dead_offset;

    AudioSource audio;

    enum SOUND_TYPE { hit,dead,bullet};
    BoxCollider2D bar;
	void Awake(){

        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;

		my_transform = this.gameObject.GetComponent<Transform> ();
		move_enemy = my_transform.Find ("ob");

		player = move_enemy.Find("attact_enemy").gameObject.GetComponent<Animator>();
        bar = this.gameObject.transform.Find("barrel").GetComponent<BoxCollider2D>();
        
		
		
		if (stand_direction == 1) {
			player.CrossFade ("stand_", 0);
		} else if (stand_direction == 2) {
			player.CrossFade ("down",0);
		}else if (stand_direction == 3) {
			player.CrossFade ("left_stand",0);
		}else if (stand_direction == 4) {
			player.CrossFade ("right_stand",0);
		}

		
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
                clip = Resources.Load<AudioClip>("sound/enemy_dead_2");
                break;
            case SOUND_TYPE.bullet:
                clip = Resources.Load<AudioClip>("sound/bullet");
                break;
        }

        audio.PlayOneShot(clip);
    }
	
	// Use this for initialization
	void Start () {
		
	}
	

	public void fire_attact(){
        load_sound(SOUND_TYPE.bullet);
		if(stand_direction == 1){
			player.CrossFade("stand_attact",0);
		}else if(stand_direction == 2){
			player.CrossFade("down_attact",0);
		}else if(stand_direction == 3){
			player.CrossFade("left_attact",0);
		}else if(stand_direction == 4){
			player.CrossFade("right_attact",0);
		}

	}

    void FixedUpdate() {
        if (move_direction == 0)
        {


        }
        else if (move_direction == 1)
        {

            move_enemy.eulerAngles = new Vector3(0, 0, 90);
            my_transform.Translate(Vector3.up * Common_data.panda_speed * Time.fixedDeltaTime);

        }
        else if (move_direction == 2)
        {

            move_enemy.eulerAngles = new Vector3(0, 0, -90);
            my_transform.Translate(Vector3.down * Common_data.panda_speed * Time.fixedDeltaTime);

        }
        else if (move_direction == 3)
        {

            move_enemy.eulerAngles = new Vector3(0, 180, 0);
            my_transform.Translate(Vector3.left * Common_data.panda_speed * Time.fixedDeltaTime);

        }
        else if (move_direction == 4)
        {

            my_transform.Translate(Vector3.right * Common_data.panda_speed * Time.fixedDeltaTime);

        }
        else if (move_direction == -1)
        {


            if (last_move == 1)
            {

                dead_time += Time.fixedDeltaTime;
                float x = x_dead_offset.Evaluate(dead_time) * x_dead_speed;
                float y = y_dead_offset.Evaluate(dead_time) * y_dead_speed;

                my_transform.position = dead_position + new Vector3(x, y, 0);
            }
            else if (last_move == 2)
            {
                dead_time += Time.fixedDeltaTime;
                float x = x_dead_offset.Evaluate(dead_time) * x_dead_speed;
                float y = y_dead_offset.Evaluate(dead_time) * y_dead_speed;

                my_transform.position = dead_position + new Vector3(x, y, 0);
            }
            else if (last_move == 3)
            {
                dead_time += Time.fixedDeltaTime;
                float x = x_dead_offset.Evaluate(dead_time) * x_dead_speed;
                float y = y_dead_offset.Evaluate(dead_time) * y_dead_speed;

                my_transform.position = dead_position + new Vector3(x, y, 0);
            }
            else if (last_move == 4)
            {
                
                dead_time += Time.fixedDeltaTime;
                float x = x_dead_offset.Evaluate(dead_time) * x_dead_speed;
                float y = y_dead_offset.Evaluate(dead_time) * y_dead_speed;

                my_transform.position = dead_position + new Vector3(x, y, 0);
            }




        }
    }

	// Update is called once per frame
	void Update () {
		
		
		
		
		
	}
	

	
	void OnTriggerEnter2D( Collider2D col ){
		
		if (move_direction == 0) {
			Debug.Log("dead tag _1is "+col.tag);	
			Debug.Log("dead psotion is "+col.transform.position.x+","+col.transform.position.y);
			if (col.gameObject.tag == Common_data.PANDA_TAG) {
                load_sound(SOUND_TYPE.hit);
                bar.enabled = false;
				player.CrossFade("dead_begin",0);
				panda_controller p_c = col.gameObject.GetComponent<panda_controller> ();
				move_direction = p_c.move_direction;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
				Debug.Log ("panda move dead move is "+p_c.move_direction);
			}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
				player.CrossFade("dead_begin",0);
                load_sound(SOUND_TYPE.hit);
                bar.enabled = false;
                barrel_controller b_c = col.gameObject.GetComponent<barrel_controller> ();
				move_direction = b_c.move_direction;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
			}else if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG){
				player.CrossFade("dead_begin",0);
				
				if (stand_direction == 1) {
					last_move = 2;
				} else if (stand_direction == 2) {
					last_move = 1;
				} else if (stand_direction == 3) {
					last_move = 4;
				} else if (stand_direction == 4) {
					last_move = 3;
				}
				player.CrossFade("dead_end",0);
				move_direction = -1;
				dead_position = my_transform.position;
				GameObject explode_instance  = (GameObject)Instantiate(explode,my_transform.position,explode.transform.rotation);
                dead();
                Destroy (this.gameObject.GetComponent<BoxCollider2D>());
			}
			
		} else {
			Debug.Log ("dead tag is "+col.gameObject.tag);

			if(col.tag == Common_data.BARELL_COLLIDER_TAG){
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
			
			
			player.CrossFade("dead_end",0);
			last_move = move_direction;
			move_direction = -1;
			dead_position = my_transform.position;
			GameObject explode_instance  = (GameObject)Instantiate(explode,dead_position,explode.transform.rotation);
            dead();
            
            Destroy (this.gameObject.GetComponent<BoxCollider2D>());
		} 
		
		
	}



    public void dead() {
        load_sound(SOUND_TYPE.dead);
        if (bar != null)
        {
            bar.enabled = false;
        }
        StartCoroutine(send_dead_msg());
    }

    IEnumerator send_dead_msg() {
       GameObject o = GameObject.Find(Common_data.GATE);
        o.SendMessage("enemy_die");

        yield return null;
        StopCoroutine(send_dead_msg());
    }

}
