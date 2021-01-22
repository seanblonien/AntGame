using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerToController : MonoBehaviour
{
    public int maxPlayers = 4;
    public int numPlayers = 0;

    public GameObject playerPrefab;
    public GameObject antPrefab;

    private AudioSource joinSound;

    public List<int> controllers { get; private set; }

    private void Start()
    {
        controllers = new List<int>();
        joinSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (numPlayers < maxPlayers)
        {
            if (Input.GetButtonDown("J1A") && !controllers.Contains(1))
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<PlayerControls>().PlayerNum = ++numPlayers;
                player.GetComponent<PlayerControls>().ControllerNum = 1;
                player.GetComponent<PlayerControls>().antPrefab = antPrefab;
                player.gameObject.name = "Player" + player.GetComponent<PlayerControls>().PlayerNum;
                controllers.Add(1);
                joinSound.Play();
            }
            else if (Input.GetButtonDown("J2A") && !controllers.Contains(2))
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<PlayerControls>().PlayerNum = ++numPlayers;
                player.GetComponent<PlayerControls>().ControllerNum = 2;
                player.GetComponent<PlayerControls>().antPrefab = antPrefab;
                player.gameObject.name = "Player" + player.GetComponent<PlayerControls>().PlayerNum;
                controllers.Add(2);
                joinSound.Play();
            }
            else if (Input.GetButtonDown("J3A") && !controllers.Contains(3))
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<PlayerControls>().PlayerNum = ++numPlayers;
                player.GetComponent<PlayerControls>().ControllerNum = 3;
                player.GetComponent<PlayerControls>().antPrefab = antPrefab;
                player.gameObject.name = "Player" + player.GetComponent<PlayerControls>().PlayerNum;
                controllers.Add(3);
                joinSound.Play();
            }
            else if (Input.GetButtonDown("J4A") && !controllers.Contains(4))
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<PlayerControls>().PlayerNum = ++numPlayers;
                player.GetComponent<PlayerControls>().ControllerNum = 4;
                player.GetComponent<PlayerControls>().antPrefab = antPrefab;
                player.gameObject.name = "Player" + player.GetComponent<PlayerControls>().PlayerNum;
                controllers.Add(4);
                joinSound.Play();
            }
            else if (Input.GetButtonDown("J5A") && !controllers.Contains(5))
            {
                GameObject player = Instantiate(playerPrefab);
                player.GetComponent<PlayerControls>().PlayerNum = ++numPlayers;
                player.GetComponent<PlayerControls>().ControllerNum = 5;
                player.GetComponent<PlayerControls>().antPrefab = antPrefab;
                player.gameObject.name = "Player" + player.GetComponent<PlayerControls>().PlayerNum;
                controllers.Add(5);
                joinSound.Play();
            }
        }
    }
}
