using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colisiondosejes : MonoBehaviour
{
    GameObject obj1, obj2;
    public float x1 = 0.0f, v1=0.0f,v2=0.0f, x2 = 0.0f, h = 0.1f, r1 = 0.0f, r2 = 0.0f, Masa_1 = 0.0f, Masa_2 = 0.0f, e = 0.0f, y1 = 0.0f, y2 = 0.0f, Angulo_1 = 0.0f, Angulo_2 = 0.0f;
    private float disx = 0.0f, disy = 0.0f, dis = 0.0f, v1xp = 0.0f, v1yp = 0.0f, v2xp = 0.0f, v2yp = 0.0f, v1xn = 0.0f, v1yn = 0.0f, v2xn = 0.0f, v2yn = 0.0f, v1n = 0.0f, v2n = 0.0f, vp1new = 0.0f, vp2new = 0.0f, v1xpnew = 0.0f, v1ypnew = 0.0f, v2xpnew = 0.0f, v2ypnew = 0.0f;
    private float angulochoque, v1p = 0.0f, v2p = 0.0f,v1x = 0.0f, v2x = 0.0f, v1y = 0.0f, v2y = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        obj1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj1.transform.localScale = new Vector3(2 * r1, 2 * r1, 2 * r1);
        obj2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj2.transform.localScale = new Vector3(2 * r2, 2 * r2, 2 * r2);
        Angulo_1 = (Angulo_1 * Mathf.PI) / 180.0f;
        Angulo_2 = (Angulo_2 * Mathf.PI) / 180.0f;
        //Velocidades movimiento
        v1x = Mathf.Cos(Angulo_1) * v1;
        v1y = Mathf.Sin(Angulo_1) * v1;
        v2x = Mathf.Cos(Angulo_2) * v2;
        v2y = Mathf.Sin(Angulo_2) * v2;
    }

    // Update is called once per frame
    void Update()
    {
        obj1.transform.position = new Vector3(x1, y1, 0f);
        obj2.transform.position = new Vector3(x2, y2, 0f);
        dis = Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2));

        if (dis > r1 + r2)
        {
            movimiento();

        }
        else if (dis <= r1 + r2)
        {
            calculos();
            colision();
            x1 = x1 + 0.1f * (v1xpnew/ v1xpnew);
            y1= y1 + 0.1f * (v1ypnew / v1ypnew);
            x2 = x2 + 0.1f * (v2xpnew / v2xpnew);
            y2 = y2 + 0.1f * (v2ypnew / v2ypnew);
            v1x = v1xpnew;
            v2x = v2xpnew;
            v1y = v1ypnew;
            v2y = v2ypnew;
        }
    }
    void movimiento()
    {
        x1 = v1x * h + x1;
        y1 = v1y * h + y1;
        x2 = v2x * h + x2;
        y2 = v2y * h + y2;
    }
    void calculos()
    {
        
        angulochoque = Mathf.Atan((y2-y1)/(x2-x1));
        v1p = v1x * Mathf.Cos(angulochoque) + v1y * Mathf.Sin(angulochoque);
        v1n = -v1x * Mathf.Sin(angulochoque) + v1y * Mathf.Cos(angulochoque);
        v2p = v2x * Mathf.Cos(angulochoque) + v2y * Mathf.Sin(angulochoque);
        v2n = -v2x * Mathf.Sin(angulochoque) + v2y * Mathf.Cos(angulochoque);
        //Velocidades rotadas
        v1xp = Mathf.Cos(angulochoque) * v1p;
        v1yp = Mathf.Sin(angulochoque) * v1p;
        v2xp = Mathf.Cos(angulochoque) * v2p;
        v2yp = Mathf.Sin(angulochoque) * v2p;
        //Velocidad post colision
        vp1new = ((Masa_1 - (e) * (Masa_2) * v1p) / (Masa_1 + Masa_2)) + ((1 + e) * (Masa_2) * v2p) / (Masa_1 + Masa_2);
        vp2new = (((1 + e) * (Masa_1) * v1p) / (Masa_1 + Masa_2)) + (((Masa_2 - (e) * (Masa_1) * v2p) / (Masa_1 + Masa_2)));
        //Rotacion inversa, velocidades nuevas
        v1xpnew = Mathf.Cos(angulochoque) * vp1new - Mathf.Sin(angulochoque)*v1n;
        v1ypnew = Mathf.Sin(angulochoque) * vp1new + Mathf.Cos(angulochoque) * v1n;
        v2xpnew = Mathf.Cos(angulochoque) * vp2new - Mathf.Sin(angulochoque) * v2n;
        v2ypnew = Mathf.Sin(angulochoque) * vp2new + Mathf.Cos(angulochoque) * v2n;
    }
    void colision()
    {
        x1 = v1xpnew * h + x1;
        y1 = v1ypnew * h + y1;
        x2 = v2xpnew * h + x2;
        y2 = v2ypnew * h + y2;

    }
}
