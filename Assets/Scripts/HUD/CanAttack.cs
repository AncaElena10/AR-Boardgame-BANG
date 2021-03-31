using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

using UnityEngine;

public class CanAttack : MonoBehaviour
{
    public Text _HUDtext;

    private void Awake()
    {
        _HUDtext.text = "";
    }

    public void Update()
    {
        if (PlayerListing._canAttack_confirm)
        {
            if (PlayerListing._canAttack)
            {
                _HUDtext.text = "ENOUGH RANGE";

            }
            else
            {
                _HUDtext.text = "NOT ENOUGH RANGE";
            }
        } else
        {
            _HUDtext.text = "";
        }
    }
}
