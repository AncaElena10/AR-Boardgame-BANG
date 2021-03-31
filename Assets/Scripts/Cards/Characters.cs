using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Characters : MonoBehaviour
{
    private static string SHERIFF = CustomEnums.CharacterCards.SERIF.ToString();
    private static string ADJUNCT = CustomEnums.CharacterCards.ADJUNCT.ToString();
    private static string RENEGATE = CustomEnums.CharacterCards.RENEGAT.ToString();
    private static string BANDIT = CustomEnums.CharacterCards.BANDIT.ToString();

    public string characterFoundChecks(string scannedCharacter, TrackableBehaviour mTrackableBehaviour)
    {
        bool foundCondition = mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";

        if (mTrackableBehaviour.TrackableName.ToString() == SHERIFF && foundCondition) {
            scannedCharacter = SHERIFF;
            print("Scanned character: " + scannedCharacter);
        } else if (mTrackableBehaviour.TrackableName.ToString() == ADJUNCT && foundCondition) {
            scannedCharacter = ADJUNCT;
            print("Scanned character: " + scannedCharacter);
        } else if (mTrackableBehaviour.TrackableName.ToString() == RENEGATE && foundCondition) {
            scannedCharacter = RENEGATE;
            print("Scanned character: " + scannedCharacter);
        } else if (mTrackableBehaviour.TrackableName.ToString() == BANDIT && foundCondition) {
            scannedCharacter = BANDIT;
            print("Scanned character: " + scannedCharacter);
        }

        return scannedCharacter;
    }

    //public string characterLostChecks(string scannedCharacter, string unasignedCharacter, TrackableBehaviour mTrackableBehaviour)
    //{
    //    bool lostCondition = mTrackableBehaviour.CurrentStatus.ToString() == "NO_POSE" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "UNKNOWN";

    //    //if ((mTrackableBehaviour.TrackableName.ToString() == SHERIFF && lostCondition) ||
    //    //    (mTrackableBehaviour.TrackableName.ToString() == ADJUNCT && lostCondition) ||
    //    //    (mTrackableBehaviour.TrackableName.ToString() == RENEGATE && lostCondition) ||
    //    //    (mTrackableBehaviour.TrackableName.ToString() == BANDIT && lostCondition)) {
    //    //    scannedCharacter = unasignedCharacter;
    //    //}
    //    return scannedCharacter;
    //}
}
