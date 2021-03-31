using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransferingOwnership : MonoBehaviourPun
{
    private void OnMouseDown()
    {
        base.photonView.RequestOwnership();
    }
}
