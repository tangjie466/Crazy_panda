using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class map_gate : MonoBehaviour,IPointerClickHandler,IPointerDownHandler,IPointerUpHandler {

    public Text t;
    public Button b;
    public GameObject enemy;
    Animator enemy_a;
    public int cur;
    int my_max_num;
    public RuntimeAnimatorController bee,normal, normal_move, fast_move, attact, unknown;
    public enum ENEMY_TAG { bee,normal,normal_move,fast_move,attact};
    public ENEMY_TAG enemy_tag;
    AudioSource button_audio;
    void Awake()
    {

        button_audio = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        button_audio.clip = Resources.Load<AudioClip>("sound/button");
        button_audio.playOnAwake = false;
        button_audio.loop = false;
    }
    
    // Use this for initialization
    void Start () {
        enemy_a = enemy.gameObject.GetComponent<Animator>();
        t.text = cur + "";
        
        reload_data();
        my_max_num = map_data_manager.max_num;
        Debug.Log("cur is "+cur+" map_cur is " + map_data_manager.cur_num);
        if (cur == map_data_manager.cur_num) {
            enemy_a.SetInteger("is_pressed", 2);
        }

    }

    void reload_data() {

        
        if (map_data_manager.max_num < cur) 
        {
            enemy_a.runtimeAnimatorController = unknown;

            b.interactable = false;
        }else
        {
            b.interactable = true;
            RuntimeAnimatorController rt = null;
            switch (enemy_tag) {
                case ENEMY_TAG.attact:
                    rt = attact;

                    break;
                case ENEMY_TAG.bee:

                    rt = bee;
                    break;
                case ENEMY_TAG.fast_move:
                    rt = fast_move;
                    break;
                case ENEMY_TAG.normal:
                    rt = normal;
                    break;
                case ENEMY_TAG.normal_move:
                    rt = normal_move;
                    break;

            }
            if(rt != null)
            enemy_a.runtimeAnimatorController = rt;

        }



    }

    // Update is called once per frame
    void Update () {
        if (my_max_num != map_data_manager.max_num) {
            reload_data();
        }
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (b.IsInteractable()) {
			if(audio_controller.sound_isplaying == 1)
            button_audio.Play();
            load_gate();
        }

       
    }

    public void OnPointerDown(PointerEventData data) {

        if (b.IsInteractable()) {
            //enemy_a.SetInteger("is_pressed", 2);
        }

    }

    public void OnPointerUp(PointerEventData data)
    {
        if (b.IsInteractable())
        {
            enemy_a.SetInteger("is_pressed", 1);
        }
    }

    public void load_gate() {
        map_data_manager.cur_num = cur;
        gate_common.next_gate = "gate_" + cur;
        Application.LoadLevel("loading_scence");
        Debug.Log("name is "+this.gameObject.name);
    }

}
