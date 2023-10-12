using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Serializable.Dicts;

namespace Utilities
{
    [Serializable]
    public class StringListDictionary : SerializableDictionary<string, List<GameObject>, GameObjectList>
    {
   
    }
}
