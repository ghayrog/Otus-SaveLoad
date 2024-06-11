using GameEngine;
using SaveSystem;
using System;
using System.Linq;
using UnityEngine;

namespace GameSaveLoad
{

    public sealed class UnitManagerSaveLoader : SaveLoader<UnitManagerState, UnitManager>
    {
        private UnitPrefabManager _unitPrefabManager;

        public UnitManagerSaveLoader(UnitPrefabManager unitPrefabManager)
        {
            _unitPrefabManager = unitPrefabManager;
        }

        protected override UnitManagerState ConvertToData(UnitManager unitManager)
        {
            UnitManagerState unitManagerState = new UnitManagerState();
            Unit[] units = unitManager.GetAllUnits().ToArray();

            unitManagerState.units = new UnitState[units.Length];

            for (int i = 0; i < unitManagerState.units.Length; i++)
            {
                unitManagerState.units[i] = new UnitState
                {
                    type = units[i].Type,
                    hitPoints = units[i].HitPoints,
                    position = Utility.Vector3ToStruct(units[i].transform.position),
                    rotation = Utility.Vector3ToStruct(units[i].transform.rotation.eulerAngles)
                };
            }

            return unitManagerState;
        }

        protected override void SetupData(UnitManager unitManager, UnitManagerState unitManagerState)
        {
            if (unitManager.GetAllUnits() != null)
            {
                Unit[] units = unitManager.GetAllUnits().ToArray();
                int unitsCount = units.Length;
                for (int i = 0; i < unitsCount; i++)
                {
                    unitManager.DestroyUnit(unitManager.GetAllUnits().ToArray()[0]);
                }
            }

            for (int i = 0; i < unitManagerState.units.Length; i++)
            { 
                
                Unit prefab = _unitPrefabManager.GetPrefabByString(unitManagerState.units[i].type);
                UnityEngine.Vector3 position = Utility.StructToVector3(unitManagerState.units[i].position);
                Quaternion rotation = Quaternion.Euler(Utility.StructToVector3(unitManagerState.units[i].rotation));
                Unit spawnedUnit = unitManager.SpawnUnit(prefab, position, rotation);
                spawnedUnit.HitPoints = unitManagerState.units[i].hitPoints;
            }
        }
    }
}
