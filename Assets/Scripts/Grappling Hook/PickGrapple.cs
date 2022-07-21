using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//quando acerta no player, ativa o grappling
public class PickGrapple : MonoBehaviour
{
    private string playerTag = "Player";
    [SerializeField] private GrapplingGun grapGun;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(playerTag == collision.tag)
        {
            PlayerPrefs.SetInt(grapGun.haveGrappleKey, 1);
            grapGun.haveGrapple = true;
            Destroy(this.gameObject);
        }
    }
}
