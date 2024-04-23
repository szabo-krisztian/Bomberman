using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

public class CollisionTests
{
    private CollisionDetectionModel _collisionDetector;

    [SetUp]
    public void SetUp()
    {
        _collisionDetector = new CollisionDetectionModel();
    }
    
    
    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsPlayer1Collider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player1.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Player"), true);
    }

    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsPlayer2Collider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player2.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Player"), true);
    }

    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsNormalZombieCollider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Zombies/NormalZombie.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Zombie"), true);
    }

    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsGhostZombieCollider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Zombies/GhostZombie.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Zombie"), true);
    }
    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsIntelligentZombieCollider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Zombies/IntelligentZombie.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Zombie"), true);
    }
    [Test]
    public void CollisionDetectionModel_GetCollidersInPosition_DetectsVeryIntelligentZombieCollider()
    {
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Zombies/VeryIntelligentZombie.prefab");
        prefab.transform.position = Vector3.zero;
        GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;

        Collider2D[] colliders = _collisionDetector.GetCollidersInPosition(Vector3.zero);

        Assert.AreEqual(_collisionDetector.IsTagInColliders(colliders, "Zombie"), true);
    }
}
