using UnityEngine;

public class FakeUmbrella : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    
    Transform marjory;

    void Start()
    {
        marjory = Level.marjory.transform;
        transform.SetPositionX(marjory.position.x);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
