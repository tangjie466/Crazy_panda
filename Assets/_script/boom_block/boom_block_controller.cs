using UnityEngine;
using System.Collections;

public class boom_block_controller : MonoBehaviour {



    const string EXPLODE = "is_explode";

    //	public int type = 0; //0 :stand , 1 :unidirection move   2 :circle move  3:line_move

    public int move_direction = 1; // 1:up,2:down,3:left,4:right

    public GameObject go_1 = null, go_2 = null, go_3 = null, go_4 = null;

    public float move_speed = 1.0f;

    public GameObject explode;
    int is_dead = 0;//0:alive , 1:dead

    Transform go_tra_1 = null, go_tra_2 = null, go_tra_3 = null, go_tra_4 = null;

    Animator player;
    void Awake() {

        if (go_1 != null)
            go_tra_1 = go_1.transform;
        if (go_2 != null)
            go_tra_2 = go_2.transform;
        if (go_3 != null)
            go_tra_3 = go_3.transform;
        if (go_4 != null)
            go_tra_4 = go_4.transform;

        if (go_tra_1 == null && go_tra_2 == null && go_tra_3 == null && go_tra_4 == null) {
            move_direction = -1;
        }

        player = transform.Find("boom_block").gameObject.GetComponent<Animator>();
        Debug.Log("boom ok");
    }

    // Use this for initialization
    void Start() {

    }

    void Update(){
        }
    // Update is called once per frame
    void FixedUpdate () {

        float y = 0.0f;

        float x = 0.0f;
       
        
        if (is_dead == 1){
			return ;
		}
        
		

		float delay_time = 0.0f;
		x = transform.position.x;
		y = transform.position.y;
		delay_time = Time.fixedDeltaTime;


		float move_offset = move_speed*delay_time ;

		if(move_direction == 1){

			if(x == go_tra_1.position.x){

				if((move_offset+y)>=go_tra_1.position.y){

					transform.position = go_tra_1.transform.position;
					if(go_2 != null){
						move_direction = 4;
					}else{
						move_direction = 2;
					}


				}else{

					transform.Translate(Vector3.up*move_offset);
				}

			}else if( x == go_tra_2.position.x){
				if((move_offset+y)>=go_tra_2.position.y){
					
					transform.position = go_tra_2.transform.position;
					if(go_1!=null){
						move_direction = 3;
					}else {
						move_direction = 2;
					}
				}else{
					
					transform.Translate(Vector3.up*move_offset);
				}
			}


		}else if(move_direction  == 2){

			if(x == go_tra_4.position.x){
				
				if((y-move_offset)<=go_tra_4.position.y){
					
					transform.position = go_tra_4.transform.position;
					if(go_3 != null){
						move_direction = 4;
					}else{
						move_direction = 1;
					}
					
					
				}else{
					
					transform.Translate(Vector3.down*move_offset);
				}
				
			}else if( x == go_tra_3.position.x){
				if((y - move_offset)<=go_tra_3.position.y){
					
					transform.position = go_tra_3.transform.position;
					if(go_4!=null){
						move_direction = 3;
					}else {
						move_direction = 1;
					}
				}else{
					
					transform.Translate(Vector3.down*move_offset);
				}
			}

		}else if(move_direction == 3){

			if(y == go_tra_1.position.y){
				
				if((x-move_offset)<=go_tra_1.position.x){
					
					transform.position = go_tra_1.transform.position;
					if(go_4 != null){
						move_direction = 2;
					}else{
						move_direction = 4;
					}
					
					
				}else{
					
					transform.Translate(Vector3.left*move_offset);
				}
				
			}else if( y == go_tra_4.position.y){
				if((x- move_offset)<=go_tra_4.position.x){
					
					transform.position = go_tra_4.transform.position;
					if(go_1!=null){
						move_direction = 1;
					}else {
						move_direction = 4;
					}
				}else{
					
					transform.Translate(Vector3.left*move_offset);
				}
			}



		}else if(move_direction == 4){
            

			if(y == go_tra_2.position.y){
				
				if((x+move_offset)>=go_tra_2.position.x){
					
					transform.position = go_tra_2.transform.position;
					if(go_3 != null){
						move_direction = 2;
					}else{
						move_direction = 3;
					}
					
					
				}else{
					
					transform.Translate(Vector3.right*move_offset);
				}
				
			}else if( y == go_tra_3.position.y){
				if((move_offset+x)>=go_tra_3.position.x){
					
					transform.position = go_tra_3.transform.position;
					if(go_2!=null){
						move_direction = 1;
					}else {
						move_direction = 3;
					}
				}else{
					
					transform.Translate(Vector3.right*move_offset);
				}
			}
		}
	}


	void OnTriggerEnter2D( Collider2D col ){
		
		if(col.gameObject.tag == Common_data.BARELL_COLLIDER_TAG){
            return;
           
		} 
        //		checkCollider (col);
        else if (col.gameObject.tag == Common_data.PANDA_TAG)
        {
            float y = 0.0f;

            float x = 0.0f;
           
            panda_controller p_c = col.gameObject.GetComponent<panda_controller>();
            p_c.dead();
            boom();
        }
        else if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG || col.gameObject.tag == Common_data.BARREL_BLOCK_TAG) {
            boom();
        }


		

	}



	public void destroy_collider(){
		Destroy(this.gameObject.GetComponent<BoxCollider2D>());
	}

	public void destroy_self(){
		Destroy(this.gameObject);
	}

	public void boom(){

        destroy_collider();
		player.SetTrigger(EXPLODE);
		is_dead = 1;
		GameObject explode_instance  = (GameObject)Instantiate(explode,transform.position,explode.transform.rotation);
	}



}
