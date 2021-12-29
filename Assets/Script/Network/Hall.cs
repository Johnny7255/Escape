using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Hall
{
    public string status;
    public string message;
    public string obj;
    public RoomData[] list;
    public string map;
}

[System.Serializable]
public class RoomData
{
    public int id;
    public string ipAddress;
    public string message;
    public string name;
    public string createTime;
    public string endTime;
    public int numberOfPlayer;
}