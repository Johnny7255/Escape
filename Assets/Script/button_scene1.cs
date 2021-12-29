using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class button_scene1 : MonoBehaviour
{

    public GameObject book;
    public GameObject door;
    private Animator buttonAnimator;
    private Animator doorAnimator;


    // Start is called before the first frame update
    void Start()
    {
        buttonAnimator=this.gameObject.GetComponent<Animator>();
        doorAnimator=door.GetComponent<Animator>();
    }

 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            buttonAnimator.SetBool("pressed",true);
            buttonAnimator.SetBool("release",false);
            doorAnimator.SetBool("open",true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
        buttonAnimator.SetBool("pressed",false);
        buttonAnimator.SetBool("release",true);
        }
    }

    

}
