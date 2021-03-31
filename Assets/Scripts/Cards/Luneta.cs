using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using Vuforia;
using System.Linq;
using System;

public class Luneta : MonoBehaviourPunCallbacks
{
    private int lunetaCount = 1;
    public PlayerRange _playerRange = new PlayerRange();

    public void LunetaFound(TrackableBehaviour mTrackableBehaviour)
    {
        bool condition = mTrackableBehaviour.TrackableName.ToString() == CustomEnums.DistanceCards.LUNETA.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";
        if (condition) {
            int playerNumber = PhotonNetwork.CountOfPlayers;
            string str = "Distance_" + playerNumber + "_players";

            // set cusom property ["Luneta"]=true
            PunHashtable customProperties_luneta = new PunHashtable();
            customProperties_luneta.Add("Luneta", true);
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties_luneta);

            string playerDistances = (string)PhotonNetwork.LocalPlayer.CustomProperties[str];

            print("OOOOOOOOO " + playerDistances);

            List<int> playerDistances_list = stringToListOfInts(playerDistances);

            for (int i = 0; i < playerDistances_list.Count; i++) {
                if (i != PhotonNetwork.LocalPlayer.ActorNumber - 1) { // don't change the distance for the current player
                    playerDistances_list[i] = playerDistances_list[i] - lunetaCount;
                }
            }

            // set the range hud
            _playerRange.increaseRange(lunetaCount, "Luneta");

            // update the custo props with the new values
            string playerDistances_string = listOfIntsToString(playerDistances_list);

            print("OOOOOOOOOOOOOOOO " + playerDistances_string);

            PunHashtable customProperties_distance = new PunHashtable();
            customProperties_distance.Add(str, playerDistances_string);
            PhotonNetwork.LocalPlayer.CustomProperties = customProperties_distance;
        }
    }

    private List<int> stringToListOfInts(string str)
    {
        return str.Split(',').Select(Int32.Parse).ToList();
    }

    private string listOfIntsToString(List<int> list)
    {
        return string.Join(",", list.ToArray());
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        print("[OnPlayerPropertiesUpdate] YO Y OY OYO from Luneta.");
        if (targetPlayer != null)
        {
            print("[OnPlayerPropertiesUpdate] AHOI YO YO from Luneta.");
            if (changedProps.ContainsKey("Luneta"))
            {
                print("[OnPlayerPropertiesUpdate] Luneta custom property has changed!");
            }
        }
    }
}
