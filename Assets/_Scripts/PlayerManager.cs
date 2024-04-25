using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
   private PhotonView _photonView;

   private void Awake()
   {
      _photonView = GetComponent<PhotonView>();
   }
   
   void Start()
   {
      if (_photonView.IsMine)
      {
         CreatePlayerController();
      }
   }
   
   public void CreatePlayerController()
   {
      PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs",
         "PlayerController"), Vector3.zero, Quaternion.identity);
   }


}
