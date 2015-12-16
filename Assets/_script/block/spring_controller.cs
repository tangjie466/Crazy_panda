using UnityEngine;
using System.Collections;

public class spring_controller : MonoBehaviour {

	// Use this for initialization
	const string IS_TRIGER = "is_spring";
	Animator player;
	Transform transform;



	const string LEFT_SPRING_TAG = "left_spring";
	const string RIGHT_SPRING_TAG = "right_spring";
	const string UP_SPRING_TAG = "up_spring";
	const string DOWN_SPRING_TAG = "down_spring";

	string my_tag = "";

	bool collider_num = false;

    AudioSource audio;
	void Awake(){
        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;
        AudioClip clip = Resources.Load<AudioClip>("sound/spring");
        audio.clip = clip;

		player = GetComponentInParent<Animator>();
		transform = GetComponentInParent<Transform>();
		my_tag = gameObject.tag;
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

	}


	void FixedUpdate(){


	}


	void OnTriggerExit2D(Collider2D col){


	}


	void OnTriggerStay2D(Collider2D col){
		//		checkCollider (col);





	}

    void load_sound() {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
		audio.Play();
    }

	void OnTriggerEnter2D( Collider2D col ){
		//		checkCollider (col);

		if(my_tag == LEFT_SPRING_TAG){
			
			if((col.gameObject.transform.position.y <= transform.position.y+0.1 || col.gameObject.transform.position.y >= transform.position.y-0.1)&& col.gameObject.transform.position.x > transform.position.x+0.5){
				if(col.gameObject.tag == Common_data.PANDA_TAG ){
					panda_controller p_c = col.gameObject.GetComponent<panda_controller>();

					player.SetTrigger(IS_TRIGER);

					col.gameObject.transform.position = transform.position+new Vector3(1.5f,0f,0f);	
					p_c.move(4);
					Debug.Log("panda");
					
				}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
					player.SetTrigger(IS_TRIGER);
					barrel_controller b_c = col.gameObject.GetComponent<barrel_controller>();
					col.gameObject.transform.position = transform.position+new Vector3(1.5f,0f,0f);
					b_c.move(4);
					
				}
				
				
				
			}
			
		}else if(my_tag == RIGHT_SPRING_TAG){
			
			if((col.gameObject.transform.position.y <= transform.position.y+0.1 || col.gameObject.transform.position.y >= transform.position.y-0.1)&& col.gameObject.transform.position.x < transform.position.x-0.5){
				if(col.gameObject.tag == Common_data.PANDA_TAG ){
					panda_controller p_c = col.gameObject.GetComponent<panda_controller>();

					player.SetTrigger(IS_TRIGER);

					col.gameObject.transform.position = transform.position+new Vector3(-1.5f,0f,0f);	
					p_c.move(3);
					Debug.Log("panda");
					
				}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
					player.SetTrigger(IS_TRIGER);
					barrel_controller b_c = col.gameObject.GetComponent<barrel_controller>();
					col.gameObject.transform.position = transform.position+new Vector3(-1.5f,0f,0f);
					b_c.move(3);
					
				}


				
			}
		}else if(my_tag == DOWN_SPRING_TAG){
			
			if((col.gameObject.transform.position.x <= transform.position.x+0.1 || col.gameObject.transform.position.x >= transform.position.x-0.1)&& col.gameObject.transform.position.y > transform.position.y+0.5){
				if(col.gameObject.tag == Common_data.PANDA_TAG ){
					panda_controller p_c = col.gameObject.GetComponent<panda_controller>();

					player.SetTrigger(IS_TRIGER);

					col.gameObject.transform.position = transform.position+new Vector3(0f,1.5f,0f);	
					p_c.move(1);
					Debug.Log("panda");
					
				}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
					player.SetTrigger(IS_TRIGER);
					barrel_controller b_c = col.gameObject.GetComponent<barrel_controller>();
					col.gameObject.transform.position = transform.position+new Vector3(0f,1.5f,0f);
					b_c.move(1);
					
				}
				
				
				
			}
			
		}else if(my_tag == UP_SPRING_TAG){

			if((col.gameObject.transform.position.x <= transform.position.x+0.1 || col.gameObject.transform.position.x >= transform.position.x-0.1)&& col.gameObject.transform.position.y < transform.position.y-0.5){
				if(col.gameObject.tag == Common_data.PANDA_TAG ){
					panda_controller p_c = col.gameObject.GetComponent<panda_controller>();

					player.SetTrigger(IS_TRIGER);

					col.gameObject.transform.position = transform.position+new Vector3(0f,-1.5f,0f);	
					p_c.move(2);
					Debug.Log("panda");
					
				}else if(col.gameObject.tag == Common_data.BARREL_BLOCK_TAG){
					player.SetTrigger(IS_TRIGER);
					barrel_controller b_c = col.gameObject.GetComponent<barrel_controller>();
					col.gameObject.transform.position = transform.position+new Vector3(0f,-1.5f,0f);
					b_c.move(2);
					
				}
				
				
				
			}

			
			
		}


	}





}
