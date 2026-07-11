using System;
using System.Diagnostics;
using NUnit.Framework;

public class TorchNonFunctionalTests
{
    private const int LogicOperationCount = 100_000;
    private const long LogicPerformanceBudgetMilliseconds = 250;

    [Test]
    public void Torch_RepeatedLightingAttempts_CompletesWithinPerformanceBudget()
    {
        var torch = new Torch();

        var stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < LogicOperationCount; i++)
        {
            torch.SetPlayerNearby((i & 1) == 0);
            torch.TryLight();
        }

        stopwatch.Stop();

        Assert.LessOrEqual(
            stopwatch.ElapsedMilliseconds,
            LogicPerformanceBudgetMilliseconds,
            $"{LogicOperationCount} lighting attempts should stay responsive for gameplay input.");
    }

    [Test]
    public void Torch_RepeatedStateChanges_DoesNotAllocateGarbage()
    {
        var torch = new Torch();

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // Warm up the call path before measuring per-thread allocations.
        torch.SetPlayerNearby(true);
        torch.TryLight();

        long beforeBytes = GC.GetAllocatedBytesForCurrentThread();

        for (int i = 0; i < LogicOperationCount; i++)
        {
            torch.SetPlayerNearby((i & 1) == 0);
            torch.TryLight();
        }

        long allocatedBytes = GC.GetAllocatedBytesForCurrentThread() - beforeBytes;

        Assert.Zero(allocatedBytes, "Torch logic should not allocate garbage during repeated gameplay use.");
    }

    [Test]
    public void Torch_RepeatedStateChanges_RemainsStable()
    {
        var torch = new Torch();

        Assert.DoesNotThrow(() =>
        {
            for (int i = 0; i < LogicOperationCount; i++)
            {
                torch.SetPlayerNearby((i & 1) == 0);
                torch.TryLight();
            }
        });

        Assert.IsTrue(torch.IsLit, "Torch should remain in a valid lit state after repeated state changes.");
    }
}
