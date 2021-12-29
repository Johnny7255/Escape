using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

/// <summary>
/// 负责定位UI到特定位置,全局管理UI，存在栈结构来依次关闭UI
/// </summary>
public class UIMGR : SingletonMono<UIMGR>
{
    public float spaceOffsetZ;
    public Transform player;
    public GameObject menuInScene;//当前场景中主菜单
    private readonly Stack<GameObject> _currentStaticUIs = new Stack<GameObject>();

    private void Start() 
    {
        player = Camera.main.transform;
    }

    /// <summary>
    /// 打开UI，并且定位
    /// </summary>
    /// <param name="UIObject"></param>
    public void OpenUI(GameObject UIObject)
    {
        if(!_currentStaticUIs.Contains(UIObject))_currentStaticUIs.Push(UIObject);
        UIObject.SetActive(true);
        LocateUI(UIObject);
    }

    public void CloseUI(GameObject UIObject)
    {
        if(_currentStaticUIs.Count>0&&_currentStaticUIs.Peek().Equals(UIObject))
        {
            _currentStaticUIs.Pop().SetActive(false);
        }
    }

    private void Update()
    {
        bool buttonDown = false;
        #if UNITY_EDITOR
        buttonDown = Input.GetKeyDown(KeyCode.E);
        #else
        buttonDown = OVRInput.GetDown(OVRInput.Button.Start);
        #endif
        if(menuInScene!=null&&buttonDown) 
        {
            //若没有开启UI，则打开菜单
            if (_currentStaticUIs.Count == 0) 
            {
                menuInScene.SetActive(true);
                _currentStaticUIs.Push(menuInScene);
            }
            //负责关闭所有UI
            else
            {
                while (_currentStaticUIs.Count > 0)
                {
                    _currentStaticUIs.Pop().SetActive(false);
                }
            }
        }
    }

    public void LocateUI(GameObject uiObject)
    {
        uiObject.transform.position = player.position + player.transform.forward*spaceOffsetZ;
        uiObject.transform.forward = player.forward;
    }
}
