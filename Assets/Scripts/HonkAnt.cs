using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HonkAnt : MonoBehaviour
{
    private GameObject gameCam;

    public AudioSource honkSound;

    // Start is called before the first frame update
    void Start()
    {
        gameCam = GameObject.Find("Main Camera");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ant")
        {
            if (honkSound) {
                if (!honkSound.isPlaying)
                {
                    honkSound.Play();
                }
                // Squash the ant
                other.transform.localScale = new Vector3(other.gameObject.transform.localScale.x, 0.2f, other.gameObject.transform.localScale.z);
                PlayerControls controls = GameObject.Find("Player" + other.GetComponent<Pickuper>().owningPlayer).GetComponent<PlayerControls>();
                if (other.gameObject == controls.LeftAnt)
                {
                    controls.LeftAnt = null;
                    gameCam.GetComponent<TrackObject>().targets.Remove(other.gameObject);
                }
                else if (other.gameObject == controls.RightAnt)
                {
                    controls.RightAnt = null;
                    gameCam.GetComponent<TrackObject>().targets.Remove(other.gameObject);
                }
                other.transform.GetChild(0).GetComponent<Animator>().enabled = false;
                other.gameObject.name = "DeadAnt";
                other.gameObject.GetComponent<Pickuper>().enabled = false;
                other.gameObject.GetComponent<Pickupable>().enabled = true;
                other.GetComponentInChildren<cakeslice.Outline>().enabled = true;
                other.gameObject.tag = "PickUp";
                other.transform.SetParent(null);
            }
        }
    }
}
