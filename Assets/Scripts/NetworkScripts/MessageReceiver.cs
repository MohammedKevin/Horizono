using DeepSpace.Udp;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

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

    /// <summary>
    /// Received Messages should look like this: 
    /// -ObjectName-;-TriggerName-;[-Message-]
    /// </summary>
    /// <param name="messageBytes"></param>
    /// <param name="senderIP"></param>
    public void ReceiveMessage(byte[] messageBytes, IPAddress senderIP)
    {
        string message = Encoding.Default.GetString(messageBytes);

        var splittedMessage = message.Split(';');

        if (splittedMessage[0] == "Homebutton")
        {
            if (_animator != null)
                _animator.SetTrigger(splittedMessage[1]);
        }
        else if (splittedMessage[0] == "MessageButton")
        {
            if (_animator != null)
            {
                _animator.SetTrigger(splittedMessage[1]);
                GameObject.Find("PhoneMessage").GetComponent<Text>().text = splittedMessage[2];
            }
        }

    }
}
