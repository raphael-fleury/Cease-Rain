public class DeathScreen : LevelScreen
{
    private void Start() =>
        Marjory.instance.life.OnDeathEvent += () => gameObject.SetActive(true);
}
