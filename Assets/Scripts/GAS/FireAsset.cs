using System;
using UnityEngine;
using GAS.Runtime;

public class FireAsset : AbilityAsset
{
    public GameObject bulletPrefab;

    public override Type AbilityType() => typeof(Fire);
    
}
