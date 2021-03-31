using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using System;
using Photon.Realtime;

public class PlayerListing : MonoBehaviour
{
    [SerializeField]
    private Text _text;

    [SerializeField]
    public GameObject player_listing_obj;

    public Player Player { get; private set; }
    private Player _player;

    public static string player_to_attack = "";

    public static bool _canAttack = false;
    public static bool _canAttack_confirm = false;

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

        checkIfCanPewPew(str);
    }

    private void checkIfCanPewPew(string distanceString)
    {
        string distancesForCurrentPlayer = (string)PhotonNetwork.LocalPlayer.CustomProperties[distanceString];
        List<int> distancesForCurrentPlayer_list = stringToListOfInts(distancesForCurrentPlayer);

        print("HHHHHHHHHHHHHHH " + PhotonNetwork.LocalPlayer.CustomProperties["Range"]);
        
        // print("LLLLLLLLLLLLLLLLL" + distancesForCurrentPlayer_list[_player.ActorNumber - 1]);

        if (_player.CustomProperties.ContainsKey("Mustang")) {
            if ((bool)_player.CustomProperties["Mustang"]) { // if the player I want to attack has Mustang, then I need a minimum range of 2
                distancesForCurrentPlayer_list[_player.ActorNumber - 1] = distancesForCurrentPlayer_list[_player.ActorNumber - 1] + 1;
                if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                    print("You can't attack the player " + _player + ". Not enough range, because he has Mustang equiped.");
                    _canAttack = false;
                } else {
                    print("PEW PEW on player " + _player + " even though he has Mustang equiped.");
                    _canAttack = true;
                }
            } else {
                if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                    print("You can't attack the player " + _player + ". Not enough range.");
                    _canAttack = false;
                } else {
                    print("PEW PEW on player " + _player);
                    _canAttack = true;
                }
            }
        } else {
            if (distancesForCurrentPlayer_list[_player.ActorNumber - 1] > 1) {
                print("You can't attack the player " + _player + ". Not enough range.");
                _canAttack = false;
            } else {
                print("PEW PEW on player " + _player);
                _canAttack = true;
            }
        }  
    }

    private List<int> stringToListOfInts(string str)
    {
        return str.Split(',').Select(Int32.Parse).ToList();
    }
}
