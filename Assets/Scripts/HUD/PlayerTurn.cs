using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;
using System;

public class PlayerTurn : MonoBehaviourPunCallbacks
{

    [SerializeField]
    GameObject endTurnBtn;

    //List<int> turnNumbersArray = new List<int>();
    //List<int> turnNumbersArrayAux = new List<int>();

    List<int> PlayerActorIds_list;
    List<int> PlayerActorIdsAux_list;

    private PlayerListing _playerListing = new PlayerListing();

    bool passTurn = false;

    //PlayerListingsMenu pl;
    //CommonElements ce;

    public void Update()
    {
        //for (int player = 1; player <= PhotonNetwork.PlayerList.Length; player++)
        //{
        //    if (PhotonNetwork.PlayerList)
        //}
    }

    public void Start()
    {
        //print("Hello from PlayerTurn Start function!");
        //pl = new PlayerListingsMenu();
        //ce = new CommonElements();

        //List<Player> playerList = PhotonNetwork.PlayerList.ToList();
        //playerList.Sort(SortByActorNumber);

        // don't forget to uncomment this
        //foreach (Player player in playerList)
        //{
        //    print("GAGAGAGAGA " + player);
        //    int playerTurnNumber = player.ActorNumber;
        //    turnNumbersArray.Add(playerTurnNumber);
        //    turnNumbersArrayAux.Add(playerTurnNumber);
        //}

        //print("------TTTTTTTT------- " + ce.ReturnArray().Count);

        //for (int i = 0; i < ce.ReturnArray().Count; i++)
        //{
        //    print("------TTTTTTTT------- " + turnNumbersArray[i]);
        //}

        //pl = GetComponent<PlayerListingsMenu>();

        // get the room custom properties
        // convert the strings to lists and use them in this script
        // lists to convert - PlayerActorIds, PlayerActorIdsAux

        // verify distances
        // foreach (Player player in PhotonNetwork.PlayerList)
        // {
        //     print("xxxxxxOOOOOOOOxxxxxx " + player.ActorNumber + "        " + player.CustomProperties["Distance_7_players"] + "      " + player);
        // }

        //print("OOOOOOOOOOOOOOOOOOOOOO ---------- " + PhotonNetwork.CurrentRoom.CustomProperties["PlayerActorIds"] + "               " + PhotonNetwork.CurrentRoom.CustomProperties["PlayerActorIdsAux"]);
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    print("OOOOOOOOOOOOOO      " + player.NickName + "     " + player.ActorNumber);
        //}
    }

    //[PunRPC]
    public void EndPlayerTurn()
    {
        print("[PlayerTurn] Player turn has ended!");

        PassTurn();
    }

    public void OnClick_Button()
    {
        EndPlayerTurn();

        // remove the attacked players from the array [PlayerListing] - TODO mai verifica asta
        _playerListing.AttackedPlayersList().Clear();

        // set back to 0 - player can only BANG one time per turn
        _playerListing.Cnt(0);

        // set PlayerToAttack to ""
        // set IndieniPlayed to false
        setPlayerToAttackToEmpty();
        setIndieniPlayedToFalse();
    }

    public void setIndieniPlayedToFalse()
    {
        PunHashtable _indieniCustomProperty = new PunHashtable();
        _indieniCustomProperty["IndieniPlayed"] = false;
        PhotonNetwork.CurrentRoom.SetCustomProperties(_indieniCustomProperty);
    }

    public void setPlayerToAttackToEmpty()
    {
        PunHashtable _playerToAttackCustomProperty = new PunHashtable();
        _playerToAttackCustomProperty["PlayerToAttack"] = "";
        PhotonNetwork.CurrentRoom.SetCustomProperties(_playerToAttackCustomProperty);
    }

    //public bool SetCustomPropertiesSafe(Room room, string key, object newValue, WebFlags webFlags = null)
    //{
    //    PunHashtable newProps = new PunHashtable(1) { { key, newValue } };
    //    PunHashtable oldProps = new PunHashtable(1) { { key, room.CustomProperties[key] } };
    //    return room.LoadBalancingClient.OpSetCustomPropertiesOfRoom(newProps, oldProps, webFlags);
    //}

