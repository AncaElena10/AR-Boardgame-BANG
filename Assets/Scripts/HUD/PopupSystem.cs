using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;
using Vuforia;
using System.Linq;
using System;

public class PopupSystem : MonoBehaviour
{
    public Canvas canvas;
    public bool displayPopup = false;

    [SerializeField]
    private GameObject roseObject;

    [SerializeField]
    private GameObject paulObject;

    [SerializeField]
    private GameObject bartObject;

    [SerializeField]
    private GameObject gringoObject;

    [SerializeField]
    private GameObject willyObject;

    public void PopupSpecialCharacterInfoCanvas()
    {
        if (!displayPopup) {
            displayPopup = true;
            canvas.enabled = true;
            checkSpecialCharacter();
        } else {
            displayPopup = false;
            canvas.enabled = false;
        }
    }

    private void checkSpecialCharacter()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("SpecialCharacter")) {
            switch((string)PhotonNetwork.LocalPlayer.CustomProperties["SpecialCharacter"]) {
                case "Rose":
                    roseObject.SetActive(true);
                    break;
                case "Paul":
                    paulObject.SetActive(true);
                    break;
                case "Bart":
                    bartObject.SetActive(true);
                    break;
                case "Gringo":
                    gringoObject.SetActive(true);
                    break;
                case "Willy":
                    willyObject.SetActive(true);
                    break;
            }
        }
    }
}
