using DeepSpace.LaserTracking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColissionScript : MonoBehaviour
{
    public GameObject Message;

    private ButtonController _buttonController;
    private Animator _animator;
    private bool _collided = false;
    private string _chosenMessage;
    // Start is called before the first frame update
    void Start()
    {
        _buttonController = GameObject.Find("ButtonController").GetComponent<ButtonController>();
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_buttonController != null)
        {
            var lightbulpA = GameObject.Find("UI_Lightbulp_A").GetComponent<Image>();
            var lightbulpB = GameObject.Find("UI_Lightbulp_B").GetComponent<Image>();

            int btnWithMaxPeople = _buttonController.GetMax();

            if (_buttonController.CountA == 0 && _buttonController.CountB == 0)
            {
                lightbulpA.color = Color.white;
                lightbulpB.color = Color.white;
            }
            else if (_buttonController.CountA == _buttonController.CountB && _buttonController.CountA > 0)
            {
                lightbulpA.color = Color.yellow;
                lightbulpB.color = Color.yellow;
            }
            else if (_buttonController.CountA > _buttonController.CountB)
            {
                lightbulpA.color = Color.green;
                lightbulpB.color = Color.red;
            }
            else if (_buttonController.CountA < _buttonController.CountB)
            {
                lightbulpA.color = Color.red;
                lightbulpB.color = Color.green;
            }
        }*/
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        _collided = true;

        yield return new WaitForSeconds(5); // Wait for 5 seconds to check, if someone is still standing on the button.
        if (_collided == true) // check, if someone is still on the button
        {
            var messageBox = GameObject.Find("PhoneMessage").GetComponent<Text>();
            _chosenMessage = Message.GetComponent<Text>().text;
            messageBox.text = _chosenMessage;

            Debug.Log("Chosen Message: " + _chosenMessage);
            if (this._animator != null)
            {
                this._animator.SetTrigger("ChooseMessageTrigger");
                var textDec = GameObject.Find("TextDec");
                Debug.Log("test");
                if (textDec != null && textDec is Text)
                    Debug.Log("Is a text");
            }
            else
                Debug.Log("Message Chosen but animator not found!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collided = false;
        /*if (this.name.Contains("A"))
        {
            Debug.Log("Collider exited Button A.");
            // 1 means Button A, 2 means Button B, 3 means Button C, 4 means Button D
            _buttonController.RemoveEntity(1);
        }
        else if (this.name.Contains("B"))
        {
            Debug.Log("Collider exited Button B.");
            // 1 means Button A, 2 means Button B, 3 means Button C, 4 means Button D
            _buttonController.RemoveEntity(2);
        }
        else if (this.name.Contains("C"))
        {
            Debug.Log("Collider exited Button C.");
            // 1 means Button A, 2 means Button B, 3 means Button C, 4 means Button D
            _buttonController.RemoveEntity(3);
        }
        else if (this.name.Contains("D"))
        {
            Debug.Log("Collider exited Button D.");
            // 1 means Button A, 2 means Button B, 3 means Button C, 4 means Button D
            _buttonController.RemoveEntity(4);
        }*/
    }
}
