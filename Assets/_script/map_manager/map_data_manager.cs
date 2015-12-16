using UnityEngine;
using System.Collections;

public class map_data_manager : MonoBehaviour {

    public pag_num page;
    int cur_page = 1;
    public map_content con;
    [HideInInspector]
    public static int max_num = 1;

    public static int cur_num = 1;
    public const string MAX_NUM = "max_num";

    AudioSource button_audio;

    // Use this for initialization
    void Awake() {

        button_audio = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        button_audio.clip = Resources.Load<AudioClip>("sound/button");
        button_audio.playOnAwake = false;
        button_audio.loop = false;

        max_num = PlayerPrefs.GetInt(MAX_NUM, 1);
        
        cur_num = max_num;
    }

	void Start () {

        cur_page = ((max_num - 1) / 10) + 1;
        if (cur_page > 5)
        {
            cur_page = 5;
        }
        else if (cur_page < 1) {
            cur_page = 1;
        }
        page.setString(cur_page);
        con.init_position(cur_page);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
	void play_sound(){
		if (audio_controller.sound_isplaying != 1) {
			return;
		}
		button_audio.Play();
	}

    public void next_map() {
       play_sound();
        cur_page++;
        if (cur_page > 5) {
            cur_page = 5;
            return;
        }
        page.setString(cur_page);
        con.set_position(cur_page);
    }

    public void pre_map() {
       play_sound();
        cur_page--;
        if (cur_page < 1) {
            cur_page = 1;
            return;
        }
        page.setString(cur_page);
        con.set_position(cur_page);
    }


    static public void add_max_num() {
        if (cur_num == max_num)
        {
            max_num++;
            cur_num++;
            PlayerPrefs.SetInt(MAX_NUM, max_num);
        }
    }

}
