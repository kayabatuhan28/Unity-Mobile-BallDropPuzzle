using UnityEngine;

public class Star : MonoBehaviour
{
    [SerializeField] private GameObject collectVFX;

    public float rotationSpeed = 180f;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, rotationSpeed * Time.deltaTime);
    }

    private void Collect()
    {
        if (GameManager.Instance.IsGameFinish) return;

        Instantiate(collectVFX, transform.position, Quaternion.identity);
        GameManager.Instance.AddDiamond();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            Collect();
        }
    }
}
