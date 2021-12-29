using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using Mirror;
using UnityEngine;
using UnityEngine.Networking;

public class RequestTable:SingletonAutoMono<RequestTable>
{
    private readonly StringBuilder _result = new StringBuilder();
    private readonly char[] _buffer = new char[256];
    private bool _updating, _deleting, _creating;
    private RoomTable _table;
    private string _roomName = "666";
    private int currentRoomID = 0;

    public string webServerIP = "http://101.43.64.166:9000/";

    /// <summary>
    /// 保持游戏gaming状态
    /// </summary>
    public void KeepGamingState() 
    {
        Debug.Log("Keeping updating endtime");
        StartCoroutine(KeepGamingRequest());
    }

    public void EndGamingState() 
    {
        StopCoroutine(KeepGamingRequest());
    }

    public  void UpdateHall(RoomTable table)
    {
        _result.Clear();
        _table = table;
        _updating = true;
        StartCoroutine(UpdatingHallCor());
    }
    /// <summary>
    /// 创建房间
    /// </summary>
    /// <param name="roomName"></param>
    public void CreateRoom(string roomName)
    {
        _roomName = roomName;
        _creating = true;
        StartCoroutine(CreatingRoomCor());
    }
    
    /// <summary>
    /// 更新大厅的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdatingHallCor()
    {
        while (_updating)
        {
            while (_updating)
            {
                UnityWebRequest request = UnityWebRequest.Get(webServerIP + "escape/room?");
                yield return request.SendWebRequest();
                _table.hall = JsonUtility.FromJson<Hall>(request.downloadHandler.text);
                _updating = false;
            }
        }
    }
/*需求修改
    /// <summary>
    /// 删除房间
    /// </summary>
    public void DeleteARoom()
    {
        _deleting = true;
        StartCoroutine(DeletCor());
    }

    /// <summary>
    /// 删除大厅的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator DeletCor()
    {
        while (_deleting)
        {
            yield return new WaitForFixedUpdate();
            HttpWebRequest createRoomRequest = WebRequest.Create("http://101.43.64.166:9000/room?&ipAddress="+NetworkManager.GetLocalIp()) as HttpWebRequest;
            Debug.Assert(createRoomRequest != null, nameof(createRoomRequest) + " != null");
            createRoomRequest.Method = "DELETE";
            createRoomRequest.ContentType = "application/json;charset=UTF-8";
            createRoomRequest.Timeout = 5000;
            try
            {
                createRoomRequest.GetResponse();
            }
            catch 
            {
                continue;
            }

            _deleting = false;
        }
    }
*/
    /// <summary>
    /// 创建一个房间的协程
    /// </summary>
    /// <returns></returns>
    private IEnumerator CreatingRoomCor()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", _roomName);
        form.AddField("ipAddress", NetworkManager.GetLocalIp());
        while (_creating)
        {
            UnityWebRequest createRoomRequest = UnityWebRequest.Post(webServerIP + "/escape/room?", form);
            yield return createRoomRequest.SendWebRequest();
            UnityWebRequest request = UnityWebRequest.Get(webServerIP + "escape/room?");
            yield return request.SendWebRequest();
            Hall hall = JsonUtility.FromJson<Hall>(request.downloadHandler.text);
            currentRoomID = hall.list[0].id;
            Debug.Log(currentRoomID);
            _creating = false;
            createRoomRequest.Dispose();
        }

    }

    private IEnumerator KeepGamingRequest() 
    {
        while (true)
        {
            byte[] data = System.Text.Encoding.Default.GetBytes("name="+_roomName+"&endTime="+DateTime.Now.ToLocalTime().ToString("yyyy-MM-dd hh:mm:ss"));
            UnityWebRequest request = UnityWebRequest.Put(webServerIP+"/escape/room?",data);
            yield return request.SendWebRequest();
            Debug.Log(request.downloadHandler.text);
            request.Dispose();
        }
    }

}

