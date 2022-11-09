using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public int HP = 1000;
    public BoardManager padre;
    

    private void Awake()
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            HP -= int.Parse(collision.gameObject.name);
            //Debug.Log("Hit by a bullet, new HP "+ HP);
            Destroy(collision.gameObject);
            if (HP < 0)
            {
                
                if(gameObject.tag== "PowerSource")
                {
                padre.Fin_Juego();
                }else
                {
                    //borrar luego
                    gameObject.GetComponent<Player>().Morir();
                    if (gameObject.tag == "Player")
                    {
                       padre.Cunidad--;
                    }
                    if (padre.Cunidad <= 0)
                    {
                        Debug.Log("askadkn");
                        padre.Fin_Juego();
                    }

                }

                Destroy(this.gameObject);
            }
        }
    }
}
