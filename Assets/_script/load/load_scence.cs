using UnityEngine;
using System.Collections;

public class load_scence : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("load_1");
        StartCoroutine(load_next());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator load_next() {
        yield return new WaitForSeconds(1);
        Debug.Log("load_1");
       Application.LoadLevelAsync(gate_common.next_gate);
        Debug.Log("load_2");
        StopCoroutine(load_next());
    }
    
    
                         


}
