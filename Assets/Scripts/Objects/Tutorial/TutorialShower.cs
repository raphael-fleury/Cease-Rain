using UnityEngine;

public class TutorialShower : MonoBehaviour
{
    #region Fields
    [Header("Status")]
    [SerializeField] State state;

    [Header("Options")]
    [SerializeField] float minToxicity;
    [SerializeField] float speed;
    [SerializeField] Limits limits;

    [Header("References")]
    [SerializeField] Tutorial tutorial;
    [SerializeField] GameObject water;
    #endregion

    enum State
    {
        Returning = -1,
        Idle = 0,
        Going = 1
    }

    #region Actions
    private void OnEnable()
    {
        water.SetActive(false);
        state = State.Going;
    }

    void TurnOn()
    {
        water.SetActive(true);
        state = State.Idle;
    }

    void Return()
    {
        water.SetActive(false);
        state = State.Returning;
    }

    private void OnDisable()
    {
        tutorial.NextEvent();
        state = State.Idle;
    }
    #endregion

    void FixedUpdate()
    {
        int compareToLimit = limits.Compare(transform.position.x);

        if (state == State.Idle)
        {
            if (Marjory.instance.toxicity > minToxicity)
                Return();
        }
        else
        {
            if (compareToLimit == 1 && state == State.Going)
                TurnOn();
            else if (compareToLimit == -1 && state == State.Returning)
                gameObject.SetActive(false);
            else
                transform.Translate(speed * (int)state, 0, 0);
        }
    }
}
