using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawer : MonoBehaviour
{

    private Animator drawerAnimator;
    


    // Start is called before the first frame update
    void Start()
    {
        drawerAnimator=this.gameObject.GetComponent<Animator>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            drawerAnimator.SetBool("pull",true);
        }
    }

   
}
//&&OVRInput.GetDown(OVRInput.Button.One)
