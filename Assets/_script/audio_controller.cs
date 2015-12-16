using UnityEngine;
using System.Collections;

public class audio_controller : MonoBehaviour {
    public static int music_isplaying = 1;
    public static int sound_isplaying = 1;
    AudioSource audio;
	// Use this for initialization
	void Start () {
        music_isplaying = PlayerPrefs.GetInt(Common_data.music_pref, 1);
        sound_isplaying = PlayerPrefs.GetInt(Common_data.sound_pref, 1);

        audio = this.gameObject.GetComponent<AudioSource>();
        if (music_isplaying == 1)
        {

            audio.Play();
        }
        else {
            audio.Pause();
        }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
