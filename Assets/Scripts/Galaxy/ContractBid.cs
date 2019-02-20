using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GameConstructs;

public class ContractBid {
    public GalaxyEntity requester;
    public int units;
    public int lengthInMinutes;
    public List<ContractBidResponse> responses = new List<ContractBidResponse>();
    public HashSet<Company> companies = new HashSet<Company>();
    public List<ContractBidCriteria> criteria = new List<ContractBidCriteria>();
    public List<ContractBidRequirement> requirements = new List<ContractBidRequirement>();

    public ContractBid(GalaxyEntity _requester, int _units, List<ContractBidCriteria> _criteria, List<ContractBidRequirement> _requirements, int _lengthInMinutes = 30000) {
        requester = _requester;
        units = _units;
        lengthInMinutes = _lengthInMinutes;
        foreach(ContractBidCriteria c in _criteria) {
            criteria.Add(c);
        }
        foreach(ContractBidRequirement r in _requirements) {
            requirements.Add(r);
        }
    }

    public List<string> SubmitResponse(Company company, Ship ship, int price) {
        ContractBidResponse response = new ContractBidResponse(company, ship, price);
        List<string> failureMessages = new List<string>();
        if (companies.Add(company)) {
            foreach (ContractBidRequirement r in requirements) {
                if (r.evaluate(response) == false) {
                    failureMessages.Add(r.failureMessage);
                }
            }
            if (failureMessages.Count == 0) {
                responses.Add(response);
                return failureMessages;
            } else {
                companies.Remove(company);
                return failureMessages;
            }
        } else {
            failureMessages.Add("You've already submited a response to this Contract Bid");
            return failureMessages;
        }
    }

    public ContractBidResponse[] GetBestBidsInOrder() {
        foreach (ContractBidCriteria c in criteria) {
            c.evaluate(responses);
        }
        foreach (ContractBidResponse response in responses) {
            responses.Sort(delegate(ContractBidResponse x, ContractBidResponse y) {
                if (x.score > y.score) return 1;
                else if (x.score < y.score) return -1;
                else return 0; //ie. they are equal
            });
        }
        return responses.ToArray();
    }

    public struct ContractBidResponse {
        public Ship ship;
        public int price;
        public Company company;
        public int score;
        public ContractBidResponse(Company _company, Ship _ship, int _price) {
            company = _company;
            ship = _ship;
            price = _price;
            score = 0;
        }
    }

    public struct ContractBidCriteria {
        public string criteriaSummary;
        public string criteriaExplaination;
        public Action<List<ContractBidResponse>> evaluate;

        public ContractBidCriteria(string _criteriaSummary, string _criteriaExplaination, Action<List<ContractBidResponse>> _evaluate) {
            criteriaSummary = _criteriaSummary;
            criteriaExplaination = _criteriaExplaination;
            evaluate = _evaluate;
        }

        public static ContractBidCriteria LowMinimumCrewCriteria(int scoring) {
            return new ContractBidCriteria(
                "Low Crew",
                "The submitted ship should be opperated with minimal crew",
                (List<ContractBidResponse> bidResponses) => {
                    ContractBidResponse bestResponse = bidResponses[0];
                    foreach(ContractBidResponse br in bidResponses) {
                        if(br.ship.crew < bestResponse.ship.crew) {
                            bestResponse = br;
                        }
                    }
                    bestResponse.score += scoring;
                });
        }
    }

    public struct ContractBidRequirement {
        public string requirementSummary;
        public string requirementExplaination;
        public string failureMessage;
        public Func<ContractBidResponse, bool> evaluate;

        public ContractBidRequirement(string _requirementSummary, string _requirementExplaination, string _failureMessage, Func<ContractBidResponse, bool> _evaluate) {
            requirementSummary = _requirementSummary;
            requirementExplaination = _requirementExplaination;
            failureMessage = _failureMessage;
            evaluate = _evaluate;
        }

        public static ContractBidRequirement ShipTypeRequirement(ShipType type) {
            return new ContractBidRequirement(
            Constants.ShipTypeString[type],
            "The submitted ship must be a " + Constants.ShipTypeString[type],
            "The submitted ship doesn't match the requested classification",
            (ContractBidResponse bid) => bid.ship.classificaiton == type);
        }

    }
}
