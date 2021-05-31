using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class Indieni : MonoBehaviourPunCallbacks
{
    public void play_indieni_card(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.LifeCards.INDIENI.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
            setIndieniPlayedToTrue();
        }
    }

    public void setIndieniPlayedToTrue()
    {
        PunHashtable _indieniCustomProperty = new PunHashtable();
        _indieniCustomProperty["IndieniPlayed"] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(_indieniCustomProperty);
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("IndieniPlayed")) {
            print("[Indieni] Room IndieniPlayed custom property has changed!");
        }
    }
}
