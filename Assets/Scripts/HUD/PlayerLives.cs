using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerLives : MonoBehaviourPunCallbacks
{
    //[SerializeField]
   // public TextMeshProUGUI _HUDtextLives;
    //public TextMeshPro _HUDtext;

    public Text _HUDtextLives;
    public Text _HUDtext_gameOver;
    public Text _HUDtext_dontForget_Bart;
    public Text _HUDtext_dontForget_Gringo;

    //private GameManager gm = new GameManager();

    public Player Player { get; private set; }

    private int lives = 0;
    //private int maxLives = 0;

    //private bool enteredOnBeerCardFunction = false;
    //private bool enteredOnSaloomCardFunction = false;

    // Start is called before the first frame update
    void Start()
    {
        //_HUDtext = FindObjectOfType<TextMeshProUGUI>();
        //_HUDtext = GetComponent<TextMeshPro>();
    }

    void Awake()
    {
        _HUDtextLives.text = "LIVES: " + lives.ToString();
        _HUDtext_gameOver.text = "";
        _HUDtext_dontForget_Bart.text = "";
        _HUDtext_dontForget_Gringo.text = "";
    }

    //public void setLivesHUDAtFirst(int result)
    //{
    //    print("hhhhhhhhhhh " + result);
    //    if (_HUDtextLives.tag == "Lives") {
    //        print("DAAAAAAAAAAAAAA!??!?!?!?");
    //        _HUDtextLives.text = "LIVES: " + result.ToString();
    //    }
    //}

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        //print("YO Y OY OYO");
        if (targetPlayer != null && targetPlayer == PhotonNetwork.LocalPlayer) {
            if (changedProps.ContainsKey("PlayerLives")) {
                print("[PlayerLives] PlayerLives custom property has changed!");
                setLivesHUDOnUpdates(targetPlayer);
            }
        }
    }

    public void setLivesHUDOnUpdates(Player player)
    {
        //enteredOnSaloomCardFunction = true;
        int result;
        if (player.CustomProperties.ContainsKey("PlayerLives")) {
            //print("DAAAAAAAAAAAAAAAA? YOYOOY");
            result = (int)player.CustomProperties["PlayerLives"];

            if (_HUDtextLives.tag == "Lives") {
                _HUDtextLives.text = "LIVES: " + result.ToString();
            }
        }
        //enteredOnSaloomCardFunction = false;
    }

    public void increasePlayerLives_Beer()
    {
        //print("AHOI1---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI2---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI3---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);

        int playerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
        int maxPlayerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerMaxLives"];
        if (playerLives < maxPlayerLives) {
            playerLives++;
            PunHashtable _myCustomProperties = new PunHashtable();
            _myCustomProperties["PlayerLives"] = playerLives;
            PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);

            setLivesHUDOnUpdates(PhotonNetwork.LocalPlayer);
        } else {
            print("[PlayerLives] Cannot have move lives. You are already at maximum: " + maxPlayerLives);
        }

        //print("AHOI4---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI5---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI6---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);
    }

    //[PunRPC]
    public void increasePlayerLives_Saloon()
    {
        foreach (Player player in PhotonNetwork.PlayerList) {
            if (player.CustomProperties.ContainsKey("PlayerLives")) {
                //print("AHOI1---- " + player.NickName);
                //print("AHOI2---- " + (int)player.CustomProperties["PlayerLives"]);
                //print("AHOI3---- " + (string)player.CustomProperties["Character"]);

                int playerLives = (int)player.CustomProperties["PlayerLives"];
                int maxPlayerLives = (int)player.CustomProperties["PlayerMaxLives"];

                if (playerLives < maxPlayerLives) {
                    playerLives++;
                    PunHashtable _myCustomProperties = new PunHashtable();
                    _myCustomProperties["PlayerLives"] = playerLives;
                    //PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);
                    player.SetCustomProperties(_myCustomProperties);

                    setLivesHUDOnUpdates(player);

                    //print("AHOI4---- " + player.NickName);
                    //print("AHOI5---- " + (int)player.CustomProperties["PlayerLives"]);
                    //print("AHOI6---- " + (string)player.CustomProperties["Character"]);

                    //print("AHOI1---- " + (int)player.CustomProperties["TurnNumber"]);
                    //print("AHOI2---- " + (bool)player.CustomProperties["Turn"]);

                    // print("TTTTTTTTTT------- " + player.ActorNumber + " " + player.CustomProperties["Turn"]);
                } else {
                    print("[PlayerLives] Cannot have move lives. Player already at maximum: " + maxPlayerLives);
                }
            }
        }
    }

    public void decreasePlayerLives_Bang()
    {
        //print("AHOI1---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI2---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI3---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);

        int playerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
        playerLives--;
        PunHashtable _myCustomProperties = new PunHashtable();
        _myCustomProperties["PlayerLives"] = playerLives;
        PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);

        setLivesHUDOnUpdates(PhotonNetwork.LocalPlayer);

        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("SpecialCharacter")) {
            switch ((string)PhotonNetwork.LocalPlayer.CustomProperties["SpecialCharacter"]) {
                case "Bart":
                    StartCoroutine(DisplayBartMessage(5));
                    break;
                case "Gringo":
                    StartCoroutine(DisplayGringoMessage(5));
                    break;
            }
        }

        if (playerLives ==  0) {
            print("[PlayerLives] This is the end. You lost.");

            // TODO - adauga un mesaj pe HUD + ce personaj avea
            _HUDtext_gameOver.text = "GAME OVER! Role: " + PhotonNetwork.LocalPlayer.CustomProperties["Character"].ToString();
        }

        //print("AHOI4---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI5---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI6---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);
    }

    IEnumerator DisplayBartMessage(float delay)
    {
        _HUDtext_dontForget_Bart.text = "You just lost a life point. Don't forget to draw a card from the deck";
        yield return new WaitForSeconds(delay);
        _HUDtext_dontForget_Bart.text = "";
    }

    IEnumerator DisplayGringoMessage(float delay)
    {
        _HUDtext_dontForget_Gringo.text = "You just lost a life point. Don't forget to draw a random card from the player who attacked you";
        yield return new WaitForSeconds(delay);
        _HUDtext_dontForget_Gringo.text = "Y";
    }

    public void increaseLives(int lives, string fromWhere)
    {
        //if (fromWhere == "Bullets") {
        //    setLivesHUDAtFirst(lives);
        //}

        if (fromWhere == "Beer") {
            increasePlayerLives_Beer();
        }

        if (fromWhere == "Saloon") {
            increasePlayerLives_Saloon();
        }
    }

    public void decreaseLives(string fromWhere)
    {
        if (fromWhere == "Bang") {
            decreasePlayerLives_Bang();
        }
    }
}
