using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using TMPro;

public class RoomListObj : MonoBehaviour
{
    public TMP_Text roomName;
    private RoomData _roomData;
    private string _ipAddress;

    public void SetRoomData(RoomData roomData)
    {
        _roomData = roomData;
        _ipAddress = _roomData.ipAddress;
        roomName.text = _roomData.name;
    }

    public void JoinTheGame()
    {
        NetworkManager.singleton.networkAddress = _ipAddress;
        MainMenu.singleton.EnterGameAsClient();
    }
}
