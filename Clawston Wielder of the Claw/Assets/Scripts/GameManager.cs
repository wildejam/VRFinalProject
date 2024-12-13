using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float time = 180;

    public InputActionReference triggerLeft;
    public InputActionReference triggerRight;

    [SerializeField]
    private GameObject receptacle;
    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    private GameObject scoreTextObj;
    [SerializeField]
    private GameObject timeTextObj;
    [SerializeField]
    private GameObject instructionsTextObj;

    private TextMeshPro scoreText;
    private TextMeshPro timeText;
    private TextMeshPro instructionsText;

    private TimeSpan timeSpan;

    private void Start()
    {
        Time.timeScale = 1;
        scoreText = scoreTextObj.GetComponent<TextMeshPro>();
        timeText = timeTextObj.GetComponent<TextMeshPro>();
        instructionsText = instructionsTextObj.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        time -= Time.deltaTime;
        timeSpan = TimeSpan.FromSeconds(time);
        timeText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        scoreText.text = score.ToString("0");

        float leftTriggerHeld = triggerLeft.action.ReadValue<float>();
        float rightTriggerHeld = triggerRight.action.ReadValue<float>();

        if (leftTriggerHeld > 0.5 && rightTriggerHeld > 0.5)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (time <= 0)
        {
            Time.timeScale = 0;
            time = 0;
            instructionsText.text = "Time's Up!\n Score: " + scoreText.text + "\n Press both triggers to restart";
        }
    }
}
