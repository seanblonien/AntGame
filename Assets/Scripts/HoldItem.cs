using UnityEngine;
using System.Collections;

public class HoldItem : MonoBehaviour
{
    public GameObject holdItem;
    public bool canHold = true;
    public float speed = 5;

    private Transform holdOver;
    private GameObject empty;
    private string interactButton;

    void Update()
    {
        if (!holdOver)
        {
            holdOver = transform.parent.gameObject.transform.Find("AntBody");
            empty = new GameObject();
            empty.transform.parent = holdOver.transform;
            if (transform.parent.name.Contains("LeftAnt"))
            {
                interactButton = "LeftInteract";
            }
            else if (transform.parent.name.Contains("RightAnt"))
            {
                interactButton = "RightInteract";
            }
        }
        if (Input.GetButtonDown(interactButton))
        {
            if (canHold)
            {
                Pickup();
            }
            else
            {
                Drop();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PickUp" && !holdItem)
        {
            holdItem = other.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "PickUp" && canHold)
        {
            holdItem = null;
        }
    }

    private void Pickup()
    {
        if (!holdItem && canHold)
        {
            return;
        }
        
        holdItem.transform.SetParent(empty.transform);
        holdItem.transform.localRotation = gameObject.transform.transform.rotation;
        holdItem.transform.position = gameObject.transform.position;
        holdItem.GetComponent<Rigidbody>().useGravity = false;
        holdItem.GetComponent<Rigidbody>().isKinematic = true;

        canHold = false;
    }

    private void Drop()
    {
        if (!holdItem && !canHold)
        {
            return;
        }
        
        holdItem.transform.parent = null;
        holdItem.GetComponent<Rigidbody>().useGravity = true;
        holdItem.GetComponent<Rigidbody>().isKinematic = false;
        canHold = true;
    }
}