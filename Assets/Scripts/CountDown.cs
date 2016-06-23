using UnityEngine;
using System.Collections;

public class CountDown : MonoBehaviour {

    TextMesh tm;
    public float time = 3.0F;
    bool paused = true;
    bool played = false;
    public AudioClip[] taiko;
    private AudioSource taikoSource;

    public AudioSource AddAudio()
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.loop = false;
        newAudio.playOnAwake = false;
        newAudio.volume = 1;
        return newAudio;
    }

    // Use this for initialization
    void Start () {
        tm = GetComponent<TextMesh>();
        //add audio components
        taikoSource = AddAudio();

        //load audio resources.
        taikoSource = GetComponent<AudioSource>();
        taiko = new AudioClip[] { (AudioClip)Resources.Load("Sounds/kokushikan_taiko") };
        gameObject.GetComponent<Renderer>().enabled = false;
    }

    public void ToggleTimer()
    {
        paused = false;
        played = false;
        time = 3.0F;
    }

    // Update is called once per frame
    void Update()
    {
        if (paused == false)
        {
            if (time > 1)
            {
                gameObject.GetComponent<Renderer>().enabled = true;
                time -= Time.deltaTime;
                tm.text = Mathf.RoundToInt(time).ToString();
            }
            else if (time > 0)
            {
                time -= Time.deltaTime;
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                if (played == false)
                {
                    taikoSource.clip = taiko[0];
                    taikoSource.volume = 1;
                    taikoSource.Play();
                    played = true;
                }
                paused = true;
                gameObject.GetComponent<Renderer>().enabled = false;
            }

        }
    }
}
