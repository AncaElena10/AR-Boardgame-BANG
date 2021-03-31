using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerLives : MonoBehaviourPunCallbacks
{
    //[SerializeField]
    public TextMeshProUGUI _HUDtext;
    //public TextMeshPro _HUDtext;

    //private GameManager gm = new GameManager();

    public Player Player { get; private set; }

    private int lives = 0;
    private int maxLives = 0;

    //private bool enteredOnBeerCardFunction = false;
    //private bool enteredOnSaloomCardFunction = false;

    // Start is called before the first frame update
    void Start()
    {
        //_HUDtext = FindObjectOfType<TextMeshProUGUI>();
        //_HUDtext = GetComponent<TextMeshPro>();
    }

    private void Awake()
    {
        _HUDtext.text = "LIVES: " + lives.ToString();
    }

    public void setLivesHUDAtFirst(int result)
    {
        PunHashtable _myCustomProperties = new PunHashtable();

        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        //{
        //    print("88888888888888888888888");
        //}

        //print("AJUNGE+++++++: " + result);
        //print("AJUNGE+++++++----- " + _HUDtext.tag);

        // set the max number of lives a player can have
        if (result > maxLives) {
            maxLives = result;
        }

        if (_HUDtext.tag == "Lives") {
            //print("DAAA?!?!??!?!1/?!?!");
            _HUDtext.text = "LIVES: " + result.ToString();
        }

        // set to players the custom property that contains the number of lives & the number of max lives
        _myCustomProperties.Add("PlayerLives", result);
        _myCustomProperties.Add("PlayerMaxLives", maxLives);
        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);

        // check if the life custom property works
        //int result1 = 0;
        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    if (player.CustomProperties.ContainsKey("PlayerLives"))
        //    {
        //        result1 = (int)player.CustomProperties["PlayerLives"];

        //    }
        //}
        //print("ASDHFKASDGFLAKSGDLKSADGKJASDFLJASFDF +++++++++++++++++++++ " + result1);

        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Character"))
        //{
        //    print("88888888888888888888888");
        //}
    }

    //public void setLivesHUDOnUpdates_Beer()
    //{
    //    PunHashtable _myCustomProperties = new PunHashtable();

    //    //print("AHOI?");
    //    //PhotonView photonView = PhotonView.Get(this);

    //    //print("AHOI2?");
    //    foreach (Player player in PhotonNetwork.PlayerList) {
    //        //print("AHOI3?");
    //        if (player.CustomProperties.ContainsKey("PlayerLives") && player.IsLocal) {
    //            print("xxxxxxxxxxxxxxxxxxxxxx1111111");
    //            int playerLives = (int)player.CustomProperties["PlayerLives"];
    //            int maxPlayerLives = (int)player.CustomProperties["PlayerMaxLives"];

    //            //print("AHOI11111 " + playerLives);
    //            //print("AHOI22222 " + maxPlayerLives);
    //            //if (playerLives < maxPlayerLives) {
    //                playerLives++;
    //                _myCustomProperties["PlayerLives"] = playerLives;
    //                player.CustomProperties = _myCustomProperties;
    //                if (_HUDtext.tag == "Lives") {
    //                    //print("DAAA?!?!??!?!1/?!?!");
    //                    _HUDtext.text = "LIVES: " + playerLives.ToString();
    //                }
    //                //photonView.Owner.SetCustomProperties(_myCustomProperties);
    //            //} else {
    //            //    print("Cannot have move lives. You are already at maximum: " + maxPlayerLives);
    //            //}
    //        }
    //    }
    //}

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        //print("YO Y OY OYO");
        if (targetPlayer != null && targetPlayer == PhotonNetwork.LocalPlayer) {
            //print("AHOI YO YO");
            if (changedProps.ContainsKey("PlayerLives")) {
                print("[OnPlayerPropertiesUpdate] AHOY YO YO YO YO from PlayerLives.");
                //if (enteredOnSaloomCardFunction) {
                setLivesHUDOnUpdates(targetPlayer);
                //} else if (enteredOnBeerCardFunction) {

                //}
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

            if (_HUDtext.tag == "Lives") {
                _HUDtext.text = "LIVES: " + result.ToString();
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
            print("Cannot have move lives. You are already at maximum: " + maxPlayerLives);
        }

        //print("AHOI4---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI5---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI6---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);
    }

    //[PunRPC]
    public void increasePlayerLives_Saloon()
    {
        //int playerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
        //playerLives++;
        //PunHashtable _myCustomProperties = new PunHashtable();
        //_myCustomProperties["PlayerLives"] = playerLives;
        //PhotonNetwork.SetPlayerCustomProperties(_myCustomProperties);

        //setLivesHUDOnUpdates_Saloon(PhotonNetwork.LocalPlayer);

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
                    print("Cannot have move lives. Player already at maximum: " + maxPlayerLives);
                }

            }
        }

        //print("YO YO OY OY OYYO OYO OY OYO " + playerLives);


        //PunHashtable _myCustomProperties = new PunHashtable();
        //int x = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
        ////_myCustomProperties["PlayerLives"] = null;
        //_myCustomProperties.Remove("PlayerLives");
        //_myCustomProperties.Add("PlayerLives", 5);
        //PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);

        ////PunHashtable _myCustomProperties = new PunHashtable();
        ////gm.OnPlayerPropertiesUpdate(PhotonNetwork.LocalPlayer, _myCustomProperties);

        //if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("PlayerLives"))
        //{
        //    print("AHOI PLS?");
        //    int toDisplay = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];

        //    print("AHOI PLS " + toDisplay);

        //    if (_HUDtext.tag == "Lives")
        //    {
        //        _HUDtext.text = "LIVES: " + toDisplay.ToString(); // de verificat daca se face broadcast si la ceilalti playeri
        //    }
        //}

        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    print("AHOI3---- " + player.NickName);
        //    print("AHOI4---- " + (int)player.CustomProperties["PlayerLives"]);
        //    print("AHOI5---- " + (string)player.CustomProperties["Character"]);
        //}

        //foreach (Player player in PhotonNetwork.PlayerList) {
        //    //print("AHOI3?");
        //    if (player.CustomProperties.ContainsKey("PlayerLives")) {
        //        print("AHOI1---- " + player.NickName);
        //        print("AHOI2---- " + (int)player.CustomProperties["PlayerLives"]);
        //        print("AHOI3---- " + (string)player.CustomProperties["Character"]);

        //        int playerLives = (int)player.CustomProperties["PlayerLives"];
        //        int maxPlayerLives = (int)player.CustomProperties["PlayerMaxLives"];
        //        //if (playerLives < maxPlayerLives) {
        //        playerLives++;

        //        print("ahoi ahoi " + playerLives);

        //        _myCustomProperties["PlayerLives"] = playerLives;
        //        _myCustomProperties["PlayerLives2"] = playerLives;
        //        player.SetCustomProperties(_myCustomProperties);

        //        //player.CustomProperties["PlayerLives"] = _myCustomProperties;

        //        //_myCustomProperties.Add("PlayerLives", playerLives);


        //        if (_HUDtext.tag == "Lives") {
        //            _HUDtext.text = "LIVES: " + playerLives.ToString(); // de verificat daca se face broadcast si la ceilalti playeri
        //        }
        //        //photonView.Owner.SetCustomProperties(_myCustomProperties);
        //        //} else {
        //        //    print("Cannot have move lives. Player already at maximum: " + maxPlayerLives);
        //        //}

        //        print("AHOI3---- " + player.NickName);
        //        print("AHOI4---- " + (int)player.CustomProperties["PlayerLives"]);
        //        print("AHOI42---- " + (int)player.CustomProperties["PlayerLives2"]);
        //        print("AHOI5---- " + (string)player.CustomProperties["Character"]);
        //    }
        //}

        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    if (player.CustomProperties.ContainsKey("PlayerLives")) {
        //        print("XXXXXXXXXXXXX22222 " + player.NickName);
        //        print("XXX " + (int)player.CustomProperties["PlayerLives"]);
        //    }
        //}

        //PunHashtable _myCustomProperties = new PunHashtable();
        //int playerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"];
        //int maxPlayerLives = (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerMaxLives"];
        //print("AHOI1---- " + playerLives);
        //print("AHOI2---- " + maxPlayerLives);
        //playerLives++;
        //_myCustomProperties["PlayerLives"] = playerLives;
        //PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
        //if (_HUDtext.tag == "Lives")
        //{
        //    _HUDtext.text = "LIVES: " + playerLives.ToString(); // de verificat daca se face broadcast si la ceilalti playeri
        //}

        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    print("AHOI3---- " + player.NickName);
        //    print("AHOI4---- " + (int)player.CustomProperties["PlayerLives"]);
        //    print("AHOI5---- " + (string)player.CustomProperties["Character"]);
        //}
    }

    public void decreasePlayerLives_Beer()
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

        if (playerLives ==  0) {
            print("This is the end. You lost.");
        }

        //print("AHOI4---- " + PhotonNetwork.LocalPlayer.NickName);
        //print("AHOI5---- " + (int)PhotonNetwork.LocalPlayer.CustomProperties["PlayerLives"]);
        //print("AHOI6---- " + (string)PhotonNetwork.LocalPlayer.CustomProperties["Character"]);
    }

    public void increaseLives(int visitedLife, string fromWhere)
    {
        //if (visitedLife > 1)
        //{
        //    bullet_life_1_visited = true;
        //}

        //print("GAGAGGAGAAGAG");

         // is visitedLife is > 1, this means that this life was visited multiple times  
        if (fromWhere == "Bullets") {
            if (visitedLife < 2) {
                lives++;
                //print("GAGAGAGAGGA ----------- " + lives);
            }
            setLivesHUDAtFirst(lives);
        }

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
            decreasePlayerLives_Beer();
        }
    }
}