    private void PassTurn()
    {
        passTurn = true;
        // get the room property
        string PlayerActorIds_string = (string)PhotonNetwork.CurrentRoom.CustomProperties["PlayerActorIds"];
        string PlayerActorIdsAux_string = (string)PhotonNetwork.CurrentRoom.CustomProperties["PlayerActorIdsAux"];

        //print("XXXXXXXXXXXXXXX111111 " + PlayerActorIds_string + " " + "XXXXXXXXXXXXXX1111111111 " + PlayerActorIdsAux_string);

        // convert the custom room property from string to list of ints
        PlayerActorIds_list = PlayerActorIds_string.Split(',').Select(Int32.Parse).ToList();
        PlayerActorIdsAux_list = PlayerActorIdsAux_string.Split(',').Select(Int32.Parse).ToList();

        //for (int i = 0; i < PlayerActorIds_list.Count; i++)
        //{
        //    print("------TTTTTTTToooooTTTTooooo------- " + PlayerActorIds_list[i]);
        //}

        changeTurnPropertyForCurrentPlayer();
        changeTurnPropertyForNextPlayers();

        //print("DDDDDDD " + pl.GetPlayerActorIds().Count);

        //pl = GetComponent<PlayerListingsMenu>();
        //foreach (int s in pl.playerActorIds)
        //{
        //    print("DDDDDDD " + s);
        //}

        //print("DDDDDD " + GetComponent<PlayerListingsMenu>().playerActorIds.Count);

        //List<int> playerActorList = pl.getPlayerActorIds();
        //print("DDDDDDD " + playerActorList.Count);

        //for (int i = 0; i < pl.playerActorIds.Count; i++)
        //{
        //    print("DDDDDDD " + pl.playerActorIds[i]);
        //}
    }

    private void changeTurnPropertyForCurrentPlayer()
    {
        //string PlayerActorIds_string_1 = (string)PhotonNetwork.CurrentRoom.CustomProperties["PlayerActorIds"];
        //print("---------111111111111111 " + PlayerActorIds_string_1);

        //for (int i = 0; i < PlayerActorIds_list.Count; i++)
        //{
        //    Debug.Log("0000000000000000000 " + PlayerActorIds_list[i]);
        //}

        //print("-------00000000-------- " + PhotonNetwork.PlayerList[PlayerActorIds_list[0] - 1] + " --------0000000000----------- " + PhotonNetwork.PlayerList[PlayerActorIds_list[0] - 1].ActorNumber);

        PunHashtable _myCustomProperties_forCurrentPlayer = new PunHashtable();
        _myCustomProperties_forCurrentPlayer["Turn"] = false;
        PhotonNetwork.PlayerList[PlayerActorIds_list[0] - 1].SetCustomProperties(_myCustomProperties_forCurrentPlayer);
    }

    private void changeTurnPropertyForNextPlayers()
    {
        //List<Player> playerList = PhotonNetwork.PlayerList.ToList();
        //playerList.Sort(SortByActorNumber);

        //for (int i = 0; i < PlayerActorIds_list.Count; i++)
        //{
        //    print("11111111111111111 " + PlayerActorIds_list[i]);
        //}

        PlayerActorIds_list.RemoveAt(0);

        //for (int i = 0; i < PlayerActorIds_list.Count; i++)
        //{
        //    print("22222222222222222 " + PlayerActorIds_list[i]);
        //}

        // convert again the new list
        string playerActorIds_String_changed = string.Join(",", PlayerActorIds_list.ToArray());

        //print("3333333333333333 " + playerActorIds_String_changed);


        // broadcast this change
        PunHashtable roomCustomProperties = new PunHashtable();
        roomCustomProperties.Add("PlayerActorIds", playerActorIds_String_changed);
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);

