using UnityEngine;
using System.Collections;

public class normal_enemy : MonoBehaviour {

	public nor_enemy_controller  n_e_c;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void move(){

		n_e_c.move ();


	}

}
