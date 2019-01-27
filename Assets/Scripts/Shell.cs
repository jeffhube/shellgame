using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public ShellType Type;

    private float _wiggleTimer;
    private bool _wiggling;

    void Start()
    {
        _wiggleTimer = 1.5f;
        _wiggling = false;
    }

    void Update()
    {
        if (_wiggling)
        {
            transform.rotation = Quaternion.Euler(0,0,Mathf.Sin(Time.time * 30) * 5);
        }
        _wiggleTimer -= Time.deltaTime;
        if (_wiggleTimer <= 0)
        {
            if (_wiggling)
            {
                transform.rotation = Quaternion.identity;
                _wiggleTimer = Random.value + 1;
            }
            else
            {
                _wiggleTimer = 0.5f;
            }
            _wiggling = !_wiggling;
        }
    }

    public enum ShellType
    {
        None,
        WallBreaking,
        DoubleJump,
        SharkResistant,
        SharkAttractant,
        Heavy,
        Speedy,
        Shiny,
        Grapple
    }
}
