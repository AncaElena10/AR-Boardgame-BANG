using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;
using System;

public class NextTurn : MonoBehaviour
{
    [SerializeField]
    GameObject yourTurnNext;

    [SerializeField]
    GameObject endTurnButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        YourTurnNext();
    }

    private void YourTurnNext()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character")) {
            List<Player> playerList = PhotonNetwork.PlayerList.ToList();
            playerList.Sort(SortByActorNumber);

            // while I am not the first actor
            if (PhotonNetwork.LocalPlayer.ActorNumber != 1) {
                if (playerList[PhotonNetwork.LocalPlayer.ActorNumber - 2].CustomProperties.ContainsKey("Turn")) {
                    if ((bool)playerList[PhotonNetwork.LocalPlayer.ActorNumber - 2].CustomProperties["Turn"]) {
                        // if the previous player has the turn, then activate the text for the current player
                        //print("1111111111");
                        yourTurnNext.SetActive(true);
                    } else {
                        //print("2222222222222");
                        yourTurnNext.SetActive(false);
                    }
                }
            }
            else // if I am the first actor, then I need a special case - check if the last player has turn
            {
                // if the last player has turn
                if (playerList[PhotonNetwork.CountOfPlayers - 1].CustomProperties.ContainsKey("Turn")) {
                    if ((bool)playerList[PhotonNetwork.CountOfPlayers - 1].CustomProperties["Turn"]) {
                        //print("3333333333333");
                        yourTurnNext.SetActive(true);
                    } else {
                        //print("444444444444");
                        yourTurnNext.SetActive(false);
                    }
                }
            }
        }
    }

    public static int SortByActorNumber(Player a, Player b)
    {
        return a.ActorNumber.CompareTo(b.ActorNumber);
    }
}
