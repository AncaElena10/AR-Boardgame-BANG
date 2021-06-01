using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
using System.Linq;
using System;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class UnderAttack : MonoBehaviourPunCallbacks
{
    //string playerToAttack = "";
    //public Text _HUDtext_underAttack;
    public Text _HUDtext_countdown;
    public float timeRemaining = 10;
    public GameObject playRatatButton;

    private bool displayHUD = false;
    private bool playerHitRatat = false;

    [SerializeField]
    GameObject _go;

    public PlayerLives _playerLives = new PlayerLives();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        //_HUDtext_underAttack.text = "";
        _HUDtext_countdown.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        //checkIfYouAreUnderAttack();
        if (displayHUD) {
            countdown();
        }
    }

    void countdown()
    {
        if (timeRemaining > 0) {
            //print("111111111111");
            timeRemaining -= Time.deltaTime;
            float seconds = Mathf.FloorToInt(timeRemaining % 60);
            enableCountdownText(seconds);
        }
        else {
            //print("2222222222222");
            //print("Time has run out!");
            disableCountdownText();
            playRatatButton.SetActive(false);
            disableUnderAttackText();

            timeRemaining = 10;
            displayHUD = false;

            // player will lose a life
            _playerLives.decreaseLives("Bang");
        }

        // verifica daca nu a apasat ratat si au trecut cele 10 secunde - pune textul pe fals si modifica room options
        // la fel si pentru buton
        // merge doar atat?
        if (playerHitRatat) {
            //print("33333333333333");
            //timeRemaining = -1;
            disableCountdownText();
            playRatatButton.SetActive(false);
            disableUnderAttackText();

            timeRemaining = 10;
            displayHUD = false;
            playerHitRatat = false;
        }
    }

    public override void OnRoomPropertiesUpdate(PunHashtable propertiesThatChanged)
    {
        //base.OnRoomPropertiesUpdate(propertiesThatChanged);
        if (propertiesThatChanged.ContainsKey("PlayerToAttack")) {
            print("[UnderAttack] Room PlayerToAttack custom property has changed!");
            
            string playerToAttack = (string)PhotonNetwork.CurrentRoom.CustomProperties["PlayerToAttack"];

            if (playerToAttack == PhotonNetwork.LocalPlayer.NickName) {
                print("[UnderAttack] Oh no, I am under attack!");

                enableUnderAttackText();
                playRatatButton.SetActive(true);

                displayHUD = true;
            }
            else {
                disableUnderAttackText();
                disableCountdownText();
                playRatatButton.SetActive(false);

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
        //_HUDtext_underAttack.enabled = true;
        //_HUDtext_underAttack.gameObject.SetActive(true);
        //_HUDtext_underAttack.text = "You are under attack";
        _go.SetActive(true);
    }

    void disableUnderAttackText()
    {
        //_HUDtext_underAttack.enabled = false;
        //_HUDtext_underAttack.gameObject.SetActive(false);
        //_HUDtext_underAttack.text = "";
        _go.SetActive(false);
    }

    public void OnMouseDown()
    {
        //print("DA!");
        playRatatButton.SetActive(false);
        playerHitRatat = true;
    }
}
