using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player[] PlayerPrefab, EnemiPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab;
    public List<int[]> ubicacion;
    public Vector2 PowerSourcelocacion;
    public int dineroU, dineroE;
    private Grid grid;
    private Player player;
    [SerializeField]
    private float moveSpeed = 2f;

    private void Awake()
    {
        Instance = this;
        ubicacion = new List<int[]>();
    }
    
    public void SetupBoard()
    {
        grid = new Grid(11, 20, 1, CellPrefab);
        
        Instantiate(PowerSourcePrefab, PowerSourcelocacion, Quaternion.identity);
        
        PathManager.Instance.powerUnitLocation = new Vector2Int((int)PowerSourcelocacion.x, (int)PowerSourcelocacion.y);

        while (dineroU >= 2)
        {
            int c=Random.Range(0,PlayerPrefab.Length);
            int[] a = { Random.Range(0, 11), Random.Range(0, 5)};
            bool colocar = true;
            foreach (int[] aa in ubicacion)
            {
                if (a.Equals(aa))
                {
                    colocar = false;
                }
                
            }

                if (PlayerPrefab[c].costo <= dineroU && colocar)
                {
                    player = Instantiate(PlayerPrefab[c], new Vector2(a[0], a[1]), Quaternion.identity);
                    player.starMoving(grid, 2);
                    ubicacion.Add(a);
                    dineroU = dineroU - PlayerPrefab[c].costo;
                } 
            
        }
        while (dineroE >= 3)
        {
            int c = Random.Range(0, PlayerPrefab.Length);
            int[] a = { Random.Range(0, 11), Random.Range(15, 20) };
            bool colocar = true;
            foreach (int[] aa in ubicacion)
            {
                if (a.Equals(aa))
                {
                    
                    colocar = false;
                }
                for (int i = 0; i < aa.Length; i++)
                {
                    Debug.Log(aa[i] + "     " + a[0]);
                }
            }

            if (PlayerPrefab[c].costo <= dineroE && colocar)
            {
                player = Instantiate(EnemiPrefab[c], new Vector2(a[0], a[1]), Quaternion.identity);
                player.starMoving(grid, 2);
                ubicacion.Add(a);
                dineroE = dineroE - EnemiPrefab[c].costo;
            }

        }
        /*player = Instantiate(PlayerPrefab[0], new Vector2(0, 0), Quaternion.identity);

        player.starMoving(grid, 2);

        player = Instantiate(PlayerPrefab[0], new Vector2(8, 0), Quaternion.identity);

        player.starMoving(grid, 3);*/
    }
}
