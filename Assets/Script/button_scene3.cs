using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_scene3 : MonoBehaviour
{

    private Animator buttonAnimator;
    public GameObject controller;
    private controller controllerComponent;



    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator=this.gameObject.GetComponent<Animator>();
        controllerComponent=controller.GetComponent<controller>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            buttonAnimator.SetBool("pressed",true);
            buttonAnimator.SetBool("release",false);
            controllerComponent.setPressed();

        }
    }

    /*private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
        buttonAnimator.SetBool("pressed",false);
        buttonAnimator.SetBool("release",true);
        }
    }
    */
}
