using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    FMOD.Studio.EventInstance hookshotSound;
    [Header("Scripts Ref:")]
    public GrapplingRope grappleRope;

    [Header("Layers Settings:")]
    public int GroundLayerNumber;


    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;



    [Header("Launching:")]
    [SerializeField, Range(0, 10)] public float launchSpeed;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;

    [Header("Max Distance:")]
    [SerializeField, Range(0, 15)] public float maxDistance;
    public bool onDistance;
    [HideInInspector] public bool haveGrapple = false;
    [HideInInspector] public string haveGrappleKey = "haveGrapple";


    private void Start(){

        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;

        //carrega se tem grapple ou nao
        if (PlayerPrefs.GetInt(haveGrappleKey, 0) == 0)
            haveGrapple = false;
        else
            haveGrapple = true;
    }

    private void Update(){

    }

    public void GrappleHook(){
        SetGrapplePoint();
    }

    public void GrappleHookCancel(){
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;
    }


    void SetGrapplePoint(){
        //permitir movimentação somente na horizontal
        //movimentação irá ocorrer somente na direção em que o player estiver olhando
        Vector2 distanceVector = new Vector2(1f, 0.0f);
        if(gunHolder.rotation.y == 0)
            distanceVector.x = 5f;
        if(gunHolder.rotation.y == -1)
            distanceVector.x = -5f;
                  

        if (Physics2D.Raycast(firePoint.position, distanceVector.normalized)){
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector.normalized);
            
            if (_hit.transform.gameObject.layer == GroundLayerNumber){
                var dist = Vector2.Distance(_hit.point, firePoint.position);
            
            //para colocar um limite no tamanho da corda
            if(dist <= maxDistance)   {
                grapplePoint = _hit.point;
                onDistance = true;
                grappleRope.enabled = true;
            }else{
                var diff = dist - maxDistance;
                grapplePoint= _hit.point;
                        
                //para limitar o tamanho da corda
                if(gunHolder.rotation.y == -1)
                    grapplePoint.x = _hit.point.x + diff;
                else
                    grapplePoint.x = _hit.point.x - diff;

                onDistance = false;
                grappleRope.enabled = true;
                    
                }     
            }
        }
    }

    public void Grapple() {
        hookshotSound = FMODUnity.RuntimeManager.CreateInstance("event:/Jogo Celestial/SFX/hookshotsound");
        hookshotSound.start();
        hookshotSound.release();
        if (onDistance){
            m_springJoint2D.connectedAnchor = grapplePoint;
            Vector2 firePointDistanceVector = firePoint.position - gunHolder.position;
            m_springJoint2D.distance = firePointDistanceVector.magnitude;
            m_springJoint2D.frequency = launchSpeed;
            m_springJoint2D.enabled = true;
            hookshotSound.setParameterByName("grip", 1);
        }
        else
            StartCoroutine(Desativar());
        
    }

    //tempo limite para a corda aparecer quando a distancia for maior que a corda
    IEnumerator Desativar() {
        yield return new WaitForSeconds(0.4f);
        
        grappleRope.enabled = false;
        StopCoroutine(Desativar());
    }
}
