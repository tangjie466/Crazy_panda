using UnityEngine;
using System.Collections;

public class trap_bottom : MonoBehaviour {
	public gates_data_manager g_d_m;

	int change_times = 0;
	Animator player;
	int current_state = 1; //2:为黄色 1:蓝色


	// Use this for initialization
	void Start () {
	
		player = this.gameObject.GetComponent<Animator> ();

	}
	
	// Update is called once per frame
	void Update () {
		int t_chang_times = g_d_m.chang_number;
		if (t_chang_times != change_times) {
			change_times = t_chang_times;
			if (current_state == 1 && player.GetInteger("color") != 1) {
				player.SetInteger ("color",1);
				current_state = 2;
			} else {
				if(player.GetInteger("color") != 2){
					player.SetInteger ("color",2);
					current_state = 1;
				}
			}

		}

	}
}
