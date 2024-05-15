using System.Collections;
using System.Collections.Generic;
using System.Data;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UIElements;

namespace Assets.EditorTests
{
    public class IntelligentTests
    {
        private MyIntelligentZombieModel _zombieModel;
        private GameObject _player;

        [SetUp]
        public void SetUp()
        {
            _zombieModel = new MyIntelligentZombieModel();
            _player = new GameObject { tag = "Player" };
        }

        [TearDown]
        public void TearDown()
        {
            foreach (var obj in GameObject.FindObjectsOfType<GameObject>())
            {
                Object.DestroyImmediate(obj);
            }
        }

        private GameObject InstantiatePrefab(string path, Vector3 position)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject instantiatedPrefab = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
            instantiatedPrefab.transform.position = position;
            return instantiatedPrefab;
        }

        [Test]
        public void GetRouteToPlayer_ReturnsRoute_WhenPathExists()
        {
            _player.transform.position = UtilityFunctions.GetCenterPosition(Vector3Int.right);
            List<Vector3Int> route = _zombieModel.GetRouteToPlayer(Vector3Int.zero);

            Assert.AreEqual(1, route.Count);
            Assert.AreEqual(Vector3Int.right, route[0]);
        }
    }
}
