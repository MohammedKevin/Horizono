using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DeepSpace.LaserTracking;
using System;
using UnityEngine.UI;

public class CollisionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var entityManager = GameObject.Find("TrackingEntityManager").GetComponent<TrackingEntityManager>();

        if (entityManager != null)
        {
            var countA = entityManager.TrackingEntityList
                .Count(t => t.AbsolutePosition.x > 2.94f && t.AbsolutePosition.y > 2.25f && t.AbsolutePosition.x < 6.318f && t.AbsolutePosition.y < 6.452f);

            var countB = entityManager.TrackingEntityList
                .Count(t => t.AbsolutePosition.x > 7.8375f && t.AbsolutePosition.y > 2.25f && t.AbsolutePosition.x < 11.19375f && t.AbsolutePosition.y < 6.452f);

            var lightbulpA = GameObject.Find("UI_Lightbulp_A").GetComponent<Image>();
            var lightbulpB = GameObject.Find("UI_Lightbulp_B").GetComponent<Image>();

            if (countA == 0 && countB == 0)
            {
                lightbulpA.color = Color.white;
                lightbulpB.color = Color.white;
            }
            else if (countA == countB && countA > 0)
            {
                lightbulpA.color = Color.yellow;
                lightbulpB.color = Color.yellow;
            }
            else if (countA > countB)
            {
                lightbulpA.color = Color.green;
                lightbulpB.color = Color.red;
            }
            else if (countA < countB)
            {
                lightbulpA.color = Color.red;
                lightbulpB.color = Color.green;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision Entered.");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Entered.");
    }
}
