using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Script usado para item Serra
/// 
/// Para usar a serra:
/// -Coloca os Prehubs da Serra e da FimDaSerra
/// -Prehub FimDaSerra serve para determinar onde a serra muda de caminho
/// </summary>
public class SerraSc : MonoBehaviour
{
    //Parametros de movimento
    [Header("Tempo de espera no canto")] public float waitTime;
    [Header("Velocidade de movimento")] public float bladeVelocity;
    //direita / cima = 1
    //esquerda / baixo = -1
    [Header("Direcao de movimento inicial")] public float direction;
    [Header("Se vai na horizontal")] public bool horizontalDirection;
    
    //Parametros de rotacao
    [Header("Velocidade de rotacao")] public float rotateSpeed;
    //horaria = 1
    //antihoraria = -1
    [Header("Sentido da rotacao")] public int rotateDirec;

    private Rigidbody2D body2d;
    private Transform trans;
    private float waitTimeCounter;
    private bool stopped = false;

    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        trans = GetComponentInChildren<Transform>();

        waitTimeCounter = waitTime;
    }

    void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        if (stopped)
        {
            waitTimeCounter -= Time.deltaTime;
            if (waitTimeCounter <= 0) stopped = false;
        }

        trans.Rotate(new Vector3(0, 0, rotateDirec * rotateSpeed));
    }

    void Move()
    {

        if (!stopped)
        {
            if (horizontalDirection)
            {
                body2d.velocity = new Vector2(direction * bladeVelocity, body2d.velocity.y);
            }
            else
            {
                body2d.velocity = new Vector2(body2d.velocity.x, direction * bladeVelocity);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("FimDaSerra"))
        {
            direction *= -1;
            if (horizontalDirection)
            {
                body2d.velocity = new Vector2(0, 0);
            }
            else
            {
                body2d.velocity = new Vector2(0, 0);
            }
        }
            stopped = true;
        }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        if (collisionInfo.CompareTag("FimDaSerra"))
        {
            waitTimeCounter = waitTime;
            stopped = false;
        } 
    }

}