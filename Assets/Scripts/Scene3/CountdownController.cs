﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    public void StartCountDown()
    {
        StartCoroutine(CountdownStart());
    }

    private IEnumerator CountdownStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        countdownDisplay.text = "GO!";
        PacketFactoryScript pf = GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>();
        pf.StartGame();
        yield return new WaitForSeconds(1);
        countdownDisplay.gameObject.SetActive(false);
    }
}