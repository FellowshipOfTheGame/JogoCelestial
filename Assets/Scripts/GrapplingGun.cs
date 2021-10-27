using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private SpringJoint2D springJoint2D;
    [SerializeField] private GrapplingRope grappleRope;

    [Header("")]
    [SerializeField] private LayerMask grabbableLayer;
    [Tooltip("How far the rope goes")][SerializeField] private float maxRadius = 20f;

    [Header("Transform Refereces")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Rotation Settings")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;
    
    private enum LaunchType
    {
        Transform_Launch,   
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] private bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;


    private bool _grapple;

    private Vector2 _targetPosition;

    private void Start()
    {
        grappleRope.enabled = false;
        springJoint2D.enabled = false;

    }

    private void Update()
    {
        if (_grapple)
        {
            if (grappleRope.enabled)
            {
                RotateGun(grapplePoint, false);
            }
            else
            {
                RotateGun(_targetPosition, true);
            }

            /*if (launchToPoint && grappleRope.isGrappling)
            {
                if (launchType == LaunchType.Transform_Launch)
                {
                    Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                    Vector2 targetPos = grapplePoint - firePointDistnace;
                    gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                }
            }*/
        }
        else
        {
            RotateGun(_targetPosition, true);
        }
    }

    public void UpdateTargetPosition(Vector2 targetPosition)
    {
        _targetPosition = targetPosition;
    }
 
    public void Shoot()
    { 
        _grapple = true;
        SetGrapplePoint();  
    }

    public void Release()
    {
        _grapple = false;
        grappleRope.enabled = false;
        springJoint2D.enabled = false;

        if (launchType == LaunchType.Transform_Launch)
            playerRigidbody.gravityScale = 1;
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        Vector2 direction = (Vector3)_targetPosition - gunPivot.position; 
        var hit = Physics2D.Raycast(firePoint.position, direction.normalized, maxRadius, grabbableLayer);
        if (hit)
        {
            grapplePoint = hit.point;
            grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
            grappleRope.enabled = true;
        }
    }

    public void Grapple()
    {
        springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            springJoint2D.distance = targetDistance;
            springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                springJoint2D.autoConfigureDistance = true;
                springJoint2D.frequency = 0;
            }

            springJoint2D.connectedAnchor = grapplePoint;
            springJoint2D.enabled = true;
        }
        else // if launch to point
        {
            switch (launchType)
            {
                case LaunchType.Physics_Launch:
                    springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    springJoint2D.distance = distanceVector.magnitude;
                    springJoint2D.frequency = launchSpeed;
                    springJoint2D.enabled = true;
                    break;
                case LaunchType.Transform_Launch:
                    playerRigidbody.gravityScale = 0;
                    playerRigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gunPivot.transform.position, maxRadius);
    }
}