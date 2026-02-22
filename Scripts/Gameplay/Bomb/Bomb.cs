using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private GameObject bombVfx;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (GameManager.Instance.IsGameFinish) return;

        if (other.CompareTag("Ball"))
        {
            Destroy(other.gameObject);
            StartCoroutine(Explosion());
        }
    }

    public IEnumerator Explosion()
    {      
        Instantiate(bombVfx, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(1.3f);
        GameManager.Instance.OnFailAreaEntered();
        Destroy(gameObject); 
    }



}
