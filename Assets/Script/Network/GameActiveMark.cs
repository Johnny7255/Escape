using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class GameActiveMark : NetworkBehaviour
{   
    [SyncVar]
    [SerializeReference] private bool isActive = true;
    void Start()
    {
        if(!isActive) gameObject.SetActive(false);
    }
    [ClientRpc]
    public void SetMarkActive(bool active)
    {
        isActive = active;
        gameObject.SetActive(active);
    }
}
