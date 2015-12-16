using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class pag_num : MonoBehaviour {

    
    Text t;
    
	// Use this for initialization
	void Start () {

        t = this.gameObject.GetComponent<Text>();
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void setString(int n) {
        t.text = n + "";
    }


}
