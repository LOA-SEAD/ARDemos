using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disapear : MonoBehaviour
{
    public bool value;
    public bool frase;
    public GameObject dropdown;
    // Start is called before the first frame update
    void Start()
    {
        if (value)
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
        frase = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (value)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);

        }

    }
    
    public void Changed(GameObject outro)
    {
        if (value)
        {
            outro.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
         value = !value;
    }
}
