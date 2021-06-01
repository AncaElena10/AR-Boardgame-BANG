using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _roomName;

    // [SerializeField]
    // private Text _playerName;

    private RoomsCanvases _roomsCanvases;
    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    public void OnClick_CreateRoom()
    {
        // create room
        // join or create room

        if (!PhotonNetwork.IsConnected) {
            return;
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 7;
        options.BroadcastPropsChangeToAll = true; // broadcast changes

        //setPlayerName();

        PhotonNetwork.JoinOrCreateRoom(_roomName.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        print("[CreateRoomMenu] Created room successfully!");
        _roomsCanvases.CurrentRoomCanvas.Show();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("[CreateRoomMenu] Room creation failed!");   
    }

    //private void setPlayerName()
    //{
    //    string playerName = _playerName.text;
    //    if (playerName != "") {
    //        PhotonNetwork.NickName = playerName;
    //    }   
    //}
}
