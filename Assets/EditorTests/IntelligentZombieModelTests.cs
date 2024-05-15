using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class IntelligentZombieModelTests
{
    private MyIntelligentZombieModel _intelligentZombieModel;

    [SetUp]
    public void SetUp()
    {
        _intelligentZombieModel = new MyIntelligentZombieModel();
    }

    [Test]
    public void MyIntelligentZombieModel_IsPlayerStandingOnPosition_ReturnsTrueIfPlayerIsOnPosition()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player1.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsTrue(_intelligentZombieModel.IsPlayerStandingOnPosition(Vector3.zero));
        Object.DestroyImmediate(instantiatedPrefab);
    }

    [Test]
    public void MyIntelligentZombieModel_IsPlayerStandingOnPosition_ReturnsFalseIfPlayerIsNotOnPosition()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player1.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsFalse(_intelligentZombieModel.IsPlayerStandingOnPosition(new Vector3(5.44f, -3.14f, 0.0f)));
        Object.DestroyImmediate(instantiatedPrefab);
    }
}