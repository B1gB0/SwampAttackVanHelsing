using UnityEngine;

public class MoveTransition : Transition
{
    [SerializeField] private Transform _head;
    [SerializeField] private float _distance;
    [SerializeField] private ContactFilter2D _filter;

    private RaycastHit2D[] _hits = new RaycastHit2D[1];
    private Vector3 _direction = new Vector3(1, 0, 0);
    private bool isFacingRight = false;

    private void Update()
    {
        int hitsCount = Physics2D.Raycast(_head.position, _direction, _filter, _hits, Mathf.Abs(_distance));
        Debug.DrawRay(_head.position, _direction, Color.red);

        if (hitsCount > 0)
        {
            NeedTransit = true;
        }
    }

    private void FixedUpdate()
    {
        if (isFacingRight == false)
        {
            Flip();
        }
        else if (isFacingRight == true)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;

        if (transform.position.x < Target.transform.position.x)
        {
            _direction *= 1;
            transform.localRotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (transform.position.x > Target.transform.position.x)
        {
            _direction *= -1;
            transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }
}