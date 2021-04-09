using DeepSpace.Udp;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;

public class MessageReceiver : MonoBehaviour
{
    public UdpReceiver udpReceiver;

    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        udpReceiver.SubscribeReceiveEvent(ReceiveMessage);
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReceiveMessage(byte[] messageBytes, IPAddress senderIP)
    {
        string message = Encoding.Default.GetString(messageBytes);

        var splittedMessage = message.Split(';');

        if (splittedMessage[0] == "Homebutton")
        {
            if (_animator != null)
                _animator.SetTrigger(splittedMessage[1]);
        }

    }
}
