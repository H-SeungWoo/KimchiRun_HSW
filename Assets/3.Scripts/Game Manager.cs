using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GameState
    {
        Intro,
        Playing,
        GameOver
    }
    public static GameManager instance;
    public GameState State = GameState.Intro;

    public int Lives = 3;

    public float PlayTime = 0;

    [Header("References")]
    public GameObject IntroUI;
    public GameObject GameOverUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldSpawner;

    public Player player;

    public TMP_Text scoreText;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        IntroUI.SetActive(true);
        GameOverUI.SetActive(false);
        EnemySpawner.SetActive(false);
        FoodSpawner.SetActive(false);
        GoldSpawner.SetActive(false);
    }

    float CalculateScore()
    {
        return Time.time - PlayTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        if(State != GameState.Playing )
        {
            return 1f;
        }
        float speed = 1 + (0.25f * Mathf.FloorToInt(CalculateScore() / 5f));
        return Mathf.Min(20, speed);
    }
    // Update is called once per frame
    void Update()
    {
        if(State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        }
        else if (State == GameState.GameOver)
        {
            scoreText.text = "High Scroe: " + GetHighScore();
        }

        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            GameOverUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldSpawner.SetActive(true);
            PlayTime = Time.time;
        }

        if (State == GameState.Playing && Lives <= 0)
        {
            State = GameState.GameOver;
            player.DiePlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldSpawner.SetActive(false);
            GameOverUI.SetActive(true);
            SaveHighScore();
        }

        if(State == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
