using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class SecretCharacter : MonoBehaviour
{
    public TextMeshProUGUI _HUDtext;

    // Start is called before the first frame update
    void Start()
    {
        _HUDtext = FindObjectOfType<TextMeshProUGUI>();
    }

    public void setCharacterHUD(string result)
    {
        if (_HUDtext.tag == "ScannedCharacter") {
            _HUDtext.text += "\n" + result;
        }
    }
}
