using UnityEngine;
using System.Collections;

public class explode : MonoBehaviour {
    AudioSource audio = null;
	bool is_done;
	// Use this for initialization
	void Start () {
		if (audio_controller.sound_isplaying != 1) {
			is_done = false;
			return;
		}
		audio = this.gameObject.AddComponent<AudioSource>();
        AudioClip clip = Resources.Load<AudioClip>("sound/explored");
        audio.clip = clip;
        audio.loop = false;
        audio.playOnAwake = false;

        audio.Play();
		is_done = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(is_done){
			Destroy(this.gameObject);
		}

	}

	public void done(){
		Debug.Log("is Done");
		is_done = true;
	}

}
