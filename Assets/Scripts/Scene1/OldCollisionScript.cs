using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DeepSpace.LaserTracking;
using System;
using UnityEngine.UI;

public class OldCollisionScript : MonoBehaviour
{
    private ButtonController buttonController;
    // Start is called before the first frame update
    void Start()
    {
        buttonController = GameObject.Find("ButtonController").GetComponent<ButtonController>();
    }

    // Update is called once per frame
    void Update()
    {
        var entityManager = GameObject.Find("TrackingEntityManager").GetComponent<TrackingEntityManager>();

        if (entityManager != null)
        {
            var lightbulpA = GameObject.Find("UI_Lightbulp_A").GetComponent<Image>();
            var lightbulpB = GameObject.Find("UI_Lightbulp_B").GetComponent<Image>();

            if (buttonController.CountA == 0 && buttonController.CountB == 0)
            {
                lightbulpA.color = Color.white;
                lightbulpB.color = Color.white;
            }
            else if (buttonController.CountA == buttonController.CountB && buttonController.CountA > 0)
            {
                lightbulpA.color = Color.yellow;
                lightbulpB.color = Color.yellow;
            }
            else if (buttonController.CountA > buttonController.CountB)
            {
                lightbulpA.color = Color.green;
                lightbulpB.color = Color.red;
            }
            else if (buttonController.CountA < buttonController.CountB)
            {
                lightbulpA.color = Color.red;
                lightbulpB.color = Color.green;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.name.Contains("A"))
        {
            Debug.Log("Collided with Button A.");
            // 1 means Button A, 2 means Button B
            buttonController.AddEntity(1);
        }
        else if (this.name.Contains("B"))
        {
            Debug.Log("Collided with Button B.");
            // 1 means Button A, 2 means Button B
            buttonController.AddEntity(2);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.name.Contains("A"))
        {
            Debug.Log("Collider exited Button A.");
            // 1 means Button A, 2 means Button B
            buttonController.RemoveEntity(1);
        }
        else if (this.name.Contains("B"))
        {
            Debug.Log("Collider exited Button B.");
            // 1 means Button A, 2 means Button B
            buttonController.RemoveEntity(2);
        }
    }
}
