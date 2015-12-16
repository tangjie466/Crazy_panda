using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class map_content : MonoBehaviour {

    ScrollRect rect;
    float cur_position = 0.0f;
    float target_position = 0.0f;
    float speed = 0.1f;
	// Use this for initialization
	void Start () {

        rect = this.gameObject.GetComponent<ScrollRect>();
        

	}
    public void init_position(int i) {
        cur_position = (i - 1) * 0.2f;
        target_position = cur_position;
        rect.horizontalNormalizedPosition = cur_position;
    }

	// Update is called once per frame
	void Update () {
	    
       


	}

    void FixedUpdate() {

        if (target_position == cur_position)
        {
            return;
        }
        else if (Mathf.Abs(target_position - cur_position) < 0.05f) {
            cur_position = target_position;
            rect.horizontalNormalizedPosition = cur_position;
        } 

        if (target_position > cur_position)
        {

            cur_position += 0.1f * speed;
            rect.horizontalNormalizedPosition = cur_position;

        }
        else if (target_position < cur_position) {
            cur_position -= 0.1f * speed;
            rect.horizontalNormalizedPosition = cur_position;
        }

    }

    public void set_position(int i) {

       target_position =  (i-1)*0.2f;

    }


}
