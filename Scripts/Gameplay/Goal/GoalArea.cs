using UnityEngine;

public class GoalArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.OnTargetReached();
        }
    }
}
