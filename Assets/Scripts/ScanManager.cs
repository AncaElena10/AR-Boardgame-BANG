using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class ScanManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    GameObject scanPanel; // ar camera with scan panel

    [SerializeField]
    GameObject scanMenuPanel; // the panel with button SCAN

    [SerializeField]
    GameObject scanningPanel; // the object that contains both panels

    //private List<PlayerListing> _listings = new List<PlayerListing>();
    public static bool characterSaved = false;

    public HUD _hudManager = new HUD();

    public void SaveCharacter()
    {
        print("[ScanManager] Character Saved! You've been assigned to " + MyDefaultTrackableEventHandler.scannedCharacter);

        //foreach (PlayerListing player in _listings)
        //{
        //    print("KKKKKKKKKKKKK " + player.Player.NickName);
        //}

        //foreach (Player player in PhotonNetwork.PlayerList)
        //{
        //    print("KKKKKKKKKKKKK " + player.NickName);
        //}

        //print("OOOOOOOOOOOOOOO");

        characterSaved = true;
        scanPanel.SetActive(false);
        scanMenuPanel.SetActive(false);
        scanningPanel.SetActive(false);

        _hudManager.SaveCustomPropertiesAfterCharacterScan();

        //PhotonNetwork.LoadLevel(3);
        //// custom property with default value
        //string result = "N/A"; // default value
        //if (player.CustomProperties.ContainsKey("Character"))
        //{
        //    result = (string)player.CustomProperties["Character"];
        //}
        //_text.text = result.ToString() + ", " + player.NickName;

        //_listings.FindIndex(x => {
        //    string result = "N/A"; // default value
        //    if (x.Player.CustomProperties.ContainsKey("Character"))
        //    {
        //        result = (string)x.Player.CustomProperties["Character"];
        //    }
        //    //return result;
        //});
    }

    public void OnClick_Button()
    {
        SaveCharacter();
    }
}
