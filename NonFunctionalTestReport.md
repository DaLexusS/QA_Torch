# Non-Functional Testing - Torch Project

For this task I tested the Torch project from a non-functional point of view.
I did not focus on whether the torch feature works correctly, because that is already covered by the regular unit and play mode tests.
Instead, I checked how the project behaves under repeated use, object load, and stress conditions.

## 1. Repeated Torch Logic Performance

The first test runs the torch logic many times in a row.
It switches the player between nearby and not nearby, then tries to light the torch.

This is meant to check if the core torch code is still fast when it is called many times during gameplay.
The test currently runs `100,000` repeated operations and expects them to finish inside a small time budget.

Result from code validation:

- The test compiles successfully.
- The performance budget is set to `250 ms`.
- This gives a clear warning if the torch logic becomes too slow later.

## 2. Memory / Garbage Test

The second test checks if the repeated torch logic creates garbage memory.
This matters because garbage allocations can cause frame drops in Unity when the garbage collector runs.

The test warms up the torch logic first, then measures allocations while running another `100,000` operations.
The expected result is `0 bytes` allocated during the repeated calls.

This is useful because the torch logic should stay lightweight and should not create new objects every time the player interacts with it.

## 3. Stability Test

The third test checks if the torch can survive repeated state changes without errors.
It runs the same style of repeated calls and confirms that no exception is thrown.

At the end, the torch must still be in a valid state.
This is not testing a new feature, it is testing reliability after a lot of repeated usage.

## 4. Load Test With Many Torch Objects

The play mode load test creates `500` torch objects in the scene.
Each one receives a `TorchMono` component and a trigger collider, then gets updated and interacted with.

This checks if the Unity object side of the torch system can handle a larger amount of objects without immediately breaking or becoming too slow.

The current load budget is `500 ms` for creating and updating the batch.

## Validation

I validated the project by building the Unity solution:

```powershell
dotnet build QA_Torch.sln --no-restore
```

Result:

- Build passed.
- `0` errors.
- `0` warnings.

I could not complete the Unity batch test run because the project was already open in Unity and had an active `Temp/UnityLockfile`.
Because of that, Unity exited with code `127` when trying to open the same project in batch mode.

To run the tests fully, close the open Unity editor and run the Edit Mode and Play Mode tests from Unity Test Runner.

## Summary

Overall, I added non-functional tests for:

- Performance under repeated torch logic calls.
- Memory stability during repeated calls.
- Reliability after many state changes.
- Load behavior when many torch objects exist at once.

The project builds successfully, and the tests are ready to run inside Unity after the editor lock is removed.
