using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class CurrentTarget : MonoBehaviour
{
    //public TextMeshProUGUI _HUDtext;

    public Text _HUDtext;

    private void Awake()
    {
        _HUDtext.text = "NO ACTIVE TARGET";
    }

    public void Update()
    {
        if (PlayerListing.player_to_attack != "") {
            _HUDtext.text = "CURRENT TARGET: " + PlayerListing.player_to_attack;

        }
    }

    //public void setCurrentTarget(string currentTarget)
    //{
    //    print("HHHHHGHGHGHGHHGHGHGHG!??!?!11121212121212 " + currentTarget);
    //    _HUDtext.text = "CURRENT TARGET: ";
    //    //if (_HUDtext.tag == "Target")
    //    //{
    //    //    print("HHHHHGHGHGHGHHGHGHGHG!??!?!");
    //    //}
    //}
}
