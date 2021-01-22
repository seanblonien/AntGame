using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupable : MonoBehaviour
{
    private Vector3 motion;
    public GameObject AttachedPickupers { get; private set; }
    public GameObject Root { get; private set; }

    public int numPickupers = 1;
    private int oldPickupers;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "PickUp";
        AttachedPickupers = new GameObject();
        Root = new GameObject();
        oldPickupers = numPickupers;
    }

    // Update is called once per frame
    void Update()
    {
        MapPlayerToController game = GameObject.Find("GameManager").GetComponent<MapPlayerToController>();
        if (game.numPlayers == 1)
        {
            numPickupers = oldPickupers;
            if (numPickupers > 2)
            {
                numPickupers = 2;
            }
        } else if (game.numPlayers == 2)
        {
            numPickupers = oldPickupers;
            if (numPickupers > 4)
            {
                numPickupers = 4;
            }
        } else if (game.numPlayers == 3)
        {
            numPickupers = oldPickupers;
            if (numPickupers > 6)
            {
                numPickupers = 6;
            }
        } else if (game.numPlayers == 4)
        {
            numPickupers = oldPickupers;
            if (numPickupers > 8)
            {
                numPickupers = 8;
            }
        }
        if (transform.parent == null)
        {
            Root.transform.position = Vector3.zero;
            Root.transform.localScale = Vector3.one;
            Root.transform.localRotation = Quaternion.identity;
            Root.name = gameObject.name;
            transform.SetParent(Root.transform);
        }
        if (AttachedPickupers.transform.parent == null)
        {
            AttachedPickupers.transform.SetParent(Root.transform);
            AttachedPickupers.transform.position = Vector3.zero;
            AttachedPickupers.transform.localScale = Vector3.one;
            AttachedPickupers.transform.localRotation = Quaternion.identity;
        }
        List<Transform> pickupers = new List<Transform>();
        for (int i = 0; i < AttachedPickupers.transform.childCount; i++)
        {
            if (AttachedPickupers.transform.GetChild(i)
                .GetComponent<Pickuper>().CanPickup
                .Contains(transform.GetComponent<Collider>()))
            {
                pickupers.Add(AttachedPickupers.transform.GetChild(i));
            }
            else
            {
                AttachedPickupers.transform.GetChild(i)
                    .GetComponent<Pickuper>().Drop();
            }
        }
        if (pickupers.Count >= numPickupers)
        {
            cakeslice.Outline outline = transform.gameObject.GetComponent<cakeslice.Outline>();
            if (!outline)
            {
                outline = transform.gameObject.GetComponentInChildren<cakeslice.Outline>();
            }
            outline.color = 1;
            Vector3 movement = Vector3.zero;
            float speed = 0f;
            foreach (Transform pickuper in pickupers)
            {
                movement += pickuper.GetComponent<Pickuper>().Motion;
                speed += pickuper.GetComponent<Pickuper>().speed;
            }
            speed /= pickupers.Count;
            Root.transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        else
        {
            cakeslice.Outline outline = transform.gameObject.GetComponent<cakeslice.Outline>();
            if (!outline)
            {
                outline = transform.gameObject.GetComponentInChildren<cakeslice.Outline>();
            }
            if(outline) outline.color = 0;
        }
    }


}
