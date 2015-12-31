using UnityEngine;
using System.Collections;
using Umeng;
public class scence_manger : MonoBehaviour {
	[HideInInspector]
    public GameObject setlayer;
	[HideInInspector]
    public GameObject o;
    public const string loading_scence = "loading_scence";
    AudioSource button_audio;

	GameObject game_setLayer;

	float ads_yz_1 = 0.95f; //RESTART

	float ads_yz_2 = 0.75f;//SELECT GATE
	float ads_yz_3 = 0.55f;//NEXT GATE

    // Use this for initialization
    void Start()
    {


        button_audio = this.gameObject.AddComponent<AudioSource>() as AudioSource;

        button_audio.clip = (AudioClip)Resources.Load<AudioClip>("sound/button");
        button_audio.playOnAwake = false;
        button_audio.loop = false;


		game_setLayer  = Resources.Load<GameObject>("sound_canvas");
		if(game_setLayer != null){
			Canvas c = game_setLayer.GetComponent<Canvas>();
			c.renderMode = RenderMode.ScreenSpaceCamera;
			c.worldCamera = Camera.current;
			
		}
		
		setlayer = Resources.Load<GameObject>("setting_canvas");


        if (setlayer != null) { 

            Canvas c = setlayer.GetComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceCamera;
            c.worldCamera = Camera.current;
        }
         o = Resources.Load<GameObject>("panda_cross_canvas");
        Canvas cross_canvas = o.GetComponent<Canvas>();
        if (cross_canvas != null) {
            cross_canvas.renderMode = RenderMode.ScreenSpaceCamera;
            cross_canvas.worldCamera = Camera.main;
            cross_canvas.sortingLayerName = "Foreground";
            cross_canvas.sortingOrder = 100;
            
        }
    }

	void play_sound(){
	
		if (audio_controller.sound_isplaying == 1) {
			button_audio.Play();
		}

	}

	public void quiteApp(){
		GA.EventEnd(tongji.GAME_BEGIN);
		Application.Quit();

	}

	// Update is called once per frame
	void Update () {
	
		if ( Application.platform == RuntimePlatform.Android &&(Input.GetKeyDown(KeyCode.Escape)))
		{
			if(Application.loadedLevelName.Equals("index")){


				ads_controller.share_ads().quite_app("quiteApp");

			}else{

				gate_common.next_gate = "index";
				Application.LoadLevel("loading_scence");
			}
		}

	}
    //开始游戏 
    public void beginGame() {

      	play_sound();
        gate_common.next_gate = "select_gate";
        
        Application.LoadLevel(loading_scence);
    }
    //退出游戏
    public void quiteGame() {
        Time.timeScale = 1;
      play_sound();
		gate_common.next_gate = "index";
		
		Application.LoadLevel(loading_scence);
    }
    //重新开始
    public void restart_scence() {
		GA.FailLevel(Application.loadedLevelName);
		GA.EventEndWithPrimarykey(tongji.GATE_TIME,Application.loadedLevelName);
      play_sound();
        Debug.Log("restart game");
		ads_controller.share_ads().show_nor_ads(ads_yz_1);
        Instantiate(o, new Vector3(0, 0, 0), o.gameObject.transform.rotation);

    }
    //游戏里的设置按钮
    public void gate_setting() {
      play_sound();
        Time.timeScale = 0;
        Instantiate(setlayer, new Vector3(0, 0, 0), setlayer.gameObject.transform.rotation);


    }
    //主页面的设置
    public void game_setting() {
		play_sound ();
	
		Instantiate(game_setLayer, game_setLayer.gameObject.transform.position, game_setLayer.gameObject.transform.rotation);
    }

    //开始某一关
    public void start_gate_scence(int i) {


    }
    //打开选关场景
    public void select_gate_scence() {
		GA.FailLevel(Application.loadedLevelName);
		GA.EventEndWithPrimarykey(tongji.GATE_TIME,Application.loadedLevelName);
		ads_controller.share_ads().show_nor_ads(ads_yz_2);
		Time.timeScale = 1;
        gate_common.next_gate = "select_gate";
      	play_sound();
        Application.LoadLevel(loading_scence);
    }

    //设置音效
    public void set_sound(bool is_open) {
    }

    //设置背景音乐
    public void set_music(bool is_open) {
    }

    //观看广告 
//    public void open_ads(int type = 1) {
//
//    }

	//

	public void next_gate(){
		ads_controller.share_ads().show_nor_ads(ads_yz_3);
		if(map_data_manager.cur_num != map_data_manager.max_num)
			map_data_manager.cur_num++;
		gate_common.next_gate = "gate_"+map_data_manager.cur_num;
		Application.LoadLevel("loading_scence");
	}

    //继续游戏
    public void continue_game() {
      
        Debug.Log("root name is "+this.transform.root.name+",parent name is "+this.transform.parent.name);
      
        Time.timeScale = 1;
      	play_sound();
        Destroy(this.gameObject.transform.root.gameObject);

    }
}
