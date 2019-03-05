using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CompanyGenerator : MonoBehaviour {
    public InputField companyName;
    public InputFieldIncrement personalFundsAdded;
    public InputFieldIncrement investorFundsAdded;
    public LogoGenerator logoGenerator;
    public Player founder;
    private static int AICompanyCount = 0;
    // Use this for initialization

    public void CancelGeneratingCompany() {
        gameObject.SetActive(false);
    }

    public void StartGeneratingCompany() {
        gameObject.SetActive(true);
        founder = PlayerManager.instance.activePlayer;
        personalFundsAdded.MaxValue = founder.GetFunds();
        personalFundsAdded.onSubmit.AddListener(SetInvestorRatio);
        SetInvestorRatio();
    }

    public void SetInvestorRatio() {
        investorFundsAdded.MaxValue = Mathf.Max(1000, personalFundsAdded.FieldValue * 4);
    }

    public void FinishGeneratingCompany() {
        Company c = new Company(founder);
        founder.ActiveCompany = c;
        c.name = companyName.text;
        c.logo = logoGenerator.Logo;
        Funds.Funds.TransferFunds(founder, c, personalFundsAdded.FieldValue);
        c.ChangeFunds(investorFundsAdded.FieldValue);
        GameDataManager.instance.AddNewCompany(c);
        gameObject.SetActive(false);
    }

    public static Company GenerateAICompany(AIPlayer founder) {
        Company c = new Company(founder);
        c.name = "AI Company "+AICompanyCount.ToString();
        AICompanyCount += 1;
        return c;
    }

}
