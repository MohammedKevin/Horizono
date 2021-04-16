using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    private bool _hasStarted = false;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInParent<Animator>();
    }

    public void StartCountDown()
    {
        if (!_hasStarted)
            StartCoroutine(CountdownStart());
    }

    private IEnumerator CountdownStart()
    {
        _hasStarted = true;
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }

        if (_animator != null)
            this._animator.SetBool("GameStartedBool", true);
        countdownDisplay.text = "GO!";
        PacketFactoryScript pf = GameObject.Find("PacketFloor").GetComponent<PacketFactoryScript>();
        pf.StartGame();
        yield return new WaitForSeconds(1);
        countdownDisplay.gameObject.SetActive(false);
    }
}
