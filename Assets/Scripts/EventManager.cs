using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public delegate void Training(bool isTraining);
    public static event Training onTraining;

    public static void TriggerTraining(bool isTraining)
    {
        if (onTraining != null)
            onTraining(isTraining);
    }

}
