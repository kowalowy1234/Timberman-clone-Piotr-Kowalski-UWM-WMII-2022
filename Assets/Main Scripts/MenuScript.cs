using UnityEngine;

public class MenuScript : MonoBehaviour
{
  public static MenuScript Instance;

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

  private void OnEnable()
  {
    Time.timeScale = 0;
  }

  public void Play()
  {
    gameController.StartGame();
    gameObject.SetActive(false);
  }

  public void Quit()
  {
    Application.Quit();
  }
}
