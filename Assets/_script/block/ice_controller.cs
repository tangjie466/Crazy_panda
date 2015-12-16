using UnityEngine;
using System.Collections;

public class ice_controller : MonoBehaviour {

	const string IS_FRAGMENT = "is_fragment";
	const string IS_ATTACT = "is_attact";

	public int cur_state = 0; 
	Transform transform;
	Animator player;

	bool is_remove_self = false;
	bool is_remove_collider = false;


	public int  atc = 0;

    AudioSource audio;
    enum SOUND_TYPE { state_1,state_2};

    void load_sound(SOUND_TYPE t) {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
        AudioClip clip = null;
        switch (t) {
            case SOUND_TYPE.state_1:
                clip = Resources.Load<AudioClip>("sound/ice_1");
                break;
            case SOUND_TYPE.state_2:
                clip = Resources.Load<AudioClip>("sound/ice_2");
                break;

        }

        audio.PlayOneShot(clip);
    }

	void Awake(){

        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = false;
        audio.loop = false;
        


		transform = GetComponentInParent<Transform>();
		player = GetComponentInParent<Animator>();
		if(cur_state == 1){

			player.SetTrigger(IS_ATTACT);
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (atc == 1)
        {
            player.SetTrigger(IS_ATTACT);
            cur_state = 1;
            atc = 0;
        }

        if (is_remove_self)
        {
            is_remove_self = false;
            DestroyImmediate(this.gameObject);

        }

    }


	void FixedUpdate(){
        

        if (is_remove_collider)
        {
            is_remove_collider = false;
            DestroyImmediate(this.gameObject.GetComponent<BoxCollider2D>());
        }

    }

	void step_down(){

	}


	void remove_collider(){
	
		is_remove_collider = true;

	}

	void remove_self(){
		is_remove_self = true;

	}

	public void attact(){
		if(cur_state  == 0){

            load_sound(SOUND_TYPE.state_1);
            atc = 1;

		}else if(cur_state == 1){
            cur_state = 2;
            load_sound(SOUND_TYPE.state_2);
            player.SetTrigger(IS_FRAGMENT);

		}

	}

	void OnTriggerEnter2D(Collider2D col){



	}



}
