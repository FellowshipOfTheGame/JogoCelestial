using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General References")]
    [SerializeField] private GrapplingGun grapplingGun;
    [SerializeField] private LineRenderer lineRenderer;

    [Header("General Settings")]
    [Tooltip("How many points in line renderer")][SerializeField] private int percision = 40;
    [Range(0, 20)] [SerializeField] private float straightenLineSpeed = 5;

    [Header("Rope Animation Settings")]
    [SerializeField] private AnimationCurve ropeAnimationCurve;
    [Range(0.01f, 4)] [SerializeField] private float StartWaveSize = 2;
    
    [Header("Rope Progression")]
    [SerializeField] private AnimationCurve ropeProgressionCurve;
    [SerializeField] [Range(1, 50)] private float ropeProgressionSpeed = 1;

    private float _moveTime = 0;
    private float _waveSize = 0;
    private bool _isGrappling = true;
    private bool _straightLine = true;

    private void OnEnable()
    {
        _moveTime = 0;
        lineRenderer.positionCount = percision;
        _waveSize = StartWaveSize;
        _straightLine = false;

        LinePointsToFirePoint();

        lineRenderer.enabled = true;
    }

    private void OnDisable()
    {
        lineRenderer.enabled = false;
        _isGrappling = false;
    }

    private void LinePointsToFirePoint()
    {
        for (int i = 0; i < percision; i++)
        {
            lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update()
    {
        _moveTime += Time.deltaTime;
        DrawRope();
    }

    void DrawRope()
    {
        if (!_straightLine)
        {
            if (lineRenderer.GetPosition(percision - 1).x == grapplingGun.grapplePoint.x)
            {
                _straightLine = true;
            }
            else
            {
                DrawRopeWaves();
            }
        }
        else
        {
            if (!_isGrappling)
            {
                grapplingGun.Grapple();
                _isGrappling = true;
            }
            if (_waveSize > 0)
            {
                _waveSize -= Time.deltaTime * straightenLineSpeed;
                DrawRopeWaves();
            }
            else
            {

                DrawStraightRope(grapplingGun.firePoint.position, grapplingGun.grapplePoint);
            }
        }
    }

    private void DrawRopeWaves()
    {
        for (int i = 0; i < percision; i++)
        {
            float delta = (float)i / ((float)percision - 1f);
            Vector2 offset = Vector2.Perpendicular(grapplingGun.grappleDistanceVector).normalized *
                ropeAnimationCurve.Evaluate(delta) * _waveSize;
            Vector2 targetPosition = Vector2.Lerp(grapplingGun.firePoint.position,
                grapplingGun.grapplePoint, delta) + offset;
            Vector2 currentPosition = Vector2.Lerp(grapplingGun.firePoint.position, targetPosition,
                ropeProgressionCurve.Evaluate(_moveTime) * ropeProgressionSpeed);

            lineRenderer.SetPosition(i, currentPosition);
        }
    }

    private void DrawStraightRope(Vector2 startPosition, Vector2 endPosition)
    {
        _waveSize = 0;

        if (lineRenderer.positionCount != 2)
            lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }
}
