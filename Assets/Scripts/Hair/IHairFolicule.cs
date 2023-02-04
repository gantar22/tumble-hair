using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHairFolicule
{
    float height { get; set; }
    Vector2 tangent { get; set; }
    Vector3 position();
    float radius();
}
