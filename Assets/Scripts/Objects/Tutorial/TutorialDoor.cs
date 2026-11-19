using UnityEngine;

public class TutorialDoor : CharacterLife
{
    [SerializeField] bool broken;

    [Header("References")]
    [SerializeField] Tutorial tutorial;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && broken)
            tutorial.End();
    }

    protected override void Death()
    {
        broken = true;
        GetComponent<Animator>().SetTrigger("break");
    }
}