using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgessScript : MonoBehaviour
{
    private Slider _slider;

    public float _progessSpeed = 0.5f;
    private float _progress = 0;

    private void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();    
    }
    // Start is called before the first frame update
    void Start()
    {
        IncrementProg(0.75f);    
    }

    // Update is called once per frame
    void Update()
    {
        if (_slider.value < _progress)
            _slider.value += _progessSpeed * Time.deltaTime;
    }

    public void IncrementProg(float newProg) => _progress = _slider.value + newProg;
}
