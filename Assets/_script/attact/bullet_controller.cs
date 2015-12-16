using UnityEngine;
using System.Collections;



public class bullet_controller : MonoBehaviour {

	public GameObject explode;

	[HideInInspector]
	public int move_direction = 0;
    AudioSource audio;
	void Awake(){
        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;
        AudioClip clip = Resources.Load<AudioClip>("sound/enemy_hit");
        audio.clip = clip;
	}

	// Use this for initialization
	void Start () {
		if(move_direction == 1){
			transform.eulerAngles = new Vector3(0,0,180);
			
		}else if(move_direction == 2){
			transform.eulerAngles = new Vector3(0,0,0);
		}else if(move_direction == 3){
			transform.eulerAngles = new Vector3(0,0,-90);
			
		}else if(move_direction == 4){
			transform.eulerAngles = new Vector3(0,0,90);
		}
		
	
	}
	
	// Update is called once per frame
	void Update () {

        /*	if(move_direction == 1){

                transform.Translate(Vector3.up*Time.deltaTime*Common_data.bullet_speed);

            }else if(move_direction == 2){

                transform.Translate(Vector3.down*Time.deltaTime*Common_data.bullet_speed);

            }else if(move_direction == 3){

                transform.Translate(Vector3.left*Time.deltaTime*Common_data.bullet_speed);

            }else if(move_direction == 4){

                transform.Translate(Vector3.right*Time.deltaTime*Common_data.bullet_speed);

            }*/
            


    }

    void FixedUpdate() {
        if (move_direction != 0)
            transform.Translate(Vector3.down * Time.fixedDeltaTime * Common_data.bullet_speed);
    }

    void load_sound() {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
        audio.Play();
    }

	void OnTriggerEnter2D( Collider2D col ){

		Debug.Log ("attact tag is "+col.tag);

		if(col.gameObject.tag == Common_data.PANDA_TAG){

			explode_effect (col);
			panda_controller p_c = col.gameObject.GetComponent<panda_controller>();
			p_c.dead();
            load_sound();
            dead();

		}else if(col.gameObject.tag == Common_data.ICE_BLOCK_TAG){
			explode_effect (col);
            dead();
			ice_controller i_c = col.gameObject.GetComponent<ice_controller>();
			i_c.attact();



		}else if(col.gameObject.tag  == Common_data.UP_BLOCK_TAG){

			if(move_direction == 1){
				unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
				u_c.open_door();
			}else if(move_direction == 2){
				explode_effect (col);
                load_sound();
                dead();

			}else {
				return;
			}

		}else if(col.gameObject.tag == Common_data.DOWN_BLOCK_TAG){

			if(move_direction == 1){
				explode_effect (col);
                load_sound();
                dead();

			}else if(move_direction == 2){
				unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
				u_c.open_door();
			}else {
				return;
			}


		}else if(col.gameObject.tag == Common_data.LEFT_BLOCK_TAG){

			if(move_direction == 3){
				unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
				u_c.open_door();
			}else if(move_direction == 4){
				explode_effect (col);
                load_sound();
                dead();

			}else {
				return;
			}


		}else if(col.gameObject.tag == Common_data.RIGHT_BLOCK_TAG){
			if(move_direction == 4){
				unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
				u_c.open_door();
			}else if(move_direction == 3){
				explode_effect (col);
                load_sound();
                dead();

			}else {
				return;
			}

		}else {

			if(col.gameObject.tag != Common_data.BARELL_COLLIDER_TAG && col.gameObject.tag != Common_data.ATTACT_ENEMY_TAG && col.gameObject.tag != Common_data.STREET_TAG && col.gameObject.tag!= "trap_collider" && col.gameObject.tag!="trap" && col.gameObject.tag != Common_data.BULLET_TAG){

				explode_effect (col);
                load_sound();
                dead();
			}

		}

	}

    void dead() {

        Destroy(this.gameObject.GetComponent<BoxCollider2D>());
        move_direction = 0;
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false; 
        StartCoroutine(destory_self(this.gameObject));
    }

    IEnumerator destory_self(GameObject o) {
        yield return new WaitForSeconds(0.5f);
        Destroy(o);
        StopCoroutine(destory_self(o));
    }

    void explode_effect(Collider2D col){
		explode.transform.eulerAngles = transform.eulerAngles;
		if(move_direction == 1){
			explode.transform.position = col.transform.position + Vector3.down*1.3f;

		}else if(move_direction == 2){
			explode.transform.position = col.transform.position + Vector3.up*1.3f;

		}else if(move_direction == 3){

			explode.transform.position = col.transform.position + Vector3.right*1.3f;

		}else if(move_direction == 4){

			explode.transform.position = col.transform.position + Vector3.left*1.3f;
		}




		Instantiate(explode,explode.gameObject.transform.position,explode.transform.rotation);
	}

}
