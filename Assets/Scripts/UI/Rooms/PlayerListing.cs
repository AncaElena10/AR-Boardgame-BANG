using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using System;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerListing : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Text _text;

    //[SerializeField]
    //public GameObject player_listing_obj;

    public Player Player { get; private set; }
    private Player _player;

    public static string player_to_attack = "";

    public static bool _canAttack = false;
    public static bool _canAttack_confirm = false;
    int cntSamePlayer = 0; // nu uita sa-l faci 0 la loc pe undeva

    public static List<string> attackedPlayersList;


    public void Start()
    {
        attackedPlayersList = new List<string>();
    }

    public void Update()
    {
        //for (int i = 0; i < attackedPlayersList.Count(); i++) {
        //    //print("AAAAAAAAAAA " + attackedPlayersList[i] + " ");
        //}
    }

    public void SetPlayerInfo(Player player)
    {
        Player = player;

        // custom property
        //int result = (int) player.CustomProperties["RandomNumber"];
        //_text.text = result.ToString() + ", " + player.NickName;

        _text.text = player.NickName;

        _player = player;
    }

    public void OnMouseDown()
    {
        //print("DAAAA AICI!!! " + _player);

        attackPlayer();
        player_to_attack = _player.NickName;
        _canAttack_confirm = true;
    }

    private void attackPlayer()
    {
        int playersNumber = PhotonNetwork.CountOfPlayers;
        string str = "Distance_" + playersNumber + "_players";

        // print("KKKKKKK " + playersNumber);

        if (checkIfCanPewPew(str)) { // player in range, can attack
            if (player_to_attack != "") {
                print("YUP!");
                attackedPlayersList.Add(player_to_attack);
            }

            for (int i = 0; i < attackedPlayersList.Count(); i++) {
                if (attackedPlayersList[i].ToString() == player_to_attack) {
                    print("da?!?!?!");
                    cntSamePlayer++;
                }
            }

            print("JJJJJJJJ " + cntSamePlayer);

            // check the array is not already populated with the player you want to attack, because you can attack a player only once per turn
            // TODO - treat special case - if VOLCANIC equiped => infinite BANGs
            if (cntSamePlayer > 1) { // already in the list, check if HasVolcanic  = true
                print("1--------------1");
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("HasVolcanic")) {
                    print("2--------------2");
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["HasVolcanic"] == true) {
                        print("[PlayerListing] Attack that player again and again and again!!!");

                        // set as a custom property the player name , so he can be notified someone wants to attack him - nu uita sa pui inapoi ""
                        PunHashtable roomCustomProperties = new PunHashtable();
                        roomCustomProperties.Add("PlayerToAttack", player_to_attack);
                        PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);
                    } else {
                        print("Player already attacked, stop it.");

                        PunHashtable roomCustomProperties = new PunHashtable();
                        roomCustomProperties.Add("PlayerToAttack", "");
                        PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);
                    }
                } else {
                    print("[PlayerListing] Error... it should never enter here...");
                }
            } else {
                print("[PlayerListing] Attack this player for the first time.");

                PunHashtable roomCustomProperties = new PunHashtable();
                roomCustomProperties.Add("PlayerToAttack", player_to_attack);
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);
            }

            //print("da?!?!?");
        } else {
            print("3--------------3");
            PunHashtable roomCustomProperties = new PunHashtable();
            roomCustomProperties.Add("PlayerToAttack", "");
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomCustomProperties);
        }
    }

    private bool checkIfCanPewPew(string distanceString)
    {
        string distancesForCurrentPlayer = (string)PhotonNetwork.LocalPlayer.CustomProperties[distanceString];
        List<int> distancesForCurrentPlayer_list = stringToListOfInts(distancesForCurrentPlayer);

        //print("FFFFFFFFFF " + distancesForCurrentPlayer);

        //print("HHHHHHHHHHHHHHH " + PhotonNetwork.LocalPlayer.CustomProperties["Range"]);
        
        // print("LLLLLLLLLLLLLLLLL" + distancesForCurrentPlayer_list[_player.ActorNumber - 1]);

        if (_player.CustomProperties.ContainsKey("Mustang")) {
            if ((bool)_player.CustomProperties["Mustang"]) { // if the player I want to attack has Mustang, then I need a minimum range of 2
                distancesForCurrentPlayer_list[_player.ActorNumber - 1] = distancesForCurrentPlayer_list[_player.ActorNumber - 1] + 1;
                if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                    print("[PlayerListing] You can't attack the player " + _player + ". Not enough range, because he has Mustang equiped.");
                    _canAttack = false;
                } else {
                    print("[PlayerListing] PEW PEW on player " + _player + " even though he has Mustang equiped.");
                    _canAttack = true;
                }
            } else {
                if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                    print("[PlayerListing] You can't attack the player " + _player + ". Not enough range.");
                    _canAttack = false;
                } else {
                    print("[PlayerListing] PEW PEW on player " + _player);
                    _canAttack = true;
                }
            }
        } else {
            if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                print("[PlayerListing] You can't attack the player " + _player + ". Not enough range.");
                _canAttack = false;
            } else {
                print("[PlayerListing] PEW PEW on player " + _player);
                _canAttack = true;
            }
        }

        return _canAttack;
    }

    private List<int> stringToListOfInts(string str)
    {
        return str.Split(',').Select(Int32.Parse).ToList();
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("PlayerToAttack")) {
            print("[PlayerListing] Room PlayerToAttack custom property has changed!");
        }
    }

    public List<string> AttackedPlayersList()
    {
        return attackedPlayersList;
    }
}
