using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [Header("General Refernces:")]
    public GrapplingGun grapplingGun;
    public LineRenderer m_lineRenderer;

    [Header("General Settings:")]
    [SerializeField] private int percision = 40;
    [HideInInspector] public bool isGrappling = true;
    bool strightLine = true;
    public Rigidbody2D _rigidbody;

    private void OnEnable(){
        m_lineRenderer.positionCount = percision;
        strightLine = false;

        LinePointsToFirePoint();

        m_lineRenderer.enabled = true;
    }

    private void OnDisable(){
        m_lineRenderer.enabled = false;
        isGrappling = false;
    }

    private void LinePointsToFirePoint(){
        
        for (int i = 0; i < percision; i++){
            m_lineRenderer.SetPosition(i, grapplingGun.firePoint.position);
        }
    }

    private void Update(){
        if(!grapplingGun.onDistance)
            grapplingGun.grapplePoint.y = grapplingGun.gunHolder.position.y;
        
        if(isGrappling && m_lineRenderer.enabled)
            _rigidbody.velocity = Vector2.zero;

        DrawRope();
    }

    void DrawRope(){
        if (!strightLine){
            strightLine = true;
            LinePointsToFirePoint();
        }
        else{
            if (!isGrappling){
                grapplingGun.Grapple();
                isGrappling = true;
            }

            else{
                if (m_lineRenderer.positionCount != 2) 
                    m_lineRenderer.positionCount = 2; 

                m_lineRenderer.SetPosition(0, grapplingGun.firePoint.position);
                m_lineRenderer.SetPosition(1, grapplingGun.grapplePoint);
            }
        }
    }

    //se deixar m_lineRenderer.SetPosition(alum valor, grapplingGun.grapplePoint); em todos os m_lineRenderer
    // a corda não vai aparecer em nenhum momento, deixando apaenas a animação da corda
            
}
