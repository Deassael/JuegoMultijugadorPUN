using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Launcher : MonoBehaviourPunCallbacks
{

    [SerializeField] private TMP_InputField roomNameInputField;
    [SerializeField] private TMP_Text roomName;
    [SerializeField] private TMP_Text errorMessage;
    
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
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorMessage.text = "Error al crear la sala" + message;
        MenuManager.Instance.OpenMenuName("Error");
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
}
