using UnityEngine;
using System.Collections;

public class non_music_button_listener : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (audio_controller.music_isplaying == 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (audio_controller.music_isplaying == 0)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    public void open_music()
    {
        audio_controller.music_isplaying = 1;
        PlayerPrefs.SetInt(Common_data.music_pref, 1);

    }
}
