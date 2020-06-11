using UnityEngine;
using UnityEngine.Events;

public class Tutorial : MonoBehaviour
{
    [Header("Status")]
    [SerializeField] int eventIndex = -1;

    [Header("References")]
    [SerializeField] IndicatorArrow arrow;
    [SerializeField] Steam steam;

    [Space(10)]
    [SerializeField] UnityEvent[] events;

    public void NextEvent()
    {
        eventIndex++;
        events[eventIndex].Invoke();
    }

    public void End()
    {
        Destroy(Marjory.instance.gameObject);
        Game.LoadScene(SceneEnum.FirstLevel);
    }

    public void ActivateArrow(Vector3 position, Vector2 pointingAt)
    {
        arrow.gameObject.SetActive(true);
        arrow.transform.position = position;
        arrow.PointAt(pointingAt);
    }

    public void ActivateArrow(float posX, float posY, float rightX, float rightY) =>
        //ActivateArrow(new Vector3(posX, posY, 0), new Vector2(rightX, rightY));
        ActivateArrow(Vector3.one, Vector2.zero);

    public void DeactivateArrow() => arrow.gameObject.SetActive(false);

    void Start()
    {
        steam.OnDisableEvent += NextEvent;
        NextEvent();
    }
}
