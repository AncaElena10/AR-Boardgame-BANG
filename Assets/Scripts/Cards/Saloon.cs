using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Saloon : MonoBehaviour
{
    public PlayerLives _playerLives = new PlayerLives();

    public void gainLifeWithSaloonCard(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.LifeCards.SALON.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
            _playerLives.increaseLives(0, "Saloon");
        }
    }
}
