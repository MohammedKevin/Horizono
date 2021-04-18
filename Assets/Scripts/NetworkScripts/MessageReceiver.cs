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
    private Text _debugText;

    // Start is called before the first frame update
    void Start()
    {
        udpReceiver.SubscribeReceiveEvent(ReceiveMessage);
        _animator = GetComponentInParent<Animator>();
        _debugText = GameObject.Find("DebugText").GetComponent<Text>();
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

        if (splittedMessage.Length < 1)
            return;

        

        if (splittedMessage[0] == "Homebutton")
        {
            if (_animator != null)
                _animator.SetTrigger(splittedMessage[1]);

            _debugText.text = message;
        }
        else if (splittedMessage[0] == "MessageButton")
        {
            if (_animator != null)
            {
                _animator.SetTrigger(splittedMessage[1]);
                GameObject.Find("PhoneMessage").GetComponent<Text>().text = splittedMessage[2];
            }
            _debugText.text = message;
        }
        else if (splittedMessage[0] == "EncWheel")
        {
            if (_animator != null)
            {
                if (splittedMessage[1].Length == 1)
                {
                    GameObject.Find("SmallLetterWheel").GetComponent<RotationScript>().input = splittedMessage[1][0]; // its is only one char, but as you cannot allocate string to char you have to access the first letter.
                    GameObject.Find("SmallLetterWheel").GetComponent<RotationScript>().readyToTurn = true;
                }
                else
                {
                    _animator.SetTrigger(splittedMessage[1]);
                }
            }
            _debugText.text = message;
        }
        else if (splittedMessage[0] == "Startbutton")
        {
            CountdownController countdown = GameObject.Find("CountdownCanvas").GetComponent<CountdownController>();
            countdown.StartCountDown();
            _debugText.text = message;
        }
        else if (splittedMessage[0] == "Game" && splittedMessage[1] == "IncreasePoints")
        {
            GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>().IncreasePoints();
            _debugText.text = message;
        }

    }
}
