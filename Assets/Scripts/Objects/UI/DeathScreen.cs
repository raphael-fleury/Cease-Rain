public class DeathScreen : LevelScreen
{
    private void Start() =>
        Level.marjory.OnDeathEvent += () => gameObject.SetActive(true);
}
