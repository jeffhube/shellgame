using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public ShellType Type;

    void Start()
    {
    }

    void Update()
    {
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
