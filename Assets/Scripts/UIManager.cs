using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject[] pauseObjects;
    private int gameJoinsDisabled = 0;
    private GameObject menuSpawnHintLeft;
    private GameObject menuSpawnHintRight;
    private GameObject menuSpawnHintTopLeft;
    private GameObject menuSpawnHintTopRight;
    private GameObject menuJoinHintLeft;
    private GameObject menuJoinHintRight;
    private GameObject menuJoinHintTopLeft;
    private GameObject menuJoinHintTopRight;
    private bool calledStartMessages;
    private GameObject sayingPicture;
    private GameObject sayingText;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        hidePaused();
        calledStartMessages = false;
        sayingPicture = GameObject.Find("SayingPicture");
        sayingText = GameObject.Find("SayingText");
        menuSpawnHintLeft = GameObject.Find("SpawnLeft");
        menuSpawnHintRight = GameObject.Find("SpawnRight");
        menuSpawnHintTopLeft = GameObject.Find("SpawnTopLeft");
        menuSpawnHintTopRight = GameObject.Find("SpawnTopRight");
        menuJoinHintLeft = GameObject.Find("JoinLeft");
        menuJoinHintRight = GameObject.Find("JoinRight");
        menuJoinHintTopLeft = GameObject.Find("JoinTopLeft");
        menuJoinHintTopRight = GameObject.Find("JoinTopRight");
        sayingPicture.SetActive(false);
        sayingText.SetActive(false);
        menuSpawnHintLeft.SetActive(false);
        menuSpawnHintRight.SetActive(false);
        menuSpawnHintTopLeft.SetActive(false);
        menuSpawnHintTopRight.SetActive(false);

        GameObject.Find("GrabHint").transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject eventSystem = GameObject.Find("EventSystem");
        MapPlayerToController manager = GameObject.Find("GameManager").GetComponent<MapPlayerToController>();
        MapPlayerToController controllers = gameObject.GetComponent<MapPlayerToController>();
        PlayerControls controls;

        if ((Input.GetButtonDown("J1Menu") && controllers.controllers.Contains(1))
            || (Input.GetButtonDown("J2Menu") && controllers.controllers.Contains(2))
            || (Input.GetButtonDown("J3Menu") && controllers.controllers.Contains(3))
            || (Input.GetButtonDown("J4Menu") && controllers.controllers.Contains(4))
            || (Input.GetButtonDown("J5Menu") && controllers.controllers.Contains(5)))
        {
            if (Input.GetButtonDown("J1Menu"))
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(1) + 1)).GetComponent<PlayerControls>();
                StandaloneInputModule input = eventSystem.GetComponent<StandaloneInputModule>();
                input.horizontalAxis = controls.leftHorizontalAxis;
                input.verticalAxis = controls.leftVerticalAxis;
                input.submitButton = controls.aButton;
                input.cancelButton = controls.bButton;
            }
            else if (Input.GetButtonDown("J2Menu"))
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(2) + 1)).GetComponent<PlayerControls>();
                StandaloneInputModule input = eventSystem.GetComponent<StandaloneInputModule>();
                input.horizontalAxis = controls.leftHorizontalAxis;
                input.verticalAxis = controls.leftVerticalAxis;
                input.submitButton = controls.aButton;
                input.cancelButton = controls.bButton;
            }
            else if (Input.GetButtonDown("J3Menu"))
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(3) + 1)).GetComponent<PlayerControls>();
                StandaloneInputModule input = eventSystem.GetComponent<StandaloneInputModule>();
                input.horizontalAxis = controls.leftHorizontalAxis;
                input.verticalAxis = controls.leftVerticalAxis;
                input.submitButton = controls.aButton;
                input.cancelButton = controls.bButton;
            }
            else if (Input.GetButtonDown("J4Menu"))
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(4) + 1)).GetComponent<PlayerControls>();
                StandaloneInputModule input = eventSystem.GetComponent<StandaloneInputModule>();
                input.horizontalAxis = controls.leftHorizontalAxis;
                input.verticalAxis = controls.leftVerticalAxis;
                input.submitButton = controls.aButton;
                input.cancelButton = controls.bButton;
            }
            else if (Input.GetButtonDown("J5Menu"))
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(5) + 1)).GetComponent<PlayerControls>();
                StandaloneInputModule input = eventSystem.GetComponent<StandaloneInputModule>();
                input.horizontalAxis = controls.leftHorizontalAxis;
                input.verticalAxis = controls.leftVerticalAxis;
                input.submitButton = controls.aButton;
                input.cancelButton = controls.bButton;
            }
            pauseControl();
        }

        var numPlayers = 0;
        var player1 = GameObject.Find("Player1");
        var player2 = GameObject.Find("Player2");
        if (player1) numPlayers++;
        if (player2) numPlayers++;
        if(numPlayers != gameJoinsDisabled) playerJoin(numPlayers);


        GameObject menuSpawnHint;
        for(int i = 1; i <= numPlayers; i++)
        {
            if (manager.controllers.IndexOf(i) >= 0)
            {
                controls = GameObject.Find("Player" + (manager.controllers.IndexOf(i) + 1)).GetComponent<PlayerControls>();
                if (controls.LeftAnt != null || controls.RightAnt != null)
                {
                    switch (controls.PlayerNum)
                    {
                        case 1:
                            menuSpawnHintTopLeft.SetActive(false);
                            break;
                        case 2:
                            menuSpawnHintTopRight.SetActive(false);
                            break;
                        case 3:
                            menuSpawnHintLeft.SetActive(false);
                            break;
                        case 4:
                            menuSpawnHintRight.SetActive(false);
                            break;
                    }
                }
                else
                {
                    switch (controls.PlayerNum)
                    {
                        case 1:
                            menuSpawnHintTopLeft.SetActive(true);
                            break;
                        case 2:
                            menuSpawnHintTopRight.SetActive(true);
                            break;
                        case 3:
                            menuSpawnHintLeft.SetActive(true);
                            break;
                        case 4:
                            menuSpawnHintRight.SetActive(true);
                            break;
                    }
                }
            }
        }

        if (SceneManager.GetActiveScene().name == "Level0" && calledStartMessages == false)
        {
            calledStartMessages = true;
            StartCoroutine(startMessages());
        }
    }

    IEnumerator startMessages()
    {
        sayingPicture.SetActive(true);
        sayingText.SetActive(true);
        var sayingPictureImage = sayingPicture.GetComponentsInChildren<Image>()[0];
        var sayingTextImage = sayingText.GetComponentsInChildren<Image>()[0];
        if (sayingPicture || sayingText)
        {
            var delay = 4;
            sayingPictureImage.sprite = Resources.Load<Sprite>("Sprites/soldierB");
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier1");

            yield return new WaitForSeconds(delay);

            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier2");

            yield return new WaitForSeconds(delay);
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier3");

            yield return new WaitForSeconds(delay);
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier4");

            yield return new WaitForSeconds(delay);
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier5");

            yield return new WaitForSeconds(delay);
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier6");

            yield return new WaitForSeconds(delay);

            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/soldier7");

            yield return new WaitForSeconds(delay);

            sayingPictureImage.sprite = Resources.Load<Sprite>("Sprites/queenB");
            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/queen1");

            yield return new WaitForSeconds(delay);

            sayingTextImage.sprite = Resources.Load<Sprite>("Sprites/queen2");

            yield return new WaitForSeconds(delay);

            sayingPicture.SetActive(false);
            sayingText.SetActive(false);
        }
    }

    private void playerJoin(int playerNum)
    {
        switch (playerNum)
        {
            case 1:
                menuJoinHintLeft.SetActive(false);
                menuSpawnHintLeft.SetActive(true);
                break;
            case 2:
                menuJoinHintRight.SetActive(false);
                menuSpawnHintRight.SetActive(true);
                break;
            case 3:
                menuJoinHintTopLeft.SetActive(false);
                menuSpawnHintTopLeft.SetActive(true);
                break;
            case 4:
                menuJoinHintTopRight.SetActive(false);
                menuSpawnHintTopRight.SetActive(true);
                break;
        }
        gameJoinsDisabled = playerNum;
    }


    public void reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void  pauseControl()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        }
    }

    public void hidePaused()
    {
        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(false);
        }
    }

    public void showPaused()
    {
        foreach (GameObject obj in pauseObjects)
        {
            obj.SetActive(true);
        }
    }
}
