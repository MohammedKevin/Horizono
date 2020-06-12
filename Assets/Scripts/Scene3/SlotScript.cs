using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.name.Contains("Blue"))
        {
            if (other.name.Contains("Blue"))
            {
                Debug.Log("Packet Collided!");
            }
        }
        else if (this.name.Contains("Green"))
        {
            if (other.name.Contains("Green"))
            {
                Debug.Log("Packet Collided!");
            }
        }
        else if (this.name.Contains("Red"))
        {
            if (other.name.Contains("Red"))
            {
                Debug.Log("Packet Collided!");
            }
        }
    }
}
