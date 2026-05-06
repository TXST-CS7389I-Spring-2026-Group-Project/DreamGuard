# Unity Async Tasks

**Documentation Index:** Learn about unity async tasks in this documentation.

---

---
title: "Unity Asynchronous Tasks"
description: "Handle asynchronous Platform SDK calls in Unity using the Task-based pattern in the Meta XR Core SDK."
---

## Asynchronous programming

Many operations in the Meta XR Core SDK are asynchronous, particularly those related to [Scene](/documentation/unity/unity-scene-overview/) and [Spatial Anchors](/documentation/unity/unity-spatial-anchors-overview/). For example, saving a spatial anchor may take a few frames. An asynchronous method produces a result sometime in the future. In some frameworks, this is referred to as [futures and promises](https://en.wikipedia.org/wiki/Futures_and_promises).

Asynchronous operations can be challenging, because the code that initiates the operation may be separated from the code that consumes the result. The Meta XR Core SDK provides a few mechanisms to facilitate working with asynchronous operations.

## Task-based programming

C# supports the `async`/`await` pattern with "tasks", allowing a more natural code flow:

```c#
var result = await anchor.SaveAsync();
```

For more on C#'s support for async and await, see Microsoft's article on [Asynchronous programming with async and await](https://learn.microsoft.com/en-us/dotnet/csharp/asynchronous-programming/).

The Meta XR Core SDK supports the task-based programming model via [`OVRTask`](/reference/unity/latest/class_o_v_r_task). It is similar to a [System.Threading.Tasks.Task](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.task-1?view=net-8.0) with a few key differences:

* It is a value-type, so it is an allocation-free object.
* An `OVRTask` cannot be canceled.
* The result can only be retrieved once (similar to a [`ValueTask`](https://learn.microsoft.com/en-us/dotnet/api/system.threading.tasks.valuetask-1?view=net-8.0)).
* An `OVRTask` cannot be threaded nor block the main thread (it does not support `WaitAll`).

## Using OVRTask

Many methods in the Core SDK return an [`OVRTask`](/reference/unity/latest/class_o_v_r_task). There are three ways to use an `OVRTask`:

1. **Await** the task (`await SomeAsyncOperation()`)
1. **Callback** (`task.ContinueWith`)
1. **Poll** for completion (`task.IsCompleted`)

### Await

`await` provides a convient way to structure your code linearly:

```c#
async void OnButtonPress() {
    // Inform user the operation is beginning
    _ui.SetPendingIconEnabled(true);

    // Initiate the operation and await the result
    // (may take multiple frames)
    var result = await _anchor.SaveAsync();

    // Operation done; update UI
    _ui.SetPendingIconEnabled(false);

    // Process the result
    if (result) {
        // do something
    } else {
        // inform user
    }
}
```

**Note:** The containing method must use the `async` keyword.

### Callback

To use an `OVRTask` with callbacks, use the [`ContinueWith`](/reference/unity/latest/class_o_v_r_task/#adb064395f1336749781b1060e3d1c5b5) method:

```c#
void SaveAnchor(OVRSpatialAnchor anchor) {
    // Inform user the operation is beginning
    _ui.SetPendingIconEnabled(true);

    // Save and await the result (may take multiple frames)
    anchor.SaveAsync().ContinueWith(OnSaveComplete);
}

void OnSaveComplete(bool success) {
    _ui.SetPendingIconEnabled(false);
    Debug.Log($"SaveAnchor completed with result {success}");
}
```

**Note:** You should not `await` a task that you have provided a callback to.

This is not allowed:

```c#
async void OnButtonPress() {
    var task = _anchor.SaveAsync();

    task.ContinueWith(OnSave);

    await task; // error; throws InvalidOperationException because
                // the task is already being used with ContinueWith
}
```

#### Capturing additional state

When using a callback, you often need to remember more information associated with the task. `ContinueWith` has an overload that captures additional state. In the above example, the method `OnSaveComplete` only gets the result of the operation, without any other context. `ContinueWith` accepts an optional second parameter to provide additional state along with the result:

```c#
void SaveAnchor(OVRSpatialAnchor anchor) {
    // Inform user the operation is beginning
    _ui.SetPendingIconEnabled(true);

    // Provide a delegate and capture a reference to the anchor being saved
    anchor.SaveAsync().ContinueWith(OnSaveComplete, anchor);
}

void OnSaveComplete(bool success, OVRSpatialAnchor anchor) {
    _ui.SetPendingIconEnabled(false);
    Debug.Log($"SaveAnchor {anchor.Uuid} completed with result {success}");
}
```

### Polling

You can also poll for completion of an async task and extract the result with `task.GetResult()`.

```c#
class MyComponent : MonoBehaviour {
    OVRSpatialAnchor _anchor;

    OVRTask<bool>? _saveTask;

    public void OnSaveButtonPressed() {
        _saveTask = _anchor.SaveAsync();
    }

    void Update() {
        if (_saveTask?.IsCompleted == true) {
            var result = _saveTask.GetResult();
            _saveTask = null;

            // use result ...
        }
    }
}
```

**Note:** You can only retrieve the result once. In the example above, we used a nullable task to distinguish between a running task and a completed task that has a result available.

Similar to a `ValueTask`, you may only extract the result once:

```c#
if (task.IsCompleted) {
    var result = task.GetResult(); // okay; result retrieved

    result = task.GetResult(); // error; no result available
}
```

Polling also makes it easy to turn an OVRTask into a coroutine:

```c#
IEnumerator Save() {
    var task = _anchor.SaveAsync();

    // "Convert" task to a coroutine
    yield return new WaitUntil(() => task.IsCompleted);

    if (task.GetResult()) {
        // ...
    }
}
```

## Caveats

### Canceling a task

OVRTasks cannot be canceled. This is because the task itself is not doing any work; rather, it is waiting for a signal from a lower-level system, like the OS.

### Threading

While C# System Tasks often imply threading, OVRTasks always operate on the main thread and use a main thread message loop to receive their result. You should never block the main thread waiting on a OVRTask. For example, this would create an infinite loop:

```c#
var task = SomeAsyncOperation();
while (!task.IsCompleted) {
    // bad; infinite loop
}
```

As well as this:

```c#
// bad; blocks calling thread
Task.WaitAll(Task.Run(async() => anchor.SaveAsync()));
```

Do not use Task.Wait

Similarly, OVRTasks are not thread safe, and you should only check for completion (`IsCompleted`) and extract results (`GetResult()`) from the main thread.