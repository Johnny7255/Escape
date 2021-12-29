
using System;
using Mirror;
using UnityEngine;
using TMPro;

public class MainMenu : BaseUIBehaviour
{
    [SerializeReference]private GameObject mainPanel, hostPanel, roomsPanel, clientPanel;
    [SerializeReference] private RoomTable table;
    public TMP_Text userNameText;
    public TMP_Text roomText;
    public static string userName;
    public string roomName;
    public GameObject netMgrGameObject;
    public static MainMenu singleton;

    private void Awake()
    {
        DontDestroyOnLoad(Instantiate(netMgrGameObject));
        if (singleton != null) Destroy(this);
        else singleton = this;
    }


    private void Update()
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.E)) EnterGameAsHost();
        else if(Input.GetKeyDown(KeyCode.C)) EnterGameAsClient();
#endif
        // client ready
        if (NetworkClient.isConnected && !NetworkClient.ready)
        {
            NetworkClient.Ready();
                if (NetworkClient.localPlayer == null)
                {
                    NetworkClient.AddPlayer();
                }
        }
    }

    public void EnterGameAsHost()
    {
        EnterOnlineGame();
        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.networkAddress = NetworkManager.GetLocalIp();
        Debug.Log(NetworkManager.singleton.networkAddress);
        RequestTable.GetInstance().CreateRoom(roomText.text);
        NetworkManager.singleton.ServerChangeScene("Scene1");
    }

    public void EnterGameAsClient()
    {
        EnterOnlineGame();
        NetworkManager.singleton.StartClient();
    }


    private void EnterOnlineGame()
    {
        roomName = roomText.text;
    }
    
}
