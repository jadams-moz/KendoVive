using UnityEngine;
using System.Collections;

public class EventListener : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
        EventManager.onTraining += beginTraining;
	}

    void OnDisable()
    {
        EventManager.onTraining -= beginTraining;
    }

    void beginTraining(bool isTraining)
    {
        Debug.Log("Is training " + isTraining);
        GetComponent<Renderer>().enabled = !isTraining;
        if (GetComponent<Collider>() != null)
            GetComponent<Collider>().enabled = !isTraining;
    }
	
}
