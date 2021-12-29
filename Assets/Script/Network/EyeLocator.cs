using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
/// <summary>
/// 用于获取场景中的摄像机，并且锁定视角
/// </summary>
public class EyeLocator : MonoBehaviour
{
    [SerializeReference]private Transform mainEye;
    [SerializeReference]private Transform locator;
    
    // Start is called before the first frame update
    void Start()
    {
        bool isLocalBehaviour = gameObject.GetComponent<NetworkIdentity>().isLocalPlayer;
        if (isLocalBehaviour)
        {
            mainEye ??= Camera.main.transform;
            mainEye.position = locator.position;
            mainEye.forward = locator.forward;
        }
        else if (mainEye != null) Destroy(mainEye.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (mainEye == null) return;
        mainEye.position = locator.position;
        locator.forward = mainEye.forward;
    }
}
