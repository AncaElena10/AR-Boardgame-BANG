using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class PlayerNamePopup : MonoBehaviour
{
    public Canvas canvas;
    public bool displayPopup = false;
    public GameObject inputField;

    public void PopupChangePlayerNameCanvas()
    {
        print("DAAAA");
        if (!displayPopup) {
            displayPopup = true;
            canvas.enabled = true;
        }
        else {
            displayPopup = false;
            canvas.enabled = false;
        }
    }

    public void ChangePlayerName()
    {
        string playerName = inputField.GetComponent<TMP_InputField>().text;
        PhotonNetwork.LocalPlayer.NickName = playerName;

        displayPopup = false;
        canvas.enabled = false;
    }
}
