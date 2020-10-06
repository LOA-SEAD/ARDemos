using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsImpL;

public class Controler : MonoBehaviour
{
    public GameObject importer;
    public GameObject First_question;
    public GameObject ActivateInformation;

    private ObjectImporterUI importerUI;
    private I_QuestionController1 QuestionControler_class;
    private Imported_ActivateInformation ActivateInformation_class;

    private bool imported;
    
    // Start is called before the first frame update
    void Start()
    {
        imported = false;
        importerUI = importer.GetComponent<ObjectImporterUI>();
        QuestionControler_class = First_question.GetComponent<I_QuestionController1>();
        ActivateInformation_class = ActivateInformation.GetComponent<Imported_ActivateInformation>();
    }

    // Update is called once per frame
    void Update()
    {
        if (importerUI.imported == true)
        {
            if (QuestionControler_class.Active == false)
            {
                //se tiver ido para a proxima questao ou para a anterior, atualiza o modelo e o index da activate information, alem de se atualizar
                if (QuestionControler_class.useNextQuestionButton)
                    if (QuestionControler_class.nextQuestion.GetComponent<I_QuestionController1>() && QuestionControler_class.nextQuestion.GetComponent<I_QuestionController1>().Controler_aux && QuestionControler_class.nextQuestion.GetComponent<I_QuestionController1>().Active)
                {
                    ActivateInformation_class.MyObject = QuestionControler_class.nextQuestion.GetComponent<I_QuestionController1>().QuestionObjects[0];
                    ActivateInformation_class.index++;                 //informa que o index da questao mudou para saber qual arquivo ler
                    ActivateInformation_class.Imported = 1;            //imported = 1 significa que o modelo foi importado mas precisa carregar o texto
                    QuestionControler_class = QuestionControler_class.nextQuestion.GetComponent<I_QuestionController1>();
                }
                else if (QuestionControler_class.usePreviousQuestionButton)
                    if (QuestionControler_class.previousQuestion.GetComponent<I_QuestionController1>().Active && QuestionControler_class.previousQuestion.GetComponent<I_QuestionController1>().Controler_aux && QuestionControler_class.previousQuestion.GetComponent<I_QuestionController1>().Active)
                {
                    ActivateInformation_class.MyObject = QuestionControler_class.previousQuestion.GetComponent<I_QuestionController1>().QuestionObjects[0];
                    ActivateInformation_class.index--;                  //informa que o index da questao mudou para saber qual arquivo ler
                    ActivateInformation_class.Imported = 1;             //imported = 1 significa que o modelo foi importado mas precisa carregar o texto
                    QuestionControler_class = QuestionControler_class.previousQuestion.GetComponent<I_QuestionController1>();
                }
            }
                

            //coloca o modelo da primeira questao
            if (imported == false)
            {
                imported = true;
                ActivateInformation_class.Imported = 1;     //imported = 1 significa que o modelo foi importado mas precisa carregar o texto
                ActivateInformation_class.MyObject = QuestionControler_class.QuestionObjects[0];

            }
        }

    }
}
