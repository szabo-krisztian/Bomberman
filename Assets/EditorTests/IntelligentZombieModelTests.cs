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

    [Test]
    public void MyIntelligentZombieModel_GetRouteToPlayer_ReturnsTheCorrectPath()
    {
        Vector3 playerPosition = new Vector3(1.5f, 1.5f, 0f);
        Vector3 zombiePosition = playerPosition + 2 * Vector3.right;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player1.prefab");
        prefab.transform.position = playerPosition;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
       
        List<Vector3Int> route = _intelligentZombieModel.GetRouteToPlayer(UtilityFunctions.GetTilemapPosition(zombiePosition));

        Assert.AreEqual(route, new List<Vector3Int> { new Vector3Int(1,1,0), new Vector3Int(2,1,0)});
        
        Object.DestroyImmediate(instantiatedPrefab);
    }
}