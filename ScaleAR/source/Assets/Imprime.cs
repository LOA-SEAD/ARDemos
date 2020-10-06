using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Imprime : MonoBehaviour
{
    public TextMeshProUGUI texto;
    public GameObject Label;
    public GameObject Mudar_texto;
    public ActivateInformation Activate;
    public GameObject local_do_modelo;
    public bool value;
    public GameObject image_target;
    public bool freeze =false;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {

            texto.text = "Modelo: \nPosição: " + System.Math.Round(local_do_modelo.transform.localPosition.x, 2) +
                " " + System.Math.Round(local_do_modelo.transform.localPosition.y, 2) +
                " " + System.Math.Round(local_do_modelo.transform.localPosition.z, 2) +
                "\nRotação: " + System.Math.Round(local_do_modelo.transform.localEulerAngles.x, 0) +
                " " + System.Math.Round(local_do_modelo.transform.localEulerAngles.y, 0) +
                " " + System.Math.Round(local_do_modelo.transform.localEulerAngles.z, 0) +
                "\nEscale: " + System.Math.Round(local_do_modelo.transform.localScale.x, 3) +
                "\nFrase: \nOrigem: " + System.Math.Round(Activate.Points[0].transform.localPosition.x, 2) +
                " " + System.Math.Round(Activate.Points[0].transform.localPosition.y, 2) +
                " " + System.Math.Round(Activate.Points[0].transform.localPosition.z, 2) +
                "\nPosição: " + System.Math.Round(Activate.texts3DT[0].transform.localPosition.x, 2) +
                " " + System.Math.Round(Activate.texts3DT[0].transform.localPosition.y, 2) +
                " " + System.Math.Round(Activate.texts3DT[0].transform.localPosition.z, 2) +
                "\nEscala: " + System.Math.Round(Mudar_texto.transform.localScale.x, 3);
        }
    }

    public void Stop_value()
    {
        freeze = !freeze;
    }

    public void aparece(GameObject a)
    {
        value = !value;
        if (value)
        {
            this.gameObject.SetActive(true);
        }
        else
            this.gameObject.SetActive(false);
    }
}
