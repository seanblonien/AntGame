using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackObject : MonoBehaviour
{
    public List<GameObject> targets;
    public int minDistFromMidpoint = 10;
    public int maxnDistFromMidpoint = 100;
    public int objectMoveSpeed = 20;
    public int objectRotateSpeed = 5;
    public int minXAngle = 25;
    public Vector3 center;
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void Update()
    {
        // Controlling where the object looks
        center = new Vector3(0f, 0f, 0f);
        List<int> toDelete = new List<int>();
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i].gameObject.Equals(null))
            {
                toDelete.Add(i);
            }
            else
            {
                center += targets[i].transform.position;
            }
        }
        foreach (int index in toDelete)
        {
            targets.RemoveAt(index);
        }
        center /= targets.Count;
        transform.LookAt(center);

        // Prevent targets from moving out of viewport by moving them towards the center
        foreach (var target in targets)
        {
            var screenPoint = cam.WorldToViewportPoint(target.transform.position);
            var onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
            if (!onScreen)
            {
                var dir = target.transform.position - center;
                target.transform.position -= dir * Time.deltaTime;
            }
        }
        
        // Controlling the motion of the object according to min/max bounds
        var heading = center - transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;
        if (distance <= minDistFromMidpoint)
        {
            // Move back if to close to midpoint
            transform.position -= direction * objectMoveSpeed * Time.deltaTime;
        }
        else if (distance >= maxnDistFromMidpoint)
        {
            // Move closer if to close to midpoint
            transform.position += direction * objectMoveSpeed * Time.deltaTime;
        }

        // Controlling the minimum z-angle of the object (enforcing the top-down view of the game)
        if (targets.Count != 0)
        {
            var angle = transform.eulerAngles;
            if (angle.x < minXAngle)
            {
                transform.position += new Vector3(0, objectRotateSpeed * Time.deltaTime, 0);
            }
        }
    }
}
