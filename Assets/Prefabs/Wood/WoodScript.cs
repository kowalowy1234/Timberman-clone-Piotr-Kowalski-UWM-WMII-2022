using System.Collections;
using UnityEngine;

public class WoodScript : MonoBehaviour
{
  private Rigidbody2D rb;
  private BoxCollider2D boxCollider;

  private void Start()
  {
    rb = gameObject.GetComponent<Rigidbody2D>();
    boxCollider = gameObject.GetComponent<BoxCollider2D>();
  }

  private void Update()
  {
    if (Physics2D.Raycast(transform.position, transform.up * (-2)).collider == null && transform.position.y + -2f > 0f)
    {
      transform.position += new Vector3(0f, -2f, 0f);
    }
  }

  private void OnEnable()
  {
    boxCollider.enabled = true;
    gameObject.layer = 6;
  }

  public void DestroyWood(Vector3 forceVector)
  {
    gameObject.layer = 7;
    boxCollider.enabled = false;
    rb.gravityScale = 1;
    rb.AddForce(new Vector3(forceVector.x, 0f, 0f) * 10, ForceMode2D.Impulse);
    rb.AddTorque(10f);
    StartCoroutine(DelayAndDeactivate());
  }

  public IEnumerator DelayAndDeactivate()
  {
    yield return new WaitForSeconds(1f);
    rb.gravityScale = 0;
    rb.velocity = Vector3.zero;
    gameObject.SetActive(false);
  }

}
