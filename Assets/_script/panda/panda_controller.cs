using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class panda_controller : MonoBehaviour {

	private int begin_x = 0;
	private int begin_y = 0;
	public int move_direction = 0;

	private Animator player;
//	private Rigidbody2D rig;
	private Transform transform;

	public const string READY_DIRECTION = "ready_direction";
	public const string ATTACT = "attact";




	public const string STICKS = "sticks";
	public const string STICKS_DIRECTION = "sticks_direction";
	public const string STAND_DIRECTION = "stand_direction";
	public const string FALL = "falling";

	public const string LEFT_CHECK = "left_check";
	public const string RIGHT_CHECK = "right_check";
	public const string UP_CHECK = "up_check";
	public const string DOWN_CHECK = "down_check";



	private Transform left_check;
	private Transform right_check;
	private Transform up_check;
	private Transform down_check;



	private string up_block_tag = "";
	private string down_block_tag = "";
	private string left_block_tag = "";
	private string right_block_tag = "";


	GameObject last_trap_obj;

	private Vector3 dead_position;

	private Vector3  last_position;

	public bool isSticks = false;


	public int off_set =  10;

	public float x_dead_speed = 0.5f;
	public float y_dead_speed = 0.2f;

	public AnimationCurve x_dead_offset;
	public AnimationCurve y_dead_offset;


	private float dead_delaytime = 0;
	private int pre_dead_move_direction = 0;

	public GameObject explode;

	[HideInInspector]
	public int move_speed = Common_data.panda_speed;



	public int stand_direction = 1;
	
	private bool isTouch = false;
	private int isSticksTriger =  0; //  0000   up down left right 



	public const string IS_DEAD = "is_dead" ;

    GameObject o;

    AudioSource panda_audio,enu_audio;

    enum SOUND_TYPE { barell,mental,standby,move,sitck,dead};
   
    void Awake(){

        panda_audio = this.gameObject.AddComponent<AudioSource>();
        panda_audio.playOnAwake = false;
        panda_audio.loop = false;

		enu_audio = this.gameObject.AddComponent<AudioSource>();
		enu_audio.playOnAwake = false;
		enu_audio.loop = false;

        //		player = GetComponentInParent<Animator>();
        last_trap_obj = null;
		transform = GetComponentInParent<Transform> ();
		player = transform.Find("panda").gameObject.GetComponentInParent<Animator>();
		left_check = transform.Find(LEFT_CHECK);
		right_check = transform.Find(RIGHT_CHECK);
		up_check = transform.Find(UP_CHECK);
		down_check = transform.Find(DOWN_CHECK);
			
		if (stand_direction == 1) {

			player.CrossFade ("down", 0);

		} else if (stand_direction == 2) {
			player.CrossFade ("stand_",0);

		}else if(stand_direction == 3){

			player.CrossFade ("left_stand_",0);
		}else if(stand_direction == 4){
			player.CrossFade ("right_stand_",0);
		}
	}


	void FixedUpdate(){

        if (move_direction != -1)
        {
            if (isTouch)
            {
                initActionState();

                if (stand_direction == 1)
                {

                    player.CrossFade("down", 0);

                }
                else if (stand_direction == 2)
                {
                    player.CrossFade("stand_", 0);

                }
                else if (stand_direction == 3)
                {

                    player.CrossFade("left_stand_", 0);
                }
                else if (stand_direction == 4)
                {
                    player.CrossFade("right_stand_", 0);
                }
                
                isTouch = false;
                move_direction = 0;
                return;
            }
        }





        if (isSticks == true)
        {





            float off_set = 0;

            if (move_direction == 1)
            {

                if ((isSticksTriger >> 3) == 1)
                {
                    
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 7);
                    stand_direction = 1;
                    move_direction = 0;
                    isSticksTriger = 0;
                    last_position = transform.position;
                    if (up_block_tag == Common_data.SNOW_BLOCK_TAG)
                    {
                        isSticks = true;
                    }
                    else
                    {
                        isSticks = false;
                    }
                    return;
                }


                transform.Translate(Vector3.up * move_speed * Time.fixedDeltaTime);
                off_set = transform.position.y - last_position.y;
                if (off_set >= 1.0)
                {
                    transform.position = last_position + Vector3.up;
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 5);

                    move_direction = 0;
                    last_position = transform.position;
                    getSticksTrigerState();
                    if (checkIsFalling())
                    {
                        player.SetTrigger(FALL);
                        return;
                    }

                    if (stand_direction == 1)
                    {
                        if (up_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 2)
                    {
                        if (down_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 3)
                    {
                        if (left_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 4)
                    {
                        if (right_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }



                }

            }
            else if (move_direction == 2)
            {

                if ((isSticksTriger & 4) == 4)
                {
                    Debug.Log("isSticksTriger up");
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 7);
                    stand_direction = 2;
                    move_direction = 0;
                    isSticksTriger = 0;
                    last_position = transform.position;
                    if (down_block_tag == Common_data.SNOW_BLOCK_TAG)
                    {
                        isSticks = true;
                    }
                    else
                    {
                        isSticks = false;
                    }
                    return;
                }


                UnityEngine.Debug.Log("attact 2");
                transform.Translate(Vector3.down * move_speed * Time.fixedDeltaTime);
                off_set = last_position.y - transform.position.y;
                if (off_set >= 1.0)
                {
                    transform.position = last_position + Vector3.down;
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 5);
                    move_direction = 0;
                    last_position = transform.position;
                    getSticksTrigerState();
                    if (checkIsFalling())
                    {
                        player.SetTrigger(FALL);
                        return;
                    }
                    if (stand_direction == 1)
                    {
                        if (up_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 2)
                    {
                        if (down_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 3)
                    {
                        if (left_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 4)
                    {
                        if (right_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                }


            }
            else if (move_direction == 3)
            {
                UnityEngine.Debug.Log("attact 3");

                if ((isSticksTriger & 2) == 2)
                {
                    Debug.Log("isSticksTriger up");
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 7);
                    stand_direction = 3;
                    move_direction = 0;
                    isSticksTriger = 0;
                    last_position = transform.position;
                    if (left_block_tag == Common_data.SNOW_BLOCK_TAG)
                    {
                        isSticks = true;
                    }
                    else
                    {
                        isSticks = false;
                    }
                    return;
                }

                transform.Translate(Vector3.left * move_speed * Time.fixedDeltaTime);
                off_set = transform.position.x - last_position.x;
                if (off_set <= -1.0)
                {
                    transform.position = last_position + Vector3.left;
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 5);
                    move_direction = 0;
                    last_position = transform.position;
                    getSticksTrigerState();
                    if (checkIsFalling())
                    {
                        player.SetTrigger(FALL);
                        return;
                    }
                    if (stand_direction == 1)
                    {
                        if (up_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 2)
                    {
                        if (down_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 3)
                    {
                        if (left_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 4)
                    {
                        if (right_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }



                }
            }
            else if (move_direction == 4)
            {
                UnityEngine.Debug.Log("attact 4");

                if ((isSticksTriger & 1) == 1)
                {
                    Debug.Log("isSticksTriger up");
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 7);
                    stand_direction = 4;
                    move_direction = 0;
                    isSticksTriger = 0;
                    last_position = transform.position;
                    if (right_block_tag == Common_data.SNOW_BLOCK_TAG)
                    {
                        isSticks = true;
                    }
                    else
                    {
                        isSticks = false;
                    }
                    return;
                }

                transform.Translate(Vector3.right * move_speed * Time.fixedDeltaTime);

                off_set = transform.position.x - last_position.x;
                if (off_set >= 1.0)
                {
                    transform.position = last_position + Vector3.right
                        ;
                    initActionState();
                    player.SetInteger(READY_DIRECTION, 5);
                    move_direction = 0;
                    last_position = transform.position;
                    getSticksTrigerState();
                    if (checkIsFalling())
                    {
                        player.SetTrigger(FALL);
                        return;
                    }
                    if (stand_direction == 1)
                    {
                        if (up_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 2)
                    {
                        if (down_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 3)
                    {
                        if (left_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                    else if (stand_direction == 4)
                    {
                        if (right_block_tag == Common_data.SNOW_BLOCK_TAG)
                        {
                            isSticks = true;
                        }
                        else
                        {
                            isSticks = false;
                        }
                    }
                }
            }
            else if (move_direction == 0)
            {

                check_win();
            }

            ListenMouseAction();

            return;
        }
        if (move_direction == 1)
        {
            UnityEngine.Debug.Log("attact 1");


            transform.Translate(Vector3.up * move_speed * Time.fixedDeltaTime);

            Collider2D up_collider = getCollider(1);
            if (up_collider)
            {


                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {


                    barrel_controller barrel = up_collider.gameObject.GetComponent<barrel_controller>();
                    barrel.move(1);


                }
                else if (up_collider.tag == Common_data.ICE_BLOCK_TAG)
                {

                    ice_controller i_c = up_collider.gameObject.GetComponent<ice_controller>();
                    if (i_c.cur_state == 1)
                    {

                        move(2);
                        i_c.attact();
                        return;
                    }

                    i_c.attact();


                }
                else if (up_collider.tag == Common_data.UP_BLOCK_TAG)
                {

                    unidirection_controller u_c = up_collider.gameObject.GetComponent<unidirection_controller>();
                    u_c.open_door();
                    return;
                }
                else if (up_collider.tag == Common_data.DOWN_BLOCK_TAG)
                {



                }
                else if (up_collider.tag == Common_data.LEFT_BLOCK_TAG)
                {
                    return;
                }
                else if (up_collider.tag == Common_data.RIGHT_BLOCK_TAG)
                {
                    return;
                }
                else if (up_collider.tag == Common_data.MOVE_BLOCK_TAG)
                {
                    Debug.Log("移动方块");
                    move_block_controller m_b_c = up_collider.gameObject.GetComponent<move_block_controller>();
                    m_b_c.move(1);
                    if (m_b_c.move_direction != 0)
                    {
                        return;
                    }

                }
                else if (up_collider.tag == Common_data.TRAP_TAG)
                {
                    bool is_trap = false;
                    if (last_trap_obj != null)
                    {
                        if (last_trap_obj.Equals(up_collider.gameObject))
                        {
                            is_trap = false;
                        }
                        else
                        {
                            is_trap = true;
                        }
                    }
                    else
                    {
                        is_trap = true;
                    }
                    if (is_trap)
                    {
                        last_trap_obj = up_collider.gameObject;
                        trap_controller t_c = up_collider.gameObject.GetComponent<trap_controller>();
                        t_c.change();
                    }
                    return;

                }
                else if (up_collider.tag == Common_data.METAL_TAG)
                {
                    initActionState();
                    player.SetTrigger(FALL);
                    move_direction = -2;
                    transform.position = up_collider.gameObject.transform.position + Vector3.down;
                    load_sound(SOUND_TYPE.mental);
                    return;

                }




                last_trap_obj = null;

                transform.position = up_collider.gameObject.transform.position + Vector3.down;
                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {
                    stand_direction = 2;
                }
                else
                {
                    stand_direction = 1;
                }
                isTouch = true;

                last_position = transform.position;
                isSticks = f_is_sticks(up_collider);
                getSticksTrigerState();

            }


        }
        else if (move_direction == 2)
        {
            UnityEngine.Debug.Log("attact 2");

            transform.Translate(Vector3.down * move_speed * Time.fixedDeltaTime);
            Collider2D up_collider = getCollider(2);
            if (up_collider)
            {
                if (up_collider.tag == Common_data.ICE_BLOCK_TAG)
                {

                    ice_controller i_c = up_collider.gameObject.GetComponent<ice_controller>();
                    if (i_c.cur_state == 1)
                    {
                        move(2);
                        i_c.attact();
                        return;
                    }
                    i_c.attact();

                }
                else if (up_collider.tag == Common_data.UP_BLOCK_TAG)
                {

                    if (up_collider.gameObject.transform.position.y + 0.5 < transform.position.y)
                    {

                    }
                    else
                    {
                        return;
                    }

                }
                else if (up_collider.tag == Common_data.DOWN_BLOCK_TAG)
                {

                    unidirection_controller u_c = up_collider.gameObject.GetComponent<unidirection_controller>();
                    u_c.open_door();
                    return;

                }
                else if (up_collider.tag == Common_data.LEFT_BLOCK_TAG)
                {
                    return;
                }
                else if (up_collider.tag == Common_data.RIGHT_BLOCK_TAG)
                {
                    return;
                }
                else if (up_collider.tag == Common_data.MOVE_BLOCK_TAG)
                {
                    Debug.Log("移动方块");
                    move_block_controller m_b_c = up_collider.gameObject.GetComponent<move_block_controller>();
                    m_b_c.move(2);
                    if (m_b_c.move_direction != 0)
                    {
                        return;
                    }
                }
                else if (up_collider.tag == Common_data.TRAP_TAG)
                {
                    bool is_trap = false;
                    if (last_trap_obj != null)
                    {
                        if (last_trap_obj.Equals(up_collider.gameObject))
                        {
                            is_trap = false;
                        }
                        else
                        {
                            is_trap = true;
                        }
                    }
                    else
                    {
                        is_trap = true;
                    }
                    if (is_trap)
                    {
                        last_trap_obj = up_collider.gameObject;
                        trap_controller t_c = up_collider.gameObject.GetComponent<trap_controller>();
                        t_c.change();
                    }
                    return;

                }




                last_trap_obj = null;




                transform.position = up_collider.gameObject.transform.position + Vector3.up;
                stand_direction = 2;
                isTouch = true;

                last_position = transform.position;

                isSticks = f_is_sticks(up_collider);
                getSticksTrigerState();

            }


        }
        else if (move_direction == 3)
        {
            UnityEngine.Debug.Log("attact 3");
            
            transform.Translate(Vector3.left * move_speed * Time.fixedDeltaTime);
            Collider2D up_collider = getCollider(3);
            if (up_collider)
            {
                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {

                    //move(2);
                    barrel_controller barrel = up_collider.gameObject.GetComponent<barrel_controller>();
                    barrel.move(3);
                    //return;
                }
                else if (up_collider.tag == Common_data.ICE_BLOCK_TAG)
                {

                    ice_controller i_c = up_collider.gameObject.GetComponent<ice_controller>();
                    if (i_c.cur_state == 1)
                    {
                        move(2);
                        i_c.attact();
                        transform.position = up_collider.gameObject.transform.position + Vector3.right;
                        return;
                    }
                    i_c.attact();

                }
                else if (up_collider.tag == Common_data.MOVE_BLOCK_TAG)
                {
                    Debug.Log("移动方块");
                    move_block_controller m_b_c = up_collider.gameObject.GetComponent<move_block_controller>();
                    m_b_c.move(3);
                    if (m_b_c.move_direction != 0)
                    {
                        return;
                    }
                }
                else if (up_collider.tag == Common_data.UP_BLOCK_TAG)
                {

                    return;
                }
                else if (up_collider.tag == Common_data.DOWN_BLOCK_TAG)
                {

                    return;

                }
                else if (up_collider.tag == Common_data.LEFT_BLOCK_TAG)
                {
                    unidirection_controller u_c = up_collider.gameObject.GetComponent<unidirection_controller>();
                    u_c.open_door();
                    return;
                }
                else if (up_collider.tag == Common_data.RIGHT_BLOCK_TAG)
                {

                }
                else if (up_collider.tag == Common_data.TRAP_TAG)
                {
                    bool is_trap = false;
                    if (last_trap_obj != null)
                    {
                        if (last_trap_obj.Equals(up_collider.gameObject))
                        {
                            is_trap = false;
                        }
                        else
                        {
                            is_trap = true;
                        }
                    }
                    else
                    {
                        is_trap = true;
                    }
                    if (is_trap)
                    {
                        last_trap_obj = up_collider.gameObject;
                        trap_controller t_c = up_collider.gameObject.GetComponent<trap_controller>();
                        t_c.change();
                    }
                    return;

                }
                else if (up_collider.tag == Common_data.METAL_TAG)
                {
                    initActionState();
                    load_sound(SOUND_TYPE.mental);
                    player.SetTrigger(FALL);
                    move_direction = -2;
                    transform.position = up_collider.gameObject.transform.position + Vector3.right;
                    return;

                }




                last_trap_obj = null;

                transform.position = up_collider.gameObject.transform.position + Vector3.right;
                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {
                    stand_direction = 2;
                }
                else
                {
                    stand_direction = 3;
                }
                isTouch = true;

                last_position = transform.position;
                isSticks = f_is_sticks(up_collider);
                getSticksTrigerState();

            }


        }
        else if (move_direction == 4)
        {
            UnityEngine.Debug.Log("attact 4");
            
            transform.Translate(Vector3.right * move_speed * Time.fixedDeltaTime);
            Collider2D up_collider = getCollider(4);
            if (up_collider)
            {
                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {

                    //	move(2);
                    barrel_controller barrel = up_collider.gameObject.GetComponent<barrel_controller>();
                    barrel.move(4);
                    //	return;
                }
                else if (up_collider.tag == Common_data.ICE_BLOCK_TAG)
                {

                    ice_controller i_c = up_collider.gameObject.GetComponent<ice_controller>();
                    if (i_c.cur_state == 1)
                    {
                        move(2);
                        i_c.attact();
                        transform.position = up_collider.gameObject.transform.position + Vector3.left;
                        return;
                    }
                    i_c.attact();

                }
                else if (up_collider.tag == Common_data.MOVE_BLOCK_TAG)
                {
                    Debug.Log("移动方块");
                    move_block_controller m_b_c = up_collider.gameObject.GetComponent<move_block_controller>();
                    m_b_c.move(4);
                    if (m_b_c.move_direction != 0)
                    {
                        return;
                    }
                }
                else if (up_collider.tag == Common_data.UP_BLOCK_TAG)
                {

                    return;
                }
                else if (up_collider.tag == Common_data.DOWN_BLOCK_TAG)
                {

                    return;

                }
                else if (up_collider.tag == Common_data.LEFT_BLOCK_TAG)
                {
                    Debug.Log("left block");
                }
                else if (up_collider.tag == Common_data.RIGHT_BLOCK_TAG)
                {
                    unidirection_controller u_c = up_collider.gameObject.GetComponent<unidirection_controller>();
                    u_c.open_door();
                    return;
                }
                else if (up_collider.tag == Common_data.TRAP_TAG)
                {
                    bool is_trap = false;
                    if (last_trap_obj != null)
                    {
                        if (last_trap_obj.Equals(up_collider.gameObject))
                        {
                            is_trap = false;
                        }
                        else
                        {
                            is_trap = true;
                        }
                    }
                    else
                    {
                        is_trap = true;
                    }
                    if (is_trap)
                    {
                        last_trap_obj = up_collider.gameObject;
                        trap_controller t_c = up_collider.gameObject.GetComponent<trap_controller>();
                        t_c.change();
                    }
                    return;

                }
                else if (up_collider.tag == Common_data.METAL_TAG)
                {
                    initActionState();
                    move_direction = -2;
                    player.SetTrigger(FALL);
                    load_sound(SOUND_TYPE.mental);
                    transform.position = up_collider.gameObject.transform.position + Vector3.left;
                    return;

                }




                last_trap_obj = null;

                Debug.Log("move1");
                transform.position = up_collider.gameObject.transform.position + Vector3.left;
                if (up_collider.tag == Common_data.BARREL_BLOCK_TAG)
                {
                    stand_direction = 2;
                }
                else
                {
                    stand_direction = 4;
                }
                isTouch = true;

                last_position = transform.position;
                isSticks = f_is_sticks(up_collider);
                getSticksTrigerState();

            }

        }
        else if (move_direction == 0)
        {
            Debug.Log("stand");
            if (stand_direction == 1)
            {
                Collider2D col = getStaticCollider(1);
                if (col == null)
                {
                    move(2);
                }
                else
                {

                    transform.position = col.gameObject.transform.position + Vector3.down;
                    check_win();
                }

            }
            else if (stand_direction == 2)
            {

                Collider2D col = getStaticCollider(2);
                if (col == null)
                {
                    move(2);
                }
                else
                {

                    transform.position = col.gameObject.transform.position + Vector3.up;
                    check_win();
                }

            }
            else if (stand_direction == 3)
            {

                Collider2D col = getStaticCollider(3);
                if (col == null)
                {
                    move(2);
                }
                else
                {

                    transform.position = col.gameObject.transform.position + Vector3.right;
                    check_win();

                }

            }
            else if (stand_direction == 4)
            {
                Collider2D col = getStaticCollider(4);
                if (col == null)
                {
                    move(2);
                }
                else
                {

                    transform.position = col.gameObject.transform.position + Vector3.left;
                    check_win();

                }
            }
        }
        else if (move_direction == -1)
        {

            dead_move();

        }


    }

    void load_sound(SOUND_TYPE sound_t) {
		if (audio_controller.sound_isplaying != 1 ) {
			return;
		}
        AudioClip clip = null;
        switch (sound_t) {
			case SOUND_TYPE.barell:
				clip = Resources.Load<AudioClip>("sound/barrel");
				enu_audio.clip = clip;
				enu_audio.PlayOneShot(clip);
			break;


            case SOUND_TYPE.mental:
                clip = Resources.Load<AudioClip>("sound/hit_mental");
				enu_audio.clip = clip;
				enu_audio.PlayOneShot(clip);
                break;
            case SOUND_TYPE.standby:
                clip = Resources.Load<AudioClip>("sound/panda_standby");
				panda_audio.clip = clip;
				panda_audio.PlayOneShot(clip);
                break;
            case SOUND_TYPE.move:
                clip = Resources.Load<AudioClip>("sound/move");
				enu_audio.clip = clip;
				enu_audio.PlayOneShot(clip);
                break;
            case SOUND_TYPE.sitck:
                clip = Resources.Load<AudioClip>("sound/panda_stick");
				enu_audio.clip = clip;
				enu_audio.PlayOneShot(clip);
                break;
            case SOUND_TYPE.dead:
                clip = Resources.Load<AudioClip>("sound/panda_dead");
				panda_audio.clip = clip;
				panda_audio.PlayOneShot(clip);
                break;
        }

        
    }



	// Use this for initialization
	void Start () {
		last_position = transform.position;
        o = GameObject.Find(Common_data.GATE);
        if(o == null) {

            Debug.Log("statrt game obj is null");
        }
    }


	public void initActionState(){
		player.SetInteger(READY_DIRECTION,0);
		player.SetInteger(STICKS_DIRECTION,0);
		player.SetInteger(STAND_DIRECTION,0);

	}
	


	void  getSticksTrigerState(){
		up_block_tag = "";
		down_block_tag = "";
		left_block_tag = "";
		right_block_tag = "";


		isSticksTriger = 0;
		Collider2D col = getCollider(1);
		if(col){
			isSticksTriger += 8;
			up_block_tag = col.gameObject.tag;
		}
		col = getCollider(2);
		if(col){
			isSticksTriger += 4;
			down_block_tag = col.gameObject.tag;
		}
		
		col = getCollider(3);
		if(col){
			isSticksTriger += 2;
			left_block_tag = col.gameObject.tag;
		}
		
		col = getCollider(4);
		if(col){
			isSticksTriger += 1;
			right_block_tag = col.gameObject.tag;
		}

	}
	


	bool checkIsFalling(){
		Debug.Log("isSticksTriger is "+isSticksTriger);
		if(stand_direction == 1){

			if((isSticksTriger&8)!=8){
				move_direction = 5;
				return true;
			}

		}else if(stand_direction == 2){

			if((isSticksTriger&4)!=4){
				move_direction = 5;
				return true;
			}


		}else if(stand_direction == 3){

			if((isSticksTriger&2)!=2){
				move_direction = 5;
				return true;
			}

		}else if(stand_direction == 4){

			if((isSticksTriger&1)!=1){
				move_direction = 5;
				return true;
			}

		}

		return false;
	}



	// Update is called once per frame
	void Update () {

		

		ListenMouseAction ();

	}

    Collider2D getStaticCollider(int direction) {
        RaycastHit2D hit = new RaycastHit2D();

        if (direction == 1)
        {
            hit = Physics2D.Linecast(new Vector2(up_check.position.x, up_check.position.y - 0.1f), new Vector2(up_check.position.x, up_check.position.y), 1 << LayerMask.NameToLayer("block"));
            if (hit.collider == null) {
                return null;
            }
            if (hit.collider.tag == Common_data.UP_BLOCK_TAG || hit.collider.tag == Common_data.LEFT_BLOCK_TAG || hit.collider.tag == Common_data.RIGHT_BLOCK_TAG)
            {
                return null;
            }
        }
        else if (direction == 2)
        {
            hit = Physics2D.Linecast(new Vector2(down_check.position.x, down_check.position.y+0.1f), new Vector2(down_check.position.x, down_check.position.y), 1 << LayerMask.NameToLayer("block"));
            if (hit.collider == null)
            {
                return null;
            }

            if (hit.collider.tag == Common_data.DOWN_BLOCK_TAG || hit.collider.tag == Common_data.LEFT_BLOCK_TAG || hit.collider.tag == Common_data.RIGHT_BLOCK_TAG)
            {
                return null;
            }


        }
        else if (direction == 3)
        {
            hit = Physics2D.Linecast(new Vector2(left_check.position.x+0.1f, left_check.position.y), new Vector2(left_check.position.x, left_check.position.y), 1 << LayerMask.NameToLayer("block"));
            if (hit.collider == null)
            {
                return null;
            }

            if (hit.collider.tag == Common_data.LEFT_BLOCK_TAG || hit.collider.tag == Common_data.UP_BLOCK_TAG || hit.collider.tag == Common_data.DOWN_BLOCK_TAG)
            {
                return null;
            }
        }
        else if (direction == 4)
        {
            hit = Physics2D.Linecast(new Vector2(right_check.position.x-0.1f, right_check.position.y), new Vector2(right_check.position.x, right_check.position.y), 1 << LayerMask.NameToLayer("block"));
            if (hit.collider == null)
            {
                return null;
            }

            if (hit.collider.tag == Common_data.RIGHT_BLOCK_TAG || hit.collider.tag == Common_data.UP_BLOCK_TAG || hit.collider.tag == Common_data.DOWN_BLOCK_TAG)
            {
                return null;
            }
        }

        //		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
        return hit.collider;
    }

	Collider2D getCollider(int direction){

		RaycastHit2D hit = new RaycastHit2D();

		if(direction == 1){
			 hit = Physics2D.Linecast(new Vector2(up_check.position.x, up_check.position.y-0.1f),new Vector2(up_check.position.x,up_check.position.y),1<<LayerMask.NameToLayer("block"));
           
        } else if(direction == 2){
			hit =  Physics2D.Linecast(new Vector2(down_check.position.x, down_check.position.y+0.1f),new Vector2(down_check.position.x,down_check.position.y),1<<LayerMask.NameToLayer("block"));
          


        } else if(direction == 3){
			hit = Physics2D.Linecast(new Vector2(left_check.position.x+0.1f, left_check.position.y),new Vector2(left_check.position.x,left_check.position.y),1<<LayerMask.NameToLayer("block"));
           
        } else if(direction == 4){
			hit =  Physics2D.Linecast(new Vector2(right_check.position.x-0.1f, right_check.position.y),new Vector2(right_check.position.x,right_check.position.y),1<<LayerMask.NameToLayer("block"));
           
        }
        
//		Debug.Log("tag is "+hit.rigidbody.gameObject.name);
		return hit.collider;
	}



	bool f_is_sticks(Collider2D col){
		if(col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG){
			return false;
		}else if(col.gameObject.tag == Common_data.SNOW_BLOCK_TAG){
			return  true;	
		}
		return false;
	}





	public void move_down(){
		move_direction = 2;
		isSticks = false;
		player.SetInteger(READY_DIRECTION,2);
		player.SetTrigger(ATTACT);
	}






	void OnTriggerEnter2D( Collider2D col ){
	
	
//		checkCollider (col);

	}
	


	//

	void OnTriggerStay2D(Collider2D col){
//		checkCollider (col);
	}

	//  do something when panda touch the normal_block

	void do_normal_block(Collider2D col){

		if(isSticks){
			if(last_position != transform.position){
				return;
			}
		}

		bool isStop  = false;
		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;
				stand_direction = 1;
			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				stand_direction = 2;
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				stand_direction = 3;
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				stand_direction = 4;
			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){


			UnityEngine.Debug.Log("oncollision x = "+transform.position.x);
			player.SetInteger(READY_DIRECTION,0);
			UnityEngine.Debug.Log("diretion is "+player.GetInteger(READY_DIRECTION));
			move_direction = 0;
			last_position = transform.position;
			isSticks = false;
		}
		//			rig.velocity = Vector2.zero;
	}


	void do_snow_block(Collider2D col){
		bool isStop  = false;
		if(move_direction == 1){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y > transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.down;
				stand_direction = 1;
			}else{
				isStop = false;
			}
			
		}else if(move_direction == 2){
			
			if( (col.gameObject.transform.position.x  == transform.position.x) && (col.gameObject.transform.position.y < transform.position.y)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.up;
				stand_direction = 2;
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 3){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x < transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.right;
				stand_direction = 3;
			}else{
				isStop = false;
			}
			
			
		}else if(move_direction == 4){
			if( (col.gameObject.transform.position.y  == transform.position.y) && (col.gameObject.transform.position.x > transform.position.x)){
				isStop = true;
				transform.position = col.gameObject.transform.position+Vector3.left;
				stand_direction = 4;
			}else{
				isStop = false;
			}
			
		}
		
		if(isStop == true){
			

			player.SetInteger(READY_DIRECTION,0);

			move_direction = 0;
			last_position = transform.position;
			isSticks = true;
		}
	}









	void checkCollider(Collider2D col){
		if(col.gameObject.tag == Common_data.NORMAL_BLOCK_TAG){
//			UnityEngine.Debug.Log("oncollision");

			do_normal_block(col);


		}else if(col.gameObject.tag == Common_data.SNOW_BLOCK_TAG){
			do_snow_block(col);


		}

	}




	// Listen mouse action to do next action  READY_DIRECTION 1:up 2:down 3:left 4:right:
	void ListenMouseAction(){
        GameObject ob = EventSystem.current.currentSelectedGameObject;
        if (ob != null)
        {
           
            touch_level l = EventSystem.current.currentSelectedGameObject.GetComponent<touch_level>();
            if (l != null) {
                touch_level cur_l = this.gameObject.GetComponent<touch_level>();
                if (cur_l.getLevel() < l.getLevel()) {
                    return;
                }
            }
        }


		if (move_direction != 0) {
			begin_x = 0;
			begin_y = 0;
			return ;
		}
		
		if (Input.GetMouseButtonDown (0)) {

			begin_x = Mathf.CeilToInt(Input.mousePosition.x); 
			begin_y = Mathf.CeilToInt(Input.mousePosition.y);
            load_sound(SOUND_TYPE.standby);
		} else if (Input.GetMouseButton (0)) {
			if (begin_x == 0 && begin_y == 0) {
				return;
			}

			int move_x = Mathf.CeilToInt(Input.mousePosition.x) - begin_x;
			int move_y = Mathf.CeilToInt(Input.mousePosition.y) - begin_y;
//			UnityEngine.Debug.Log("x = "+move_x+",y = "+move_y);
			if(Mathf.Abs(move_x) <= off_set && Mathf.Abs(move_y) <= off_set){
				initActionState();
				player.SetInteger(READY_DIRECTION,5);
			}else{
				if(Mathf.Abs(move_x)>=Mathf.Abs(move_y)){
					if(move_x > 0){
						if(stand_direction == 4){
							initActionState();
							player.SetInteger(READY_DIRECTION,5);
							move_direction = 0;
						}else{
							initActionState();
							player.SetInteger(READY_DIRECTION,4);
						}

					}else{
						if(stand_direction == 3){
							initActionState();
							player.SetInteger(READY_DIRECTION,5);
							move_direction = 0;

						}else{
							initActionState();
							player.SetInteger(READY_DIRECTION,3);
						}

					}

				}else{
					if(move_y > 0){
						if(stand_direction == 1){
							initActionState();
							player.SetInteger(READY_DIRECTION,5);
							move_direction = 0;
						}else{
							initActionState();
							player.SetInteger(READY_DIRECTION,1);
						}

					}else{
						if(stand_direction == 2){
							initActionState();
							player.SetInteger(READY_DIRECTION,5);
							move_direction = 0;
						}else{
							initActionState();
							player.SetInteger(READY_DIRECTION,2);
						}

					}

				}
			}

		} else if (Input.GetMouseButtonUp (0)) {
			if (begin_x == 0 && begin_y == 0) {
				return;
			}
		

			int move_x = Mathf.CeilToInt(Input.mousePosition.x) - begin_x;
			int move_y = Mathf.CeilToInt(Input.mousePosition.y) - begin_y;
//			UnityEngine.Debug.Log("up x = "+move_x+",y = "+move_y);
			if(isSticks == true){

				if(Mathf.Abs(move_x) <= off_set && Mathf.Abs(move_y) <= off_set){
					initActionState();
					player.SetInteger(READY_DIRECTION,5);
				}else{
					if(Mathf.Abs(move_x)>=Mathf.Abs(move_y)){
						if(move_x > 0){
							// attact  right
							if(stand_direction == 4){
								initActionState();
								player.SetInteger(READY_DIRECTION,5);
								move_direction = 0;
							}else if(stand_direction == 3){

								initActionState();
								player.SetInteger(READY_DIRECTION,4);
								player.SetTrigger(STICKS);
                                load_sound(SOUND_TYPE.sitck);
								move_direction = 0;
							}else{
								if(stand_direction == 1){
									// stand left      face right      action :stics_move_right_up
									initActionState();
									player.SetInteger(STICKS_DIRECTION,4);
									player.SetInteger(STAND_DIRECTION,2);
									
								}else if(stand_direction == 2){
									initActionState();
									Debug.Log("right ");
									player.SetInteger(STICKS_DIRECTION,4);
									player.SetInteger(STAND_DIRECTION,1);
								}
                                load_sound(SOUND_TYPE.move);
								move_direction = 4;
							}
						}else{
							
							if(stand_direction == 3){
								move_direction = 0;
								initActionState();
								player.SetInteger(READY_DIRECTION,5);
								
							}else if(stand_direction == 4){
								initActionState();
								player.SetInteger(READY_DIRECTION,3);
								player.SetTrigger(STICKS);
                                load_sound(SOUND_TYPE.sitck);
                                move_direction = 0;
							}else{
								if(stand_direction == 1){
								
									initActionState();
									player.SetInteger(STICKS_DIRECTION,3);
									player.SetInteger(STAND_DIRECTION,2);
									
								}else if(stand_direction == 2){
									initActionState();
									player.SetInteger(STICKS_DIRECTION,3);
									player.SetInteger(STAND_DIRECTION,1);
								}
                                load_sound(SOUND_TYPE.move);
								move_direction = 3;
							}
						}
						
					}else{
						if(move_y > 0){
							if(stand_direction == 1){
								move_direction = 0;
								initActionState();
								player.SetInteger(READY_DIRECTION,5);
								
							}else if(stand_direction == 2){
								initActionState();
								player.SetInteger(READY_DIRECTION,1);
								player.SetTrigger(STICKS);
                                load_sound(SOUND_TYPE.sitck);
								move_direction = 0;

							}else {
								if(stand_direction == 3){
									// stand left      face right      action :stics_move_right_up
									initActionState();
									player.SetInteger(STICKS_DIRECTION,1);
									player.SetInteger(STAND_DIRECTION,4);
									
								}else if(stand_direction == 4){
									initActionState();
									player.SetInteger(STICKS_DIRECTION,1);
									player.SetInteger(STAND_DIRECTION,3);
								}

                                load_sound(SOUND_TYPE.move);
								move_direction = 1;
							}
						}else{
							
							if(stand_direction == 2){
								move_direction = 0;
								initActionState();
								player.SetInteger(READY_DIRECTION,5);
							}else if(stand_direction == 1){
								initActionState();
								player.SetInteger(READY_DIRECTION,2);
								player.SetTrigger(STICKS);
                                load_sound(SOUND_TYPE.sitck);
								move_direction = 0;
							}else{
								if(stand_direction == 3){
									initActionState();
									// stand left      face right      action :stics_move_right_up
									player.SetInteger(STICKS_DIRECTION,2);
									player.SetInteger(STAND_DIRECTION,4);
									
								}else if(stand_direction == 4){
									initActionState();
									player.SetInteger(STICKS_DIRECTION,2);
									player.SetInteger(STAND_DIRECTION,3);
								}
                                load_sound(SOUND_TYPE.move);
								move_direction = 2;
							}
						}
						
					}
					
				}



				return ;
			}

			if(Mathf.Abs(move_x) <= off_set && Mathf.Abs(move_y) <= off_set){
				initActionState();
				player.SetInteger(READY_DIRECTION,5);
			}else{
				if(Mathf.Abs(move_x)>=Mathf.Abs(move_y)){
					if(move_x > 0){
						if(stand_direction == 4){
							initActionState();
							player.SetInteger(READY_DIRECTION,5);
							move_direction = 0;
						}else{
							UnityEngine.Debug.Log("4");
                            load_sound(SOUND_TYPE.move);
							move(4);
						}
					}else{

						if(stand_direction == 3){
							move_direction = 0;
							initActionState();
							player.SetInteger(READY_DIRECTION,5);

						}else{
							UnityEngine.Debug.Log("3");

							move(3);
                            load_sound(SOUND_TYPE.move);
                        }
					}
					
				}else{
					if(move_y > 0){
						if(stand_direction == 1){
							move_direction = 0;
							initActionState();
							player.SetInteger(READY_DIRECTION,5);

						}else {
							move(1);
                            load_sound(SOUND_TYPE.move);
                        }
					}else{

						if(stand_direction == 2){
							initActionState();
							move_direction = 0;
							player.SetInteger(READY_DIRECTION,5);
						}else{
							move(2);
                            load_sound(SOUND_TYPE.move);
                        }
					}
					
				}
			
			}

				
		}


	}




	public void push_move(int direction){
		if(move_direction == 0){
			Collider2D col = getCollider(direction);
			if(col == null){
				move(direction);
			}
		}else{
			move(direction);
		}
	}


	public void move(int direction){
		if(isSticks == false){

			if (move_direction == -1) {
				return;
			}
			Debug.Log("move");
			initActionState();
			player.SetInteger(READY_DIRECTION,direction);
			player.SetTrigger(ATTACT);
			move_direction = direction;
			isTouch = false;
			Debug.Log("move_direction is "+direction);


		}else{


		}


	}

	void dead_state(){
		pre_dead_move_direction = move_direction;
		dead_position = transform.position;
		move_direction = -1;

		stand_direction = -1;
	}

	public void dead_move(){



		if(pre_dead_move_direction == 1){

			if(dead_position.x >= Camera.main.transform.position.x){
				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(-x,y,0);
			}else{

				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(x,y,0);
			}

		}else if(pre_dead_move_direction == 2){

			if(dead_position.x >= Camera.main.transform.position.x){
				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(-x,y,0);
			}else{
				
				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(x,y,0);
			}
			
		}else if(pre_dead_move_direction == 3){
			
			dead_delaytime+=Time.fixedDeltaTime;
			
			float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
			float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
			
			transform.position = dead_position + new Vector3(x,y,0);

		}else if(pre_dead_move_direction == 4){

			dead_delaytime+=Time.fixedDeltaTime;

			float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
			float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;

			transform.position = dead_position + new Vector3(-x,y,0);

			
		}else if(pre_dead_move_direction == 0){
			if(dead_position.x >= Camera.main.transform.position.x){
				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(-x,y,0);
			}else{
				
				dead_delaytime+=Time.fixedDeltaTime;
				
				float x = x_dead_offset.Evaluate(dead_delaytime)*x_dead_speed;
				float y = y_dead_offset.Evaluate(dead_delaytime)*y_dead_speed;
				
				transform.position = dead_position + new Vector3(x,y,0);
			}
			
		}

	}


	public void dead(){
	
		Debug.Log ("panda dead");
		initActionState();
		dead_state();
        load_sound(SOUND_TYPE.dead);
		GameObject explode_instance  = (GameObject)Instantiate(explode,dead_position,explode.transform.rotation);

		Destroy(this.gameObject.GetComponent<Rigidbody2D>());
		Destroy(this.gameObject.GetComponent<BoxCollider2D>());
		player.CrossFade ("dead_",0);
        StartCoroutine(send_dead_msg(o));
	}

    IEnumerator send_dead_msg(GameObject o) {

        yield return new WaitForSeconds(1);
        
        o.SendMessage("lose");
        StopCoroutine(send_dead_msg(o));
    }


    public void check_win()
    {
        if (o == null)
        {
            Debug.Log("null");
        }
        Debug.Log("panda_check_win");
        o.SendMessage("check_win");


    }
}

   
        



