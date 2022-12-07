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

  private void OnEnable()
  {
    boxCollider.enabled = true;
  }

  public void DestroyWood(Vector3 forceVector)
  {
    boxCollider.enabled = false;
    rb.gravityScale = 1;
    rb.AddForce(new Vector3(forceVector.x, 0f, 0f) * 10, ForceMode2D.Impulse);
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
