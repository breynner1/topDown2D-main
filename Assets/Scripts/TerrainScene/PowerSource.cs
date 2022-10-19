using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSource : MonoBehaviour
{
    public int HP = 1000;

    private void Awake()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == "Bullet")
        {
            
            HP -= int.Parse(collision.gameObject.name);
            Debug.Log("Hit by a bullet, new HP "+ HP);
            Destroy(collision.gameObject);
            if (HP < 0)
            {
                Destroy(this.gameObject);
                if(this.gameObject.tag== "PowerSource")
                {
                GameManager.Instance.UpdateGameState(GameManager.GameStateEnum.end);
                }
            }
        }
    }
}
