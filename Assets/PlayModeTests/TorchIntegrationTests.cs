using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TorchIntegrationTests
{
    private GameObject _playerGO;
    private GameObject _torchGO;
    private PlayerController _playerController;
    private TorchMono _torchMono;

    [SetUp]
    public void Setup()
    {
        _playerGO = new GameObject("Player");
        _playerGO.tag = "Player";
        _playerController = _playerGO.AddComponent<PlayerController>();
        
        _playerController.rb = _playerGO.AddComponent<Rigidbody2D>();
        _playerController.rb.bodyType = RigidbodyType2D.Dynamic;
        _playerController.rb.gravityScale = 0f;

        var playerCollider = _playerGO.AddComponent<BoxCollider2D>();
        playerCollider.isTrigger = true;

        _torchGO = new GameObject("Torch");
        _torchMono = _torchGO.AddComponent<TorchMono>();
        var torchCollider = _torchGO.AddComponent<BoxCollider2D>();
        torchCollider.isTrigger = true;

        _playerGO.transform.position = new Vector3(-5f, 0f, 0f);
        _torchGO.transform.position = Vector3.zero;
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(_playerGO);
        Object.Destroy(_torchGO);
    }

    [UnityTest]
    public IEnumerator Player_EntersTrigger_IntegratesWith_TorchLogic()
    {
        _playerGO.transform.position = Vector3.zero;
        
        yield return new WaitForFixedUpdate();
        
        Assert.IsTrue(_torchMono.Logic.IsPlayerNearby, "Physics trigger failed to update Torch pure logic state.");
    }
}