using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGrounded : MonoBehaviour {
    private PlayerControler player;
    private Rigidbody2D rb2d;
	// Use this for initialization
	void Start () {
        player=GetComponentInParent<PlayerControler>();
        rb2d = GetComponentInParent<Rigidbody2D>();
	}

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D col)
    {
        //detecta cuando tocamos el suelo ya sea una plataforma o el suelo
        if (col.gameObject.tag == "Platform")
        {
            rb2d.velocity = new Vector3(0f, 0f, 0f);
            player.transform.parent = col.transform;
            player.grounded = true;

        }
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            player.grounded = true;
        }
        if (col.gameObject.tag == "Platform")
        {
            player.transform.parent = col.transform;
            player.grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag != "Ground")
        {
            player.grounded = false;
        }
        if (col.gameObject.tag == "Platform")
        {
            player.transform.parent = null;
            player.grounded = false;
        }
        //no deja subir los cambios
    }
}
