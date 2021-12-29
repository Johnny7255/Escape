using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkButton : MonoBehaviour
{
    // public GameObject Button;
    public GameObject talkUI;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("Player")) talkUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player")) talkUI.SetActive(false);
    }

    // private void Update()
    // {
    //     if (Button.activeSelf)
    //     //if (Button.activeSelf && Input.GetKeyDown(KeyCode.R))
    //     {
    //         talkUI.SetActive(true);
    //     }
    // }

}
