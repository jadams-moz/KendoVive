using UnityEngine;
using System.Collections;

public class parentFixedJoint : MonoBehaviour {
    public Rigidbody rigidBodyAttachPoint;

    SteamVR_TrackedObject trackedObj;
    SteamVR_Controller.Device device;
    FixedJoint fixedJoint;
    // Use this for initialization
    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        device = SteamVR_Controller.Input((int)trackedObj.index);
    }

    void OnTriggerStay(Collider col)
    {
        //Debug.Log("Collided with " + col.name + "and acivated OnTriggerStay");
        if (fixedJoint == null && device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            fixedJoint = col.gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = rigidBodyAttachPoint;
        }
        else if(fixedJoint != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            GameObject go = fixedJoint.gameObject;
            Rigidbody rigidbody = go.GetComponent<Rigidbody>();
            Object.Destroy(fixedJoint);
            fixedJoint = null;
            tossObject(rigidbody);
        }
    }
    private void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }
    }

}
