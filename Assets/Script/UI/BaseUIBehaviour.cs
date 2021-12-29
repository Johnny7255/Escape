using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


public class BaseUIBehaviour : MonoBehaviour
{
    public string gameSceneName; 
    public string mainMenuName;
    private string _targetScene;
    [SerializeReference] private AudioMixer mixer;

    public void SetVolume(int volume) 
    {
        mixer.SetFloat("Master", -80 + volume);
    }
    /// <summary>
    /// 退出游戏
    /// </summary>
    public void QuitGame()
    {
        Application.Quit();
    }
    /// <summary>
    /// 进入游戏
    /// </summary>
    public void EnterGame()
    {
        NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartHost();
        NetworkManager.singleton.ServerChangeScene("Scene1");
    }
    /// <summary>
    /// 进入主菜单
    /// </summary>
    public void BackToMainMenu()
    {
        var netMgr = NetworkManager.singleton;
            netMgr.StopHost();
            SceneTransfer.GetInstance().TransferScene(mainMenuName);
    }

}
