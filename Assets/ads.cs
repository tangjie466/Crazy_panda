using UnityEngine;
using System.Collections;

public class ads : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void show_reward_ads(){

		ads_controller.share_ads().show_reward_ads();
	}

	public void reward_result(string s){

		ads_controller.share_ads().reward_result(s);
	}

}
