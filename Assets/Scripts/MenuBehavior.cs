using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    public bool isStart;
    public bool isQuit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        if (isStart)
        {
            SceneManager.LoadScene("Level0");
        }
        if (isQuit)
        {
            Application.Quit();
        }
    }
}
