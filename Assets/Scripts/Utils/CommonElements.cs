using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonElements : MonoBehaviour
{
    PlayerListingsMenu pl;

    // Start is called before the first frame update
    void Start()
    {
        pl = new PlayerListingsMenu();
    }

    public List<int> ReturnArray()
    {
        return pl.GetPlayerActorIds();
    }

    public List<int> ReturnArrayAux()
    {
        return pl.GetPlayerActorIdsAux();
    }
}
