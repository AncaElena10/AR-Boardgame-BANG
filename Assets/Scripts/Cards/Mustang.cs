using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using Vuforia;
using System.Linq;
using System;

public class Mustang : MonoBehaviourPunCallbacks
{
    public void MustangFound(TrackableBehaviour mTrackableBehaviour)
    {
        bool condition = mTrackableBehaviour.TrackableName.ToString() == CustomEnums.DistanceCards.MUSTANG.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";
        if (condition)
        {
            // set cusom property ["Mustang"]=true
            PunHashtable customProperties_mustang = new PunHashtable();
            customProperties_mustang.Add("Mustang", true);
            PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties_mustang);
        }
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        //print("[OnPlayerPropertiesUpdate] YO Y OY OYO from Mustang.");
        if (targetPlayer != null) {
            //print("[OnPlayerPropertiesUpdate] AHOI YO YO from Mustang.");
            if (changedProps.ContainsKey("Mustang")) {
                print("[Mustang] Mustang custom property has changed!");
            }
        }
    }
}
