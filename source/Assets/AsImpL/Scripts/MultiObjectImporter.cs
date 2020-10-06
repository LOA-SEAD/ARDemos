using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace AsImpL
{
    /// <summary>
    /// Load the objects in the given list with their parameters and positions.
    /// </summary>
    public class MultiObjectImporter : ObjectImporter
    {
        [Tooltip("Load models in the list on start")]
        public bool autoLoadOnStart = false;

        [Tooltip("Models to load on startup")]
        public List<ModelImportInfo> objectsList = new List<ModelImportInfo>();

        [Tooltip("Default import options")]
        public ImportOptions defaultImportOptions = new ImportOptions();

        [SerializeField]
        private PathSettings pathSettings = null;

        public int cont = 0;
        public string RootPath
        {
            get
            {
                return pathSettings != null ? pathSettings.RootPath : "";
            }
        }

        //responsavel por indicar qual o modelo q sera pego
        public int user_id = 0;

        /// <summary>
        /// Load a set of files with their own import options
        /// </summary>
        /// <param name="modelsInfo">List of file import entries</param>
        public void ImportModelListAsync(ModelImportInfo[] modelsInfo)
        {
            if (modelsInfo == null)
            {
                return;
            }
            for (int i = 0; i < modelsInfo.Length; i++)
            {
                if (modelsInfo[i].skip) continue;
                string objName = modelsInfo[i].name;
                string filePath = modelsInfo[i].path;
                if (string.IsNullOrEmpty(filePath))
                {
                    Debug.LogErrorFormat("File path missing for the model at position {0} in the list.", i);
                    continue;
                }

                filePath = RootPath + filePath;

                ImportOptions options = modelsInfo[i].loaderOptions;
                if (options == null || options.modelScaling == 0)
                {
                    options = defaultImportOptions;
                }
                ImportModelAsync(objName, filePath, transform, options);
            }
        }


        /// <summary>
        /// Import the list of objects in objectList.
        /// </summary>
        protected virtual void Start()
        {

            string texto;
            string arquivo;

            if (autoLoadOnStart)
            {
                //variaveis 
                arquivo = "Models.txt";
                Debug.Log(arquivo);
                BetterStreamingAssets.Initialize();
                texto = BetterStreamingAssets.ReadAllText(arquivo);
                int[] separacoes = new int[2];
                int i = 0, cont = 0;
                //carregar a lista de objectos a importar
                do
                {
                    separacoes[0] = i;
                    Debug.Log("Entrou com cont = " + cont);
                    //pega o endereco do modelo
                    for (int j = 0; i < texto.Length && j < 8; i++)
                    {
                        if (texto[i] == '\n' || texto[i] == ' ')
                        {
                            if (j < 1)
                                separacoes[1] = i;
                            //Debug.Log(separacoes[j]);
                            j++;
                        }
                    }

                    //configura para carregar o modelo
                    ModelImportInfo mii = new ModelImportInfo();
                    mii.name = "object" + cont;
                    this.objectsList.Add(mii);
                    this.objectsList[cont].path = texto.Substring(separacoes[0], separacoes[1] - separacoes[0] - 1);
                    if (objectsList[cont].path[objectsList[cont].path.Length - 1] == 10)
                    {
                        objectsList[cont].path = objectsList[cont].path.Substring(0, objectsList[cont].path.Length - 2);
                    }
                    cont++;

                } while (i < texto.Length);

                //importar os  modelos
                ImportModelListAsync(objectsList.ToArray());
            }
        }

    }
}
