using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDesigned {
    bool IsDesigned { get; set; }
    int DesignCost { get; set; }
    float DesignProgress { get; set; }
    event Action<IDesigned> OnDesignProgressEvent;
    event Action<IDesigned> OnDesignChangeEvent;
}
