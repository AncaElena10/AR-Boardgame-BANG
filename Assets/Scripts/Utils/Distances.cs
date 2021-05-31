using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class Distances : MonoBehaviourPunCallbacks
{
    public void AddDistances(Player player)
    {
        if (player.ActorNumber == 1) {
            Player1_distances(player);
        }
        if (player.ActorNumber == 2) {
            Player2_distances(player);
        }
        if (player.ActorNumber == 3) {
            Player3_distances(player);
        }
        if (player.ActorNumber == 4) {
            Player4_distances(player);
        }
        if (player.ActorNumber == 5) {
            Player5_distances(player);
        }
        if (player.ActorNumber == 6) {
            Player6_distances(player);
        }
        if (player.ActorNumber == 7) {
            Player7_distances(player);
        }
    }

    private void Player1_distances(Player player)
    {
        PunHashtable customPorperties_for_player1 = new PunHashtable();
        customPorperties_for_player1.Add("Distance_2_players", "0, 1, 0, 0, 0, 0, 0");
        customPorperties_for_player1.Add("Distance_3_players", "0, 1, 1, 0, 0, 0, 0");
        customPorperties_for_player1.Add("Distance_4_players", "0, 1, 2, 1, 0, 0, 0");
        customPorperties_for_player1.Add("Distance_5_players", "0, 1, 2, 2, 1, 0, 0");
        customPorperties_for_player1.Add("Distance_6_players", "0, 1, 2, 3, 2, 1, 0");
        customPorperties_for_player1.Add("Distance_7_players", "0, 1, 2, 3, 3, 2, 1");
        player.SetCustomProperties(customPorperties_for_player1);
    }

    private void Player2_distances(Player player)
    {
        PunHashtable customPorperties_for_player2 = new PunHashtable();
        customPorperties_for_player2.Add("Distance_2_players", "1, 0, 0, 0, 0, 0, 0");
        customPorperties_for_player2.Add("Distance_3_players", "1, 0, 1, 0, 0, 0, 0");
        customPorperties_for_player2.Add("Distance_4_players", "1, 0, 1, 2, 0, 0, 0");
        customPorperties_for_player2.Add("Distance_5_players", "1, 0, 1, 2, 2, 0, 0");
        customPorperties_for_player2.Add("Distance_6_players", "1, 0, 1, 2, 3, 2, 0");
        customPorperties_for_player2.Add("Distance_7_players", "1, 0, 1, 2, 3, 3, 2");
        player.SetCustomProperties(customPorperties_for_player2);
    }

    private void Player3_distances(Player player)
    {
        PunHashtable customPorperties_for_player3 = new PunHashtable();
        customPorperties_for_player3.Add("Distance_3_players", "1, 1, 0, 0, 0, 0, 0");
        customPorperties_for_player3.Add("Distance_4_players", "2, 1, 0, 1, 0, 0, 0");
        customPorperties_for_player3.Add("Distance_5_players", "2, 1, 0, 1, 2, 0, 0");
        customPorperties_for_player3.Add("Distance_6_players", "2, 1, 0, 1, 2, 3, 0");
        customPorperties_for_player3.Add("Distance_7_players", "2, 1, 0, 1, 2, 3, 3");
        player.SetCustomProperties(customPorperties_for_player3);
    }

    private void Player4_distances(Player player)
    {
        PunHashtable customPorperties_for_player4 = new PunHashtable();
        customPorperties_for_player4.Add("Distance_4_players", "1, 2, 1, 0, 0, 0, 0");
        customPorperties_for_player4.Add("Distance_5_players", "2, 2, 1, 0, 1, 0, 0");
        customPorperties_for_player4.Add("Distance_6_players", "3, 2, 1, 0, 1, 2, 0");
        customPorperties_for_player4.Add("Distance_7_players", "3, 2, 1, 0, 1, 2, 3");
        player.SetCustomProperties(customPorperties_for_player4);
    }

    private void Player5_distances(Player player)
    {
        PunHashtable customPorperties_for_player5 = new PunHashtable();
        customPorperties_for_player5.Add("Distance_5_players", "1, 2, 2, 1, 0, 0, 0");
        customPorperties_for_player5.Add("Distance_6_players", "2, 3, 2, 1, 0, 1, 0");
        customPorperties_for_player5.Add("Distance_7_players", "3, 3, 2, 1, 0, 1, 2");
        player.SetCustomProperties(customPorperties_for_player5);
    }

    private void Player6_distances(Player player)
    {
        PunHashtable customPorperties_for_player6 = new PunHashtable();
        customPorperties_for_player6.Add("Distance_6_players", "1, 2, 3, 2, 1, 0, 0");
        customPorperties_for_player6.Add("Distance_7_players", "2, 3, 3, 2, 1, 0, 1");
        player.SetCustomProperties(customPorperties_for_player6);
    }

    private void Player7_distances(Player player)
    {
        PunHashtable customPorperties_for_player7 = new PunHashtable();
        customPorperties_for_player7.Add("Distance_7_players", "1, 2, 3, 3, 2, 1, 0");
        player.SetCustomProperties(customPorperties_for_player7);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null) {
            //print("[Distances] AHOI YO YO from PlayerListingMenu.");
            if (changedProps.ContainsKey("Distance_2_players")) {
                print("[Distances] Distance_2_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_3_players")) {
                print("[Distances] Distance_3_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_4_players")) {
                print("[Distances] Distance_4_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_5_players")) {
                print("[Distances] Distance_5_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_6_players")) {
                print("[Distances] Distance_6_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_7_players")) {
                print("[Distances] Distance_7_players custom property has changed!");
            }
        }
    }
}
