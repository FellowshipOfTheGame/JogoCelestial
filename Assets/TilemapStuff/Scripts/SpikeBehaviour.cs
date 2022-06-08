using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D other)
    {
        SpriteRenderer r = other.gameObject.GetComponent<SpriteRenderer>();
        if (r is null)
            return;
        r.color = Color.red;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        SpriteRenderer r = other.gameObject.GetComponent<SpriteRenderer>();
        if (r is null)
            return;
        r.color = Color.green;
    }
}
