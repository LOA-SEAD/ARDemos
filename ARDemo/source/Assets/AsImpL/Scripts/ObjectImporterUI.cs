using UnityEngine;
using UnityEngine.UI;

namespace AsImpL
{
    /// <summary>
    /// UI controller for <see cref="ObjectImporter"/>
    /// </summary>
    [RequireComponent(typeof(ObjectImporter))]
    public class ObjectImporterUI : MonoBehaviour
    {
        //public QuestionController1 proprio;

        [Tooltip("Text for activity messages")]
        public Text progressText;

        [Tooltip("Slider for the overall progress")]
        public Slider progressSlider;

        [Tooltip("Panel with the Image Type set to Filled")]
        public Image progressImage;

        protected ObjectImporter objImporter;

        //responsavel por saber qual a questao relacionada
        public bool imported = false;
        public GameObject first_question;
        public GameObject Marker;
        private GameObject Model;
        public Imported_ActivateInformation active_info;
        private MultiObjectImporter multiobject;

        
        private void Awake()
        {
            multiobject = gameObject.GetComponent<MultiObjectImporter>();
            if (progressSlider != null)
            {
                progressSlider.maxValue = 100f;
                progressSlider.gameObject.SetActive(false);
            }
            if (progressImage != null)
            {
                progressImage.gameObject.SetActive(false);
            }
            if (progressText != null)
            {
                progressText.gameObject.SetActive(false);
            }
            objImporter = GetComponent<ObjectImporter>();
            // TODO: check and warn
        }


        private void OnEnable()
        {
            objImporter.ImportingComplete += OnImportComplete;
            objImporter.ImportingStart += OnImportStart;
        }


        private void OnDisable()
        {
            objImporter.ImportingComplete -= OnImportComplete;
            objImporter.ImportingStart -= OnImportStart;
        }


        private void Update()
        {
            multiobject = gameObject.GetComponent<MultiObjectImporter>();
            bool loading = Loader.totalProgress.singleProgress.Count > 0;
            if (!loading) return;
            //int numTotalImports = objImporter.NumImportRequests;      parou de funcionar
            int numTotalImports = multiobject.cont;
            int numImportCompleted = numTotalImports - Loader.totalProgress.singleProgress.Count;

            if (loading)
            {
                //Debug.Log("numTotalImports = " + numTotalImports + "\n  numImportCompleted = " + numImportCompleted);
                float progress;
                if (numTotalImports != 0)
                    progress = 100.0f * numImportCompleted / numTotalImports;
                else
                    progress = 0;
                float maxSubProgress = 0.0f;
                foreach (SingleLoadingProgress progr in Loader.totalProgress.singleProgress)
                {
                    if (maxSubProgress < progr.percentage) maxSubProgress = progr.percentage;
                }
                progress += maxSubProgress / numTotalImports;
                if (progressSlider != null)
                {
                    progressSlider.value = progress;
                    progressSlider.gameObject.SetActive(loading);
                }
                if (progressImage != null)
                {
                    progressImage.fillAmount = progress / 100f;
                    progressImage.gameObject.SetActive(loading);
                }
                if (progressText != null)
                {
                    if (loading)
                    {
                        progressText.gameObject.SetActive(loading);
                        progressText.text = "Loading " + Loader.totalProgress.singleProgress.Count + " objects...";
                        string loadersText = "";
                        int count = 0;
                        foreach (SingleLoadingProgress i in Loader.totalProgress.singleProgress)
                        {
                            if (count > 4) // maximum 4 messages
                            {
                                loadersText += "...";
                                break;
                            }
                            if (!string.IsNullOrEmpty(i.message))
                            {
                                if (count > 0)
                                {
                                    loadersText += "; ";
                                }
                                loadersText += i.message;
                                count++;
                            }
                        }
                        if (loadersText != "")
                        {
                            progressText.text += "\n" + loadersText;
                        }
                    }
                    else
                    {
                        progressText.gameObject.SetActive(false);
                        progressText.text = "";
                    }
                }
            }
            else
            {
                OnImportComplete();
            }
        }


