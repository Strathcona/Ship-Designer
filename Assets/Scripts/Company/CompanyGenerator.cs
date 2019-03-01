using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompanyGenerator : MonoBehaviour {
    public GameObject companyGeneratorScreen;
    public InputField companyName;
    public InputFieldIncrement personalFundsAdded;
    public InputFieldIncrement investorFundsAdded;
    public Player founder;
    private static int AICompanyCount = 0;
    // Use this for initialization

    private void Start() {
        personalFundsAdded.onSubmit.AddListener(SetInvestorRatio);
    }

    public void SetInvestorRatio() {
        investorFundsAdded.MaxValue = Mathf.Max(1000, personalFundsAdded.FieldValue * 4);
    }

    public void GenerateCompany(HumanPlayer player) {

    }

    public void FinishGeneratingCompany() {

    }

    public static Company GenerateAICompany(AIPlayer founder) {
        Company c = new Company();
        c.ceo = founder;
        c.name = "AI Company "+AICompanyCount.ToString();
        AICompanyCount += 1;
        return c;
    }

}
