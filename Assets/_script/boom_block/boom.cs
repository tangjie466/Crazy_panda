using UnityEngine;
using System.Collections;

public class boom : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void destroy_collider(){

		boom_block_controller  b_b_c = transform.parent.gameObject.GetComponent<boom_block_controller>();
		b_b_c.destroy_collider();

	}

	public void destroy_self(){
		boom_block_controller  b_b_c = transform.parent.gameObject.GetComponent<boom_block_controller>();
		b_b_c.destroy_self();

	}
}
