using UnityEngine;
using System.Collections;

public class cross_img : MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioSource a_s = this.gameObject.GetComponent<AudioSource> ();
		if (audio_controller.music_isplaying != 1) {
			a_s.enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
       
            
	}

    void reload() {
        Application.LoadLevel(Application.loadedLevel);
    }

}
