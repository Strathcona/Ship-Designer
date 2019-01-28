using UnityEngine;
using System.Collections.Generic;
using System.Text;

public static class Utility {
    public static List<string> SplitOnTopLevelBrackets(string s) {
        bool quote = false;
        bool escape = false;
        int nestedBrackets = 0;
        List<string> strings = new List<string>();
        StringBuilder currentString = new StringBuilder();
        foreach (char c in s) {
            if (quote) { //if we're inside a quote, we ignore everything except for escape characters and the end quote
                if (c == '/' && escape != true) {
                    escape = true;
                }
                if (escape == true) {//if we're escaped, the next character is added even if it's a quote
                    currentString.Append(c);
                    escape = false;
                } else {
                    if (c == '"') {
                        quote = false; //we're out of the quote
                        currentString.Append(c);
                    } else {
                        currentString.Append(c); // if it's not a closing quote, add it to the list
                    }
                }
            } else {
                if (c == ']') {
                    nestedBrackets -= 1;
                    if (nestedBrackets == 0) { //we're back at top level, store the string
                        strings.Add(currentString.ToString());
                        currentString.Clear();
                    } else {
                        currentString.Append(c);
                    }
                } else if (c == '[') { //head one deeper
                    nestedBrackets += 1;
                    if (nestedBrackets > 1) {
                        currentString.Append(c);
                    }
                } else if (c == '"') {
                    quote = true;
                    currentString.Append(c);
                } else {
                    currentString.Append(c);
                }
            }
        }
        return strings;
    }
}
