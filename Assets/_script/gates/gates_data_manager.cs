using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Umeng;
public class gates_data_manager : MonoBehaviour {


	public int chang_number = 0;
    int enemy_num = 0;
    bool is_win = false;
    bool is_lose = false;
   
    GameObject win_obj,lose_obj;
    // Use this for initialization
    AudioSource audio;

    void Start () {

		GA.StartLevel(Application.loadedLevelName);
		Dictionary<string,string> dic = new Dictionary<string,string >();
		dic["gate_name"] = Application.loadedLevelName;

		GA.EventBeginWithPrimarykeyAndAttributes(tongji.GATE_TIME,Application.loadedLevelName,dic);


        audio = this.gameObject.AddComponent<AudioSource>();
        audio.playOnAwake = true;
        audio.loop = true;
        AudioClip clip = Resources.Load<AudioClip>("sound/game_background");
        audio.clip = clip;
		if (audio_controller.sound_isplaying != 1) {

		} else {
			audio.Play ();
		}
        enemy_num = transform.Find("enemy").childCount;
        Debug.Log("enemy num is "+enemy_num);
         win_obj = Resources.Load<GameObject>("win_canvas");
        Canvas win_ca = win_obj.GetComponent<Canvas>();
        win_ca.renderMode = RenderMode.ScreenSpaceCamera;
        win_ca.worldCamera = Camera.main;
        win_ca.sortingLayerName = "Foreground";
        win_ca.sortingOrder = -1;
		AudioSource w_s = win_ca.gameObject.GetComponent<AudioSource> ();
		if (audio_controller.music_isplaying != 1) {
			w_s.enabled = false;
		}

         lose_obj = Resources.Load<GameObject>("lose_canvas");
        Canvas lose_ca = lose_obj.GetComponent<Canvas>();
        lose_ca.renderMode = RenderMode.ScreenSpaceCamera;
        lose_ca.worldCamera = Camera.main;
        lose_ca.sortingLayerName = "Foreground";
        lose_ca.sortingOrder = -1;
		AudioSource l_s = lose_ca.gameObject.GetComponent<AudioSource> ();
		if (audio_controller.music_isplaying != 1) {
			l_s.enabled = false;		}
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void enemy_die() {

        enemy_num--;
        
    }

    public void  check_win() {

//        Debug.Log("gate_data_manager check_win's enemy_num is "+enemy_num);
        if (is_win || is_lose) {
            return;
        }
        if (enemy_num == 0) {
            win();
            is_win = true;
        }

    }

    public void win() {
		GA.FinishLevel(Application.loadedLevelName);

		GA.EventEndWithPrimarykey(tongji.GATE_TIME,Application.loadedLevelName);
        audio.Stop();
        map_data_manager.add_max_num();
        Instantiate(win_obj, new Vector3(0, 0, 0), win_obj.transform.rotation);
    }

    public void lose() {

		if (is_lose || is_win) {
            return;
        }
		GA.FailLevel(Application.loadedLevelName);
		GA.EventEndWithPrimarykey(tongji.GATE_TIME,Application.loadedLevelName);
        audio.Stop();
        Instantiate(lose_obj, new Vector3(0, 0, 0), lose_obj.transform.rotation);
        Debug.Log("lose");
        is_lose = true;
    }

}
