using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class touch_level : MonoBehaviour {

    public Common_data.touch_enum level = 0;

    // Use this for initialization
    void Start() {

      
    }

   

    // Update is called once per frame
    void Update() {

    }

    public Common_data.touch_enum getLevel() {
        return level;
    }





}
