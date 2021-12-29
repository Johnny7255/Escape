using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class NetworkDebugHelper : MonoBehaviour
{
    public NetworkManager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager.StartHost();
    }

}
