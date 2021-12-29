using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 在场景中加载的UI的父类
/// </summary>
public class UILocater : MonoBehaviour
{
    private GameObject VRUIVisual;
    protected virtual void OnEnable()
    {
        #if !UNITY_EDITOR
        VRUIVisual = GameObject.FindObjectOfType<LaserPointer>().GetComponent<LaserPointer>().cursorVisual;
        #endif
        UIMGR.GetInstance().OpenUI(gameObject);
    }

    protected virtual void Update()
    {
        UIMGR.GetInstance().LocateUI(gameObject);
    }

    protected virtual void OnDisable()
    {
        #if !UNITY_EDITOR
        UIMGR.GetInstance().CloseUI(gameObject);
        #endif
    }
    /// <summary>
    /// 获取射线在UI上的位置坐标
    /// </summary>
    /// <returns></returns>
    protected Vector3 GetLaserPointerPosition()
    {
        return VRUIVisual.transform.position;
    }
}
