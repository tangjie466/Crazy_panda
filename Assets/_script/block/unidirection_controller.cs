using UnityEngine;
using System.Collections;

public class unidirection_controller : MonoBehaviour {

	const string OPEN = "open";
	Animator player;

	void Awake(){

		player = GetComponentInParent<Animator>();

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void open_door(){

		player.SetInteger(OPEN,1);

	}

	public void close_door(){

		player.SetInteger(OPEN,2);

	}

}
