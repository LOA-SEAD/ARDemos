using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Frase : MonoBehaviour
{
    public GameObject Rotaciona;
    public GameObject Move;
    public GameObject Dropdown_modelo;
    public GameObject Dropdown_frase;
    public bool esta_na_frase;
    public GameObject canvas;
    public GameObject controle;
    public ActivateInformation activate;
    public TMP_Dropdown aux;
    Disapear aux1, aux2;
    // Start is called before the first frame update
    void Start()
    {
        aux1 = Rotaciona.transform.GetComponent(typeof(Disapear)) as Disapear;
        aux2 = Move.transform.GetComponent(typeof(Disapear)) as Disapear;
        if (esta_na_frase)
        {
            Dropdown_frase.SetActive(true);
            Dropdown_modelo.SetActive(false);
        }
        else
        {
            Dropdown_frase.SetActive(false);
            Dropdown_modelo.SetActive(true);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Muda_flag()
    {
        
        esta_na_frase = !(gameObject.GetComponent<Toggle>().isOn);
        if (esta_na_frase)
        {
            Dropdown_frase.SetActive(true);
            Move.transform.SetParent(canvas.transform);
            
            Dropdown_modelo.SetActive(false);
            Move.SetActive(true);
        }
        else
        {
            Dropdown_modelo.SetActive(true);
            Dropdown_frase.SetActive(false);
            Move.transform.SetParent(Dropdown_modelo.transform);
            aux = Dropdown_modelo.GetComponent(typeof(TMP_Dropdown)) as TMP_Dropdown;
            
        }
    }
}
