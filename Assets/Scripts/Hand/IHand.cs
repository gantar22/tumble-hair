using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHand
{
    void Summon(Vector3 target);
    void UnSummon();
    bool activeTarget(out Vector3 target);
}
