using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    public GameObject destroyedPrefab;
    // Prefab to spawn when object is destroyed

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ball"))
        {
            // Spawn destroyed prefab at the same position as the current object
            Instantiate(destroyedPrefab, transform.position, transform.rotation);

            // Destroy the current object
            Destroy(gameObject);
        }
    }
}
