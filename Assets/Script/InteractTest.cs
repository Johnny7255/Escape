using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTest : MonoBehaviour
{
    public Material AfterInterac;
    public Material BeforeInteract;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            print("交互完成");
            this.GetComponent<MeshRenderer>().materials[0] = AfterInterac;
        }

        if (Input.GetKey(KeyCode.S))
        {
            print("重置");
            this.GetComponent<MeshRenderer>().materials[0] = BeforeInteract;
        }
    }
}
