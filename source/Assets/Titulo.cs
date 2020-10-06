using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Titulo : MonoBehaviour
{

    public TextMeshProUGUI credito;
    // Start is called before the first frame update
    void Start()
    {
        string texto;
        string arquivo;
        TextMeshProUGUI titulo;

        arquivo = "Title.txt";

        BetterStreamingAssets.Initialize();
        texto = BetterStreamingAssets.ReadAllText(arquivo);

        titulo = gameObject.GetComponent<TextMeshProUGUI>();
        titulo.text = texto;
    }

    void creditos()
    {
        string texto;
        string arquivo;

        arquivo = "Credits.txt";

        BetterStreamingAssets.Initialize();
        texto = BetterStreamingAssets.ReadAllText(arquivo);
        credito.text += texto;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
