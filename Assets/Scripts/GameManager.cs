using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int score = 0;
    public static bool playing;
    public Text gameOverText;
    [SerializeField] AudioSource music_sound;

    // Start is called before the first frame update
    void Start()
    {
        playing = true;
        score = 0;
        gameOverText.text = "";
        music_sound.Play();

    }

    // Update is called once per frame
    void Update()
    {
        if (score > PlayerPrefs.GetInt("score", 0))
        {
            gameOverText.text = "Clear!";
            music_sound.Stop();
        }
        playing = true;
    }
}
