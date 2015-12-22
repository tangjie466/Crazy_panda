using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Umeng;
using UnityEngine.Advertisements;
public class ads_controller : MonoBehaviour {

	int unity_ads_state = 0;

	AndroidJavaClass mJc;
	AndroidJavaObject mJo;
	static ads_controller cont = null;

	[HideInInspector]
	public int youmi_video_state = 0;
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
		this.youmi_video_state = mJo.Call<int>("getVideostate");


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
		unity_ads_state = get_unity_ads(1);
		int state_2 = this.youmi_video_state;
		state = unity_ads_state+state_2;
		Debug.Log("UNITY ADS state is "+unity_ads_state);

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

		GA.Event(tongji.ADS,tongji.NOR_ADS);
#if UNITY_IOS

		show_nor_unity_ads();
#elif UNITY_ANDROID
		if(Advertisement.IsReady()){


			show_nor_unity_ads();
		}else{

			show_nor_youmi_ads();
		}

//		show_nor_youmi_ads();
#endif

	}

	public void show_nor_unity_ads(){
		Dictionary<string,string> dic = new Dictionary<string,string>();
		dic[tongji.NOR_ADS] = tongji.UNITY_ADS;
		GA.Event(tongji.ADS,dic);
		Advertisement.Show();

	}


	public void show_nor_youmi_ads(){
		Dictionary<string,string> dic = new Dictionary<string,string>();
		dic[tongji.NOR_ADS] = tongji.YOUMI_ADS;
		GA.Event(tongji.ADS,dic);
		if(this.youmi_video_state ==0){

			mJo.Call("showSpot","-1");
			return;
		}

		mJo.Call("showVideo","-1");
	}

	public  void  show_reward_ads(){
		GA.Event(tongji.ADS,tongji.NEXT_GATE_ADS);
#if UNITY_IOS
		show_reward_unity_ads();
#elif UNITY_ANDROID
		if(unity_ads_state == 1){
			show_reward_unity_ads();
		}else{
			show_reward_youmi_ads();
		}

//		show_reward_youmi_ads();
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

			Dictionary<string,string > dic = new Dictionary<string,string>();
			dic[tongji.ADS_SUC] = tongji.YOUMI_ADS;
			GA.Event(tongji.ADS,dic);

			break;
		case 0:
			Dictionary<string,string> f_dic = new Dictionary<string,string>();
			f_dic[tongji.ADS_FAILED] = tongji.YOUMI_ADS;
			GA.Event(tongji.ADS,f_dic);
			Debug.Log("ads failed");
			break;
		case -1:
			Debug.Log("ads skipped");
			break;
		}
		reward(s_result);

	}

	void reward(string s_result){
		int result = int.Parse(s_result);
		switch(result){
		case 1:


			GA.Event(tongji.GATE_ADS,Application.loadedLevelName);
			
			
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
			Dictionary<string,string> dic = new Dictionary<string,string>();
			dic[tongji.ADS_SUC] = tongji.UNITY_ADS;
			GA.Event(tongji.ADS,dic);
			break;
		case ShowResult.Failed:
			Dictionary<string,string> f_dic = new Dictionary<string,string>();
			f_dic[tongji.ADS_FAILED] = tongji.UNITY_ADS;
			GA.Event(tongji.ADS,f_dic);
			r = 0;
			break;
		case ShowResult.Skipped:
			r = -1;
			break;
		}
		reward(""+r);
	}
	

	public void quite_app(string s){

		mJo.Call("quiteApp",s);
	}

}