        foreach (Player player in PhotonNetwork.PlayerList) {
            //print("----------33333333---------");
            // as long as the array contains data
            if (PlayerActorIds_list.Count > 0) {

                if (player.ActorNumber == PlayerActorIds_list[0]) {
                    //print("4444444444444444 " + player.ActorNumber);
                    PunHashtable _myCustomProperties_forNextPlayer = new PunHashtable();
                    _myCustomProperties_forNextPlayer["Turn"] = true;
                    player.SetCustomProperties(_myCustomProperties_forNextPlayer);

                    //PhotonView pv = PhotonNetwork.GetPhotonView(player.ActorNumber);
                    //print("PVPPVPVPV " + pv.gameObject);
                    //pv.gameObject.SetActive(true);
                }
            } else {
                //print("55555555555555555555");
                // populate turnNumbersArray with turnNumbersArrayAux
                for (int i = 0; i < PlayerActorIdsAux_list.Count; i++) {
                    PlayerActorIds_list.Add(PlayerActorIdsAux_list[i]);
                }

                if (player.ActorNumber == PlayerActorIds_list[0]) {
                    //print("66666666666666666666666 " + player.ActorNumber);
                    PunHashtable _myCustomProperties_forNextPlayer = new PunHashtable();
                    _myCustomProperties_forNextPlayer["Turn"] = true;
                    player.SetCustomProperties(_myCustomProperties_forNextPlayer);
                }

                //for (int i = 0; i < PlayerActorIds_list.Count; i++)
                //{
                //    print("777777777777777777777 " + PlayerActorIds_list[i]);
                //}

                // convert again and again the list
                string playerActorIds_String_changed_again = string.Join(",", PlayerActorIds_list.ToArray());

                //print("8888888888888888888 " + playerActorIds_String_changed_again);

                // broadcast again and again the change
                PunHashtable roomCustomProperties_again = new PunHashtable();
                roomCustomProperties_again.Add("PlayerActorIds", playerActorIds_String_changed_again);
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties_again);
            }
        }
    }

    private void activateTurnButton()
    {
        endTurnBtn.SetActive(true);
    }

    private void deactivateTurnButton()
    {
        endTurnBtn.SetActive(false);
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("PlayerActorIds") || propertiesThatChanged.ContainsKey("PlayerActorIdsAux")) {
            print("[PlayerTurn] Room PlayerActorIds/PlayerActorIdsAux custom property has changed!");
        }
        if (propertiesThatChanged.ContainsKey("PlayerToAttack")) {
            print("[PlayerTurn] Room PlayerToAttack custom property has changed!");
        }
        if (propertiesThatChanged.ContainsKey("IndieniPlayed")) {
            print("[PlayerTurn] Room IndieniPlayed custom property has changed!");
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        //print("[OnPlayerPropertiesUpdate] YO Y OY OYO from PlayerTurn.");
        if (targetPlayer != null) {
            //print("[OnPlayerPropertiesUpdate] AHOI YO YO from PlayerTurn.");
            if (changedProps.ContainsKey("Turn")) {
                print("[PlayerTurn] Turn custom property has changed!");
                //print("DAAAAAAAAAAAA11111111");
                if (passTurn) {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"]) {
                        activateTurnButton();

                        //print("-----------------");
                        //foreach (Player player in PhotonNetwork.PlayerList)
                        //{
                        //    print("EEE111      " + player.NickName + "     " + player.ActorNumber + "         " + player.CustomProperties["Turn"]);
                        //}
                    }

                    if (!(bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"]) {
                        deactivateTurnButton();

                        //print("-----------------");
                        //foreach (Player player in PhotonNetwork.PlayerList)
                        //{
                        //    print("EEE222      " + player.NickName + "     " + player.ActorNumber + "         " + player.CustomProperties["Turn"]);
                        //}
                    }

                    //if (targetPlayer == PhotonNetwork.LocalPlayer)
                    //{
                    //    print("A LOCAL PLAYER OOOOOOOOOOOOOOOOOOOO");
                    //    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"])
                    //    {
                    //        print("------------------ Local player button true. ------------------");
                    //        endTurnBtn.SetActive(true);
                    //    }
                    //    else
                    //    {
                    //        print("------------------ Local player button false. ------------------");
                    //        endTurnBtn.SetActive(false);
                    //    }
                    //}
                    //else
                    //{
                    //    print("NOT A LOCAL PLAYER OOOOOOOOOOOOOOOOOO");
                    //    foreach (Player player in PhotonNetwork.PlayerList)
                    //    {
                    //        print("TTTTTTTTTT " + player.ActorNumber + " " + player.CustomProperties["Turn"]);
                    //        if ((bool)player.CustomProperties["Turn"])
                    //        {
                    //            print("HEI HEI HEI 1");
                    //            print("AHOI1---- " + player.ActorNumber);
                    //            endTurnBtn.SetActive(true);
                    //        }

                    //        if (!(bool)player.CustomProperties["Turn"])
                    //        {
                    //            print("HEI HEI HEI 2");
                    //            print("AHOI2---- " + player.ActorNumber);
                    //            endTurnBtn.SetActive(false);
                    //        }
                    //    }
                    //}


                    //foreach (Player player in PhotonNetwork.PlayerList)
                    //{
                    //    if ((bool)player.CustomProperties["Turn"] == true)
                    //    {
                    //        print("enters here 1");
                    //        activateButton();
                    //    }

                    //    if ((bool)player.CustomProperties["Turn"] == false)
                    //    { // the code only reach this if
                    //        print("enters here 2");
                    //        deactivateButton();
                    //    }
                    //}
                }


                //if (passTurn)
                //{
                //    print("DAAAAAAAAAAAAAAAAA22222222222222222");
                //    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"])
                //    {
                //        print("Hei hei hei 1111111111111111111111");
                //        //endTurnBtn.SetActive(true);

                //        foreach (Player player in PhotonNetwork.PlayerList)
                //        {
                //            print("AHOI1---- " + player.ActorNumber);
                //            print("AHOI2---- " + (bool)player.CustomProperties["Turn"]);

                //            //if ((bool)player.CustomProperties["Turn"])
                //            //{
                //            //    print("DA, TRUE HERE");
                //            //    endTurnBtn.SetActive(true);
                //            //}
                //        }
                //        //passTurn = false;
                //    } else
                //    {
                //        print("WOOOOOOHHHHHHHHHHHOOOOOOOOOOO???????");
                //        //endTurnBtn.SetActive(false);

                //        foreach (Player player in PhotonNetwork.PlayerList)
                //        {
                //            print("AHOI3---- " + player.ActorNumber);
                //            print("AHOI4---- " + (bool)player.CustomProperties["Turn"]);

                //            //if (!(bool)player.CustomProperties["Turn"])
                //            //{
                //            //    print("DA, FALSE HERE");
                //            //    endTurnBtn.SetActive(true);
                //            //}

                //            //print("LLLLLLLLLLLLLLLLLL " + player.ActorNumber);
                //        }
                //    }
                //    //else
                //    //{
                //    //    print("Hei hei hei 22222222222222222222");
                //    //    endTurnBtn.SetActive(false);

                //    //    foreach (Player player in PhotonNetwork.PlayerList)
                //    //    {
                //    //        print("AHOI3---- " + (int)player.CustomProperties["TurnNumber"]);
                //    //        print("AHOI4---- " + (bool)player.CustomProperties["Turn"]);
                //    //    }
                //    //    passTurn = false;
                //    //}
                //} else
                //{
                //    print("WOOOOOOOOOOOOOOOHHHHHHHHHHHHHHOOOOOOOOOOOO22222222222???????");
                //}
            }
            //else
            //{
            //    print("Hei hei hei 2222");
            //    endTurnBtn.SetActive(false);

            //    foreach (Player player in PhotonNetwork.PlayerList)
            //    {
            //        print("AHOI3---- " + (int)player.CustomProperties["TurnNumber"]);
            //        print("AHOI4---- " + (bool)player.CustomProperties["Turn"]);
            //    }
            //}
        }
    }

    //public void EndPlayerTurn()
    //{
    //    print("Player turn ended!");
    //    //endTurnBtn.SetActive(false);

    //    //int totalNumberOfConnectedPlayers = PhotonNetwork.CountOfPlayers;
    //    //print("There are currently " + totalNumberOfConnectedPlayers + " players connected.");
    //    //int[] turnNumbersArray = new int[totalNumberOfConnectedPlayers];

    //    List<int> turnNumbersArrayAux = new List<int>();

    //    foreach (Player player in PhotonNetwork.PlayerList)
    //    {
    //        int turnNumberProperty = (int)player.CustomProperties["TurnNumber"];
    //        turnNumbersArray.Add(turnNumberProperty);
    //        turnNumbersArrayAux.Add(turnNumberProperty);
    //    }

    //    // sort the lists
    //    turnNumbersArray.Sort();
    //    turnNumbersArrayAux.Sort();

    //    for (int i = 0; i < turnNumbersArray.Count; i++)
    //    {
    //        print(turnNumbersArray[i]);
    //    }

    //    PassTurn();
    //}

    //private void PassTurn()
    //{
    //    passTurn = true;


    //    for (int i = 0; i <= turnNumbersArray.Count - 1; i++) {
    //        //PunHashtable _myCustomProperties = new PunHashtable();

    //        int currentPlayerTurnNumber = (int)PhotonNetwork.LocalPlayer.CustomProperties["TurnNumber"];

    //        print("1111111111111111 " + currentPlayerTurnNumber);

    //        int nextPlayerTurnNumber;

    //        // all the elements minus the last
    //        if (currentPlayerTurnNumber == turnNumbersArray[i]) {
    //            nextPlayerTurnNumber = turnNumbersArray[i + 1];
    //            print("22222222222222222 " + nextPlayerTurnNumber);

    //            foreach (Player player in PhotonNetwork.PlayerList)
    //            {
    //                int turnNumberProperty = (int)player.CustomProperties["TurnNumber"];
    //                print("33333333333333333 " + turnNumberProperty);

    //                if (turnNumberProperty == nextPlayerTurnNumber)
    //                {
    //                    print("4444444444444444 " + player.CustomProperties["Character"]);
    //                    print("4444444444444444444444 " + player.CustomProperties["Turn"]);
    //                    print("444444444444444444444444444444 " + player.CustomProperties["TurnNumber"]);
    //                    PunHashtable _myCustomProperties_forNextPlayer = new PunHashtable();
    //                    _myCustomProperties_forNextPlayer["Turn"] = true;
    //                    player.SetCustomProperties(_myCustomProperties_forNextPlayer);

    //                }
    //            }
    //        }
    //            // last element
    //        if (currentPlayerTurnNumber == turnNumbersArray[turnNumbersArray.Count - 1]) {
    //            nextPlayerTurnNumber = turnNumbersArray[0];
    //            print("55555555555555555 " + nextPlayerTurnNumber);

    //            foreach (Player player in PhotonNetwork.PlayerList)
    //            {
    //                int turnNumberProperty = (int)player.CustomProperties["TurnNumber"];
    //                print("666666666666666666 " + turnNumberProperty);

    //                if (turnNumberProperty == nextPlayerTurnNumber)
    //                {
    //                    print("7777777777777777777");
    //                    PunHashtable _myCustomProperties_forNextPlayer = new PunHashtable();
    //                    _myCustomProperties_forNextPlayer["Turn"] = true;
    //                    player.SetCustomProperties(_myCustomProperties_forNextPlayer);
    //                    //passTurn = true;
    //                }
    //            }
    //        }
    //    }

    //    // set Turn to false for the current player
    //    PunHashtable _myCustomProperties_currentPlayer = new PunHashtable();
    //    _myCustomProperties_currentPlayer["Turn"] = false;
    //    PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties_currentPlayer);
    //    //endTurnBtn.SetActive(false);
    //}
}
