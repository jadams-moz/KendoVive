using UnityEngine;
using System.Collections;

public class testDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
 
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.DrawRay(transform.position, Vector3.down, Color.red, 20f, true);
    }
}
