using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public interface IConversationReader{
    void DisplayText(string text);
    void DisplayChoice(List<string> choices, Action<int> response);
}
