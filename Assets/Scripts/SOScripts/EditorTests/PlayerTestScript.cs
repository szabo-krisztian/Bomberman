using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerTestScript
{
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerTestScriptMove()
    {
        //GameObject playerObject = new GameObject();
        //var playerController = playerObject.AddComponent<PlayerController>();
        //Assert.AreEqual(new Vector3(0, 0, 0), playerObject.transform.position, "Player did not move forward as expected.");
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Game/Players/Player1.prefab");
        Assert.AreEqual(null, prefab);
    }
}
