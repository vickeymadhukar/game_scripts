using UnityEngine;

public class wallstick : MonoBehaviour
{
    public LayerMask surfacemask;
    public float customgarvity;
    public Vector3 currentNormal = Vector3.up;
    public float radius = 2f;
    bool isGrounded = false;
    public float checkdistance=2f;
    public Camera cam;
    public Rigidbody rb;
    void Start()
    {
        rb.useGravity = false;
        
    }


     void FixedUpdate()
    {
        DetectSurface();
    }

    void DetectSurface()
    {
        RaycastHit hit;
        Vector3 origin = transform.position;
        bool foundsurface = false;
        Vector3 detectnormal = currentNormal;

        Vector3 castdirection = isGrounded ? -currentNormal : Vector3.down;
        if(Physics.SphereCast(origin,radius,-currentNormal,out hit, checkdistance, surfacemask))
        {
            detectnormal=hit.normal;
            foundsurface = true;
            Debug.DrawRay(hit.point, detectnormal * 2f, Color.red,10f);
        }

        if(Physics.SphereCast(origin,radius*0.9f,cam.transform.forward,out RaycastHit hitinfo, checkdistance, surfacemask))
        {


            float angle=Vector3.Angle(currentNormal,hitinfo.normal);

            if (angle > 5f)
            {
                detectnormal = hitinfo.normal;
                foundsurface = true;
                Debug.DrawRay(hitinfo.point, detectnormal * 2f, Color.green, 10f);
            }
            
        }

        if (foundsurface) {
            currentNormal = Vector3.Slerp(currentNormal, detectnormal, Time.deltaTime * 10f);
            isGrounded = true;
    
        }

        if (isGrounded)
        {
            rb.AddForce(-currentNormal * customgarvity, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(Vector3.down * customgarvity, ForceMode.Acceleration);

        }


    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,radius);
        
    }
   


}
