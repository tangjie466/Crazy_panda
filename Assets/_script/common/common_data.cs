using UnityEngine;
using System.Collections;

public class Common_data
{

    public enum touch_enum{ 
        panda = 1,
        button,
        setting
    };




    public static string sound_pref = "sound";
    public static string music_pref = "music";

	public static int panda_speed = 10;
	public static int barrel_speed = 10;
	public static int bullet_speed = 30;
    public static string GATE = "gates_test";

	public static  string NORMAL_BLOCK_TAG = "normal_block";
	public static  string PANDA_TAG = "panda";
	public  static string BARREL_BLOCK_TAG = "barrel_block";

	public static  string SNOW_BLOCK_TAG = "snow_block";

	public static  string ICE_BLOCK_TAG = "ice_block";

	public static string UP_BLOCK_TAG = "up_block";
	public static string DOWN_BLOCK_TAG = "down_block";
	public static string LEFT_BLOCK_TAG = "left_block";
	public static string RIGHT_BLOCK_TAG =  "right_block";
	public static string MOVE_BLOCK_TAG = "move_block";

	public static string TRAP_TAG = "trap";
	public static string METAL_TAG = "metal_block";
    public static string TRAP_COLLIDER_TAG = "trap_collider";

	public static string STREET_TAG = "street";


	public static string NOR_ENEMY_TAG = "normal_enemy";

	public static string BARELL_COLLIDER_TAG = "barell_collider";
	public static string ATTACT_ENEMY_TAG = "attact_enemy";
	public static string BULLET_TAG = "Bullet";


}

