using UnityEngine;
using System.Collections;

public class create_audio_setting : MonoBehaviour {
	public GameObject o;
	public const string loading_scence = "loading_scence";
	// Use this for initialization
	void Start () {
		Canvas s = o.gameObject.GetComponent<Canvas> ();
		s.worldCamera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void open_pannel(){
		Instantiate (o,o.transform.position,o.transform.rotation);

	}

	public void quite(){

		gate_common.next_gate = "index";
		
		Application.LoadLevel(loading_scence);


	}
}
