using UnityEngine;
using System.Collections;
using Umeng;



public class tongji : MonoBehaviour {

	public  static  string  GATE_ADS = "gate_ads";
	public  static string GATE_TIME="gate_time";
	public  static string GAME_BEGIN = "game_time";

	public static string ADS="advertisement";
	public static string ADS_SUC="请求广告成功";
	public static string ADS_FAILED = "请求广告失败";

	public static string UNITY_ADS="UNITY广告";
	public static string YOUMI_ADS="YOUMI广告";
	public static string ADMOB_ADS = "google广告";

	public static string NEXT_GATE_ADS="过关请求广告";
	public static string NOR_ADS="正常广告播放";

	string url1= "http://otherser3.mypanda.cn/cgi-bin/tempreport5";
	string url2 = "http://otherser3.mypanda.cn/cgi-bin/tempreport6";


	int pd = 1087;

	void Awake(){

#if UNITY_IOS
		GA.StartWithAppKeyAndChannelId("5669a3c4e0f55ab3910035d8", "App Store");
#elif UNITY_ANDROID
		GA.StartWithAppKeyAndChannelId("5662893567e58eedd4006126", "Android");

#endif
		GA.EventBegin(GAME_BEGIN);

	}
	// Use this for initialization
	void Start () {
		firstInstall();
		todayLogin();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void firstInstall(){

		int p = PlayerPrefs.GetInt("firstInstall",0);
		if(p == 0){

			string data = ";; pd="+pd+";;vc="+Application.version+";;qd="+ads_controller.share_ads().getQD()+";;dd="+ads_controller.share_ads().getDeviceID()
				+";;sd="+ads_controller.share_ads().getSD();
			Debug.Log("http data is :"+data);
			HttpUtil.shareInstance().httpPost(url1,data,1);
		

		}





	}


	public void todayLogin(){

		string curday = TimeUtil.getCurrentDay();
		string lastLogingDay = PlayerPrefs.GetString("today");
		
		if(curday != lastLogingDay){
			
			string data = ";; pd="+pd+";;vc="+Application.version+";;qd="+ads_controller.share_ads().getQD()+";;dd="+ads_controller.share_ads().getDeviceID()
				+";;sd="+ads_controller.share_ads().getSD();
			Debug.Log("http data is :"+data);
			HttpUtil.shareInstance().httpPost(url2,data,2);
			
		}

	}






}
