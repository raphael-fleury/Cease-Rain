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
    new PlayerCamera camera;
    Limits cameraLimits;
    #endregion

    [Header("Status")]
    public bool active;
    public int enemiesAlive;

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

        foreach(EnemySpawn e in enemiesToSpawn) {
            GameObject obj = Instantiate(e.enemy, e.pos.position, Quaternion.identity);
            obj.GetComponent<Character>().onDeath += EnemyDeath;
            obj.GetComponent<ILimits>().SetLimits(limits);
            enemiesAlive++;
        }


        camera = Level.activeCamera.GetComponent<PlayerCamera>();
        if (camera) 
        {
            cameraLimits = camera.limits;
            camera.limits = limits;
        }
    }

    public void EnemyDeath()
    {
        enemiesAlive--;
        if (enemiesAlive == 0) 
            Finish();
    }

    void Finish()
    {
        camera.limits = cameraLimits;
        Destroy(gameObject);
    }
}
