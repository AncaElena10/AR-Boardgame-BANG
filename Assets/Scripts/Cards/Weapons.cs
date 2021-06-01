using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using Photon.Pun;
using Photon.Realtime;
using PunHashtable = ExitGames.Client.Photon.Hashtable;

public class Weapons : MonoBehaviourPunCallbacks
{
    private static string COLT = CustomEnums.WeaponCards.COLT.ToString();
    private static string VOLCANIC = CustomEnums.WeaponCards.VOLCANIC.ToString();
    private static string SCHOFIELD = CustomEnums.WeaponCards.SCHOFIELD.ToString();
    private static string REMINGTON = CustomEnums.WeaponCards.REMINGTON.ToString();
    private static string REV_CARABINE = CustomEnums.WeaponCards.REV_CARABINE.ToString();
    private static string WINCHESTER = CustomEnums.WeaponCards.WINCHESTER.ToString();

    public PlayerRange _playerRange = new PlayerRange();

    //public static bool hasWeapon = false;

    public int weaponFoundChecks(int currentRange, TrackableBehaviour mTrackableBehaviour)
    {
        bool foundCondition = mTrackableBehaviour.CurrentStatus.ToString() == "TRACKED" && mTrackableBehaviour.CurrentStatusInfo.ToString() == "NORMAL";

        if (mTrackableBehaviour.TrackableName.ToString() == COLT && foundCondition) {
            currentRange = 1;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            setHasVolcanicCustomProperty(false);
            //hasWeapon = true;
        } else if (mTrackableBehaviour.TrackableName.ToString() == VOLCANIC && foundCondition) {
            currentRange = 1;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            // set a custom property if this weapon is equiped so this player can have infinite BANGs for the nearby players
            setHasVolcanicCustomProperty(true);

            //hasWeapon = true;
        } else if (mTrackableBehaviour.TrackableName.ToString() == SCHOFIELD && foundCondition) {
            currentRange = 2;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            setHasVolcanicCustomProperty(false);
            //hasWeapon = true;
        } else if (mTrackableBehaviour.TrackableName.ToString() == REMINGTON && foundCondition) {
            currentRange = 3;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            setHasVolcanicCustomProperty(false);
            //hasWeapon = true;
        } else if (mTrackableBehaviour.TrackableName.ToString() == REV_CARABINE && foundCondition) {
            currentRange = 4;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            setHasVolcanicCustomProperty(false);
            //hasWeapon = true;
        } else if (mTrackableBehaviour.TrackableName.ToString() == WINCHESTER && foundCondition) {
            currentRange = 5;
            print("[Weapons] Current range: " + currentRange);
            _playerRange.increaseRange(currentRange, "Weapons");

            setHasVolcanicCustomProperty(false);
            //hasWeapon = true;
        }

        return currentRange;
    }

    public void setHasVolcanicCustomProperty(bool value)
    {
        PunHashtable _volcanicCustomProperty = new PunHashtable();
        _volcanicCustomProperty["HasVolcanic"] = value;
        PhotonNetwork.LocalPlayer.SetCustomProperties(_volcanicCustomProperty);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        //base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        if (targetPlayer != null && targetPlayer == PhotonNetwork.LocalPlayer) {
            if (changedProps.ContainsKey("HasVolcanic")) {
                print("[Weapons] HasVolcanic custom property has changed!");
            }
        }
    }
}
