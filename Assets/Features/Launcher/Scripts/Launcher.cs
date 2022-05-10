using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace PUN2NetWorking
{
    public class Launcher : MonoBehaviourPunCallbacks
    {
        #region Fields

        private string _gameVersion = "1";

        [SerializeField] private byte _maxPlayerPerRoom = 4;

        #endregion

#region Monobehaviour callbacks
        private void Awake()
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        private void Start()
        {
            Connect();
        }
        #endregion
        #region Methods

        public void Connect()
        {
            if (PhotonNetwork.IsConnected)
            {
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {

                PhotonNetwork.ConnectUsingSettings();
                PhotonNetwork.GameVersion = _gameVersion;
            }
        }

        #endregion

        #region MonobehaviourPunCallbacks

        public override void OnConnectedToMaster()
        {
           print("Launcher|OnConnectedToMaster");
           PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
          print($"Launcher|Ondisconnected,Cause:{cause}");
        }

        public override void OnJoinedRoom()
        {
            print("Launcher|OnJoinedRoom");
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            print($"Launcher|OnJoinRandomFailed,ReturnCode: {returnCode}, message: {message}");
            PhotonNetwork.CreateRoom(
                default, 
                new RoomOptions()
                {
                    MaxPlayers = _maxPlayerPerRoom
                }
                );
        }

        #endregion
    }
}


