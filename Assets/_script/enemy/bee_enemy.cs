using UnityEngine;
using System.Collections;

public class bee_enemy : MonoBehaviour {

	[HideInInspector]
	public int move_direction = 0;
	public GameObject explode;
	const string DEAD_MOVE = "dead_move";
	private Vector3 dead_position;

	public float x_dead_speed = 0.5f;
	public float y_dead_speed = 0.2f;
	int last_move = 0;
	float dead_time=0;

	public AnimationCurve x_dead_offset,y_dead_offset;
	Transform bee;

	Animator player;

    AudioSource audio;
    enum SOUND_TYPE {hit,dead };

	void Awake(){

        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;

		bee = transform.Find ("bee_enemy");
		player = bee.gameObject.GetComponent<Animator> ();
       
    }

    void load_sound(SOUND_TYPE t) {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
		AudioClip clip = null;
        switch (t)
        {

            case SOUND_TYPE.hit:
                clip = Resources.Load<AudioClip>("sound/enemy_hit");
                break;
            case SOUND_TYPE.dead:
                clip = Resources.Load<AudioClip>("sound/enemy_dead_2");
                break;


        }
        audio.clip = clip;
        audio.Play();
    }

	// Use this for initialization
	void Start () {
	
	}

    void UpDate() { }

	// Update is called once per frame
	void FixedUpdate () {
		if (move_direction == 0) {
			

		} else if (move_direction == 1) {

			bee.eulerAngles = new Vector3(0,0,90);
			transform.Translate (Vector3.up * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 2) {

			bee.eulerAngles = new Vector3(0,0,-90);
			transform.Translate (Vector3.down * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 3) {
			bee.eulerAngles = new Vector3(0,180,0);
			transform.Translate (Vector3.left * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == 4) {

			transform.Translate (Vector3.right * Common_data.panda_speed * Time.fixedDeltaTime);

		} else if (move_direction == -1) {


			if (last_move == 1) {

				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				transform.position = dead_position + new Vector3 (x, y, 0);
			} else if (last_move == 2) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				transform.position = dead_position + new Vector3 (x, y, 0);
			}else if (last_move == 3) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				transform.position = dead_position + new Vector3 (x, y, 0);
			}else if (last_move == 4) {
				dead_time += Time.fixedDeltaTime;
				float x = x_dead_offset.Evaluate (dead_time) * x_dead_speed;
				float y = y_dead_offset.Evaluate (dead_time) * y_dead_speed;

				transform.position = dead_position + new Vector3 (x, y, 0);
			}




		}
	}

	void initActionState(){
      
        player.SetInteger (DEAD_MOVE , 0);
        
	}

	void OnTriggerEnter2D( Collider2D col ){

		if (move_direction == 0) {

			if (col.gameObject.tag == Common_data.PANDA_TAG) {
				initActionState ();
				player.SetTrigger ("dead_begin");
				panda_controller p_c = col.gameObject.GetComponent<panda_controller> ();
				move_direction = p_c.move_direction;
                load_sound(SOUND_TYPE.hit);
				GameObject explode_instance  = (GameObject)Instantiate(explode,transform.position,explode.transform.rotation);
				Debug.Log ("panda move dead move is "+p_c.move_direction);
			}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
				initActionState ();
				player.SetTrigger ("dead_begin");
				barrel_controller b_c = col.gameObject.GetComponent<barrel_controller> ();
				move_direction = b_c.move_direction;
                load_sound(SOUND_TYPE.hit);
                GameObject explode_instance  = (GameObject)Instantiate(explode,transform.position,explode.transform.rotation);
			}else if(col.gameObject.tag == Common_data.MOVE_BLOCK_TAG){
				initActionState ();

				move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
				last_move = m_b_c.move_direction;
				player.SetInteger (DEAD_MOVE,2);
				move_direction = -1;
				dead_position = transform.position;
				GameObject explode_instance  = (GameObject)Instantiate(explode,transform.position,explode.transform.rotation);
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
			dead_position = transform.position;
			GameObject explode_instance  = (GameObject)Instantiate(explode,dead_position,explode.transform.rotation);
            dead();
            Destroy (this.gameObject.GetComponent<BoxCollider2D>());
		} 


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
