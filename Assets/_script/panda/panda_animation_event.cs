using UnityEngine;
using System.Collections;

public class panda_animation_event : MonoBehaviour {

	// Use this for initialization
	panda_controller p;
	void Awake(){

		p = (panda_controller)transform.parent.gameObject.GetComponent<panda_controller>();

	}

	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void moveDown(){
		p.move_down();

	}

	void initActionState(){
		p.initActionState();
	}

	void callMoveRight(){
		Debug.Log("move right");
	}

}
