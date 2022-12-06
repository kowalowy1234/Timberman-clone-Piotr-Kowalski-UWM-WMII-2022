using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private bool movementBlocked = true;
  private bool canTouch;
  public Animator animator;
  public Vector3 leftPosition;
  public Vector3 rightPosition;

  public AudioSource audioSource;
  public Transform RaySource;
  public Transform RayDestination;
  public Transform WoodForcePoint;
  private Vector3 newScale;
  private GameController gameController;

  private void Start()
  {
    gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    StartCoroutine(Delay(0.1f));
  }

  void Update()
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
          // Chop();
          movementBlocked = true;
          transform.position = leftPosition;
          if (transform.localScale.x < 0)
          {
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
          }
          animator.SetTrigger("Chop");
        }

        if (touchPosition.x > RayDestination.position.x)
        {
          // Chop();
          movementBlocked = true;
          transform.position = rightPosition;
          if (transform.localScale.x > 0)
          {
            newScale.x *= (-1);
            transform.localScale = newScale;
          }
          animator.SetTrigger("Chop");
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

  public void Chop()
  {
    RaycastHit2D hit = Physics2D.Raycast(RaySource.position, RayDestination.position - RaySource.position, 2f);
    if (hit.collider != null)
    {
      // hit.collider.gameObject.SetActive(false);
      hit.collider.gameObject.GetComponent<WoodScript>().DestroyWood(WoodForcePoint.position);
      gameController.AddScore(1);
      audioSource.Play();
    }
    movementBlocked = false;
    // StartCoroutine(Delay(0.1f));
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
