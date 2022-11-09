using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public int HP = 1000;
    BoardManager M;

    private void Awake()
    {

        M = GameObject.FindGameObjectWithTag("Manager").GetComponent<BoardManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == "Bullet")
        {
            
            HP -= int.Parse(collision.gameObject.name);
            //Debug.Log("Hit by a bullet, new HP "+ HP);
            Destroy(collision.gameObject);
            if (HP < 0)
            {
                
                if(gameObject.tag== "PowerSource")
                {
                GameManager.Instance.UpdateGameState(GameManager.GameStateEnum.end);
                }else
                {
                    //borrar luego
                    gameObject.GetComponent<Player>().Morir();
                    if (gameObject.tag == "Player")
                    {
                       M.Cunidad--;
                    }
                    if (M.Cunidad <= 0)
                    {
                        Debug.Log("askadkn");
                        GameManager.Instance.UpdateGameState(GameManager.GameStateEnum.end);
                    }

                }

                Destroy(this.gameObject);
            }
        }
    }
}
