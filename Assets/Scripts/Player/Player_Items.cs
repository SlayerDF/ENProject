using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public bool HasShovel { get => hasShovel; }

    bool hasShovel = false;

    public void AddShovel()
    {
        hasShovel = true;

        gameUI.ShowShovel();
    }
}
