using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompanyGenerator : MonoBehaviour {
    public GameObject companyGeneratorScreen;
    public InputField companyName;
    public InputFieldIncrement personalFundsAdded;
    public InputFieldIncrement investorFundsAdded;
    public LogoGenerator logoGenerator;
    public Player founder;
    private static int AICompanyCount = 0;
    // Use this for initialization

    private void Start() {
        personalFundsAdded.onSubmit.AddListener(SetInvestorRatio);
        SetInvestorRatio();
    }

    public void SetInvestorRatio() {
        investorFundsAdded.MaxValue = Mathf.Max(1000, personalFundsAdded.FieldValue * 4);
    }

    public void FinishGeneratingCompany() {
        Company c = new Company();
        c.name = companyName.text;
        c.logo = logoGenerator.Logo;
        c.ChangeFunds(personalFundsAdded.FieldValue + investorFundsAdded.FieldValue);
        c.ChangeOwner(founder);
        GameDataManager.instance.AddNewCompany(c);
    }

    public static Company GenerateAICompany(AIPlayer founder) {
        Company c = new Company();
        c.name = "AI Company "+AICompanyCount.ToString();
        AICompanyCount += 1;
        return c;
    }

}
