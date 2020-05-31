using UnityEngine;

public class Jeter : Enemy, ILimits
{
    [Header("Status")]
    [SerializeField] [Range(-1,1)] int direction;

    [Header("Options")]
    [SerializeField] float speed;
    [SerializeField] Limits limits;

    void Update()
    {
        if(!transform.position.x.IsBetween(limits))
            direction = limits.Compare(transform.position.x) * -1;
        
        transform.Translate(direction * (speed / 100), 0f, 0f);
    }

    public void SetLimits(Limits limits) => this.limits.Set(limits);
}
