using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
    public void Break()
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        Destroy(gameObject, 5000);
    }
}
