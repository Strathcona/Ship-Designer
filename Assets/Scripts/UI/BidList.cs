using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BidList : MonoBehaviour {
    public Transform bidDisplayPrefabs;
    public Part activePart;
    public int bidsToDisplay = 3;
    public List<GameObject> bidDisplays = new List<GameObject>();

    public void GetBidsOnPart() {
        if( bidsToDisplay > bidDisplays.Count) {
            int neededBidDisplays = bidsToDisplay - bidDisplays.Count;
            Debug.Log("Needed bidDisplays " + neededBidDisplays);
            for (int i = 0; i <= neededBidDisplays; i++) {
                Transform t = Instantiate(bidDisplayPrefabs);
                t.SetParent(this.transform);
                t.gameObject.SetActive(false);
                bidDisplays.Add(t.gameObject);
            }
        }
        for(int i = 0; i < bidsToDisplay; i++) {
            bidDisplays[i].SetActive(true);
            bidDisplays[i].GetComponent<CompanyBidDisplay>().DisplayNewBid(CompanyBid.GetNewBidOnPart(activePart));
        }
    }

}
