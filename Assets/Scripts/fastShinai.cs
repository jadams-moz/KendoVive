using UnityEngine;
using System.Collections;
public class fastShinai : MonoBehaviour
{
    public float radius;
    public float strength;
    private Vector3 lastPosition;

    void awake()
    {
        lastPosition = transform.position;
 
    }

    void OnTriggerStay()
    {
        //Debug.Log("Trigger stayed");

    }
    
    void FixedUpdate()
    {
        Vector3 direction = transform.position - lastPosition;
        //Vector3 localDirection = transform.InverseTransformDirection(direction);
        lastPosition = transform.position;

        //Debug.DrawRay(transform.position+ Vector3.forward*4, direction*100, Color.red, 20f, true);
        RaycastHit hit;
        if (Physics.CapsuleCast(transform.position, transform.TransformPoint(Vector3.up * 1.5f), 0.2f, direction,out hit,0.05f))
            if (hit.collider.gameObject.name == "dummyShinai")
                Debug.Log(hit.collider.gameObject.name + " is at distance " + hit.distance);
    }

 //   void OnCollisionEnter(Collision collision)
  //  {
   //     Rigidbody rb = GetComponent<Rigidbody>();
    //    foreach (ContactPoint contact in collision.contacts)
     //   {
      //      rb.AddExplosionForce(strength, contact.point, radius, 0, ForceMode.Impulse);
       //     Debug.Log("AddExplosion on : " + rb.name + ", strenght " + strength + ", at " + contact.point + ", radius " + radius);
        //}
   // }
//    void OnCollisionStay(Collision collision)
//    {
//        Rigidbody rb = GetComponent<Rigidbody>();
//        foreach (ContactPoint contact in collision.contacts)
//        {
//            rb.AddExplosionForce(strength, contact.point, radius, 0, ForceMode.Impulse);
//            Debug.Log("OnStay AddExplosion on : " + rb.name + ", strenght " + strength + ", at " + contact.point + ", radius " + radius);
//        }
//    }

}