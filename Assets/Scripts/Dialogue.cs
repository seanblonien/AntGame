using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public UnityEngine.UI.Text dialogue;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ant")) {
            dialogue.gameObject.SetActive(true);
            dialogue.text = "WE are strong";
        } 
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ant"))
        {
            dialogue.text = "You are weak";
        }
    }
}
