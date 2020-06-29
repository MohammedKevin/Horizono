using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSpotScript : MonoBehaviour
{
    private bool _hasPacketOnSpawnSpot = false;

    public bool HasPacketOnSpawnSpot
    { 
        get { return _hasPacketOnSpawnSpot; }
        private set { _hasPacketOnSpawnSpot = value; } 
    }

    private void OnTriggerEnter(Collider other)
    {
        HasPacketOnSpawnSpot = true;
    }

    private void OnTriggerExit(Collider other)
    {
        HasPacketOnSpawnSpot = false;
    }
}
