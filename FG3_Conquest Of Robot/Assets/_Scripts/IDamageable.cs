using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeHit(float dame, RaycastHit2D hit);
    void TakeDame(float dame);
}
