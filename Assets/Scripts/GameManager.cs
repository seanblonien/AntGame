using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject deathCubePrefab;
    public GameObject[] players;
    
    void Update()
    {
        /*if (Input.anyKeyDown)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode))) {
                if (Input.GetKey(code))
                {
                    Debug.Log("Key pressed was " + code);
                }
            }
        }*/

        List<GameObject> ants = new List<GameObject>();

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            PlayerControls controls = player.GetComponent<PlayerControls>();
            if (controls.LeftAnt != null)
            {
                ants.Add(controls.LeftAnt);
            }
            if (controls.RightAnt != null)
            {
                ants.Add(controls.RightAnt);
            }
        }

        /*if (Input.GetKeyDown(KeyCode.K) && ants.Count > 0)
        {
            int rand = (int)Random.Range(0f, (float)(ants.Count - 1));
            Instantiate(deathCubePrefab, ants[rand].transform.position + new Vector3(0f, 3f, 0f), deathCubePrefab.transform.rotation);
        }*/
    }


}
