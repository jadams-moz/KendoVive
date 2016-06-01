using UnityEngine;
using System.Collections;
public class fastShinai : MonoBehaviour
{
    public float radius;
    public float strength;
    public float hitDelay;
    private Vector3 lastPosition;
    private float lastMagnitude;
    public Renderer hitSphere;
    public Renderer fastSphere;
    public Rigidbody dumShinai;

    void awake()
    {
    }

    void OnTriggerStay()
    {
 
        if (lastMagnitude > 0.04f)
        {
            hitSphere.material.color = Color.red;
            //ExplosionForce centered near end of shinai acting on the dummyShinai only
            dumShinai.AddExplosionForce(strength, transform.TransformPoint(Vector3.up * 1.0f), radius, 0, ForceMode.Impulse);
            //Wait a bit to prevent trigger from repeating from same hit
            StartCoroutine(WaitAndAnimate(hitDelay));
        }
    }
        



IEnumerator WaitAndAnimate(float waitTime)
{
    yield return new WaitForSeconds(waitTime);
        hitSphere.material.color = Color.white;
    }

void FixedUpdate()
    {
        //Establish magnitude of direction change near tip of shinai
        Vector3 direction = transform.TransformPoint(Vector3.up * 1.0f) - lastPosition;
        lastPosition = transform.TransformPoint(Vector3.up * 1.0f);
        lastMagnitude = direction.magnitude;
        //Color fastSphere and remove physics of shinai based on speed of shinai tip area
        if (direction.magnitude > 0.08f)
        {
            GetComponent<CapsuleCollider>().isTrigger = true;
            fastSphere.material.color = Color.red;
        }
        else if(direction.magnitude > 0.04f)
        {
            GetComponent<CapsuleCollider>().isTrigger = true;
            fastSphere.material.color = Color.yellow;
        }
        else if (direction.magnitude > 0.02f)
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            fastSphere.material.color = Color.green;
        }
        else
        {
            GetComponent<CapsuleCollider>().isTrigger = false;
            fastSphere.material.color = Color.white;
        }
    }

    //CapsuleCast test
    //void FixedUpdate()
    //{
    //    Vector3 direction = transform.position - lastPosition;
    //    lastPosition = transform.position;
    //
    //    RaycastHit hit;
    //    if (Physics.CapsuleCast(transform.position, transform.TransformPoint(Vector3.up * 1.5f), 0.2f, direction,out hit,0.05f))
    //        if (hit.collider.gameObject.name == "dummyShinai")
    //            Debug.Log(hit.collider.gameObject.name + " is at distance " + hit.distance);
    //}

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