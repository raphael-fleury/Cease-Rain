using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] int eventIndex = 0;

    public UnityEvent[] events;

    public void NextEvent()
    {
        eventIndex++;
        events[eventIndex].Invoke();
    }

    public void End()
    {
        Destroy(Level.marjory.gameObject);
        Game.LoadScene(SceneEnum.FirstLevel);
    }

    void Start()
    {
        events[eventIndex].Invoke();
    }
}
