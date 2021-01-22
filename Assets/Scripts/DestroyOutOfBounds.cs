using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public static float upperY = 300f;
    public static float lowerY = -5f;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < lowerY || transform.position.y > upperY)
        {
            Destroy(gameObject);
        }
    }
}
