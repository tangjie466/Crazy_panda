using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class advertisement_bt : MonoBehaviour {


	int cur_state = 0;
	// Use this for initialization
	void Start () {
	
		cur_state = ads_controller.share_ads().get_reward_ads_state();
		Debug.Log("cur_video_state is "+cur_state);
		if(cur_state == 0){
			this.gameObject.SetActive(false);
		}else{
			this.gameObject.SetActive(true);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if(cur_state == 0){
			cur_state = ads_controller.share_ads().get_reward_ads_state();
			if(cur_state == 1){
				this.gameObject.SetActive( true);
			}
		}

	}
}
