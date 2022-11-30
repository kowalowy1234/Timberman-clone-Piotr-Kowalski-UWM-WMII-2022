using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  private bool movementBlocked;
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
  }

  void LateUpdate()
  {
    newScale = transform.localScale;

    if (!movementBlocked)
    {
      if (Input.GetKeyDown(KeyCode.A))
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

      if (Input.GetKeyDown(KeyCode.D))
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

  private void Chop()
  {
    animator.SetTrigger("Chop");
    RaycastHit2D hit = Physics2D.Raycast(RaySource.position, RayDestination.position - RaySource.position, 2f);
    if (hit.collider != null)
    {
      hit.collider.gameObject.SetActive(false);
      gameController.AddScore(1);
    }
    StartCoroutine(Delay());
  }

  private IEnumerator Delay()
  {
    yield return new WaitForSeconds(0.1f);
    movementBlocked = false;
  }

  private void OnCollisionEnter2D(Collision2D other)
  {
    Destroy(gameObject);
  }
}
