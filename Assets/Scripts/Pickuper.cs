using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuper : MonoBehaviour
{
    public Vector3 Motion { get; set; }
    public string InteractButton { get; set; }
    public List<Collider> CanPickup { get; } = new List<Collider>();

    public int owningPlayer;
    public bool canMove = true;
    public bool isHolding = false;
    public float speed = 10f;
    public float rotSpeed = 0.1f;
    
    private AudioClip dropSound;
    private AudioClip grabSound;
    private AudioClip lifeSound;

    private Collider pickedUp;
    private GameObject menuGrabHint;

    private void Start()
    {
        menuGrabHint = GameObject.Find("GrabHint");
        dropSound = Resources.Load<AudioClip>("Audio/antdrop");
        grabSound = Resources.Load<AudioClip>("Audio/antpickup");
        lifeSound = Resources.Load<AudioClip>("Audio/antlife");
        AudioSource.PlayClipAtPoint(lifeSound, gameObject.transform.position);
    }

    private void Update()
    {
        if (Input.GetButtonDown(InteractButton))
        {
            if (isHolding)
            {
                Drop();
            }
            else
            {
                Pickup();
            }
        }
        
        if (CanPickup.Find(x => x.GetComponent<Pickupable>() != null) && !isHolding)
        {
            menuGrabHint.transform.localScale = Vector3.one;
        }
        else
        {
            menuGrabHint.transform.localScale = Vector3.zero;
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CanPickup.Contains(other))
        {
            CanPickup.Add(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CanPickup.Remove(other);
    }

    private void Pickup()
    {
        var pickedUp = CanPickup.Find(x => x.GetComponent<Pickupable>());
        if (pickedUp)
        {
            var pickupable = pickedUp.GetComponent<Pickupable>();
            var attatchedPickerUppers = pickupable.AttachedPickupers;
            if (attatchedPickerUppers)
            {
                AudioSource.PlayClipAtPoint(grabSound, gameObject.transform.position);
                transform.SetParent(pickupable.AttachedPickupers.transform);
                isHolding = true;
                pickedUp.GetComponent<Rigidbody>().useGravity = true;
                pickedUp.GetComponent<Rigidbody>().isKinematic = false;
                canMove = false;
            }
        }
    }

    public void Drop()
    {
        isHolding = false;
        canMove = true;
        transform.SetParent(null);
        transform.localScale = Vector3.one;
        AudioSource.PlayClipAtPoint(dropSound, gameObject.transform.position);
    }
}
