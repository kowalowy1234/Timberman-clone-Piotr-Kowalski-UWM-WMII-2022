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
    if (Physics2D.Raycast(transform.position, transform.up * (-2)).collider == null)
    {
      transform.position += new Vector3(0f, -2f, 0f);
    }
    Debug.DrawRay(transform.position, transform.up * (-2), Color.white);
  }

}
