using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
/// <summary>
/// 通关触发器
/// </summary>
public class LevelJumper : NetworkBehaviour
{
    [SerializeReference] private string nextLevel;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag.Equals("Player")) NetworkManager.singleton.ServerChangeScene(nextLevel);
    }
}
