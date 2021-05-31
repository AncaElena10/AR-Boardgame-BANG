using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class IndieniAttack : MonoBehaviourPunCallbacks
{
    public Text _HUDtext_playBangCard;
    public Text _HUDtext_countdown;
    public float timeRemaining = 10;
    public GameObject playBangButton;

    private bool playerHitBang = false;
    private bool indieniPlayed = false;

    private bool displayHUD = false;

    public PlayerLives _playerLives = new PlayerLives();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        _HUDtext_playBangCard.text = "";
        _HUDtext_countdown.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //print("AAAAAAAAAAAAAAA " + PhotonNetwork.LocalPlayer.CustomProperties["Turn"]);
        // indieni only applies to the other players
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Turn")) {
            //print("DA?!?!?!?!?!11111111111111111");
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"] == false) {
                if (displayHUD) {
                    countdown();
                }
            }
        }
    }

    void countdown()
    {
        if (timeRemaining > 0) {
            timeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            enableCountdownText(seconds);
        }
        else {
            print("OOOOOOOOOOOOOO11111111");
            disableCountdownText();
            playBangButton.SetActive(false);
            disableUnderAttackText();

            timeRemaining = 10;
            displayHUD = false;

            // player will lose a life
            _playerLives.decreaseLives("Bang");
        }

        if (playerHitBang) {
            print("OOOOOOOOOOO222222");
            disableCountdownText();
            playBangButton.SetActive(false);
            disableUnderAttackText();

            timeRemaining = 10;
            displayHUD = false;
            playerHitBang = false;
        }
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("IndieniPlayed")) {
            print("[IndieniAttack] Oh no, I am under attack, I need to play a BANG card!");

            if ((bool)PhotonNetwork.CurrentRoom.CustomProperties["IndieniPlayed"] == true) {
                print("[UnderAttack] Oh no, I am under attack!");

                // indieni only applies to the other players
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Turn")) {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"] == false) {
                        enableUnderAttackText();
                        playBangButton.SetActive(true);
                        displayHUD = true;
                    }
                }
            } else {
                disableUnderAttackText();
                disableCountdownText();
                playBangButton.SetActive(false);

                displayHUD = false;
            }
        }
    }

    void enableCountdownText(float seconds)
    {
        _HUDtext_countdown.enabled = true;
        _HUDtext_countdown.gameObject.SetActive(true);
        _HUDtext_countdown.text = seconds + "s";
    }

    void disableCountdownText()
    {
        _HUDtext_countdown.enabled = false;
        _HUDtext_countdown.gameObject.SetActive(false);
        _HUDtext_countdown.text = "";
    }

    void enableUnderAttackText()
    {
        _HUDtext_playBangCard.enabled = true;
        _HUDtext_playBangCard.gameObject.SetActive(true);
        _HUDtext_playBangCard.text = "INDIENI! Discard a BANG! card or lose one life point";
    }

    void disableUnderAttackText()
    {
        _HUDtext_playBangCard.enabled = false;
        _HUDtext_playBangCard.gameObject.SetActive(false);
        _HUDtext_playBangCard.text = "";
    }

    public void OnMouseDown()
    {
        print("DA!");
        playBangButton.SetActive(false);
        playerHitBang = true;
    }
}
