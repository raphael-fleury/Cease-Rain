using UnityEngine;

[System.Serializable]
public struct Shooter
{
    public GameObject prefab;
    public Transform output;
    public float speed;
        
    public GameObject Instantiate()
    {
        return GameObject.Instantiate(prefab,
            output.position, Quaternion.identity);
    }

    public GameObject Shoot(Vector2 force)
    {
        GameObject shot = Instantiate();
        shot.GetComponent<Rigidbody2D>().AddForce(force * speed);
        return shot;
    }
}
