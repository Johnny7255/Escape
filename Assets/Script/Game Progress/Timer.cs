
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;

/// <summary>
/// 链式观察者模式，因为间隔会比update大，而用协程，并且不存在多个内容
/// 因为用了排序结构，不支持代码加入新的事件对
/// </summary>
public class Timer : NetworkBehaviour
{
    [SyncVar]
    // Start is called before the first frame update
    public List<TimeEventPair> TimeEvents;
    private Queue<TimeEventPair> _eventQueue = new Queue<TimeEventPair>();
    [SyncVar]
    private int passSecond = 0;
    void Start()
    {
        if (!isServer) return; 
        TimeEvents.Sort((a,b)=> a.waitSeconds - b.waitSeconds);//按照从小到大排序时间
        foreach (var item in TimeEvents)
      {
          _eventQueue.Enqueue(item);
      }

      StartCoroutine(Invoker());
    }
    
    private IEnumerator Invoker()
    {
        while (true)
        {
            if(_eventQueue.Count==0) break;//毕竟是一次性脚本
            while (_eventQueue.Count>0&&_eventQueue.Peek().waitSeconds <= passSecond)
            {
                TimeEventPair pair = _eventQueue.Dequeue();
                pair.Invoke();
            }
            yield return new WaitForSeconds(1);
            passSecond++;
        }
        Destroy(this);
    }

    /// <summary>
    /// 时间事件队
    /// </summary>
    [Serializable]
    public class TimeEventPair
    {
        [SyncVar]
        public int waitSeconds;
        public UnityEvent timeEvent;
        [Server]
        public void Invoke()
        {
            timeEvent.Invoke();
        }
    }
}
