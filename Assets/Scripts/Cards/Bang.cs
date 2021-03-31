using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Bang : MonoBehaviour
{
    public PlayerLives _playerLives = new PlayerLives();

    public void loseLifeWithBangCard(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.BangCards.BANG.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
            _playerLives.decreaseLives("Bang");
        }
    }
}
