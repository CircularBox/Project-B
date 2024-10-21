using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_Saving : MonoBehaviour
{
    private Player player;
    private float highScore;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        LoadHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            float currentDistance = player.distance;
            if (currentDistance > highScore)
            {
                highScore = currentDistance;
                SaveHighScore();
            }
        }
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetFloat("HighScore", highScore);
        PlayerPrefs.Save();
        Debug.Log("High Score Saved: " + highScore);
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        Debug.Log("High Score Loaded: " + highScore);
    }

    public float GetHighScore()
    {
        return highScore;
    }
}
