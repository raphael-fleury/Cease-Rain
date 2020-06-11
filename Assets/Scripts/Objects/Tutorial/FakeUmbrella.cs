using UnityEngine;

public class FakeUmbrella : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] GameObject dialog;

    Marjory marjory;

    void OnEnable()
    {
        marjory = Marjory.instance;
        transform.SetPositionX(marjory.transform.position.x);
    }

    void Update() => marjory.controllable = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            if (!dialog.activeSelf)
                marjory.controllable = true;

            Instantiate(prefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
