public class DeathScreen : LevelScreen
{
    private void Start() =>
        Marjory.instance.OnDeathEvent += () => gameObject.SetActive(true);
}
