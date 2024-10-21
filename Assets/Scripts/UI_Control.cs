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

    GameObject deathScreen;
    TextMeshProUGUI distanceResult;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<TextMeshProUGUI>();

        distanceResult = GameObject.Find("DistanceResult").GetComponent<TextMeshProUGUI>();

        deathScreen = GameObject.Find("DeathScreen");
        deathScreen.SetActive(false);

    }

    // Start is called before the first frame update
    void Start()
    {

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
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("Test Level");
    }
}
