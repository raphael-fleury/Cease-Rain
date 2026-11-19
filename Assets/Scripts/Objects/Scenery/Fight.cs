using System.Collections.Generic;
using UnityEngine;

public class Fight : MonoBehaviour
{
    #region Fields
    new PlayerCamera camera;
    Limits cameraLimits;

    [Header("Status")]
    [SerializeField] bool active;
    [SerializeField] int enemiesAlive;

    [Header("Options")]
    [SerializeField] List<EnemySpawn> enemiesToSpawn;
    [SerializeField] GameObject colliders;
    [SerializeField] Limits limits;
    #endregion

    #region Methods
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
        {
            GameObject obj = Instantiate(e.enemy, e.pos.position, Quaternion.identity);
            obj.GetComponent<CharacterLife>().OnDeathEvent += EnemyDeath;
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

    void EnemyDeath()
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
    #endregion

    #region Structs
    [System.Serializable]
    public struct EnemySpawn
    {
        public GameObject enemy;
        public Transform pos;
    }
    #endregion
}
