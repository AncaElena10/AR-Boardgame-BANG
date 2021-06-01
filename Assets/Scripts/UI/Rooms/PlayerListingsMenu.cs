using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    [SerializeField]
    private Transform _content;

    [SerializeField]
    private PlayerListing _playerListings;

    // [SerializeField]
    // private Text _playerName;

    private List<PlayerListing> _listings = new List<PlayerListing>();

    private RoomsCanvases _roomsCanvases;

    public static List<int> playerActorIds = new List<int>();
    public static List<int> playerActorIdsAux = new List<int>();

    private Distances _distances = new Distances();

    //bool _ready;

    public void FirstInitialize(RoomsCanvases canvases)
    {
        _roomsCanvases = canvases;
    }

    private void Awake()
    {
        
    }

    // private void setPlayerName()
    // {
    //     print("OOOOOOOOOOOOOOO2222222222222222");
    //     string playerName = _playerName.text;
    //     print("LLLLLLLLLLLLLLLLLL " + playerName);
    //     if (playerName != "") {
    //         print("MMMMMMMMMMMMM ");
    //         PhotonNetwork.NickName = playerName;
    //         PhotonNetwork.LocalPlayer.NickName = playerName;
    //     }
    // }

    public override void OnEnable()
    {
        base.OnEnable();

        //setPlayerName();

        //print("VVVVVVVVVVVVVV " + PhotonNetwork.NickName + "    " + PhotonNetwork.LocalPlayer.NickName);

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
        // a dict
        if (!PhotonNetwork.IsConnected) {
            return;
        }
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) {
            return;
        }

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players) {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player player)
    {
        //print("KKKKKKKKKKKKKKKKKK " + player);
        // if the list exists
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1) {
            //print("111111111111111111111111111");
            _listings[index].SetPlayerInfo(player);
        } else {
            //print("2222222222222222222222222222");
            PlayerListing listing = Instantiate(_playerListings, _content);

            if (listing != null) {
                //print("33333333333333333333333333333");
                listing.SetPlayerInfo(player); // todo: vezi ce-i asta
                _listings.Add(listing);
            }
        }

        playerActorIds.Add(player.ActorNumber);
        playerActorIds.Sort();
        playerActorIdsAux.Add(player.ActorNumber);
        playerActorIdsAux.Sort();

        //print("DDDDDDD1111 " + playerActorIds.Count);
        //for (int i = 0; i < playerActorIds.Count; i++)
        //{
        //    print("DDDDDDD2222 " + playerActorIds[i]);
        //}

        string playerActorIds_String = string.Join(",", playerActorIds.ToArray());
        string playerActorIdsAux_String = string.Join(",", playerActorIdsAux.ToArray());

        //print("XXXXXXXXXXXXXXX " + playerActorIds_String + " " + "XXXXXXXXXXXXXX " + playerActorIdsAux_String);

        PunHashtable roomCustomProperties = new PunHashtable();
        roomCustomProperties.Add("PlayerActorIds", playerActorIds_String);
        roomCustomProperties.Add("PlayerActorIdsAux", playerActorIdsAux_String);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);

        // add distances
        _distances.AddDistances(player);

        // player to attack
        roomCustomProperties.Add("PlayerToAttack", "");
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);

        //// set the player's range at the beginning
        //PunHashtable playerCustomProperties = new PunHashtable();
        //player.CustomProperties.Add("Range", 0);
        //player.SetCustomProperties(playerCustomProperties);
    }

    public List<int> GetPlayerActorIds()
    {
        return playerActorIds;
    }

    public List<int> GetPlayerActorIdsAux()
    {
        return playerActorIdsAux;
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("PlayerActorIds") || propertiesThatChanged.ContainsKey("PlayerActorIdsAux"))
        {
            print("[PlayerListingsMenu] Room PlayerActorIds/PlayerActorIdsAux custom property has changed!");
        }
        if (propertiesThatChanged.ContainsKey("PlayerToAttack")) {
            print("[PlayerListingsMenu] Room PlayerToAttack custom property has changed!");
        }
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    //{
    //    //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    //    if (targetPlayer != null && targetPlayer == PhotonNetwork.LocalPlayer) {
    //        if (changedProps.ContainsKey("Range")) {
    //            print("[PlayerListingsMenu] Range custom property has changed!");
    //        }
    //    }
    //}

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //print("DDDDDDDDDDDDDDDDDDD " + newPlayer);

        // string playerName = _playerName.text;
        // //print("LLLLLLLLLLLLLLLLLLxxxxxx " + playerName);
        // if (playerName != "") {
        //     //print("MMMMMMMMMMMMMxxxxxx ");
        //     newPlayer.NickName = playerName;
        // }

        AddPlayerListing(newPlayer);

        //print("DDDDDDDDDDDDDDDDDDDxxxxxxxxxx " + newPlayer);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1) {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
        }
    }

    public void OnClick_StartGame()
    {
        // only the master can start the game
        //if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(1);
        //}
    }
}
