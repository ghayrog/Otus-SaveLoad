using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameEngine
{
    [Serializable]
    public sealed class UnitPrefabManager
    {
        [SerializeField]
        private Unit[] _unitPrefabs;

        private Dictionary<string, Unit> _unitPrefabsDict;

        public void InitializePrefabDictionary()
        { 
            _unitPrefabsDict = new();

            for (int i = 0; i < _unitPrefabs.Length; i++)
            {
                _unitPrefabsDict.Add(_unitPrefabs[i].Type, _unitPrefabs[i]);
            }
        }

        public Unit GetPrefabByString(string prefabName)
        { 
            return _unitPrefabsDict[prefabName];
        }

        public string GetPrefabName(Unit gameObject)
        {
            return _unitPrefabsDict.FirstOrDefault(x => x.Value == gameObject).Key;
        }
    }
}