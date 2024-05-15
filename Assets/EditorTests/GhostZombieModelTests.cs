using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GhostZombieModelTests
{
    private GhostModel _ghostModel;

    [SetUp]
    public void SetUp()
    {
        _ghostModel = new GhostModel();
    }

    [Test]
    public void GhostModel_IsZombieStandingInBomb_ReturnsTrueIfZombieInBomb()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Bomb/Bomb.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        Assert.IsTrue(_ghostModel.IsZombieStandingInBomb(Vector3.zero));
        Object.DestroyImmediate(instantiatedPrefab);
    }

    [Test]
    public void GhostModel_IsZombieStandingInBomb_ReturnsFalseIfZombieNotInBomb()
    {
        Assert.IsFalse(_ghostModel.IsZombieStandingInBomb(Vector3.zero));
    }

    [Test]
    public void GhostModel_GetPivotPoint_FindsCorrectPositionRight()
    {
        Vector3 zombiePosition = new Vector3(0.5f, 0.5f, 0);
        Vector3 box1Position = zombiePosition + Vector3.right;
        Vector3 box2Position = box1Position + Vector3.right;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Wall.prefab");

        prefab.transform.position = box1Position;
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        prefab.transform.position = box2Position;
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Assert.AreEqual(_ghostModel.GetPivotPoint(Vector3.right, zombiePosition), box2Position + Vector3.right);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }

    [Test]
    public void GhostModel_GetPivotPoint_FindsCorrectPositionLeft()
    {
        Vector3 zombiePosition = new Vector3(0.5f, 0.5f, 0);
        Vector3 box1Position = zombiePosition + Vector3.left;
        Vector3 box2Position = box1Position + Vector3.left;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Wall.prefab");

        prefab.transform.position = box1Position;
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        prefab.transform.position = box2Position;
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Assert.AreEqual(_ghostModel.GetPivotPoint(Vector3.left, zombiePosition), box2Position + Vector3.left);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }

    [Test]
    public void GhostModel_GetPivotPoint_FindsCorrectPositionUp()
    {
        Vector3 zombiePosition = new Vector3(0.5f, 0.5f, 0);
        Vector3 box1Position = zombiePosition + Vector3.up;
        Vector3 box2Position = box1Position + Vector3.up;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Wall.prefab");

        prefab.transform.position = box1Position;
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        prefab.transform.position = box2Position;
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Assert.AreEqual(_ghostModel.GetPivotPoint(Vector3.up, zombiePosition), box2Position + Vector3.up);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }

    [Test]
    public void GhostModel_GetPivotPoint_FindsCorrectPositionDown()
    {
        Vector3 zombiePosition = new Vector3(0.5f, 0.5f, 0);
        Vector3 box1Position = zombiePosition + Vector3.down;
        Vector3 box2Position = box1Position + Vector3.down;

        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/MapElements/Wall.prefab");

        prefab.transform.position = box1Position;
        GameObject instantiatedPrefab1 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
        prefab.transform.position = box2Position;
        GameObject instantiatedPrefab2 = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Assert.AreEqual(_ghostModel.GetPivotPoint(Vector3.down, zombiePosition), box2Position + Vector3.down);

        Object.DestroyImmediate(instantiatedPrefab1);
        Object.DestroyImmediate(instantiatedPrefab2);
    }
}
