using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotColliderScript : MonoBehaviour
{
    private Animator _animator;
    private bool _collided = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        //this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().input - 'a';
        //this.gameObject.transform.Find("SmallLetterWheel").GetComponent<RotationScript>().readyToTurn = true;
        _collided = true;

        yield return new WaitForSeconds(5); // Wait for 5 seconds to check, if someone is still standing on the button.
        if (_collided == true) // check, if someone is still on the button
        {
                this.gameObject.transform.parent.parent.parent.Find("SmallLetterWheel").GetComponent<RotationScript>().input = this.name.ToLower()[0];
                this.gameObject.transform.parent.parent.parent.Find("SmallLetterWheel").GetComponent<RotationScript>().readyToTurn = true;
                //this.gameObject.transform.parent.parent.parent.GetComponent<Animator>().SetTrigger("StartMoveLetter");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collided = false;
    }
}
