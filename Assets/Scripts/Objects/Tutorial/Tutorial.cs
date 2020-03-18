using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] int eventIndex = 0;

    [Header("References")]
    public IndicatorArrow arrow;
    [SerializeField] UnityEvent[] events;

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

    public void ActivateArrow(Vector3 position, Vector2 pointingAt)
    {
        arrow.gameObject.SetActive(true);
        arrow.transform.position = position;
        arrow.PointAt(pointingAt);
    }

    void Start()
    {
        events[eventIndex].Invoke();
    }
}
