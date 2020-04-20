using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomebuttonScript : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (this.animator != null)
        {
            this.animator.SetTrigger("ColissionWithHomebuttonTrigger");
        }
        else
            Debug.Log("Collided with homebutton but animator not found!");
    }
}
