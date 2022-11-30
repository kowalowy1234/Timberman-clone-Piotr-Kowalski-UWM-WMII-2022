using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private int score;

  public float startingTime;
  private float timeDecreaseRate;

  // this should be a script that controls the bar, not a game object - change it
  public GameObject timeBar;

  public void AddScore(int scoreToAdd)
  {
    score += scoreToAdd;
  }

  public void ResetScore()
  {
    score = 0;
  }
}
