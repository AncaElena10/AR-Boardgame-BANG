using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Beer : MonoBehaviour
{
    public PlayerLives _playerLives = new PlayerLives();

    public void gainLifeWithBeerCard(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.LifeCards.BERE.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
            _playerLives.increaseLives(0, "Beer");
        }
    }
}
