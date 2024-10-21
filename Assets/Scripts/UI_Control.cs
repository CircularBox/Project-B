using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Control : MonoBehaviour
{
    Player player;
    TextMeshProUGUI distanceText;
    TextMeshProUGUI highScoreText;

    GameObject deathScreen;
    TextMeshProUGUI distanceResult;

    private Score_Saving scoreSaving;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();
        highScoreText = GameObject.Find("HighScoreText").GetComponent<TextMeshProUGUI>();

        distanceResult = GameObject.Find("DistanceResult").GetComponent<TextMeshProUGUI>();

        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);

        scoreSaving = FindObjectOfType<Score_Saving>();
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + "m";

        if (player.dead)
        {
            deathScreen.SetActive(true);
            distanceResult.text = distance + "m";
            UpdateHighScoreText();
        }
    }

    private void UpdateHighScoreText()
    {
        if (scoreSaving != null)
        {
            highScoreText.text = "High Score: " + Mathf.FloorToInt(scoreSaving.GetHighScore()) + "m";
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Test Level");
    }
}
