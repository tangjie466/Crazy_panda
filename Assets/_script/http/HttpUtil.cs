using UnityEngine;
using System.Collections;

public class HttpUtil : MonoBehaviour {

	static HttpUtil instance = null; 

	public static HttpUtil shareInstance(){

		if (instance == null){

			GameObject ob = new GameObject();
			ob.name = "httpUtil";
			instance = (HttpUtil)ob.AddComponent<HttpUtil>();
			DontDestroyOnLoad(ob);

		}

		return instance;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	public void httpPost(string url,string data,int type){

		StartCoroutine(post(url,data,type));


	}

	IEnumerator post(string url,string sData,int type){
		byte[] data = System.Text.Encoding.Default.GetBytes(sData);
		WWW w = new WWW(url,data);
		
		yield return w;
		
		if(w.error != null){

		}else{
			if(type == 1){

				PlayerPrefs.SetInt("firstInstall",1);

			}else if(type == 2){

				PlayerPrefs.SetString("today",TimeUtil.getCurrentDay());

			}

			
		}

		StopCoroutine(post (url,sData,type));
	}





}
