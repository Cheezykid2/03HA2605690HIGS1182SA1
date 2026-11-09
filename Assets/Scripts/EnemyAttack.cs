using UnityEngine;

[System.Diagnostics.DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private Laser[] _lasers;

    private Transform _target;
    private Vector3 _hitPosition;

    private void Update()
    {
        if (!TargetPlayer()) return;

        if (TargetInFront() && HaveRaycastLineOfSight())
        {
            FireLaser();
        }
    }

    private bool TargetInFront()
    {
        Vector3 directionToTarget = _target.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        return angle < 90f;
    }

    private bool HaveRaycastLineOfSight()
    {
        Vector3 attackDirection = (_target.position - transform.position).normalized;

        foreach (var laser in _lasers)
        {
            if (Physics.Raycast(
                laser.transform.position,
                attackDirection,
                out RaycastHit hit,
                laser.Distance))
            {
                if (hit.transform.CompareTag("Player"))
                {
                    Debug.DrawRay(
                        laser.transform.position,
                        attackDirection * laser.Distance,
                        Color.green);

                    _hitPosition = hit.point;
                    return true;
                }
            }
        }

        return false;
    }

    private void FireLaser()
    {
        foreach (var laser in _lasers)
        {
            laser.FireLaser(_hitPosition, _target);
        }
    }

    private bool TargetPlayer()
    {
        if (_target == null)
        {
            var player = GameObject.FindGameObjectWithTag("Player");

            if (player != null)
            {
                _target = player.transform;
            }
        }

        return _target != null;
    }

    private string GetDebuggerDisplay()
    {
        return ToString();
    }
}