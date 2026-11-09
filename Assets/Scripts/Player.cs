using UnityEngine;

[DisallowMultipleComponent]
public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 50f;
    [SerializeField] private float _turnSpeed = 60f;
    [SerializeField] private Thruster[] _thruster;
    [SerializeField] private Laser[] _laser;

    private void Update()
    {
        Fire();
        Turn();
        Move();
    }

    private void Fire()
    {
        if (Input.GetButton("Fire1"))
        {
            foreach (var laser in _laser)
            {
                laser.FireLaser();
            }
        }
    }

    private void Turn()
    {
        float pitch = _turnSpeed * Time.deltaTime * Input.GetAxis("Vertical");
        float yaw = _turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");

        float roll = 0f;

        if (Input.GetKey(KeyCode.Q))
        {
            roll = _turnSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.E))
        {
            roll = -_turnSpeed * Time.deltaTime;
        }

        transform.Rotate(pitch, yaw, roll);
    }

    private void Move()
    {
        float throttle = Input.GetAxis("Fire3");

        if (throttle > 0)
        {
            transform.position +=
                _movementSpeed
                * throttle
                * Time.deltaTime
                * transform.forward;

            SetThrusterIntensity(throttle);
        }
        else
        {
            SetThrusterIntensity(0f);
        }
    }

    private void SetThrusterIntensity(float intensity)
    {
        foreach (var thruster in _thruster)
        {
            thruster.Intensity(intensity);
        }
    }
}