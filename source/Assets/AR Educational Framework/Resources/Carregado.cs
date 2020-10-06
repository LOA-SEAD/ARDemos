using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsImpL;
using UnityEngine.UI;

public class Carregado : MonoBehaviour
{
    public ObjectImporterUI importer;
    public Button start;
    public Button HowToPlay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (importer.imported)
        {
            start.interactable = true;
            HowToPlay.interactable = true;
        }
            
    }
}
