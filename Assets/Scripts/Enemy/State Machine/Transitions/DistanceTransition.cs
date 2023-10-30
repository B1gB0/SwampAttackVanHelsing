using UnityEngine;

public class DistanceTransition : Transition
{
    [SerializeField] private float _transitionRange;

    private void Update()
    {
        if(Mathf.Abs(Vector2.Distance(transform.position, Target.transform.position)) < _transitionRange)
        {
            NeedTransit = true;
        }
    }
}
