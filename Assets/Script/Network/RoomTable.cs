using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTable : MonoBehaviour
{
    [SerializeReference] private GameObject roomPrefab;
    [SerializeReference] private Transform listView;
    [SerializeReference] private MainMenu mainMenu;
    public Hall hall;
    private List<GameObject> _rooms = new List<GameObject>();
    private Dictionary<string, RoomData> _activeRooms = new Dictionary<string, RoomData>();
    private void OnEnable()
    {
        StartCoroutine(UpdateHallInfo());
    }

    private void Update()
    {                                               
        _activeRooms.Clear();
        if(hall != null)
        foreach (var item in hall.list)
        {
            if (_activeRooms.ContainsKey(item.ipAddress) == false) _activeRooms.Add(item.ipAddress, item);
            else _activeRooms[item.ipAddress] = item;
        }

        int additionalRoom = _activeRooms.Count - _rooms.Count;
        for (int i = additionalRoom; i > 0; i--)
        {
            _rooms.Add(Instantiate(roomPrefab, listView));
        }

        int len = hall.list.Length;
        for (int i = 0; i < _rooms.Count; i++)
        {
            if (i < len)
            {
                _rooms[i].SetActive(true);
                _rooms[i].GetComponent<RoomListObj>().SetRoomData(hall.list[i]);
            }
            else _rooms[i].SetActive(false);
        }

    }

    private void OnDisable() 
    {
        StopCoroutine(UpdateHallInfo());
    }

    private IEnumerator UpdateHallInfo()
    {
        while (true)
        {
            RequestTable.GetInstance().UpdateHall(this);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
