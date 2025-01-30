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

    [Header("References")]
    public GameObject IntroUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldSpawner;

    public Player player;

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
        EnemySpawner.SetActive(false);
        FoodSpawner.SetActive(false);
        GoldSpawner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldSpawner.SetActive(true);
        }

        if (State == GameState.Playing && Lives <= 0)
        {
            State = GameState.GameOver;
            player.DiePlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldSpawner.SetActive(false);
        }

        if(State == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
