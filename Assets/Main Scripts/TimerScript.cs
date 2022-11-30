using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
  public static TimerScript Instance;

  public bool timerStarted;
  public float timerSpeedUpAmount = 1f;
  public float timerSpeedUpInterval = 5f;
  private float currentSpeedUpAmount = 1f;
  public float maximumTime;
  private float currentTime;

  public Slider timeSlider;
  public GameController gameController;

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
    ResetSlider();
    InvokeRepeating("SpeedUpTimer", 5f, timerSpeedUpInterval);
  }

  private void Update()
  {
    if (timerStarted)
    {
      currentTime -= Time.deltaTime * currentSpeedUpAmount;
      SetSliderCurrentTime(currentTime);
    }

    if (currentTime <= 0)
    {
      gameController.GameOver();
    }
  }

  public void SetSliderMaximumTime(float maximumTime)
  {
    timeSlider.maxValue = maximumTime;
  }

  public void SetSliderCurrentTime(float currentTime)
  {
    timeSlider.value = currentTime;
  }

  public void AddTime(float time)
  {
    if (currentTime + time <= maximumTime)
    {
      currentTime += time;
      SetSliderCurrentTime(currentTime);
    }
  }

  public void ResetSlider()
  {
    currentSpeedUpAmount = 1f;
    currentTime = maximumTime / 2;
    SetSliderMaximumTime(maximumTime);
    SetSliderCurrentTime(currentTime);
  }

  public void StartTimer()
  {
    timerStarted = true;
  }

  public void StopTimer()
  {
    timerStarted = false;
    ResetSlider();
  }

  private void SpeedUpTimer()
  {
    if (timerStarted)
    {
      currentSpeedUpAmount += timerSpeedUpAmount;
    }

    Debug.Log("Timer speed up");
  }
}
