using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using Vuforia;
using System.Linq;
using System;

public class Gringo : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

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
        livesNumber = 3;
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerLivesNumber();
    }

    public void GringoFound(TrackableBehaviour mTrackableBehaviour)
    {
        bool condition = mTrackableBehaviour.TrackableName.ToString() == CustomEnums.Characters.EL_GRINGO.ToString() && mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";
        if (condition) {
            // set custom property ["PlayerLives"]=number of lives
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character") && !PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("SpecialCharacter")) {
                if ((string)PhotonNetwork.LocalPlayer.CustomProperties["Character"] == CustomEnums.CharacterCards.SERIF.ToString()) {
                    print("[Gringo] Player SERIF. Total number of lives = lives + 1");
                    livesNumber = livesNumber + 1;

                    _4lives.SetActive(true);
                }
                else {
                    print("[Gringo] Player not SERIF.");

                    _3lives.SetActive(true);
                }

                PunHashtable customProperties = new PunHashtable();

                customProperties.Add("SpecialCharacter", "Gringo");
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

                customProperties.Add("PlayerLives", livesNumber);
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);

                customProperties.Add("PlayerMaxLives", livesNumber);
                PhotonNetwork.LocalPlayer.SetCustomProperties(customProperties);
            }
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null) {
            if (changedProps.ContainsKey("SpecialCharacter")) {
                print("[Gringo] SpecialCharacter custom property has changed! Player assigned to Gringo.");
            }

            if (changedProps.ContainsKey("PlayerLives")) {
                print("[Gringo] PlayerLives custom property has changed!");

                // send the number of lives to HUD
                _playerLives.increaseLives(livesNumber, "Bullets");
            }
        }
    }

    public void checkPlayerLivesNumber()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerLives")) {
            int playerLivesCustomProp = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
            switch (playerLivesCustomProp) {
                case 4:
                    _4lives.SetActive(true);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;

                case 3:
                    _4lives.SetActive(false);
                    _3lives.SetActive(true);
                    _2lives.SetActive(false);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;
                case 2:
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(true);
                    _1lives.SetActive(false);
                    _0lives.SetActive(false);
                    break;
                case 1:
                    _4lives.SetActive(false);
                    _3lives.SetActive(false);
                    _2lives.SetActive(false);
                    _1lives.SetActive(true);
                    _0lives.SetActive(false);
                    break;
                case 0:
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
