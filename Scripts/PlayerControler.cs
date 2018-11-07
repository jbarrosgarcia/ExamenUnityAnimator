using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {
    public float speed = 2f;
    public float maxSpeed = 5f;
    private Rigidbody2D rb2d;
    private Animator anim;
    public bool grounded;
    public float jumPower = 6.5f;
    private bool jump;
    private bool doubleJump;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", Mathf.Abs(rb2d.velocity.x));//este codigo es para coger la velocidad absoluta 
        anim.SetBool("Grounded", grounded);//esto es para detectar si estas en el suelo o no

        if (grounded)
        {
            doubleJump = true;//con esto activas el doble salto solo si has tocado el suelo o lo estabas tocando
        }

        if (Input.GetKeyDown(KeyCode.Space))
         //con esto detectamos si estamos pulsando la barra espaciadora para saltar aunque podiamos usar otra tecla
        {
            if (grounded) {
                jump = true;
                doubleJump = true;
                //esto hace que podamos usar doble salto si estabamos en tierra
            }else if (doubleJump)
            {
                //aqui anulamos el doble salto al no estar tocando tierra para no saltar de forma infinita
                jump = true;
                doubleJump = false;

            }
           
        }
		}
    private void FixedUpdate()
    {
        Vector3 fixedVelocity = rb2d.velocity;
        fixedVelocity.x *= 0.75f;
        if (grounded)
        {
            rb2d.velocity = fixedVelocity;
        }
        float h = Input.GetAxis("Horizontal");
        float limitedSpeed = Mathf.Clamp(rb2d.velocity.x, -maxSpeed, maxSpeed);
        rb2d.AddForce(Vector2.right * speed * h);
        rb2d.velocity = new Vector2(limitedSpeed, rb2d.velocity.y);
        if (h > 0.1f){
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        if (h < -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (jump)
        {
            rb2d.AddForce(Vector2.up*jumPower, ForceMode2D.Impulse);
            jump = false;
        }

    }
}
