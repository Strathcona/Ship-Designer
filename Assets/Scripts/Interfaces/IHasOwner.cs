using UnityEngine;
using System.Collections;
using System;
public interface IHasOwner {
    Person GetOwner();
    void ChangeOwner(Magnate player);
    event Action<Magnate> OnOwnerChangeEvent;

}
