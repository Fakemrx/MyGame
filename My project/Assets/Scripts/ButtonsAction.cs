using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsAction : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject playPanel;
    public GameObject pauseButton;
    public GameObject hpBar;
    public GameObject dynamicJoystick;
    public GameObject boss;
    public GameObject player;
    private bool isPaused = false;

    //private void Update()
    //{
    //    if (isPaused == false)
    //    {
    //        Time.timeScale = 0;
    //        pausePanel.SetActive(true);
    //        isPaused = true;
    //    }
    //}
    public void PauseOn()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void StartTheGame()
    {
        GetComponent<Animation>().Play("CameraAnimate");
        Time.timeScale = 1;
        playPanel.SetActive(false);
        pauseButton.SetActive(true);
        hpBar.SetActive(true);
        dynamicJoystick.SetActive(true);
        boss.SetActive(true);
    }

    public void ReturnHome()
    {
        player.transform.position = new Vector3(0, 0.3f, -9.95f);
        transform.position = new Vector3(0, 4, 6.86f);
        transform.rotation = Quaternion.Euler(8.97f,180,0);
        playPanel.SetActive(true);
        hpBar.SetActive(false);
        dynamicJoystick.SetActive(false);
        boss.SetActive(false);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }


}
