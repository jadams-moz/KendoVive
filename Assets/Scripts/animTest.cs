using UnityEngine;
using System.Collections;

public class animTest : MonoBehaviour {

    GameObject bao;
    GameObject rho;
    GameObject lho;
    GameObject rao;
    GameObject txtcap;
    public CountDown timerScript;
    public float strikeCount;
    
    void Awake()
    {
        bao = GameObject.Find("spine_01");
        lho = GameObject.Find("hand_l");
        rho = GameObject.Find("hand_r");
        rao = GameObject.Find("lowerarm_r");
        txtcap = GameObject.Find("txtCapsule");

    }

    void OnTriggerEnter()
    {
        timerScript = GameObject.Find("txtCountDown").GetComponent<CountDown>();
        timerScript.ToggleTimer();



        StartCoroutine(WaitAndAnimate());
         

    }

    void doTween(){     
        iTween.RotateTo(bao,iTween.Hash(
            "x",                    25,
            "time",                 1.5F));
        iTween.RotateAdd(rho,iTween.Hash(
            "y",                    -15,
            "x",                    -15,
            "time",                 1.5F));
        iTween.RotateAdd(lho, iTween.Hash(
            "x",                    -30,
            "time",                 1.5F));
        iTween.RotateAdd(rao, iTween.Hash(
            "y",                    20,
            "x",                    -20,
            "time",                 1.5F));
        
    }

    void returnTween()
    {
        iTween.RotateTo(bao, iTween.Hash(
            "x", 0,
            "time", 1.5F));
        iTween.RotateAdd(rho, iTween.Hash(
            "y", 15,
            "x", 15,
            "time", 1.5F));
        iTween.RotateAdd(lho, iTween.Hash(
            "x", 30,
            "time", 1.5F));
        iTween.RotateAdd(rao, iTween.Hash(
            "y", -20,
            "x", 20,
            "time", 1.5F));

    }



    IEnumerator WaitAndAnimate()
    {
        gameObject.GetComponent<Renderer>().enabled = false;
        txtcap.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(Random.Range(4, 6));

        for (int i = 1; i <= strikeCount; i++)
        {
            doTween();
            yield return new WaitForSeconds(Random.Range(2,5));
            returnTween();
            yield return new WaitForSeconds(Random.Range(3,5));
        }
        gameObject.GetComponent<Renderer>().enabled = true;
        txtcap.GetComponent<Renderer>().enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
    }
}
