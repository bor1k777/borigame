using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private int _speed = 10;
    [SerializeField] private int _jumpForce = 10;
    [SerializeField] private int _coins = 0;

    private PlayerInputActions _playerInputActions;

    private bool _canJump = false;

    private void Start()
    {
        _rigidbody= GetComponent<Rigidbody>();
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Player.Jump.performed += Jump_OnPerformed;

        _playerInputActions.Enable();
    }

    private void Jump_OnPerformed(InputAction.CallbackContext obj)
    {
        if (_canJump)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
            _canJump = false;
        }
            
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            _canJump = true;
            Debug.Log("полёг");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("collect"))
        {
            _coins++;
            Destroy(other.gameObject);
            Debug.Log("pua");
        }
    }

    private void Update()
    {
        
        Vector2 direction = _playerInputActions.Player.Move.ReadValue<Vector2>() * _speed;
        _rigidbody.linearVelocity = new Vector3(direction.x, _rigidbody.linearVelocity.y, direction.y);
    }

}
