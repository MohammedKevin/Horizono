using DeepSpace;
using DeepSpace.Udp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageSender : MonoBehaviour
{
    public UdpSender SenderToWall;
    public UdpSender SenderToFloor;

    private CmdConfigManager.AppType appType;

    // Start is called before the first frame update
    void Start()
    {
        var configMgr = GameObject.Find("UdpCmdConfigMgr").GetComponent<UdpCmdConfigMgr>();
        appType = configMgr.applicationType;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void SendMessage(string message)
    {
        if (appType == CmdConfigManager.AppType.FLOOR)
            SenderToFloor.AddMessage(message);
        else
            SenderToWall.AddMessage(message);
    }
}
