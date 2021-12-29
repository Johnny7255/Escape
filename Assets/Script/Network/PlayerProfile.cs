using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;

/// <summary>
/// 用于标注角色的ID
/// </summary>
public class PlayerProfile : NetworkBehaviour
{
    [SyncVar]
    private string _userName;
    [SerializeReference] private Canvas profileUI;
    [SerializeReference] private TMP_Text nameArea;

    private void Start()
    {
        profileUI.worldCamera = Camera.main;
        if (isLocalPlayer)
        {   _userName = MainMenu.userName;
            _userName ??= "hello world";
            SetProfile(_userName);
        }
        else nameArea.text = _userName;
        profileUI.gameObject.SetActive(true); 
    }
    [Command]
    private void SetProfile(string userName)
    {
        nameArea.text = userName;
    }
 
}