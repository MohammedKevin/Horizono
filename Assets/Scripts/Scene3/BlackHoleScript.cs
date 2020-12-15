using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject BluePacketPrefab;
    private PacketFactoryScript packetFactory;
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
        if (packetFactory == null)
            packetFactory = GameObject.Find("PacketFactoryScript").GetComponent<PacketFactoryScript>();

        if(other.name.ToUpper().Contains("PREFAB")) //wir fragen nach prefab, da alle packages den namen prefab enthalten
        {
            GameObject.Destroy(other.transform.parent.gameObject); //Gameobject vom Collider wird zerstört

            //Nun wird es in die Liste der Prefabs hinzugefügt.
            packetFactory.prefabs.Add(BluePacketPrefab);
        }
    }
}
