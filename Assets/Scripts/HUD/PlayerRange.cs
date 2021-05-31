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

public class PlayerRange : MonoBehaviourPunCallbacks
{
    //public TextMeshProUGUI _HUDtext;
    public Text _HUDtext;

    public Player Player { get; private set; }

    private int previousRange = 0;

    void Awake()
    {
        //_HUDtext.text = "RANGE: " + (previousRange + 1).ToString();

        // TODO
        _HUDtext.text = "RANGE: " + previousRange.ToString();
    }

    public void increaseRange(int scannedRange, string fromWhere)
    {
        // set custom property for Range

        if (fromWhere == "Weapons") {
            if (checkIfWeaponAlreadyRead(scannedRange)) {
                print("[PlayerRange] Already scanned the weapon, it's enough.");
                //print("111111111111111111");
            } else {
                print("[PlayerRange] Let's change that range!");

                //print("2222222222222222222");

                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta")) {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"]) {
                        //print("3333333333333333");
                        PunHashtable rangeCustomProperty = new PunHashtable();
                        rangeCustomProperty.Add("Range", scannedRange);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(rangeCustomProperty);

                        changePlayerRangeArray(scannedRange);
                        //print("DAAAAAAAAAAAAAAAAAA!!!--------11111111 " + scannedRange);

                        if (scannedRange > 5) {
                            scannedRange = 5;
                        }

                        int actualRange = scannedRange + 1;
                        if (actualRange > 5) {
                            actualRange = 5;
                        }

                        if (_HUDtext.tag == "Range") {
                            _HUDtext.text = "RANGE: " + actualRange.ToString();
                        }

                        previousRange = scannedRange + 1; // +1 from Luneta
                        if (previousRange > 5) {
                            previousRange = 5;
                        }
                    } else {
                        //print("444444444444444444");
                        PunHashtable rangeCustomProperty = new PunHashtable();
                        rangeCustomProperty.Add("Range", scannedRange);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(rangeCustomProperty);

                        changePlayerRangeArray(scannedRange);

                        if (scannedRange > 5) {
                            scannedRange = 5;
                        }

                        if (_HUDtext.tag == "Range") {
                            _HUDtext.text = "RANGE: " + scannedRange.ToString();
                        }

                        previousRange = scannedRange;
                    }
                } else {
                    //print("55555555555555555555555");
                    PunHashtable rangeCustomProperty = new PunHashtable();
                    rangeCustomProperty.Add("Range", scannedRange);
                    PhotonNetwork.LocalPlayer.SetCustomProperties(rangeCustomProperty);

                    changePlayerRangeArray(scannedRange);

                    if (scannedRange > 5) {
                        scannedRange = 5;
                    }

                    if (_HUDtext.tag == "Range") {
                        _HUDtext.text = "RANGE: " + scannedRange.ToString();
                    }

                    previousRange = scannedRange;
                }
            }
        }

        if (fromWhere == "Luneta") {
            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta")) {
                if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"])
                {
                    print("[PlayerRange] Already scanned Luneta, it's enough.");
                }
            } else {
                //print("666666666666666666666");
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Range")) {
                    if (previousRange == 0) { // this case when Luneta scanned first
                        //print("------777777777---------");
                        PunHashtable rangeCustomProperty = new PunHashtable();
                        rangeCustomProperty.Add("Range", previousRange);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(rangeCustomProperty);

                        previousRange = 1;
                    } else {
                        //print("777777777777777777777");
                        int currentRange = (int)PhotonNetwork.LocalPlayer.CustomProperties["Range"] + scannedRange;
                        if (currentRange > 5) {
                            currentRange = 5;
                        }

                        if (_HUDtext.tag == "Range") {
                            _HUDtext.text = "RANGE: " + currentRange.ToString();
                        }

                        PunHashtable rangeCustomProperty = new PunHashtable();
                        rangeCustomProperty.Add("Range", currentRange);
                        PhotonNetwork.LocalPlayer.SetCustomProperties(rangeCustomProperty);

                        previousRange = currentRange;
                    }
                }
            }
        }
    }

    private bool checkIfWeaponAlreadyRead(int incomingRange)
    {
        //print("JJJJJJJJJJJJJJJJJJJJ " + incomingRange + "        " + previousRange);
        // if current range == incoming range => pas
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta")) {
            if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"]) {
                return previousRange - 1 == incomingRange; // substract -1 from Luneta
            }
        }
        
        return previousRange == incomingRange;
    }

    private void changePlayerRangeArray(int range)
    {
        int playerNumber = PhotonNetwork.CountOfPlayers;
        dealWith_x_players(range, playerNumber);
    }

    private void dealWith_x_players(int range, int count)
    {
        string str = "Distance_" + count + "_players";
        substractRange(range, str);
    }

    private void substractRange(int scannedRange, string customProperty_string)
    {
        string currentRange = (string)PhotonNetwork.LocalPlayer.CustomProperties[customProperty_string];

        //print("KKKKKKKKKKKKKKKKKKK11111111111 " + currentRange);

        List<int> currentRange_list = stringToListOfInts(currentRange);

        // for (int i = 0; i < currentRange_list.Count; i++)
        // {
        //     print("WWWWWWWWWWWWWW111111 " + currentRange_list[i]);
        // }

        // print("JJJJJJJ " + range + "    " + _range);


        //print("------XXXXXXXXXXXXXXXX-------------    " + scannedRange + "      " + previousRange);


        //if (scannedRange == 1)
        //{
        //    print("888888888888888888");
        //    print("Range 1. Redo distances.");
        //    for (int i = 0; i < currentRange_list.Count; i++) {
        //        if (i != PhotonNetwork.LocalPlayer.ActorNumber - 1) { // don't change the distane for the current player
        //            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta"))
        //            {
        //                if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"]) {
        //                    print("99999999999999999");
        //                    currentRange_list[i] = currentRange_list[i] + previousRange - 1; // add the previous range // -1 because Luneta equiped
        //                } else {
        //                    print("0000000000000000000000");
        //                    currentRange_list[i] = currentRange_list[i] + previousRange; // add the previous range
        //                }
        //            } else {
        //                print("0000000111111111110101010101001010");
        //                currentRange_list[i] = currentRange_list[i] + previousRange; // add the previous range
        //            }
        //        }
        //    }
        //} else {
        for (int i = 0; i < currentRange_list.Count; i++) {
            if (i != PhotonNetwork.LocalPlayer.ActorNumber - 1) { // don't change the distane for the current player
                if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta")) {
                    if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"]) {
                        if (previousRange == 1) { // it is possible that this if won't ever be reached
                            //print("------111111111111");
                            currentRange_list[i] = currentRange_list[i] - scannedRange - 1;
                        } else {
                            if (scannedRange + 1 > 5) { // if what I scanned + a possible Luneta > 5 then don't substract the -1 from Luneta
                                //print("--222222222--- AGAIN");
                                currentRange_list[i] = currentRange_list[i] + previousRange;
                                currentRange_list[i] = currentRange_list[i] - scannedRange;
                            } else {
                                //print("--222222222--- AGAIN AND AGAIN");
                                currentRange_list[i] = currentRange_list[i] + previousRange;
                                currentRange_list[i] = currentRange_list[i] - scannedRange - 1;
                            }
                        }
                    } else {
                    if (scannedRange == 1) { // if the previous range was 1, the distances are already in place, only substract the current range - intra aici
                        //print("------55555555555555555555");
                        currentRange_list[i] = currentRange_list[i] + previousRange; // substract the current range
                    } else {
                        if (previousRange != 1) {
                            //print("------66666666666666666666");
                            currentRange_list[i] = currentRange_list[i] + previousRange; // add the previous range
                            currentRange_list[i] = currentRange_list[i] - scannedRange; // substract the current range
                        } else {
                            // if the previous value was 1, then the distances are already set to default
                            // only substract the current range
                            currentRange_list[i] = currentRange_list[i] - scannedRange; // substract the current range
                        }
                    }
                }
                } else {
                    if (scannedRange == 1) { // if the previous range was 1, the distances are already in place, only substract the current range - intra aici
                        //print("------55555555555555555555");
                        currentRange_list[i] = currentRange_list[i] + previousRange; // substract the current range
                    } else {
                        if (previousRange != 1) {
                            print("------66666666666666666666");
                            currentRange_list[i] = currentRange_list[i] + previousRange; // add the previous range
                            currentRange_list[i] = currentRange_list[i] - scannedRange; // substract the current range
                        } else {
                            // if the previous value was 1, then the distances are already set to default
                            // only substract the current range
                            currentRange_list[i] = currentRange_list[i] - scannedRange; // substract the current range
                        }
                    }
                }
            }

            //if (currentRange_list[i] < 0) { // don't use negative values
            //    currentRange_list[i] = 1;
            //}
        //}
        }

        // for (int i = 0; i < currentRange_list.Count; i++)
        // {
        //     print("WWWWWWWWWWWW222222 " + currentRange_list[i]);
        // }

        string currentRange_string = listOfIntsToString(currentRange_list);

        //print("KKKKKKKKKKKKKKKKKKK22222222222 " + currentRange_string);

        PunHashtable customProps = new PunHashtable();
        customProps.Add(customProperty_string, currentRange_string);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProps);

        // print("EEEEEEEEEE " + PhotonNetwork.LocalPlayer.CustomProperties[customProperty_string]);
    }

    private List<int> stringToListOfInts(string str)
    {
        return str.Split(',').Select(Int32.Parse).ToList();
    }

    private string listOfIntsToString(List<int> list)
    {
        return string.Join(",", list.ToArray());
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, PunHashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null) {
            //print("[OnPlayerPropertiesUpdate] AHOI YO YO from PlayerListingMenu.");
            if (changedProps.ContainsKey("Distance_2_players")) {
                print("[PlayerRange] Distance_2_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_3_players")) {
                print("[PlayerRange] Distance_3_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_4_players")) {
                print("[PlayerRange] Distance_4_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_5_players")) {
                print("[PlayerRange] Distance_5_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_6_players")) {
                print("[PlayerRange] Distance_6_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Distance_7_players")) {
                print("[PlayerRange] Distance_7_players custom property has changed!");
            }
            if (changedProps.ContainsKey("Range")) {
                print("[PlayerRange] Range custom property has changed!");
            }
        }
    }

    //public void increaseRange(int rangeResult, string fromWhere)
    //{
    //    //print("DAAAAA " + rangeResult);

    //    if (fromWhere == "Weapons") {
    //        if (checkIfWeaponAlreadyRead(rangeResult)) {
    //            print("Already scanned the weapon, it's enough.");
    //        } else {
    //            print("Here [Weapons]! Let's change that range");
    //            if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Luneta")) {
    //                if ((bool)PhotonNetwork.LocalPlayer.CustomProperties["Luneta"])
    //                {
    //                    print("11111111111111    " + rangeResult + "    " + _range);
    //                    changePlayerRangeArray(rangeResult);
    //                    int rangeToUpdate_after_luneta = _range + rangeResult;

    //                    if (rangeToUpdate_after_luneta > 5)
    //                    {
    //                        print("Maximum range!!!");
    //                        rangeToUpdate_after_luneta = 5;
    //                    }

    //                    if (_HUDtext.tag == "Range")
    //                    {
    //                        _HUDtext.text = "RANGE: " + rangeToUpdate_after_luneta.ToString();
    //                    }

    //                    _range = rangeToUpdate_after_luneta;
    //                } else
    //                {
    //                    print("222222222222222");
    //                    changePlayerRangeArray(rangeResult);
    //                    if (rangeResult > 5)
    //                    {
    //                        print("Maximum range!!!");
    //                        rangeResult = 5;
    //                    }

    //                    if (_HUDtext.tag == "Range")
    //                    {
    //                        _HUDtext.text = "RANGE: " + rangeResult.ToString();
    //                    }
    //                    _range = rangeResult;
    //                }
    //            } else {
    //                print("333333333333333333");
    //                changePlayerRangeArray(rangeResult);
    //                if (rangeResult > 5)
    //                {
    //                    print("Maximum range!!!");
    //                    rangeResult = 5;
    //                }
    //                if (_HUDtext.tag == "Range")
    //                {
    //                    _HUDtext.text = "RANGE: " + rangeResult.ToString();
    //                }
    //                _range = rangeResult;
    //            }
    //        }     
    //    }

    //    if (fromWhere == "Luneta") {
    //        print("444444444444444444");
    //        print("Here [Luneta]! Let's change that range");

    //        int rangeToUpdate_after_luneta = _range + rangeResult;
    //        // set a maximum of 5 range for each player
    //        if (rangeToUpdate_after_luneta > 5) {
    //            print("Maximum range!!!");
    //            rangeToUpdate_after_luneta = 5;
    //        }

    //        if (_HUDtext.tag == "Range") {
    //            _HUDtext.text = "RANGE: " + rangeToUpdate_after_luneta.ToString();
    //        }

    //        // save in _range the previous range
    //        _range = rangeToUpdate_after_luneta;
    //    }
    //}
}
