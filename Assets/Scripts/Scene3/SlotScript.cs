using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (this.name.Contains("Blue") && other.name.Contains("Blue"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
        }
        else if (this.name.Contains("Green") && other.name.Contains("Green"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
        }
        else if (this.name.Contains("Red") && other.name.Contains("Red"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
        }
        else if (other.name.Contains("PharusTrack_"))
        {
            TrackingEntity te = GameObject.Find(other.name).GetComponent<TrackingEntity>();
            te.HasPacketOnTrackingEntity = false;
        }
    }
}
