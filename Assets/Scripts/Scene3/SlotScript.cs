using DeepSpace;
using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotScript : MonoBehaviour
{
    private CmdConfigManager.AppType appType;
    private MessageSender _messageSender;

    private void Start()
    {
        var configMgr = GameObject.Find("UdpCmdConfigMgr").GetComponent<UdpCmdConfigMgr>();
        appType = configMgr.applicationType;

        _messageSender = GameObject.Find("MessageFinder").GetComponent<MessageSender>();
    }

    private void OnTriggerEnter(Collider other)
    {
        bool increasePoints = false;

        if (this.name.Contains("Blue") && other.name.Contains("Blue"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
            increasePoints = true;
        }
        else if (this.name.Contains("Green") && other.name.Contains("Green"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
            increasePoints = true;
        }
        else if (this.name.Contains("Red") && other.name.Contains("Red"))
        {
            Destroy(other.gameObject);
            other.gameObject.GetComponent<PacketScript>().Collider.HasPacketOnTrackingEntity = false;
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
            increasePoints = true;
        }
        else if (other.name.Contains("PharusTrack_"))
        {
            TrackingEntity te = GameObject.Find(other.name).GetComponent<TrackingEntity>();
            te.HasPacketOnTrackingEntity = false;
        }

        if (increasePoints && this.appType == CmdConfigManager.AppType.FLOOR)
        {
            _messageSender.SendMessage("Game;IncreasePoints");
        }
    }
}
