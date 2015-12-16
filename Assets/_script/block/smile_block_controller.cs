using UnityEngine;
using System.Collections;

public class smile_block_controller : MonoBehaviour {

	public gates_data_manager g_d_m;

	int change_times = 0;

	public int current_state = 0; //0:为无碰撞体 ，1:为有碰撞体
	const string ADD_BODY = "add_body";
	Animator player;
	Collider2D col;

	void Awake(){
		change_times = 0;
		player = this.gameObject.GetComponent<Animator> ();
		col = this.gameObject.GetComponent<Collider2D> ();
		if (current_state == 0) {
			delete_body ();
		} else {
			add_body ();
		}

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		

	}

    void FixedUpdate() {
        int t_chang_times = g_d_m.chang_number;
        if (t_chang_times != change_times)
        {
            change_times = t_chang_times;
            if (current_state == 1)
            {
                delete_body();
                current_state = 0;
            }
            else
            {
                add_body();
                current_state = 1;
            }

        }
    }
	void delete_body(){
		player.CrossFade ("no_body",0f);
		col.enabled = false;
	}

	void add_body(){

		col.enabled = true;
		player.CrossFade ("body",0f);
	}

}
