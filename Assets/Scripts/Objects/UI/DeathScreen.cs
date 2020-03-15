public class DeathScreen : LevelScreen
{
    private void Start() =>
        Level.marjory.OnDeath += delegate { gameObject.SetActive(true); };
}
