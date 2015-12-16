using UnityEngine;
using System.Collections;

public class trap_controller : MonoBehaviour {

	public int direction = 1;//1:左 2:右

	public gates_data_manager g_d_m;

	int change_times = 0;

	Animator player;
    AudioSource audio;
	void Awake(){

        audio = this.gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("sound/trap");
        audio.playOnAwake = false;
        audio.loop = false;
        audio.clip = clip;
		change_times = g_d_m.chang_number;
		player = this.gameObject.GetComponent<Animator> ();
		if (direction == 1) {
		
			player.CrossFade ("left_idle",0f);

		} else {
			player.CrossFade ("right_idle",0f);
		}
	}

    void load_sound() {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
        audio.Play();
    }

	// Use this for initialization
	void Start () {
		

	}
	
	// Update is called once per frame
	void Update () {
	
		if(change_times != g_d_m.chang_number){
			chang_diretion();
			
		}

	}

    void FixedUpdate() {
        if (change_times != g_d_m.chang_number)
        {
            
            change_times = g_d_m.chang_number;
        }
    }

	void chang_diretion(){
		if (direction == 2) {
			player.SetInteger ("move_direction",1);
			direction = 1;
		} else {
			player.SetInteger ("move_direction",2);
			direction = 2;
		}

	}

	public void change(){
        load_sound();
		change_times++;
		g_d_m.chang_number++;
		chang_diretion();


	}

	public int getChangeTimes(){

		return change_times;

	}

	void OnTriggerEnter2D( Collider2D col ){

		if(col.tag == "trap_collider")
			change();


	}


	
}
