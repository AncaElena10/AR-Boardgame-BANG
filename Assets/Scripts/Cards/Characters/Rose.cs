using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using Vuforia;
using System.Linq;
using System;

public class Rose : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject _5lives;

    [SerializeField]
    private GameObject _4lives;

    [SerializeField]
    private GameObject _3lives;

    [SerializeField]
    private GameObject _2lives;

    [SerializeField]
    private GameObject _1lives;

    [SerializeField]
    private GameObject _0lives;

    int livesNumber;

    public PlayerLives _playerLives = new PlayerLives();

    void Start()
    {
        livesNumber = 4;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerLivesNumber();
    }

    public void RoseFound(TrackableBehaviour mTrackableBehaviour)
    {
        bool condition = mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Characters.ROSE_DOOLAN.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";
        if (condition) {
            // set custom property ["PlayerLives"]=number of lives
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character") && !PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("SpecialCharacter")) {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == CustomEnums.CharacterCards.SERIF.ToString()) {
                    print("[Rose] Player SERIF. Total number of lives = lives + 1");
                    livesNumber = livesNumber + 1;

                    _5lives.SetActive(true);
                } else {
                    print("[Rose] Player not SERIF.");

                    _4lives.SetActive(true);
                }

                PunHashtable customProperties = new PunHashtable();

                customProperties.Add("SpecialCharacter", "Rose");
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

                customProperties.Add("PlayerLives", livesNumber);
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

                customProperties.Add("PlayerMaxLives", livesNumber);
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

                // special power - Luneta
                customProperties.Add("Luneta", true);
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null) {
            if (changedProps.ContainsKey("SpecialCharacter")) {
                print("[Rose] SpecialCharacter custom property has changed! Player assigned to Rose.");
            }

            if (changedProps.ContainsKey("PlayerLives")) {
                print("[Rose] PlayerLives custom property has changed!");

                // send the number of lives to HUD
                //_playerLives.increaseLives(livesNumber, "Bullets");
            }

            if (changedProps.ContainsKey("Luneta")) {
                print("[Rose] Luneta custom property has changed to true!");
            }
        }
    }

    public void checkPlayerLivesNumber()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerLives")) {
            int playerLivesCustomProp = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
            switch(playerLivesCustomProp) {
                case 5:
                    _5lives.SetActive(true);
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;
                case 4:
                    _5lives.SetActive(false);
                    _4lives.SetActive(true);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;

                case 3:
                    _5lives.SetActive(false);
                    _4lives.SetActive(false);
                    _3lives.SetActive(true);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;
                case 2:
                    _5lives.SetActive(false);
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(true);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;
                case 1:
                    _5lives.SetActive(false);
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(true);
                    _0lives.SetActive(false);
                    break;
                case 0:
                    _5lives.SetActive(false);
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(true);
                    break;
            }
        }
    }
}
