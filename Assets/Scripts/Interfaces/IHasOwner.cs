using UnityEngine;
using System.Collections;
using System;
public interface IHasOwner {
    Player GetOwner();
    void ChangeOwner(Player player);
    event Action<Player> OnOwnerChangeEvent;

}
