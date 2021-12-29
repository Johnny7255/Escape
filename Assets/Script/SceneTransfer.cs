using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 场景切换器
/// </summary>
public class SceneTransfer: SingletonAutoMono<SceneTransfer>
{
    public AsyncOperation sceneTransferOperation { get; private set;}
    private string targetSceneName;
    /// <summary>
    /// 通过传入场景名字进行异步切换场景
    /// </summary>
    /// <param name="sceneName"></param>
    public void TransferScene(string sceneName)
    {
        targetSceneName = sceneName;
        StartCoroutine(TransferCoroutine());
    }

    private IEnumerator TransferCoroutine()
    {
        sceneTransferOperation = SceneManager.LoadSceneAsync(targetSceneName);
        while(!sceneTransferOperation.isDone)
        {
            yield return null;
        }
    }
}
