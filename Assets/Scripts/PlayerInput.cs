using System.Collections;
using UnityEngine;

public class PlayerInput : Health
{
    [Header("Flight settings")]
    [SerializeField] private float _flySpeed = 50f;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _boostTime = 3f;
    [SerializeField] private FloatingJoystick _variableJoystick;

    private Rigidbody _rb;
    private Shield _shield;
    private PlayerController _shoot;
    private const float _collisionContactForce = 1000;
    [SerializeField] private float _rotateSmooth = 0.5f;

    private float inputMoveHorizontal { get; set; }
    private float inputMoveVertical { get; set; }
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        currHealth = maxHealth;
        _shield = GetComponentInChildren<Shield>();
        _shoot = GetComponentInChildren<PlayerController>();

    }

    private void FixedUpdate()
    {
        DetectPlatform();
        Vector3 dir = new Vector3(inputMoveHorizontal * _flySpeed, inputMoveVertical * _flySpeed, _flySpeed);

        _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, _maxSpeed - _flySpeed);

        _rb.AddForce(dir * _flySpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if (inputMoveHorizontal != 0 | inputMoveVertical != 0)
        {
            transform.Rotate(-dir.y * Time.fixedDeltaTime * _rotateSmooth, 0, -dir.x * Time.fixedDeltaTime * _rotateSmooth);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, 0.05f);
        }

    }

    private void DetectPlatform()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            inputMoveHorizontal = Input.GetAxisRaw("Horizontal");
            inputMoveVertical = Input.GetAxisRaw("Vertical");
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            inputMoveHorizontal = _variableJoystick.Horizontal;
            inputMoveVertical = _variableJoystick.Vertical;

        }
    }

    public void ActiveShield()
    {
        StartCoroutine(StartShield());
    }
    public void ActiveBoost()
    {
        StartCoroutine(StartBoost());

    }
    public void Shoot()
    {
        _shoot.Shoot();
    }

    private IEnumerator StartShield()
    {
        _shield._isShieldEnabled = true;
        _shield._shieldVFX.SetActive(true);
        yield return new WaitForSeconds(_shield._shieldTime);
        _shield._shieldVFX.SetActive(false);
        _shield._isShieldEnabled = false;
    }

    private IEnumerator StartBoost()
    {
        _rb.velocity += new Vector3(0, 0, _flySpeed * 2);
        _maxSpeed *= 2;
        yield return new WaitForSeconds(_boostTime);
        _maxSpeed /= 2;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_shield._isShieldEnabled)
        {
            SetHealth(collision.collider.contactOffset * _collisionContactForce);
        }
    }
}
