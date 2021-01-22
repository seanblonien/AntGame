using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int counter = 0;
    public bool counting = false;
    private int counterMax = 150;

    public bool isGoal = true;
    private bool goalFound = false;
    public string nextScene = "Menu";

    private List<Collider> ants;

    private void Start()
    {
        ants = new List<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (counting)
        {
            counter++;
            if (counter == counterMax)
            {
                SceneManager.LoadScene(nextScene);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ant")
        {
            ants.Remove(other);
            if (ants.Count == 0 && (goalFound || !isGoal))
            {
                counting = false;
                counter = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ant")
        {
            ants.Add(other);
            if (!counting && (goalFound || !isGoal))
            {
                counting = true;
            }
        }
        if (other.tag == "Goal")
        {
            Pickuper[] pickupers = other.transform.parent.parent.GetComponentsInChildren<Pickuper>();
            foreach (Pickuper pickuper in pickupers)
            {
                pickuper.Drop();
            }
            Destroy(other.transform.parent.gameObject);
            goalFound = true;
        }
    }
}
