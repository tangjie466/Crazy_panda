using UnityEngine;
using System.Collections;

public class music_button : MonoBehaviour {


     
	// Use this for initialization
	void Start () {
        if (audio_controller.music_isplaying == 1)
        {
            this.gameObject.SetActive(true);
        }
        else {
            this.gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (audio_controller.music_isplaying == 1)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void close_music() {
        audio_controller.music_isplaying = 0;
        PlayerPrefs.SetInt(Common_data.music_pref,0);

    }

}
