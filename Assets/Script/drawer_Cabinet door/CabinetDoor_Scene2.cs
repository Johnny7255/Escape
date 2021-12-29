using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabinetDoor_Scene2 : MonoBehaviour
{
   private OVRGrabbable myGrabbable;
    private OVRGrabber grabber;
    private GameObject hand;
    private Rigidbody thisRigid;
    private Rigidbody handRigid;

    private Vector3 rotateAxis;
    private float rotateAxisZ;
    public int rotateDirection; //1顺时针转，-1逆时针
    public int boundary;
    private Vector3 rotateVelocity;

    // Start is called before the first frame update
    void Start()
    {
        rotateAxisZ=transform.position.z+rotateDirection*this.gameObject.GetComponent<Renderer>().bounds.size.x/2;
        rotateAxis=new Vector3(transform.position.x,0,rotateAxisZ);
        
        myGrabbable = GetComponent<OVRGrabbable>();

        if (GetComponent<Rigidbody>() != null)
        {
            thisRigid = GetComponent<Rigidbody>();
        }



    }

    // Update is called once per frame
    void Update()
    {
        getHand();
        pullAndPush(hand);
    }
    private void getHand()
    {
        if (myGrabbable.grabbedBy != null)
        {
            grabber = myGrabbable.grabbedBy;
            hand = grabber.gameObject;
            if (hand.GetComponent<Rigidbody>() != null)
            {
                handRigid=hand.GetComponent<Rigidbody>();
            }
        }
    }
    private void pullAndPush(GameObject hand)
    {
        if (hand != null && transform.rotation.y*rotateDirection<boundary&&transform.rotation.y*rotateDirection>=0)
        {
            if (handRigid != null&& thisRigid!=null)
            {
                transform.RotateAround(rotateAxis,Vector3.up,handRigid.angularVelocity.y);
            }
        }
    

    }
}
