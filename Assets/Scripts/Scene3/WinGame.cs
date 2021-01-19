using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public PacketFactoryScript packetFactoryScript;
    private bool _isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        _isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        _isActive = true;

        yield return new WaitForSeconds(6);

        if(_isActive)
        {
            packetFactoryScript.WinGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isActive = false;
    }
}
