using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Random = UnityEngine.Random;


public class Launcher : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text errorMessage;
    [SerializeField] Transform roomListContent;
    [SerializeField] GameObject roomItemPrefab;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject playerItemPrefab;

    
    public static Launcher Instance;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("Conectando");
        PhotonNetwork.ConnectUsingSettings();
        MenuManager.Instance.OpenMenuName("Loading");
    }
    
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado");
        PhotonNetwork.JoinLobby();
    }
    
    public override void OnJoinedLobby()
    {
        Debug.Log("Conectado al lobby ");
        MenuManager.Instance.OpenMenuName("Home");
        PhotonNetwork.NickName = "player" + Random.Range(0, 1000).ToString("0000");
    }
    
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        PhotonNetwork.CreateRoom(roomNameInputField.text);
        MenuManager.Instance.OpenMenuName("Loading");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenuName("Room");
        roomName.text = PhotonNetwork.CurrentRoom.Name;
        foreach (Transform playerTransform in playerListContent)
        {
            Destroy(playerTransform.gameObject);
        }

        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            Instantiate(playerItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "Error al crear la sala" + message;
        MenuManager.Instance.OpenMenuName("Error");
    }

    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenuName("Loading");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenuName("Loading");
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenuName("Home");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform transform1 in roomListContent)
        {
            Destroy(transform1.gameObject);
        }
        
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList) continue;
            Instantiate(roomItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(playerItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
}
