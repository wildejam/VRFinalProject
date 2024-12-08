using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float time = 0;

    [SerializeField]
    private GameObject receptacle;
    [SerializeField]
    private GameObject scoreboard;
    [SerializeField]
    private GameObject scoreTextObj;
    [SerializeField]
    private GameObject timeTextObj;

    private TextMeshPro scoreText;
    private TextMeshPro timeText;

    private void Start()
    {
        scoreText = scoreTextObj.GetComponent<TextMeshPro>();
        timeText = timeTextObj.GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("0.00");
        scoreText.text = score.ToString("0");
    }

}
