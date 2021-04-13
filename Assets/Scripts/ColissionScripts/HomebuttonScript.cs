using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomebuttonScript : MonoBehaviour
{
    public MessageSender messageSender;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
        _animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this._animator != null)
        {
            this._animator.SetTrigger("ColissionWithHomebuttonTrigger");
            messageSender.SendMessage("Homebutton;ColissionWithHomebuttonTrigger");
        }
        else
            Debug.Log("Collided with homebutton but animator not found!");
    }
}
