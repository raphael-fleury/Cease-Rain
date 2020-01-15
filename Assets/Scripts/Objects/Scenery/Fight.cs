using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawn
    {
        public GameObject enemy;
        public Transform pos;
    }

    #region Camera
    new CustomCamera camera;
    Limits cameraLimits;
    #endregion

    [Header("Status")]
    public bool active;
    public List<GameObject> enemies;

    [Header("Options")]
    public List<EnemySpawn> enemiesToSpawn;
    public GameObject colliders;
    public Limits limits;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && !active)
            Go();
    }

    void Go()
    {
        active = true;
        if (colliders)
            colliders.SetActive(true);

        foreach(EnemySpawn e in enemiesToSpawn) 
            enemies.Add(Instantiate(e.enemy, e.pos.position, Quaternion.identity));

        foreach(GameObject e in enemies) 
            e.GetComponent<Enemy>().fight = this;

        camera = Level.activeCamera.GetComponent<CustomCamera>();
        if (camera) 
        {
            cameraLimits = camera.limits;
            camera.limits = limits;
        }
    }

    public void EnemyDeath(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0) 
            Finish();
    }

    void Finish()
    {
        camera.limits = cameraLimits;
        Destroy(gameObject);
    }
}
