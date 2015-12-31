using UnityEngine;
using System.Collections;

public class barrel_controller : MonoBehaviour {
	[HideInInspector]
	public int move_direction = 0;
	[HideInInspector]
	public int move_speed = Common_data.barrel_speed;

	const string DOWN_CHECK = "down_check";

	Transform down_check ;

	bool is_stand = true;

	GameObject last_trap_obj;

    AudioSource audio;



	void Awake(){
        audio = this.gameObject.GetComponent<AudioSource>();
		last_trap_obj = null;
		move_direction = 0;
		down_check = transform.Find(DOWN_CHECK);

	}
	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame

    
    void Update () {
		
		
        
        
	}

	void FixedUpdate(){
        if (move_direction == 0)
        {
            RaycastHit2D hit = new RaycastHit2D();
            float x = down_check.position.x;
            float y = down_check.position.y + 0.1f;

            hit = Physics2D.Linecast(new Vector2(x, y), new Vector2(down_check.position.x, down_check.position.y), 1 << LayerMask.NameToLayer("block"));

            last_trap_obj = null;

            if (hit.collider != null && hit.collider.tag != "fire_block" && hit.collider.tag != Common_data.DOWN_BLOCK_TAG && hit.collider.tag != Common_data.LEFT_BLOCK_TAG && hit.collider.tag != Common_data.RIGHT_BLOCK_TAG)
            {



            }
            else
            {
                RaycastHit2D panda_hit = new RaycastHit2D();
                panda_hit = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(down_check.position.x, down_check.position.y), 1 << LayerMask.NameToLayer("panda"));
                //				Debug.Log("tongzi  hit panda tag is "+panda_hit.collider.tag);
                if (panda_hit.collider == null)
                {
                    //					Debug.Log("tongzi  mei pengdao");
                    move_direction = 2;
                }
            }


        }else if (move_direction == 1){
			transform.Translate(Vector3.up*Time.fixedDeltaTime*move_speed);
		}else if(move_direction == 2){

			transform.Translate(Vector3.down*Time.fixedDeltaTime * move_speed);
		}else if(move_direction == 3){
			transform.Translate(Vector3.left*Time.fixedDeltaTime * move_speed);
		}else if(move_direction == 4){
			transform.Translate(Vector3.right*Time.fixedDeltaTime*move_speed);
		}


	}
	void OnTriggerStay2D(Collider2D col){
        //		checkCollider (col);

        if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG)
        {
            do_move_block(col);

        }


    }

	void do_move_block(Collider2D col){

//		if(move_direction == 1){
//			move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
//			m_b_c.move (1);
//			if (m_b_c.move_direction != 0) {
//				return;
//			}
//
//		}else if(move_direction == 2){
//			move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
//			m_b_c.move (2);
//			if (m_b_c.move_direction != 0) {
//				return;
//			}
//		}else if(move_direction == 3){
//
//			move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
//			m_b_c.move (3);
//			if (m_b_c.move_direction != 0) {
//				return;
//			}
//		}else if(move_direction == 4){
//			move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
//			m_b_c.move (4);
//			if (m_b_c.move_direction != 0) {
//				return;
//			}
//
//		}
		move_block_controller m_b_c = col.gameObject.GetComponent<move_block_controller> ();
		m_b_c.move (move_direction);
		if (m_b_c.move_direction != 0) {
			return;
		}
		do_normal_block (col);

	}

    void load_sound() {
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
        audio.Play();
    }
	void OnTriggerEnter2D( Collider2D col ){


        if (col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG || col.gameObject.tag == Common_data.METAL_TAG || col.gameObject.tag == "smile_block")
        {
               load_sound();
               do_normal_block(col);
        }
        else if (col.gameObject.tag == Common_data.PANDA_TAG)
        {
            load_sound();
            do_panda(col);
        }
        else if (col.gameObject.tag == Common_data.UP_BLOCK_TAG)
        {
           
            do_up_block(col);
        }
        else if (col.gameObject.tag == Common_data.DOWN_BLOCK_TAG)
        {
            do_down_block(col);
        }
        else if (col.gameObject.tag == Common_data.LEFT_BLOCK_TAG)
        {
            do_left_block(col);
        }
        else if (col.gameObject.tag == Common_data.RIGHT_BLOCK_TAG)
        {
            do_right_block(col);
        }
        else if (col.gameObject.tag == Common_data.ICE_BLOCK_TAG)
        {
            do_ice_block(col);

        }
        else if (col.gameObject.tag == Common_data.BARREL_BLOCK_TAG)
        {

            do_barrel_block(col);
        }
        else if (col.gameObject.tag == Common_data.TRAP_TAG)
        {
            do_trap(col);
        }
        else if (col.gameObject.tag == Common_data.MOVE_BLOCK_TAG)
        {
           

        }
    }


	void do_up_block(Collider2D col){

		Debug.Log("do_normal_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){
			
			unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
			u_c.open_door();
			return;
		}else if(move_direction == 2){
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				//Debug.Log("down");
			}else{
				isStop = false;
			}

			
		}else if(move_direction == 3){
			return;
			
			
		}else if(move_direction == 4){
			return;
			
		}

		if(isStop == true){
            load_sound();
            if (col.gameObject.transform.position.y+0.5 < transform.position.y){

				move_direction = 0;
			}else{
				return;
			}
		}



	}

	void do_down_block(Collider2D col){

		//Debug.Log("do_down_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){

			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				//Debug.Log("down");
			}else{
				isStop = false;
			}
		}else if(move_direction == 2){

			unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
			u_c.open_door();
			return;



		}else if(move_direction == 3){
			return;


		}else if(move_direction == 4){
			return;

		}

		if(isStop == true)
        {
            load_sound();
            if (col.gameObject.transform.position.y+0.5 < transform.position.y){

				move_direction = 0;
			}else{
				return;
			}
		}


	}


	void do_left_block(Collider2D col){
		bool isStop  = false;
		if(move_direction == 1){
			return;
		}else if(move_direction == 2){

			return;


		}else if(move_direction == 3){
			unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
			u_c.open_door();
			return;



		}else if(move_direction == 4){

			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				//Debug.Log("down");
			}else{
				isStop = false;
			}

		}

		if(isStop == true)
        {
            load_sound();
            if (col.gameObject.transform.position.x-0.5 > transform.position.x){

				move_direction = 0;
			}else{
				return;
			}
		}



	}

	void do_right_block(Collider2D col){
		bool isStop  = false;
		if(move_direction == 1){
			return;
		}else if(move_direction == 2){

			return;


		}else if(move_direction == 4){
			unidirection_controller u_c = col.gameObject.GetComponent<unidirection_controller>();
			u_c.open_door();
			return;



		}else if(move_direction == 3){

			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				//Debug.Log("down");
			}else{
				isStop = false;
			}

		}

		if(isStop == true)
        {
            load_sound();
            if (col.gameObject.transform.position.x+0.5 < transform.position.x){

				move_direction = 0;
			}else{
				return;
			}
		}


	}

	
	void do_barrel_block(Collider2D col){
		Debug.Log("do_barrel_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;
				
			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				Debug.Log("down");
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				
			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){
			barrel_controller b_c = col.gameObject.GetComponent<barrel_controller>();
			b_c.move(move_direction);
            /*if(move_direction == 2){
				move_direction = 0;
			}else{
				move_direction = 2;
			}*/
            move_direction = 0;
		}
	}


	void do_ice_block(Collider2D col){
		Debug.Log("do_ice_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;
				
			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				Debug.Log("down");
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				
			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){
			ice_controller b_c = col.gameObject.GetComponent<ice_controller>();
			b_c.attact();
            move_direction = 0;
		}
	}

	void do_panda(Collider2D col){
		bool isStop  = false;

		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;
				
			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				Debug.Log("down");
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				
			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){
			panda_controller p_c = col.gameObject.GetComponent<panda_controller>();
			p_c.push_move(move_direction);

            move_direction = 0;
		}


	}


	void do_normal_block(Collider2D col){
		
		Debug.Log("do_normal_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;

			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				Debug.Log("down");
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;

			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;

			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){
            move_direction = 0;
		}
		//			rig.velocity = Vector2.zero;
	}


	void do_trap(Collider2D col){
		Debug.Log("do_normal_block "+ move_direction);
		bool isStop  = false;
		if(move_direction == 1){

			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;


			}else{
				isStop = false;
			}

		}else if(move_direction == 2){

			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;

				Debug.Log("down");
			}else{
				isStop = false;
			}


		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;


			}else{
				isStop = false;
			}


		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;


			}else{
				isStop = false;
			}

		}

		if(isStop == true){

			bool is_trap = false;
			if (last_trap_obj != null) {
				if (last_trap_obj.Equals (col.gameObject)) {
					is_trap = false;
				} else {
					is_trap = true;
				}
			} else {
				is_trap = true;
			}
			if (is_trap) {
				last_trap_obj = col.gameObject;
				trap_controller t_c = col.gameObject.GetComponent<trap_controller> ();
				t_c.change ();
			}

		}

	}


	public void move(int direction){

		last_trap_obj = null;
        if (direction == 2)
        {
            RaycastHit2D hit = new RaycastHit2D();
            float x = down_check.position.x;
            float y = down_check.position.y + 0.1f;

            hit = Physics2D.Linecast(new Vector2(x, y), new Vector2(down_check.position.x, down_check.position.y), 1 << LayerMask.NameToLayer("block"));

            last_trap_obj = null;

            if (hit.collider != null && hit.collider.tag != "fire_block" && hit.collider.tag != Common_data.DOWN_BLOCK_TAG && hit.collider.tag != Common_data.LEFT_BLOCK_TAG && hit.collider.tag != Common_data.RIGHT_BLOCK_TAG)
            {

                Debug.Log("tong zi hit  tag is " + hit.collider.tag);

            }
            else
            {
                RaycastHit2D panda_hit = new RaycastHit2D();
                panda_hit = Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(down_check.position.x, down_check.position.y), 1 << LayerMask.NameToLayer("panda"));
                //				Debug.Log("tongzi  hit panda tag is "+panda_hit.collider.tag);
                if (panda_hit.collider == null)
                {
                    //					Debug.Log("tongzi  mei pengdao");
                    move_direction = 2;
                }
            }


        }
        else {
			load_sound();
            move_direction = direction;
        }

    }





}
