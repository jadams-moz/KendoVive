using UnityEngine;
using System.Collections;

public class animTest : MonoBehaviour {

    Hashtable ht = new Hashtable();
    Hashtable lt = new Hashtable();
    Hashtable rt = new Hashtable();
    Hashtable ra = new Hashtable();


    GameObject ro;
    GameObject lo;
    GameObject rao;


    void Awake()
    {

        rao = GameObject.Find("lowerarm_r");
        ra.Add("y", 20);
        ra.Add("x", -20);
        ra.Add("time", 1.5F);
        //ra.Add("onupdate", "myUpdateFunction");
        ra.Add("looptype", iTween.LoopType.pingPong);
        ra.Add("onComplete", "pauseTween");

        ht.Add("x", 25);
        ht.Add("time", 1.5F);
        //ht.Add("onupdate", "myUpdateFunction");
        ht.Add("looptype", iTween.LoopType.pingPong);
        ht.Add("onComplete", "pauseTween");


        lo = GameObject.Find("hand_l");
        lt.Add("x", -30);
        lt.Add("time", 1.5F);
        //lt.Add("onupdate", "myUpdateFunction");
        lt.Add("looptype", iTween.LoopType.pingPong);
        lt.Add("onComplete", "pauseTween");

        ro = GameObject.Find("hand_r");
        rt.Add("y", -15);
        rt.Add("x", -15);
        rt.Add("time", 1.5F);
        //rt.Add("onupdate", "myUpdateFunction");
        rt.Add("looptype", iTween.LoopType.pingPong);
        rt.Add("oncomplete", "pauseTween");






    }

    // Use this for initialization
    void Start () {

        iTween.RotateTo(gameObject,ht);
        iTween.RotateAdd(ro, rt);
        iTween.RotateAdd(lo, lt);
        iTween.RotateAdd(rao, ra);
        

    }
    void pauseTween()
    {

        StartCoroutine(WaitAndAnimate(3));

    }
    IEnumerator WaitAndAnimate(float waitTime)
    {
        iTween.Pause();
        yield return new WaitForSeconds(waitTime);
        iTween.Resume();
    }
    // Update is called once per frame
    void FixedUpdate () {

            //transform.rotation = Vector3(9.798, -8.178, -29.643);

    }
}
