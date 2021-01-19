   using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SzeneOneAnimation : MonoBehaviour
{
    private Animator animator;
    private bool isAnimating;
    // Start is called before the first frame update
    void Start()
    {
        isAnimating = true;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
