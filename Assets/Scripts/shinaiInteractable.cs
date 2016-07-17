using UnityEngine;
using System.Collections;

namespace NewtonVR
{
    public class ShinaiInteractable : NVRInteractableItem
    {
        public float HapticStrengthMultiplier;
        public AudioClip[] hitBamboo;
        public AudioClip[] airSlice;
        public AudioClip[] hitMen;
        private AudioSource hitAudio;
        private AudioSource sliceAudio;
        private Vector3 lastPosition;
        private float lastMagnitude;



        public AudioSource AddAudio()
        {
            AudioSource newAudio = gameObject.AddComponent<AudioSource>();
            newAudio.loop = false;
            newAudio.playOnAwake = false;
            newAudio.volume = 1;
            return newAudio;
        }

        protected override void Awake()
        {
            base.Awake();
            //NewtonVR variable tweakage
            //AttachedRotationMagic = 20f;
            //AttachedPositionMagic = 3000f;
            //this.Rigidbody.maxAngularVelocity = 100f;

            //add audio components
            hitAudio = AddAudio();
            sliceAudio = AddAudio();

            //load audio resources.
            hitAudio = GetComponent<AudioSource>();
            hitBamboo = new AudioClip[]{(AudioClip)Resources.Load("Sounds/hitbamboo_hard1"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard2"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard3"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard4"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard5"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard6"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_hard7"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_med1"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_med2"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_med3"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_light1"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_light2"),
                                     (AudioClip)Resources.Load("Sounds/hitbamboo_light3"),
                                     (AudioClip)Resources.Load("Sounds/rubbamboo1")};
            airSlice = new AudioClip[]{(AudioClip)Resources.Load("Sounds/airslice_hard1"),
                                     (AudioClip)Resources.Load("Sounds/airslice_hard2"),
                                     (AudioClip)Resources.Load("Sounds/airslice_hard3"),
                                     (AudioClip)Resources.Load("Sounds/airslice_hard4"),
                                     (AudioClip)Resources.Load("Sounds/airslice_light1"),
                                     (AudioClip)Resources.Load("Sounds/airslice_light2")};
            hitMen = new AudioClip[]{(AudioClip)Resources.Load("Sounds/hitmen_hard1"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_hard2"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_hard3"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_hard4"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_med1"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_med2"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_med3"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_med4"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light1"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light2"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light3"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light4"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light5"),
                                     (AudioClip)Resources.Load("Sounds/hitmen_light6")};

        }

        protected void FixedUpdate()
        {
            //base.FixedUpdate();
            //shinai tip velocity calc
            Vector3 direction = transform.TransformPoint(Vector3.up * 1.0f) - lastPosition;
            lastPosition = transform.TransformPoint(Vector3.up * 1.0f);
            lastMagnitude = direction.magnitude;
            //airSlice audio
            if (!sliceAudio.isPlaying && lastMagnitude > 0.25)
            {
                sliceAudio.volume = 1;
                sliceAudio.clip = airSlice[Random.Range(0, 3)];
                sliceAudio.Play();

            }
            else if (!sliceAudio.isPlaying && lastMagnitude > 0.15)
            {
                sliceAudio.clip = airSlice[Random.Range(4, 5)];
                sliceAudio.volume = (lastMagnitude / 0.025F);
                sliceAudio.Play();
            }



        }

        void OnCollisionEnter(Collision collision)
        {
            if (IsAttached == true)
            {
                foreach (ContactPoint contact in collision.contacts)
                {
                    //haptic rumble
                    ushort uHapticStrength = (ushort)(HapticStrengthMultiplier * lastMagnitude);
                    if (uHapticStrength <= 3000)
                        AttachedHand.TriggerHapticPulse(uHapticStrength);
                    else
                        AttachedHand.LongHapticPulse(1);

                    //on hit audio picker
                    if (collision.gameObject.name == "Men" || collision.gameObject.name == "KoteR" || collision.gameObject.name == "KoteL")
                    {
                        //Debug.Log("hit " + collision.gameObject.name);
                        if (!hitAudio.isPlaying && lastMagnitude > 0.14)
                        {
                            hitAudio.clip = hitMen[Random.Range(0, 3)];
                            hitAudio.volume = 1;
                            hitAudio.Play();
                        }
                        else if (!hitAudio.isPlaying && lastMagnitude > 0.08)
                        {
                            hitAudio.clip = hitMen[Random.Range(4, 7)];
                            hitAudio.volume = (lastMagnitude / 0.14F);
                            hitAudio.Play();
                        }
                        else if (!hitAudio.isPlaying && lastMagnitude > 0.01)
                        {
                            hitAudio.clip = hitMen[Random.Range(8, 13)];
                            hitAudio.volume = (lastMagnitude / 0.08F);
                            hitAudio.Play();
                        }
                    }
                    else
                    {
                        if (!hitAudio.isPlaying && lastMagnitude > 0.1)
                        {
                            hitAudio.clip = hitBamboo[Random.Range(0, 6)];
                            hitAudio.volume = 1;
                            hitAudio.Play();
                        }
                        else if (!hitAudio.isPlaying && lastMagnitude > 0.03)
                        {
                            hitAudio.clip = hitBamboo[Random.Range(7, 9)];
                            hitAudio.volume = (lastMagnitude / 0.1F);
                            hitAudio.Play();
                        }
                        else if (!hitAudio.isPlaying && lastMagnitude > 0.005)
                        {
                            hitAudio.clip = hitBamboo[Random.Range(10, 12)];
                            hitAudio.volume = (lastMagnitude / 0.03F);
                            hitAudio.Play();
                        }
                        else if (!hitAudio.isPlaying && lastMagnitude > 0.001)
                        {
                            hitAudio.clip = hitBamboo[13];
                            hitAudio.volume = (lastMagnitude / 0.005F);
                            hitAudio.Play();
                        }
                    }
                }
            }
        }

    }
}
