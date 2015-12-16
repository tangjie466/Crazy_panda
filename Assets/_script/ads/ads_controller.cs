using UnityEngine;
using System.Collections;

using UnityEngine.Advertisements;
public class ads_controller : MonoBehaviour {


	void Awake(){

		if(Advertisement.isSupported){
			Advertisement.Initialize("1012354");
		}else{
			Debug.Log("Plaftform is not supported");
		}
		
	}



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   
	void result_suc(UnityEngine.Advertisements.ShowResult result){

		switch(result){
		case ShowResult.Finished :
			Debug.Log("ads ok");

			break;
		case ShowResult.Failed:
			Debug.Log("ads failed");
			break;
		case ShowResult.Skipped:
			Debug.Log("ads skipped");
			break;
		}
	}

	public void show_ads(){
		
		ShowOptions op = new ShowOptions();
		op.resultCallback = result_suc;
		Advertisement.Show(null,op);
		
		
	}
}
