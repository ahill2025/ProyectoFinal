using UnityEngine;

public class GroundControllerT5 : MonoBehaviour
{
    [SerializeField]
    private float _groundDistanceTolerance; //Float field for the ground distance tolerance (1) (delete start function before editing)
    // This is to allow slight inaccuracies not affect ground detection.
    [SerializeField]
    private LayerMask _groundLayerMask;     // Will allow us to select which layers to check against.

    private CapsuleCollider _capsuleCollider;   // Private field for the box collider of player
    // Unity uses this to detect collisions with the player

    public bool isGrounded { get; private set; }

    public float?  DistanceToGround { get; private set; } // ? is added to allow for nullable condition

    private void Awake()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();

    }


    // Update is called once per frame
    void Update()
    {
        float sphereCastRadius = _capsuleCollider.radius - 0.1f;
        Vector3 sphereCastOrigin = transform.position + new Vector3(0, _capsuleCollider.radius, 0);

        bool isGroundBelow = Physics.SphereCast(
            sphereCastOrigin,
            sphereCastRadius,
            Vector3.down,
            out RaycastHit hitInfor,
            1000,
            _groundLayerMask,
            QueryTriggerInteraction.Ignore);

        if (isGroundBelow)
        {
            DistanceToGround = transform.position.y - hitInfor.point.y;
        }
        else
        {
            DistanceToGround = null;
        }

        isGrounded = isGroundBelow && DistanceToGround <= _groundDistanceTolerance;

    }
}
