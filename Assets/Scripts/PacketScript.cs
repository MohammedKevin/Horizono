using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeepSpace.LaserTracking;
using System;

public class PacketScript : MonoBehaviour
{
    private int _packetId;
    private Vector3 _absolutePosition;
    private Collider _collider; // The TrackingEntity, that collides with this packet.

    public int PacketId
    {
        get { return _packetId; }
    }

    public Vector3 AbsolutePosition
    {
        get { return _absolutePosition; }
        set { _absolutePosition = value; }
    }

    public PacketScript(int packetId, Vector3 pos)
    {
        this._packetId = packetId;
        this._absolutePosition = pos;
        this._collider = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            TrackingEntity trackingEntity = GameObject.Find(other.gameObject.name).GetComponent<TrackingEntity>();

            if (_collider == null)
            {
                if (other.gameObject.name.Contains("PharusTrack_") && !trackingEntity.HasPacketOnTrackingEntity)
                {
                    this._collider = other;
                    trackingEntity.HasPacketOnTrackingEntity = true;
                }
            }
        } catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this._collider != null)
        {
            this.transform.position = _collider.transform.position;
        }
    }
}
