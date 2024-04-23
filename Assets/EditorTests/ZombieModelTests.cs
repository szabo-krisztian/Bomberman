using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class ZombieModelTests
{
    private MyZombieModel _zombieModel;

    [SetUp]
    public void SetUp()
    {
        _zombieModel = new MyZombieModel();
    }

    [Test]
    public void MyZombieModel_IsPositionFree_ReturnsTrueIfFreePosition()
    {
        Assert.IsTrue(_zombieModel.IsPositionFree(Vector3.zero));
    }

    [Test]
    public void MyZombieModel_IsPositionFree_ReturnsFalseIfWallOnPosition()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Wall.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsFalse(_zombieModel.IsPositionFree(Vector3.zero));
        Object.DestroyImmediate(instantiatedPrefab);
    }

    [Test]
    public void MyZombieModel_IsPositionFree_ReturnsFalseIfBoxOnPosition()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsFalse(_zombieModel.IsPositionFree(Vector3.zero));
        Object.DestroyImmediate(instantiatedPrefab);
    }

    [Test]
    public void MyZombieModel_IsPositionFree_ReturnsFalseIfBombOnPosition()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Bomb/Bomb.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsFalse(_zombieModel.IsPositionFree(Vector3.zero));
        Object.DestroyImmediate(instantiatedPrefab);
    }

    [Test]
    public void MyZombieModel_GetFreeNeighbourPositions_ReturnsAllPositionsIfNoObstacleNearby()
    {
        Assert.AreEqual(_zombieModel.GetFreeNeighbourPositions(UtilityFunctions.GetTilemapPosition(Vector3.zero)).Count, 4);
    }

    [Test]
    public void MyZombieModel_GetFreeNeighbourPositions_ReturnsAllPositionValuesIfNoObstacleNearby()
    {
        Vector3Int offset = new Vector3Int(0, 1, 0);
        List<Vector3Int> freePositions = _zombieModel.GetFreeNeighbourPositions(UtilityFunctions.GetTilemapPosition(Vector3.zero + offset));
        
        foreach (Vector3Int direction in UtilityFunctions.Directions)
        {
            Assert.IsTrue(freePositions.Contains(direction + offset));
        }
    }

    [Test]
    public void MyZombieModel_GetFreeNeighbourPositions_ReturnsNoPositionsIfSurroundedByObstacles()
    {
        GameObject prefab1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = Vector3.left;
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab1) as GameObject;

        GameObject prefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab2.transform.position = Vector3.right;
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab2) as GameObject;

        GameObject prefab3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab3.transform.position = Vector3.up;
        GameObject instantiatedPrefab3 = PrefabUtility.InstantiatePrefab(prefab3) as GameObject;

        GameObject prefab4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab4.transform.position = Vector3.down;
        GameObject instantiatedPrefab4 = PrefabUtility.InstantiatePrefab(prefab4) as GameObject;

        Assert.AreEqual(_zombieModel.GetFreeNeighbourPositions(UtilityFunctions.GetTilemapPosition(Vector3.zero)).Count, 0);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
        Object.DestroyImmediate(instantiatedPrefab3);
        Object.DestroyImmediate(instantiatedPrefab4);
    }

    [Test]
    public void MyZombieModel_GetFreeNeighbourPositions_ReturnsSomePositionsIfSurroundedPartially()
    {
        GameObject prefab1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = UtilityFunctions.GetCenterPosition(Vector2.left);
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab1) as GameObject;

        GameObject prefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = UtilityFunctions.GetCenterPosition(Vector2.right);
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab2) as GameObject;

        Assert.AreEqual(_zombieModel.GetFreeNeighbourPositions(Vector3Int.zero).Count, 2);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }

    [Test]
    public void MyZombieModel_GetRandomDirection_ReturnsFreeDirection()
    {
        GameObject prefab1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = UtilityFunctions.GetCenterPosition(Vector2.left);
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab1) as GameObject;

        GameObject prefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = UtilityFunctions.GetCenterPosition(Vector2.right);
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab2) as GameObject;

        Vector3Int direction = _zombieModel.GetRandomDirection(Vector3.zero);
        Assert.IsTrue(direction == Vector3Int.up || direction == Vector3Int.down);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }

    [Test]
    public void MyZombieModel_IsIsolatedPosition_ReturnsTrueIfSurrounded()
    {
        GameObject prefab1 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab1.transform.position = UtilityFunctions.GetCenterPosition(Vector2.left);
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab1) as GameObject;

        GameObject prefab2 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab2.transform.position = UtilityFunctions.GetCenterPosition(Vector2.right);
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab2) as GameObject;

        GameObject prefab3 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab3.transform.position = UtilityFunctions.GetCenterPosition(Vector2.up);
        GameObject instantiatedPrefab3 = PrefabUtility.InstantiatePrefab(prefab3) as GameObject;

        GameObject prefab4 = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Box.prefab");
        prefab4.transform.position = UtilityFunctions.GetCenterPosition(Vector2.down);
        GameObject instantiatedPrefab4 = PrefabUtility.InstantiatePrefab(prefab4) as GameObject;

        Assert.IsTrue(_zombieModel.IsIsolatedPosition(Vector3.zero));
        
        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
        Object.DestroyImmediate(instantiatedPrefab3);
        Object.DestroyImmediate(instantiatedPrefab4);
    }
}
