using UnityEngine;

public class WoodTriggerScript : MonoBehaviour
{
  public Transform parentTransform;

  private void OnTriggerExit2D(Collider2D other)
  {
    parentTransform.position += new Vector3(0f, -2f, 0f);
  }
}
