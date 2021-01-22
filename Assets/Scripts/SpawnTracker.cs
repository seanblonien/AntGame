using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTracker : MonoBehaviour
{
    public List<Collider> ants { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        ants = new List<Collider>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ant")
        {
            ants.Remove(other);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ant")
        {
            ants.Add(other);
        }
    }
}
