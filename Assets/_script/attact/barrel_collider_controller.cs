using UnityEngine;
using System.Collections;

public class barrel_collider_controller : MonoBehaviour {
	[HideInInspector]
	public int stand_direction = 0;
	int move_direction = 0;
	public GameObject bullet;
	public attact atc;

	bool is_attact = false;

	void Awake(){

		stand_direction = atc.stand_direction;
		if(stand_direction == 1){
			
			transform.eulerAngles = new Vector3(0,0,0);
			move_direction = 1;
		}else if(stand_direction == 2){
			
			transform.eulerAngles = new Vector3(0,0,180);
			move_direction = 2;
			
		}else if(stand_direction == 3){
			
			transform.eulerAngles = new Vector3(0,0,-90);
			move_direction = 4;
			
		}else if(stand_direction == 4){
			
			transform.eulerAngles = new Vector3(0,0,90);
			move_direction = 3;
		}
		is_attact = false;
	}

	// Use this for initialization
	void Start () {

	}
    void FixedUpdate() {
        if (atc.move_direction != 0)
        {
            Destroy(this.gameObject);
        }
    }
	// Update is called once per frame
	void Update () {

		
	
	}


	void OnTriggerEnter2D( Collider2D col ){

		if(atc.move_direction != 0){
			return;
		}
		if(col.gameObject.tag == Common_data.PANDA_TAG){

			bullet_controller b_c = bullet.GetComponent<bullet_controller>();
			b_c.move_direction = this.move_direction;
			atc.fire_attact();
			Instantiate(bullet,transform.position,bullet.transform.rotation);
			is_attact = true;
		}


	}

	void OnTriggerStay2D(Collider2D col){
		if(atc.move_direction != 0){
			return;
		}
		if(col.gameObject.tag == Common_data.PANDA_TAG){

			panda_controller p_c = col.gameObject.GetComponent<panda_controller> ();
			if (p_c.move_direction > 0 ) {

				if (is_attact == true) {
					return;
				} 

			} else if (p_c.move_direction == 0) {
				is_attact = false;
				return;
			} else if (p_c.move_direction < 0) {
				return;
			}
			is_attact = true;
			bullet_controller b_c = bullet.GetComponent<bullet_controller>();
			b_c.move_direction = this.move_direction;
			atc.fire_attact();
			Instantiate(bullet,transform.position,bullet.transform.rotation);

		}

	}


}
