using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour
{
  public static GameController Instance;

  [SerializeField]
  private int score;
  public bool firstRun = true;

  public AudioSource audioSource;
  public GameObject player;
  public GameObject menu;
  public GameObject finalScoreObject;
  public Text finalScoreText;
  public Text scoreText;
  public TimerScript timer;

  public AudioClip failSound;
  public AudioClip music;

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
      DontDestroyOnLoad(gameObject);
    }
    else if (Instance != null)
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    ResetScore();
    finalScoreObject.SetActive(false);
  }

  private void Update()
  {
    if (player == null)
    {
      player = GameObject.FindGameObjectWithTag("Player");
    }
  }

  public void AddScore(int scoreToAdd)
  {
    score += scoreToAdd;
    scoreText.text = score + "";
    timer.AddTime(0.3f);
  }

  public void ResetScore()
  {
    score = 0;
    scoreText.text = score + "";
  }

  public void GameOver()
  {
    timer.StopTimer();
    timer.ResetSlider();
    Destroy(player);
    Time.timeScale = 0;
    menu.SetActive(true);
    finalScoreText.text = score + "";
    finalScoreObject.SetActive(true);
    audioSource.Stop();
    audioSource.loop = false;
    audioSource.clip = failSound;
    audioSource.Play();
  }

  public void StartGame()
  {
    SceneManager.LoadScene("SampleScene");
    ResetScore();
    Time.timeScale = 1;
    finalScoreObject.SetActive(false);
    timer.StartTimer();
    audioSource.clip = music;
    audioSource.loop = true;
    audioSource.Play();
  }
}
