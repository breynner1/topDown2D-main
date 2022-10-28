using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedN : MonoBehaviour
{
    // Start is called before the first frame update
    public int Npesos=55;
    class nose {

        static void main (string arg)
        {
            neurona p = new neurona();
            bool sw = false;
            float peso;
            p.peso = new float[55];
            while (sw)
            {
                sw = true;
                peso = Random.value;
                sw = false;
            }
        }
        

	}

    

    // Update is called once per frame
    void Update()
    {
        
    }
    public float hola (float d)
    {

        return d > 0 ? 1 : 0;
    }

    //#############################################Neurona##########################################################
    class neurona
    {
        
        public float[] peso;
        public float Umbral;

        public float neu(float[] entradas)
        {
            float sum = 0;
            for (int i = 0; i < peso.Length; i++)
            {
                sum += entradas[i] * peso[i];
            }
            sum += Umbral;
            return sum;
        }

        public float salida(float[] entradas)
        {
            return fun(neu(entradas));
        }
        public float fun(float d)
        {
            return d > 0 ? 1 : 0;
        }
    }
}
