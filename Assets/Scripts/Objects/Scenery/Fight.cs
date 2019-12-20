using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    [System.Serializable]
    public class EnemySpawn
    {
        public Enemy enemy;
        public Transform pos;
    }

    #region Camera
    new CustomCamera camera;
    Limits cameraLimits;
    #endregion

    [Header("Status")]
    public List<Enemy> enemies;

    [Header("Options")]
    public List<EnemySpawn> enemiesToSpawn;
    public Transform spawnHeigth;
    public Transform leftLimit;
    public Transform rightLimit;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
            Go();
    }

    void Go()
    {
        foreach(EnemySpawn e in enemiesToSpawn) 
        {
            enemies.Add(Instantiate(e.enemy, e.pos.position, Quaternion.identity));
        }

        camera = Level.activeCamera.GetComponent<CustomCamera>();
        if (camera) 
        {
            cameraLimits = camera.limits;
            camera.limits = cameraLimits;
        }
    }

    public void EnemyDeath(Enemy enemy)
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