        private void OnImportStart()
        {
            
            if (progressText != null)
            {
                progressText.text = "";
            }
            if (progressSlider != null)
            {
                progressSlider.value = 0.0f;
                progressSlider.gameObject.SetActive(true);
            }
            if (progressImage != null)
            {
                progressImage.fillAmount = 0;
                progressImage.gameObject.SetActive(true);
            }
        }


        private void OnImportComplete()
        {

            //ocorre se estiver no Question controller 1s
            if (first_question.GetComponent<I_QuestionController1>() != null)
            {
                first_question.GetComponent<I_QuestionController1>().Active = true;
                first_question.GetComponent<I_QuestionController1>().QuestionObjects[0].SetActive(true);
            }
            else
                if (first_question.GetComponent<I_QuestionController2>() != null)
            {
                first_question.GetComponent<I_QuestionController2>().Active = true;
                first_question.GetComponent<I_QuestionController2>().AnswerAlternatives[0].SetActive(true);
            }
            else        //ocorre no Imported_ActiveInformation  
            {
                //coloca o modelo como filho do filho do marcador 1
                Model = gameObject.transform.GetChild(0).gameObject;
                Model.transform.SetParent(Marker.transform.GetChild(0).transform);

                string texto;
                string arquivo;
                //ler o arquivo do modelo
                arquivo = "models.txt";
                BetterStreamingAssets.Initialize();
                texto = BetterStreamingAssets.ReadAllText(arquivo);

                //separacoes tera 9 pontos, o inicio, o fim do endereco, o fim das 3 variavies de posicao, fimdas 3 variaveis de angulacao e fim da variavel de escala 
                int[] separacoes = new int[9];

                int aux = 0, i = 0;

                for (int j = 0; i < texto.Length && j < 9; i++)
                {
                    if (aux == 0 && j == 0)
                        j++;
                    if (texto[i] == '\n' || texto[i] == ' ')
                    {
                        separacoes[j] = i;
                        j++;
                    }
                    //remove . from the numbers
                    else if (j > 0 && texto[i] == '.')
                    {
                        texto = texto.Remove(i, 1);
                        texto = texto.Insert(i, ",");
                    }
                }
                if (i == texto.Length) { 
                    separacoes[8] = i;
                }

                float div = this.Marker.transform.localScale.x;

                Debug.Log(new Vector3(float.Parse(texto.Substring(separacoes[4] + 1, separacoes[5] - separacoes[4])), float.Parse(texto.Substring(separacoes[5], separacoes[6] - separacoes[5])), float.Parse(texto.Substring(separacoes[6], separacoes[7] - separacoes[6]))));
                if (div != 0)
                    Model.transform.localPosition = new Vector3(float.Parse(texto.Substring(separacoes[1] + 1, separacoes[2] - separacoes[1] - 1)) / div, float.Parse(texto.Substring(separacoes[2] + 1, separacoes[3] - separacoes[2] - 1)) / div, float.Parse(texto.Substring(separacoes[3], separacoes[4] - separacoes[3])) / div);
                else
                {
                    Debug.Log("Error, Marker scale is 0");
                }

                Model.transform.Rotate(Model.transform.localRotation.x * -1, Model.transform.localRotation.y * -1, Model.transform.localRotation.z * -1);
                Model.transform.Rotate(float.Parse(texto.Substring(separacoes[4] + 1, separacoes[5] - separacoes[4])), float.Parse(texto.Substring(separacoes[5], separacoes[6] - separacoes[5])), float.Parse(texto.Substring(separacoes[6], separacoes[7] - separacoes[6])));
                Model.transform.localScale = new Vector3(float.Parse(texto.Substring(separacoes[7] + 1, separacoes[8] - separacoes[7] - 1)), float.Parse(texto.Substring(separacoes[7] + 1, separacoes[8] - separacoes[7] - 1)), float.Parse(texto.Substring(separacoes[7] + 1, separacoes[8] - separacoes[7] - 1)));

                active_info.MyObject = Model;
                active_info.Imported = 1;

            }

            //indica que os modelos foram importados
            imported = true;




            //parte do codigo original
            if (progressText != null)
            {
                progressText.text = "";
            }
            if (progressSlider != null)
            {
                progressSlider.value = 100.0f;
                //progressSlider.gameObject.SetActive(false);
            }
            if (progressImage != null)
            {
                progressImage.fillAmount = 1f;
                progressImage.gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
        

    }
}
