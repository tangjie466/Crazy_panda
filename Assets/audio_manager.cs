using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class audio_manager : MonoBehaviour {
	public Button sound,non_sound,music,non_music;

	public GameObject panel;
	AudioSource audio;

	// Use this for initialization
	void Start () {

		audio = this.gameObject.GetComponent<AudioSource> ();

		if(music == null){
			return;
		}
		if (audio_controller.music_isplaying == 0) {
			music.gameObject.SetActive(true);
			non_music.gameObject.SetActive(false);
		
		} else {
			music.gameObject.SetActive(false);
			non_music.gameObject.SetActive(true);
		}

		if (audio_controller.sound_isplaying == 0) {

			sound.gameObject.SetActive(true);
			non_sound.gameObject.SetActive(false);
		
		} else {

			sound.gameObject.SetActive(false);
			non_sound.gameObject.SetActive(true);

		}

	}
	
	// Update is called once per frame
	void Update () {
		if (audio == null) {
			return;
		}
		if (audio_controller.music_isplaying == 1) {
			if(!audio.isPlaying)
				audio.Play ();
		} else {
			audio.Pause();
		}

	}
	public void close_panel(){
	
		Destroy (panel);
	}



	public void open_sound(){
		sound.gameObject.SetActive(false);
		non_sound.gameObject.SetActive(true);
		audio_controller.sound_isplaying = 1;
		PlayerPrefs.SetInt (Common_data.sound_pref,1);
	}

	public void close_sound(){
		sound.gameObject.SetActive(true);
		non_sound.gameObject.SetActive(false);
		audio_controller.sound_isplaying = 0;
		PlayerPrefs.SetInt (Common_data.sound_pref,0);

	}

	public void open_music(){

		music.gameObject.SetActive(false);
		non_music.gameObject.SetActive(true);
		audio_controller.music_isplaying = 1;
		PlayerPrefs.SetInt (Common_data.music_pref,1);
	}

	public void close_music(){
		music.gameObject.SetActive(true);
		non_music.gameObject.SetActive(false);
		audio_controller.music_isplaying = 0;
		PlayerPrefs.SetInt (Common_data.music_pref,0);
	}

}
