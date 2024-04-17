using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text roomListName;
    public RoomInfo RoomInfo;
    
    public void SetUp(RoomInfo info)
    {
        RoomInfo = info;
        roomListName.text = info.Name;
    }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(RoomInfo);
    }
}
