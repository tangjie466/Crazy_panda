using UnityEngine;
using System.Collections;

public class move_block_controller : MonoBehaviour {

	Transform up,down,left,right;

	float x = 0.0f;
	float y = 0.0f;

	public int move_direction = 0;

	int is_done = 0;

	void Awake(){

		up = transform.Find("up_check");
		down = transform.Find("down_check");
		left = transform.Find("left_check");
		right = transform.Find("right_check");

	}

	// Use this for initialization
	void Start () {
		
	}


    void FixedUpdate() {
        float delay_time = Time.fixedDeltaTime;

        float x = transform.position.x;
        float y = transform.position.y;

        if (move_direction == 1)
        {

            if ((this.y + 1.0f) > (y + delay_time * Common_data.panda_speed))
            {
                transform.Translate(Vector3.up * delay_time * Common_data.panda_speed);
            }
            else
            {
                transform.position = new Vector3(this.x, this.y + 1.0f, 0);
                is_done = 1;
            }

        }
        else if (move_direction == 2)
        {

            if ((this.y - 1.0f) < (y - delay_time * Common_data.panda_speed))
            {
                transform.Translate(Vector3.down * delay_time * Common_data.panda_speed);
            }
            else
            {
                transform.position = new Vector3(this.x, this.y - 1.0f, 0);
                is_done = 1;
            }


        }
        else if (move_direction == 3)
        {

            if ((this.x - 1.0f) < (x - delay_time * Common_data.panda_speed))
            {
                transform.Translate(Vector3.left * delay_time * Common_data.panda_speed);
            }
            else
            {
                transform.position = new Vector3(this.x - 1.0f, this.y, 0);
                is_done = 1;
            }


        }
        else if (move_direction == 4)
        {
            if ((this.x + 1.0f) > (x + delay_time * Common_data.panda_speed))
            {
                transform.Translate(Vector3.right * delay_time * Common_data.panda_speed);
            }
            else
            {
                transform.position = new Vector3(this.x + 1.0f, this.y, 0);
                is_done = 1;
            }

        }
    }
	// Update is called once per frame
	void Update () {

		

	
	}




	public void move(int direction){

		bool is_run = false;

		if (is_done == 1) {
			move_direction = 0;
			is_done = 0;
			return;
		}

		if (move_direction != 0) {
			return;
		}

		Collider2D col = getCollider_street(direction);
		if (col == null) {
			return;
		}

		Debug.Log ("direction is "+move_direction+",tag is "+col.gameObject.tag);



			street_line s_l = col.gameObject.GetComponent<street_line>();
			is_run = s_l.is_run(direction);
			




		if (is_run) {
			col = getCollider(direction);
			if(col != null){

				if(col.tag == "fire_block"){
					return;
				}

				if(col.tag == Common_data.ICE_BLOCK_TAG){



					ice_controller i_c = col.gameObject.GetComponent<ice_controller>();
					if(i_c.cur_state == 0 ){
						i_c.attact();
						return;
					}

					i_c.attact();


				}else if(col.tag == Common_data.BARREL_BLOCK_TAG){
					barrel_controller barrel = col.gameObject.GetComponent<barrel_controller> ();
					barrel.move (move_direction);
                    return;

				}else {
					return;
				}

			}

			bool is_cross = true;
			Collider2D enemy = getEnemyCollider (direction);
			if (enemy != null) {
				
				if (enemy.gameObject.tag == Common_data.NOR_ENEMY_TAG) {
					Debug.Log ("enemy tag is "+enemy.gameObject.tag);
					nor_enemy_controller enc = enemy.gameObject.GetComponent<nor_enemy_controller> ();
					is_cross = enc.is_cross (direction);
				}
			}
			if (is_cross) {
				move_direction = direction;
				x = transform.position.x;
				y = transform.position.y;
			}
		}


	}

	Collider2D getEnemyCollider(int direction){
		RaycastHit2D hit = new RaycastHit2D();
		if(direction == 1){
			

			hit = Physics2D.Linecast(new Vector2(up.position.x,up.position.y-0.1f),new Vector2(up.position.x,up.position.y),1<<LayerMask.NameToLayer("enemy"));
			if (hit.collider != null) {
				return hit.collider;
			}


		}else if(direction == 2){


			hit =  Physics2D.Linecast(new Vector2(down.position.x,down.position.y+0.1f),new Vector2(down.position.x,down.position.y),1<<LayerMask.NameToLayer("enemy"));
			if(hit.collider != null){
				return hit.collider;
			}


		}else if(direction == 3){
			
			hit =  Physics2D.Linecast(new Vector2(left.position.x+0.1f,left.position.y),new Vector2(left.position.x,left.position.y),1<<LayerMask.NameToLayer("enemy"));
			if(hit.collider != null){
				return hit.collider;
			}


		}else if(direction == 4){


			hit =  Physics2D.Linecast(new Vector2(right.position.x-0.1f,right.position.y),new Vector2(right.position.x,right.position.y),1<<LayerMask.NameToLayer("enemy"));
			if(hit.collider != null){
				return hit.collider;
			}


		}
		//		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
		return hit.collider;
	}


	Collider2D getCollider_street(int direction){

		RaycastHit2D hit = new RaycastHit2D();
		if(direction == 1){

			hit = Physics2D.Linecast(new Vector2(up.position.x,up.position.y-0.1f),new Vector2(up.position.x,up.position.y),1<<LayerMask.NameToLayer("street"));
			return hit.collider;
		
		}else if(direction == 2){

			
			hit =  Physics2D.Linecast(new Vector2(down.position.x,down.position.y+0.1f),new Vector2(down.position.x,down.position.y),1<<LayerMask.NameToLayer("street"));
			return hit.collider;
			
		}else if(direction == 3){
			
			hit = Physics2D.Linecast(new Vector2(left.position.x+0.1f,left.position.y),new Vector2(left.position.x,left.position.y),1<<LayerMask.NameToLayer("street"));
			return hit.collider;
			
		}else if(direction == 4){

			
			
			hit =  Physics2D.Linecast(new Vector2(right.position.x-0.1f,right.position.y),new Vector2(right.position.x,right.position.y),1<<LayerMask.NameToLayer("street"));
			return hit.collider;
			
		}
		//		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
		return hit.collider;

	}

	Collider2D getCollider(int direction){
		
		RaycastHit2D hit = new RaycastHit2D();
		
		if(direction == 1){
			hit = Physics2D.Linecast(new Vector2(up.position.x,up.position.y-0.1f),new Vector2(up.position.x,up.position.y),1<<LayerMask.NameToLayer("block"));
			if(hit.collider != null){
				return hit.collider;
			}



		}else if(direction == 2){
			hit =  Physics2D.Linecast(new Vector2(down.position.x,down.position.y+0.1f),new Vector2(down.position.x,down.position.y),1<<LayerMask.NameToLayer("block"));
			if(hit.collider != null){
				return hit.collider;
			}




			
		}else if(direction == 3){
			hit = Physics2D.Linecast(new Vector2(left.position.x+0.1f,left.position.y),new Vector2(left.position.x,left.position.y),1<<LayerMask.NameToLayer("block"));
			if(hit.collider != null){
				return hit.collider;
			}



			
		}else if(direction == 4){
			hit =  Physics2D.Linecast(new Vector2(right.position.x-0.1f,right.position.y),new Vector2(right.position.x,right.position.y),1<<LayerMask.NameToLayer("block"));
			if(hit.collider != null){
				return hit.collider;
			}




			
		}
		//		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
		return hit.collider;
	}




}
