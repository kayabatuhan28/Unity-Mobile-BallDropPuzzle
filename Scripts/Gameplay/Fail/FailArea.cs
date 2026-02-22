using UnityEngine;

public class FailArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.OnFailAreaEntered();
        }
    }
}
