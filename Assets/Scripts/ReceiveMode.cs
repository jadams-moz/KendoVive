using UnityEngine;
using System.Collections;

public class ReceiveMode : MonoBehaviour {

    GameObject bao;
    GameObject rho;
    GameObject lho;
    GameObject rao;
    TextMesh tm;
    public CountDown timerScript;
    public float strikeCount;
    
    void Awake()
    {
        bao = GameObject.Find("spine_01");
        lho = GameObject.Find("hand_l");
        rho = GameObject.Find("hand_r");
        rao = GameObject.Find("lowerarm_r");

    }

    void OnTriggerEnter()
    {
        timerScript = GameObject.Find("txtCountDown").GetComponent<CountDown>();
        timerScript.ToggleTimer();
        StartCoroutine(WaitAndAnimate());

    }

    void doTween(){
        tm = GetComponent<TextMesh>();
        if (tm.text == "Kote Strike")
        {
            //Kote vars
            iTween.RotateAdd(rho, iTween.Hash(
                "y", 15,
                "x", 15,
                "time", 1.5F));
            iTween.RotateAdd(lho, iTween.Hash(
                "x", 30,
                "y", -20,
                "time", 1.5F));
            iTween.RotateAdd(rao, iTween.Hash(
                "y", -10,
                "x", 20,
                "time", 1.5F));
        }
        else if (tm.text == "Men Strike")
        {
            //Men vars
            iTween.RotateTo(bao, iTween.Hash(
                "x", 25,
                "time", 1.5F));
            iTween.RotateAdd(rho, iTween.Hash(
                "y", -15,
                "x", -15,
                "time", 1.5F));
            iTween.RotateAdd(lho, iTween.Hash(
                "x", -30,
                "time", 1.5F));
            iTween.RotateAdd(rao, iTween.Hash(
                "y", 20,
                "x", -20,
                "time", 1.5F));
        }
    }

    void returnTween()
    {
        tm = GetComponent<TextMesh>();
        if (tm.text == "Kote Strike")
        {

            //Kote vars
            iTween.RotateAdd(rho, iTween.Hash(
                "y", -15,
                "x", -15,
                "time", 1.5F));
            iTween.RotateAdd(lho, iTween.Hash(
                "x", -30,
                "y", 20,
                "time", 1.5F));
            iTween.RotateAdd(rao, iTween.Hash(
                "y", 10,
                "x", -20,
                "time", 1.5F));
        }
        else if (tm.text == "Men Strike")
        {
            //Men vars
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
    }



    IEnumerator WaitAndAnimate()
    {
        
        EventManager.TriggerTraining(true);
        yield return new WaitForSeconds(Random.Range(4, 6));

        for (int i = 1; i <= strikeCount; i++)
        {
            doTween();
            yield return new WaitForSeconds(Random.Range(2,5));
            returnTween();
            yield return new WaitForSeconds(Random.Range(3,5));
        }
        EventManager.TriggerTraining(false);
    }
}
