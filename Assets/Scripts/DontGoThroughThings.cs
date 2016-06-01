﻿using UnityEngine;
using System.Collections;

public class DontGoThroughThings : MonoBehaviour
{
    // Careful when setting this to true - it might cause double
    // events to be fired - but it won't pass through the trigger
    public bool sendTriggerMessage = false;

    public LayerMask layerMask = -1; //make sure we aren't in this layer 
    public float skinWidth = 0.1f; //probably doesn't need to be changed 
    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Vector3 previousPosition;
    private Rigidbody myRigidbody;
    private Collider myCollider;

    //public Rigidbody dumShinai;

    //initialize values 
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myCollider = GetComponent<Collider>();
        previousPosition = myRigidbody.position;
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;
    }

    void FixedUpdate()
    {
        //have we moved more than our minimum extent? 
        Vector3 movementThisStep = myRigidbody.position - previousPosition;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        if (movementSqrMagnitude > sqrMinimumExtent)
        {
            float movementMagnitude = Mathf.Sqrt(movementSqrMagnitude);
            RaycastHit hitInfo;
                    
            //check for obstructions we might have missed 
            if (Physics.Raycast(previousPosition, movementThisStep, out hitInfo, movementMagnitude, layerMask.value))
            {
                if (!hitInfo.collider)
                {
                    Debug.Log("DontGoThroughThings triggered on non collider");
                    return;
                }
                if (hitInfo.collider.isTrigger)
                {
                    hitInfo.collider.SendMessage("OnTriggerEnter", myCollider);
                    Debug.Log("DontGoThroughThings triggered on trigger");
                }
                if (!hitInfo.collider.isTrigger)
                {
                    myRigidbody.position = hitInfo.point - (movementThisStep / movementMagnitude) * partialExtent;
                    //gameObject.transform.SetParent(null);
                    myRigidbody.isKinematic = false;

                    //adding force here triggers inconsistently
                    //dumShinai.AddExplosionForce(100, transform.TransformPoint(Vector3.up * 1.0f), 2, 0, ForceMode.Impulse);

                    Debug.Log("DontGoThroughThings triggered on collision");
                }                   


            }
        }

        previousPosition = myRigidbody.position;
    }


}