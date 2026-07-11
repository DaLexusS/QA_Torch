using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class TorchLoadTests
{
    private const int TorchCount = 500;
    private const long LoadPerformanceBudgetMilliseconds = 500;

    private readonly List<GameObject> _createdObjects = new List<GameObject>();

    [TearDown]
    public void TearDown()
    {
        foreach (GameObject createdObject in _createdObjects)
        {
            if (createdObject != null)
            {
                Object.Destroy(createdObject);
            }
        }

        _createdObjects.Clear();
    }

    [UnityTest]
    public IEnumerator TorchMono_LoadManyTorches_StaysResponsiveAndStable()
    {
        var torches = new List<TorchMono>(TorchCount);
        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < TorchCount; i++)
        {
            var torchObject = new GameObject($"Load Test Torch {i}");
            _createdObjects.Add(torchObject);

            var torchMono = torchObject.AddComponent<TorchMono>();
            var collider = torchObject.AddComponent<BoxCollider2D>();
            collider.isTrigger = true;

            torchMono.Logic.SetPlayerNearby(true);
            torchMono.Interact();
            torchMono.UpdateVisuals();

            torches.Add(torchMono);
        }

        stopwatch.Stop();

        yield return null;

        Assert.LessOrEqual(
            stopwatch.ElapsedMilliseconds,
            LoadPerformanceBudgetMilliseconds,
            $"{TorchCount} torches should instantiate and update within the load budget.");

        foreach (TorchMono torch in torches)
        {
            Assert.IsTrue(torch.Logic.IsLit, "Torch load test should keep each component in a valid state.");
        }
    }
}
