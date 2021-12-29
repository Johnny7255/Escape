using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insert : MonoBehaviour
{
    public bool isInsert;
    private OVRGrabbable myGrabbable;

    // Start is called before the first frame update
    void Start()
    {
        myGrabbable=GetComponent<OVRGrabbable>();

    }

    
    // Update is called once per frame
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("KeyHole")&&myGrabbable.isGrabbed) // 2
        {
            Destroy(GetComponent<OVRGrabbable>());
            this.transform.Translate(other.gameObject.transform.position);
            isInsert=true;
        }
    }
}
