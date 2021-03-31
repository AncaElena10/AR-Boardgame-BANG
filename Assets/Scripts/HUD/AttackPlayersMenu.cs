using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class AttackPlayersMenu : MonoBehaviourPunCallbacks, IInRoomCallbacks
{


    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListing _playerListings;

    private List<PlayerListing> _listings = new List<PlayerListing>();

    public void Start()
    {
        // print("TTTTTTTTTTTTTTTTTTT-----------------????????");
    }

    public void Update()
    {
    }

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++) {
            Destroy(_listings[i].gameObject);
        }
        _listings.Clear();
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected) {
            return;
        }

        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) {
            return;
        }

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) {
            //print("EEEEEEEEEEE " + playerInfo.Value);

            // display all the players except for the current player
            if (playerInfo.Value.ActorNumber != PhotonNetwork.LocalPlayer.ActorNumber)
            {
                AddPlayerListing(playerInfo.Value);
            }
        }
    }

    private void AddPlayerListing(Player player)
    {
        // if the list exists
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1) {
            _listings[index].SetPlayerInfo(player);
        } else {
            PlayerListing listing = Instantiate(_playerListings, _content);

            if (listing != null) {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1) {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }
}
