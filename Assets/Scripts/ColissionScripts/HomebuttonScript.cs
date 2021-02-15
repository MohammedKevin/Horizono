using DeepSpace;
using DeepSpace.Udp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomebuttonScript : MonoBehaviour
{
    private Animator _animator;
    private UdpCmdConfigMgr _udpCmdConfigMgr;
    private UdpSender _udpSender;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _udpCmdConfigMgr = GameObject.Find("UdpCmdConfigMgr").GetComponent<UdpCmdConfigMgr>();

        if (_udpCmdConfigMgr.applicationType == CmdConfigManager.AppType.FLOOR)
            _udpSender = GameObject.Find("UdpSenderToWall").GetComponent<UdpSender>();
        else
            _udpSender = GameObject.Find("UdpSenderToFloor").GetComponent<UdpSender>();


        Debug.Log(_udpCmdConfigMgr.applicationType.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this._animator != null)
        {
            _udpSender.AddMessage("ColissionWithHomebuttonTrigger");
            this._animator.SetTrigger("ColissionWithHomebuttonTrigger");
        }
        else
            Debug.Log("Collided with homebutton but animator not found!");
    }
}
