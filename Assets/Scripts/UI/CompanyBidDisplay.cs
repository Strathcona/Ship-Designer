using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanyBidDisplay : MonoBehaviour {

    CompanyBid bid;
    Text displayText;

    private void Awake() {
        displayText = GetComponentInChildren<Text>();
    }

    public void DisplayNewBid(CompanyBid b) {
        bid = b;
        displayText.text = b.GetBidString();
    }
}
