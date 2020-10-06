using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muda_x : MonoBehaviour
{
    GameObject modelo;
    public GameObject frase;
    public GameObject frase2;
    public ActivateInformation activate;
    public bool escolha;       //true, escolha = frase
    float[] values;     //para a escolha
    float[] values2;    //para o adicional
    // Start is called before the first frame update
    void Start()
    {
        modelo = GameObject.Find("ImageTarget/pasta/MudarTexto/localDoModelo");
        frase = GameObject.Find("ImageTarget/pasta/MudarTexto");
        frase2 = GameObject.Find("ImageTarget/pasta/New Text");
        values = new float[3];
        values2 = new float[3];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Muda_Escolha()
    {
        escolha = !escolha;
    }

    public void Aumenta_scala()
    {
        //modelo.transform.position.x += 0.1;
        if (escolha)
        {
            values2[0] = frase2.transform.localScale.x;
            values2[1] = frase2.transform.localScale.y;
            values2[2] = frase2.transform.localScale.z;
            values2[0] *= 1.01f;
            values2[1] *= 1.01f;
            values2[2] *= 1.01f;
            frase2.transform.localScale = new Vector3(values2[0], values2[1], values2[2]);
        }
        else
        {
            values[0] = modelo.transform.localScale.x;
            values[1] = modelo.transform.localScale.y;
            values[2] = modelo.transform.localScale.z;
            values[0] *= 1.01f;
            values[1] *= 1.01f;
            values[2] *= 1.01f;
            modelo.transform.localScale = new Vector3(values[0], values[1], values[2]);
        }
    }

    public void Reduz_scala()
    {
        //modelo.transform.position.x += 0.1;
        if (escolha)
        {
            values2[0] = frase2.transform.localScale.x;
            values2[1] = frase2.transform.localScale.y;
            values2[2] = frase2.transform.localScale.z;
            values2[0] *= 0.99f;
            values2[1] *= 0.99f;
            values2[2] *= 0.99f;
            frase2.transform.localScale = new Vector3(values2[0], values2[1], values2[2]);
        }
        else
        {
            values[0] = modelo.transform.localScale.x;
            values[1] = modelo.transform.localScale.y;
            values[2] = modelo.transform.localScale.z;
            values[0] *= 0.99f;
            values[1] *= 0.99f;
            values[2] *= 0.99f;
            modelo.transform.localScale = new Vector3(values[0], values[1], values[2]);
        }
    }
    

    public void Rotaciona_aumenta_x(GameObject Alvo)
    {
        Alvo.transform.Rotate(1f, 0, 0, Space.Self);   
    }

    public void Rotaciona_reduz_x(GameObject Alvo)
    {
        Alvo.transform.Rotate(-1f, 0, 0, Space.Self);
    }

    public void Rotaciona_aumenta_y(GameObject Alvo)
    {
        Alvo.transform.Rotate(0, 1f, 0, Space.Self);
    }

    public void Rotaciona_reduz_y(GameObject Alvo)
    {
        Alvo.transform.Rotate(0, -1f, 0, Space.Self);
    }

    public void Rotaciona_aumenta_z(GameObject Alvo)
    {
        Alvo.transform.Rotate(0, 0, 1f, Space.Self);
    }

    public void Rotaciona_reduz_z(GameObject Alvo)
    {
        Alvo.transform.Rotate(0, 0, -1f, Space.Self);
    }


    public void Move_transform_aumenta_x(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[0] += 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_transform_reduz_x(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[0] -= 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_transform_aumenta_y(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[1] += 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_transform_reduz_y(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[1] -= 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_transform_aumenta_z(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[2] += 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_transform_reduz_z(GameObject Alvo)
    {
        values[0] = Alvo.transform.localPosition.x;
        values[1] = Alvo.transform.localPosition.y;
        values[2] = Alvo.transform.localPosition.z;
        values[2] -= 0.01f;
        Alvo.transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }


    public void Move_aumenta_X_origem(GameObject Activate)

    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[0] += 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_reduz_X_origem(GameObject Activate)
    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[0] -= 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_aumenta_Y_origem(GameObject Activate)
    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[1] += 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_reduz_Y_origem(GameObject Activate)
    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[1] -= 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_aumenta_Z_origem(GameObject Activate)
    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[2] += 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

    public void Move_reduz_Z_origem(GameObject Activate)
    {
        values[0] = activate.texts3DT[0].transform.localPosition.x;
        values[1] = activate.texts3DT[0].localPosition.y;
        values[2] = activate.texts3DT[0].localPosition.z;
        values[2] -= 0.01f;
        activate.texts3DT[0].transform.localPosition = new Vector3(values[0], values[1], values[2]);
    }

}
