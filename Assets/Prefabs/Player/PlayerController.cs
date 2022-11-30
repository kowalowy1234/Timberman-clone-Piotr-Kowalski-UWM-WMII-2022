using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private bool movementBlocked = true;
  private bool canTouch;
  public Animator animator;
  public Vector3 leftPosition;
  public Vector3 rightPosition;

  public Transform RaySource;
  public Transform RayDestination;
  private Vector3 newScale;
  private GameController gameController;

  private void Start()
  {
    gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    StartCoroutine(Delay(0.1f));
  }

  void LateUpdate()
  {
    newScale = transform.localScale;

    if (!movementBlocked && Time.timeScale > 0f)
    {
      if (Input.touchCount > 0 && canTouch)
      {
        Touch touch = Input.GetTouch(0);
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        if (touchPosition.x < RayDestination.position.x)
        {
          movementBlocked = true;
          transform.position = leftPosition;
          if (transform.localScale.x < 0)
          {
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
          }
          Chop();
        }

        if (touchPosition.x > RayDestination.position.x)
        {
          movementBlocked = true;
          transform.position = rightPosition;
          if (transform.localScale.x > 0)
          {
            newScale.x *= (-1);
            transform.localScale = newScale;
          }
          Chop();
        }
      }
    }

    if (Input.GetTouch(0).phase == TouchPhase.Ended)
    {
      canTouch = true;
    }
    else
    {
      canTouch = false;
    }
  }

  private void Chop()
  {
    animator.SetTrigger("Chop");
    RaycastHit2D hit = Physics2D.Raycast(RaySource.position, RayDestination.position - RaySource.position, 2f);
    if (hit.collider != null)
    {
      hit.collider.gameObject.SetActive(false);
      gameController.AddScore(1);
    }
    StartCoroutine(Delay(0.1f));
  }

  private IEnumerator Delay(float delayAmount)
  {
    yield return new WaitForSeconds(delayAmount);
    movementBlocked = false;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    gameController.GameOver();
  }
}
