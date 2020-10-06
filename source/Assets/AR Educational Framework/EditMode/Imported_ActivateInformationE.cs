using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsImpL;

[ExecuteInEditMode]
public class Imported_ActivateInformationE : MonoBehaviour {

    [HideInInspector] public bool firstTime = true;//variavel para executar uma unica ves o codigo, lembrando que está sendo usado [ExecuteInEditMode]
    private GameObject marker1;
    private GameObject Object;
    private Vector3 ObjectPosition;
    private Imported_ActivateInformation[] activateInformationComponents;

    //objetos usados para importar os modelos
    private GameObject model_importer;
    private MultiObjectImporter importer_class;
    private ObjectImporterUI importer_UI;

    private void Awake()
    {
        activateInformationComponents = gameObject.GetComponents<Imported_ActivateInformation>();
    }

    // Use this for initialization
    void Start () {

        if (!Application.isPlaying)
        {
            if (firstTime)
            {
                //cria o importador de objetos
                model_importer = new GameObject("Model_Importer");
                importer_UI = model_importer.AddComponent<ObjectImporterUI>();
                importer_class = model_importer.AddComponent<MultiObjectImporter>();
                model_importer.AddComponent<PathSettings>();

                importer_UI.active_info = activateInformationComponents[0];

                //seta as variaveis para o importador funcionar
                importer_class.autoLoadOnStart = true;

                if (activateInformationComponents.Length > 0)
                {
                    for (int i = 0; i < activateInformationComponents.Length; i++)
                    {
                        //CRIAR INSTANCIA DO MARCADOR1 
                        Object resourceImageTarget1Prefab = Resources.Load("ImageTarget1Prefab");
                        if (resourceImageTarget1Prefab != null)
                        {
                            //criar instancia do marcador
                            marker1= (GameObject)GameObject.Instantiate(resourceImageTarget1Prefab);
                            marker1.name = "ImageTarget";

                            //Informar o importador, qual e o marcador
                            importer_UI.Marker = marker1;

                            //criar objeto para teste
                            Object = new GameObject();
                            Object.name = "model location";

                            //colocar objeto como filho do marcador
                            Object.transform.SetParent(marker1.transform);

                            //acrescenta automaticamente o marcador e seu objeto, nos campos da classe Imported_ActivateInformation
                            activateInformationComponents[i].Marker1 = marker1;
                            activateInformationComponents[i].MyObject = Object;

                            for (int j = 0; j < activateInformationComponents[i].Texts3D.Length; j++)
                            {
                                //CRIAR O 3D TEXT DE INFORMACAO A SER ATIVADO SOBRE O OBJETO
                                GameObject text3D = new GameObject("New Text_" + j);
                                text3D.AddComponent<TextMesh>();
                                text3D.GetComponent<TextMesh>().text = "Hello World";
                                text3D.GetComponent<TextMesh>().anchor = TextAnchor.MiddleCenter;
                                text3D.GetComponent<TextMesh>().alignment = TextAlignment.Center;
                                text3D.GetComponent<TextMesh>().fontSize = 30;
                                text3D.GetComponent<TextMesh>().font = Resources.Load("Arial", typeof(Font)) as Font;
                                text3D.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                                //ajusta posicao para ficar na mesma do objeto
                                text3D.transform.localPosition = ObjectPosition;
                                //ajusta posicao para aumentar 1.5 em Y
                                text3D.transform.SetParent(Object.transform);
                                Vector3 positionT3 = text3D.transform.localPosition;
                                positionT3.y += 1.5f;
                                text3D.transform.localPosition = positionT3;

                                //acrescenta automaticamente 3D TEXT, no campo da classe Imported_ActivateInformation
                                activateInformationComponents[i].Texts3D[j] = text3D.GetComponent<TextMesh>();
                                    //--------------
                            }

                            for (int j = 0; j < activateInformationComponents[i].Points.Length; j++)
                            {
                                //CRIAR O GAME OBJECT VAZIO PARA INDICAR O PONTO DE ORIGEM DA LINHA 
                                GameObject point = new GameObject("P1_" + j);
                                //ajusta posicao para ficar na mesma do objeto
                                point.transform.localPosition = ObjectPosition;
                                point.transform.SetParent(Object.transform);

                                //acrescenta automaticamente objeto vazio, no campo da classe Imported_ActivateInformation
                                activateInformationComponents[i].Points[j] = point;
                                    //--------------
                            }

                        }
                        else
                        {
                            Debug.LogError("AR Educational Framework Message: The GameObject Prefab: ImageTarget1Prefab is not found in the folder Resources.");
                        }
                        //CRIAR INSTANCIA DO MARCADOR2
                        Object resourceImageTarget2Prefab = Resources.Load("ImageTarget2Prefab");
                        if (resourceImageTarget2Prefab != null)
                        {
                            GameObject marker2 = (GameObject)GameObject.Instantiate(resourceImageTarget2Prefab);
                            marker2.name = "ImageTarget2";
                            Vector3 positionT = marker2.transform.localPosition;
                            positionT.x += 1.4f;
                            marker2.transform.localPosition = positionT;
                            //acrescenta automaticamente o marcador, no campo da classe Imported_ActivateInformation
                            activateInformationComponents[i].Marker2=marker2;
                        }
                        else
                        {
                            Debug.LogError("AR Educational Framework Message: The GameObject Prefab: ImageTarget2Prefab is not found in the folder Resources.");
                        }
                        //OBTER A ARCAMERA ATUAL E ACRESCENTAR ELA NO campo da classe Imported_ActivateInformation, caso de nao encontrar ela, entao enviar uma mensagem de erro
                        GameObject myARCamera = GameObject.Find("ARCamera");
                        if (myARCamera!=null)
                        {
                            activateInformationComponents[i].MyARCamera = myARCamera;
                        }
                        else
                        {
                            Debug.LogError("AR Educational Framework Message: ARCamera not Found, you need have a ARCamera in the Scene before add this class.");
                        }
                    }
                }
                else
                {
                    Debug.LogError("AR Educational Framework Message: The GameObject: " + gameObject.name + " doesn't have Imported_ActivateInformation component. you need to add it before add this class.");
                }

                firstTime = false;
            }
        }

	}
	

}
