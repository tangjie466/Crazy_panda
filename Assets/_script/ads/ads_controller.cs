using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;
public class ads_controller : MonoBehaviour {



	AndroidJavaClass mJc;
	AndroidJavaObject mJo;
	static ads_controller cont = null;
	public static ads_controller share_ads(){
		if(cont == null){
			cont = new ads_controller();
		}
		return cont;
	}


	public  ads_controller(){
#if UNITY_ANDROID
		mJc=new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		mJo=mJc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
	}

	void Awake(){


		
		
	}



	public int get_nor_ads_state(){
		int state = 0; //0:no ads,1:have ads
#if UNITY_IOS
		state = get_unity_ads(0);
#elif UNITY_ANDROID
		get_unity_ads(0);
		state = 1;
#endif
		return state;
	}

	public int get_reward_ads_state(){
		int state = 0; //0:no ads,1:have ads
#if UNITY_IOS
		state = get_unity_ads(1);
#elif UNITY_ANDROID
		get_unity_ads(1);
		state = 1;
#endif
		return state;

	}



		
	int get_unity_ads(int type){
		
		if(type == 0){
			
			if(Advertisement.IsReady()){
				return 1;
			}
			return 0;
		}
		if(Advertisement.IsReady("rewardedVideo")){
			
			return 1;
		}
		return 0;
		
	}

	public void show_nor_ads(float ads_yz){
		float t = Random.value;
		if(t>ads_yz){

			return;
		}

#if UNITY_IOS
		show_nor_unity_ads();
#elif UNITY_ANDROID
		if(Advertisement.IsReady()){
			show_nor_unity_ads();
		}else{
			show_nor_youmi_ads();
		}
#endif

	}

	public void show_nor_unity_ads(){
		Advertisement.Show();

	}


	public void show_nor_youmi_ads(){

		mJo.Call("showVideo","-1");
	}

	public  void  show_reward_ads(){
#if UNITY_IOS
		show_reward_unity_ads();
#elif UNITY_ANDROID
		if(Advertisement.IsReady("rewardeVideo")){
			show_reward_unity_ads();
		}else{
			show_reward_youmi_ads();
		}
#endif


		
	}

	void show_reward_youmi_ads(){
		mJo.Call("showVideo","reward_result");

	}

	void  show_reward_unity_ads(){
		
		UnityEngine.Advertisements.ShowOptions op = new ShowOptions();
		op.resultCallback = result_suc;
		Advertisement.Show("rewardedVideo",op);
		
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void reward_result(string  s_result){
		int result = int.Parse(s_result);
		switch(result){
		case 1:
			Debug.Log("ads ok");
			map_data_manager.add_max_num();
			if(map_data_manager.cur_num != map_data_manager.max_num)
				map_data_manager.cur_num++;
			gate_common.next_gate = "gate_"+map_data_manager.cur_num;
			Application.LoadLevel("loading_scence");
			break;
		case 0:
			Debug.Log("ads failed");
			break;
		case -1:
			Debug.Log("ads skipped");
			break;
		}

	}
	
	void result_suc(ShowResult result){
		int r = 0;
		switch(result){
		case ShowResult.Finished :
			r = 1;
			break;
		case ShowResult.Failed:
			r = 0;
			break;
		case ShowResult.Skipped:
			r = -1;
			break;
		}
		reward_result(""+r);
	}
	
	
}
