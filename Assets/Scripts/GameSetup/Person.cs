using System.Collections;
using System.Collections.Generic;
using System;
using GameConstructs;

public class Person : IDisplayed{
    protected string title = "";
    public string Title {
        get { return title; }
        set { title = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    protected string epithet = "";
    public string Epithet {
        get { return epithet; }
        set { epithet = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    protected string firstName;
    public string FirstName {
        get { return firstName; }
        set {
            firstName = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    protected string lastName;
    public string LastName {
        get { return lastName; }
        set {
            lastName = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    public string FullName { get {
            string toReturn = "";
            if (title != "") {
                toReturn += title + " ";
            }
            toReturn += firstName + " " + lastName;
            if(epithet != "") {
                toReturn += " " + epithet;
            }
            return toReturn; } }
    protected LayeredColoredSprite portrait;
    public LayeredColoredSprite Portrait {
        get { return portrait; }
        set {
            portrait = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    protected Species species;
    public Species Species {
        get { return species; }
        set {
            species = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }
    protected Gender gender;
    public Gender Gender {
        get { return gender; }
        set {
            gender = value;
            OnPlayerDetailsChangeEvent?.Invoke(this);
        }
    }

    public string[] DisplayStrings { get { return new string[3] { FirstName +" "+LastName, Title, Epithet }; } }
    public LayeredColoredSprite[] DisplaySprites { get { return new LayeredColoredSprite[1] { Portrait }; } }
    public event Action<IDisplayed> DisplayUpdateEvent;
    public event Action<Person> OnPlayerDetailsChangeEvent;

}
