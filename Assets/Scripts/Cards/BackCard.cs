using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Vuforia;

public class BackCard : MonoBehaviour
{
    [SerializeField]
    GameObject sheriffModelToDisplay;

    [SerializeField]
    GameObject adjunctModelToDisplay;

    [SerializeField]
    GameObject renegateModelToDisplay;

    [SerializeField]
    GameObject banditModelToDisplay;

    private static string SHERIFF = CustomEnums.CharacterCards.SERIF.ToString();
    private static string ADJUNCT = CustomEnums.CharacterCards.ADJUNCT.ToString();
    private static string RENEGATE = CustomEnums.CharacterCards.RENEGAT.ToString();
    private static string BANDIT = CustomEnums.CharacterCards.BANDIT.ToString();

    public void backCharacterCardFound(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Stuff.BACK_CHAR.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL") {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character")) {
                if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == SHERIFF) {
                    print("[BackCard] Secret card: Sheriff displayed.");
                    sheriffModelToDisplay.SetActive(true);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == ADJUNCT) {
                    print("[BackCard] Secret card: Adjunct displayed.");
                    adjunctModelToDisplay.SetActive(true);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == RENEGATE) {
                    print("[BackCard] Secret card: Renegate displayed.");
                    renegateModelToDisplay.SetActive(true);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == BANDIT) {
                    print("[BackCard] Secret card: Bandit displayed.");
                    banditModelToDisplay.SetActive(true);
                }
            }
        }

        //print("AHOI!00000AEFFA");

        //print("AHOI!00000");
        //print("OOOOOOOOOOOOOOOOOOOOO " + scannedCharacter.ToString());
        //if (MyDefaultTrackableEventHandler.scannedCharacter.ToString() == CustomEnums.CharacterCards.SERIF.ToString()) {
        //    print("DAAAAAAAAAAA SERIFFFFFFFFFFFFFFF");
        //    sheriffModelToDisplay.SetActive(true);
        //}

        //foreach (Player player in PhotonNetwork.PlayerList) {
        //    print("AHOI!111");
        //    if (player.CustomProperties.ContainsKey("Character")) {
        //        print("AHOI!2222");
        //        if (player.CustomProperties["Character"].ToString() == CustomEnums.CharacterCards.SERIF.ToString()) {
        //            print("Secret card: Sheriff displayed.");
        //            sheriffModelToDisplay.SetActive(true);
        //        } else if (player.CustomProperties["Character"].ToString() == CustomEnums.CharacterCards.ADJUNCT.ToString()) {
        //            print("Secret card: Adjunct displayed.");
        //            adjunctModelToDisplay.SetActive(true);
        //        } else if (player.CustomProperties["Character"].ToString() == CustomEnums.CharacterCards.RENEGAT.ToString()) {
        //            print("Secret card: Renegate displayed.");
        //            renegateModelToDisplay.SetActive(true);
        //        } else if (player.CustomProperties["Character"].ToString() == CustomEnums.CharacterCards.BANDIT.ToString()) {
        //            print("Secret card: Bandit displayed.");
        //            banditModelToDisplay.SetActive(true);
        //        }
        //    }
        //}

        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerLives"))
        //{
        //    print("JJJJJJJJJJJJ " + PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"].ToString());
        //}
        //    print("AHOI!111");


        //}
    }

    public void backCharacterCardLost(TrackableBehaviour mTrackableBehaviour)
    {
        if (mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Stuff.BACK_CHAR.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "NO_POSE" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "UNKNOWN") {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character")) {
                if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == SHERIFF) {
                    print("[BackCard] Secret card: Sheriff not displayed.");
                    sheriffModelToDisplay.SetActive(false);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == ADJUNCT) {
                    print("[BackCard] Secret card: Adjunct not displayed.");
                    adjunctModelToDisplay.SetActive(false);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == RENEGATE) {
                    print("[BackCard] Secret card: Renegate not displayed.");
                    renegateModelToDisplay.SetActive(false);
                } else if (PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString() == BANDIT) {
                    print("[BackCard] Secret card: Bandit not displayed.");
                    banditModelToDisplay.SetActive(false);
                }
            }
        }
    }
}
