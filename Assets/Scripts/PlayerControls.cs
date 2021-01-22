using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject spawner;
    public GameObject antPrefab;

    public AudioSource cantSpawn;

    public int ControllerNum { get; set; }
    public int PlayerNum { get; set; }

    public string leftHorizontalAxis { get; private set; }
    public string leftVerticalAxis { get; private set; }
    public string rightHorizontalAxis { get; private set; }
    public string rightVerticalAxis { get; private set; }
    public string leftStick { get; private set; }
    public string rightStick { get; private set; }
    public string aButton { get; private set; }
    public string bButton { get; private set; }
    public string menuButton { get; private set; }
    public string leftBumper { get; private set; }
    public string rightBumper { get; private set; }

    public GameObject LeftAnt { get; set; }
    public GameObject RightAnt { get; set; }

    private GameObject gameCam;

    private bool moving = true;
    private float camAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        gameCam = GameObject.Find("Main Camera");
        spawner = GameObject.FindGameObjectWithTag("Spawner");
        leftHorizontalAxis = "J" + ControllerNum + "LeftHorizontal";
        leftVerticalAxis = "J" + ControllerNum + "LeftVertical";
        rightHorizontalAxis = "J" + ControllerNum + "RightHorizontal";
        rightVerticalAxis = "J" + ControllerNum + "RightVertical";
        leftStick = "J" + ControllerNum + "LeftSpawn";
        rightStick = "J" + ControllerNum + "RightSpawn";
        aButton = "J" + ControllerNum + "A";
        bButton = "J" + ControllerNum + "B";
        menuButton = "J" + ControllerNum + "Menu";
        leftBumper = "J" + ControllerNum + "LeftGrab";
        rightBumper = "J" + ControllerNum + "RightGrab";
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            Pickuper leftAntPickuper = null;
            Pickuper rightAntPickuper = null;
            if (LeftAnt)
            {
                leftAntPickuper = LeftAnt.GetComponent<Pickuper>();
            }
            if (RightAnt)
            {
                rightAntPickuper = RightAnt.GetComponent<Pickuper>();
            }

            Vector3 camLook = Vector3.left;
            if (gameCam.GetComponent<TrackObject>())
            {
                camLook = gameCam.GetComponent<TrackObject>().center - gameCam.transform.position;
                camLook.y = 0f;
                camLook.Normalize();
            }
            if (!moving)
            {
                camAngle = Vector3.Angle(camLook, Vector3.forward);
            }

            // Move left ant
            if (LeftAnt != null)
            {
                float moveHorizontal = Input.GetAxis(leftHorizontalAxis);
                float moveVertical = Input.GetAxis(leftVerticalAxis);

                Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical);
                if (camAngle > 45f && camAngle <= 135f && camLook.x > 0)
                {
                    movement = new Vector3(moveVertical, 0f, -moveHorizontal);
                }
                else if (camAngle > 45f && camAngle <= 135f && camLook.x <= 0F)
                {
                    movement = new Vector3(-moveVertical, 0f, moveHorizontal);
                }
                else if (camAngle <= 45f)
                {
                    movement = new Vector3(moveHorizontal, 0f, moveVertical);
                }
                else if (camAngle > 135f)
                {
                    movement = new Vector3(-moveHorizontal, 0f, -moveVertical);
                }

                if (leftAntPickuper)
                {
                    leftAntPickuper.Motion = movement;
                }

                if ((moveHorizontal != 0 || moveVertical != 0) && leftAntPickuper.canMove)
                {
                    moving = true;
                    LeftAnt.transform.rotation = Quaternion.Slerp(LeftAnt.transform.rotation, Quaternion.LookRotation(movement), leftAntPickuper.rotSpeed);
                    LeftAnt.transform.Translate(LeftAnt.transform.forward * leftAntPickuper.speed * Time.deltaTime, Space.World);
                }
                else
                {
                    moving = false;
                }
                LeftAnt.transform.GetChild(0).GetComponent<Animator>().SetBool("IsWalking", (moveVertical != 0 || moveHorizontal != 0));
            }
            else if (Input.GetAxis(leftStick) == 1)
            {
                if (spawner.GetComponent<SpawnTracker>().ants.Count == 0)
                {
                    GameObject newAnt = Instantiate(antPrefab, spawner.transform.position, antPrefab.transform.rotation);
                    newAnt.name = "LeftAnt" + PlayerNum;
                    LeftAnt = newAnt;
                    LeftAnt.GetComponent<Pickuper>().InteractButton = leftBumper;
                    LeftAnt.GetComponent<Pickuper>().owningPlayer = PlayerNum;
                    var track = gameCam.GetComponent<TrackObject>();
                    if (track) track.targets.Add(newAnt);
                } else
                {
                    if (!cantSpawn.isPlaying)
                    {
                        cantSpawn.Play();
                    }
                }
            }
            // Move right ant
            if (RightAnt != null)
            {
                float moveHorizontal = Input.GetAxis(rightHorizontalAxis);
                float moveVertical = Input.GetAxis(rightVerticalAxis);

                Vector3 movement = new Vector3(moveVertical, 0f, moveHorizontal);
                if (camAngle > 45f && camAngle <= 135f && camLook.x > 0)
                {
                    movement = new Vector3(moveVertical, 0f, -moveHorizontal);
                }
                else if (camAngle > 45f && camAngle <= 135f && camLook.x <= 0F)
                {
                    movement = new Vector3(-moveVertical, 0f, moveHorizontal);
                }
                else if (camAngle <= 45f)
                {
                    movement = new Vector3(moveHorizontal, 0f, moveVertical);
                }
                else if (camAngle > 135f)
                {
                    movement = new Vector3(-moveHorizontal, 0f, -moveVertical);
                }

                if (rightAntPickuper)
                {
                    rightAntPickuper.Motion = movement;
                }

                if ((moveHorizontal != 0 || moveVertical != 0) && rightAntPickuper.canMove)
                {
                    moving = true;
                    RightAnt.transform.rotation = Quaternion.Slerp(RightAnt.transform.rotation, Quaternion.LookRotation(movement), rightAntPickuper.rotSpeed);
                    RightAnt.transform.Translate(RightAnt.transform.forward * rightAntPickuper.speed * Time.deltaTime, Space.World);
                }
                else
                {
                    moving = false;
                }
                RightAnt.transform.GetChild(0).GetComponent<Animator>().SetBool("IsWalking", (moveVertical != 0 || moveHorizontal != 0));
            }
            else if (Input.GetAxis(rightStick) == 1)
            {
                if (spawner.GetComponent<SpawnTracker>().ants.Count == 0)
                {
                    GameObject newAnt = Instantiate(antPrefab, spawner.transform.position, antPrefab.transform.rotation);
                    newAnt.name = "RightAnt" + PlayerNum;
                    RightAnt = newAnt;
                    RightAnt.GetComponent<Pickuper>().InteractButton = rightBumper;
                    RightAnt.GetComponent<Pickuper>().owningPlayer = PlayerNum;
                    var track = gameCam.GetComponent<TrackObject>();
                    if (track) track.targets.Add(newAnt);
                }
                else
                {
                    if (!cantSpawn.isPlaying)
                    {
                        cantSpawn.Play();
                    }
                }
            }
        }
    }
}
