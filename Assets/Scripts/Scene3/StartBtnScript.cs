using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBtnScript : MonoBehaviour
{
    private bool _collided;
    private bool _gameStarted;
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
        if (!_gameStarted)
        {
            _collided = true;
            yield return new WaitForSeconds(3);

            if (_collided)
            {
                CountdownController countdown = GameObject.Find("CountdownCanvas").GetComponent<CountdownController>();
                countdown.StartCountDown();
                _gameStarted = true;
                this.gameObject.SetActive(false);
                //this.GetComponent<Renderer>().SetActive
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _collided = false;
    }
}
