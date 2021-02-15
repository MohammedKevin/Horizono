using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleScript : MonoBehaviour
{
    public GameObject BluePacketPrefab;
    public GameObject GreenPacketPrefab;
    public GameObject RedPacketPrefab;

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
            packetFactory = GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>();

        Debug.Log("Not collided oida");
        if (other.name.ToUpper().Contains("PREFAB")) //wir fragen nach prefab, da alle packages den namen prefab enthalten
        {
            Debug.Log("Blackhole collided");
            Destroy(other.gameObject); //Gameobject vom Collider wird zerstört

            //Nun wird es in die Liste der Prefabs hinzugefügt.
            if (other.name.ToUpper().Contains("GREEN"))
                packetFactory.prefabs.Add(GreenPacketPrefab);
            else if (other.name.ToUpper().Contains("BLUE"))
                packetFactory.prefabs.Add(BluePacketPrefab);
            else if (other.name.ToUpper().Contains("RED"))
                packetFactory.prefabs.Add(RedPacketPrefab);

            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            //packetFactory.InstantiatePacket(packetFactory.GetFirstOfListAndRemoveIt(), packetFactory.Spawn1);
            Debug.Log("Amount of packages!!! " + packetFactory.PacketOnSpawnFloor);
        }
    }
}
