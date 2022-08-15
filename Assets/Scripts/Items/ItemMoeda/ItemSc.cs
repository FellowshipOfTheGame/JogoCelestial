using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemSc : MonoBehaviour
{
    //public
    public float force; //forca do movimento
    public float minDistance; //distancia para nao colocar mais forca no item
    public float maxGroundTime; //tempo que precisa ficar no chao para conseguir pegar o item

    //private
    private bool isFollowPlayer = false;
    private float groundTime = 0.0f;

    GameObject player;
    string playerName = "Player";
    PlayerMovement playerSc;

    Rigidbody2D body;


    
    void Start()
    {
        //player
        player = GameObject.Find(playerName);
        playerSc = player.GetComponent<PlayerMovement>();

        //componentes do item
        body = GetComponent<Rigidbody2D>();
    }

  

    private void Update()
    {
        Vector2 forceVec;
        Vector2 playerPos;
        Vector2 itemPos;
        Vector2 playerDistanceVec;
        float moveForce;
        if (isFollowPlayer)
        {
            //movimenta o item
            playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
            itemPos = new Vector2(transform.position.x, transform.position.y);
            playerDistanceVec = (playerPos - itemPos).normalized;

            moveForce = ((playerPos - itemPos).magnitude - minDistance) * force;
            if (moveForce < 0) moveForce = 0f;

            forceVec = new Vector2(playerDistanceVec.x * moveForce, playerDistanceVec.y * moveForce);
            body.AddForce(forceVec);

            //conta o tempo que o player ta no chao
            if (playerSc._onGround) groundTime += Time.deltaTime;
            else groundTime = 0f;

            //sistema de pegar o item
            if(groundTime >= maxGroundTime)
            {
                
                isFollowPlayer = false;
                this.gameObject.SetActive(false);
                GameManager.gm.coins++;

                Transform children = this.transform.parent.GetComponentInChildren<Transform>();
                for (int i = 0; i < children.childCount; i++)
                {
                    if (children.transform.GetChild(i).gameObject.name == this.gameObject.name)
                    {
                        string coinKey = "coin" + SceneManager.GetActiveScene().name + i.ToString();
                        PlayerPrefs.SetInt(coinKey, 0);
                    }
                }
                
                
            }
        }
    }



    //ve se colidiu com player
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        string playerTag = "Player";
        if (collision.tag == playerTag)
        {
            isFollowPlayer = true;
        }
    }
}
