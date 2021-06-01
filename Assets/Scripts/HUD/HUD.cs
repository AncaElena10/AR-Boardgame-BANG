using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class HUD : MonoBehaviourPunCallbacks
{
    //[SerializeField]
    //public GameObject _hud;

    [SerializeField]
    public GameObject endTurnButton;

    [SerializeField]
    private GameObject _playersToAttack_Panel;

    [SerializeField]
    private GameObject _attack_Text;

    [SerializeField]
    private GameObject _currentTarget_Text;

    [SerializeField]
    private GameObject _canAttack_Text;

    public Player Player { get; private set; }

    //public SecretCharacter _secretChar = new SecretCharacter();
    //public PlayerLives _playerLives = new PlayerLives();

    public void SaveCustomPropertiesAfterCharacterScan()
    {
        PunHashtable _myCustomProperties = new PunHashtable();
        _myCustomProperties.Add("Character", MyDefaultTrackableEventHandler.scannedCharacter);

        // set the properties related to Turn
        SetPlayerTurnProperty(MyDefaultTrackableEventHandler.scannedCharacter, _myCustomProperties);

        PhotonNetwork.LocalPlayer.SetCustomProperties(_myCustomProperties);
        
        if (ScanManager.characterSaved) {
            //string result = "N/A";
            //foreach (Player player in PhotonNetwork.PlayerList) {
            //    if (player.CustomProperties.ContainsKey("Character")) {
            //        result = (string)player.CustomProperties["Character"];
            //    }
            //}
            //result = MyDefaultTrackableEventHandler.scannedCharacter.ToString();

            //_hud.SetActive(true);
            //_secretChar.setCharacterHUD(MyDefaultTrackableEventHandler.scannedCharacter.ToString());

            //_playerLives.setLivesHUD(0);
        }
    }

    private void SetPlayerTurnProperty(string secretCharacter, PunHashtable punHt)
    {
        // assign a Turn property - true for sheriff, false for the rest
        // SHERIFF will always be the first player to start the game
        if (secretCharacter == CustomEnums.CharacterCards.SERIF.ToString()) {
            //print("DA?!??!");
            //punHt.Add("TurnNumber", 1);
            punHt.Add("Turn", true);
        } else {
            //int randomValue = Random.Range(2, 9999);
            //punHt.Add("TurnNumber", randomValue);
            punHt.Add("Turn", false);
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);

        //print("[OnPlayerPropertiesUpdate] YO Y OY OYO from HUD.");
        if (targetPlayer != null && targetPlayer == PhotonNetwork.LocalPlayer) {
            //print("[OnPlayerPropertiesUpdate] AHOI YO YO from HUD.");
            if (changedProps.ContainsKey("Turn")) {
                print("[HUD] Turn custom property has changed!");
                if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Turn"]) {
                    print("[HUD] You may start the game since you are the Sheriff.");
                    endTurnButton.SetActive(true);
                    _playersToAttack_Panel.SetActive(true);
                    _attack_Text.SetActive(true);
                    _currentTarget_Text.SetActive(true);
                    _canAttack_Text.SetActive(true);
                } else {
                    print("[HUD] You can't start the game since you are not the Sheriff. Wait for your turn. ");
                    endTurnButton.SetActive(false);
                    _playersToAttack_Panel.SetActive(false);
                    _attack_Text.SetActive(false);
                    _currentTarget_Text.SetActive(false);
                    _canAttack_Text.SetActive(false);
                }
            }
        }
    }
}
