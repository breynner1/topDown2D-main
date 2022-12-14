using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;
    [SerializeField] private Cell CellPrefab;
    [SerializeField] private Player[] PlayerPrefab, EnemiPrefab;
    [SerializeField] private PowerSource PowerSourcePrefab,EnvioInfo,Personaje;
    public List<int[]> ubicacion;
    public Vector2 PowerSourcelocacion;
    public int dineroU, dineroE, Cunidad;
    private Grid grid;
    private Player player;
    GameObject unidades,torres, base_central;
    public GameObject sucesor; 
    Vector3 pos;
    [SerializeField]
    private float moveSpeed = 2f;




    public void  Fin_Juego(){
        Instantiate(sucesor, new Vector3(0,0,0), Quaternion.identity);

        Destroy(gameObject);
        
    }

    private void Awake()
    {
        Instance = this;
        ubicacion = new List<int[]>();
        this.transform.position = new Vector3((int)(this.transform.position.x), (int)(this.transform.position.y), (int)(this.transform.position.z));
        
        unidades =new GameObject("Unidades");
        torres = new GameObject("Torres");
        unidades.transform.parent = this.transform;
        torres.transform.parent = this.transform;
        SetupBoard();
        
    }
    
    public void SetupBoard()
    {
        grid = new Grid(11, 20, 1, CellPrefab);
        grid.padre.transform.position = this.transform.position;
        pos = this.transform.position;
        grid.padre.transform.parent= this.transform;
        //PowerSourcelocacion=new Vector2((int)(PowerSourcelocacion.x +pos.x), (int)(PowerSourcelocacion.y +(int)pos.y));
        
        base_central = Instantiate(PowerSourcePrefab.gameObject, new Vector2((int)(PowerSourcelocacion.x +pos.x), (int)(PowerSourcelocacion.y +(int)pos.y)), Quaternion.identity);
        EnvioInfo=base_central.GetComponent<PowerSource>();
        EnvioInfo.padre = this;
        base_central.gameObject.transform.parent = this.transform;
        PathManager.Instance.powerUnitLocation = new Vector2Int((int)PowerSourcelocacion.x, (int)PowerSourcelocacion.y);
        
        
        crear_torres();
        crear_unidades ();
        
    }

    public void crear_unidades (){
        Cunidad = 0;
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
                    player = Instantiate(PlayerPrefab[c], new Vector2(a[0]+pos.x, a[1]+pos.y), Quaternion.identity);
                    player.postablero = new Vector2Int((int)pos.x, (int)pos.y);
                    
                    Personaje = player.GetComponent<PowerSource>();
                    Personaje.padre = this;
                    player.starMoving(grid, 2);
                    player.powerUnitLocation=new Vector2Int((int)PowerSourcelocacion.x, (int)PowerSourcelocacion.y);
                    ubicacion.Add(a);
                    Cunidad++;
                    dineroU = dineroU - PlayerPrefab[c].costo;
                    player.transform.parent = unidades.transform;
                } 
            
        }
        foreach (int[] aa in ubicacion)
        {
            //Debug.Log(aa[0] +"    " +aa[1]);

        }
    }

    public void crear_torres(){
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
                
            }
            if (colocar)
            {
                //Debug.Log(a[0]+"  "+a[1]);
            }
            if (PlayerPrefab[c].costo <= dineroE && colocar)
            {
                player = Instantiate(EnemiPrefab[c], new Vector2(a[0]+ pos.x, a[1]+pos.y), Quaternion.identity);
                player.postablero = new Vector2Int((int)pos.x, (int)pos.y);
                Personaje = player.GetComponent<PowerSource>();
                Personaje.padre = this;
                player.starMoving(grid, 2);
                player.powerUnitLocation=new Vector2Int((int)PowerSourcelocacion.x, (int)PowerSourcelocacion.y);
                ubicacion.Add(a);
                dineroE = dineroE - EnemiPrefab[c].costo;
                player.transform.parent = torres.transform;
            }

        }
    }
}
