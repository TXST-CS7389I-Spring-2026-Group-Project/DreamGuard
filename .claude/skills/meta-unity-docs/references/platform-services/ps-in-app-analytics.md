# Ps In App Analytics

**Documentation Index:** Learn about ps in app analytics in this documentation.

---

---
title: "In-App Analytics"
description: "Track user behavior with segments, metric events, and event counters in your Meta Quest Unity app."
last_updated: "2026-03-25"
---

<oc-devui-note type="note" heading="This is a Platform SDK feature requiring Data Use Checkup"> To use this or any other Platform SDK feature, you must <a href="/resources/publish-data-use/">complete a Data Use Checkup (DUC)</a>. The DUC ensures that you comply with Developer Policies. It requires an administrator from your team to certify that your use of user data aligns with platform guidelines. Until the app review team reviews and approves your DUC, platform features are only available for <a href="/resources/test-users/">test users</a>.</oc-devui-note>

In-App Analytics lets you track user behavior and app performance within your Meta Quest Unity application. Use it to record timed segments (such as gameplay sessions, menu visits, or tutorials), fire one-off metric events, and aggregate high-frequency counters. All data is sent to the Meta analytics backend for analysis.

The API is organized around three concepts:

- **Segments** — Named time intervals that represent a phase of user activity, for example a match, a tutorial, or a menu visit. You open a segment when the phase begins and close it when the phase ends. The platform records the duration automatically.
- **Metric events** — Individual data points with a name and numeric value. Use these for discrete measurements such as scores, distances, or action counts.
- **Event counters** — Lightweight accumulators for high-frequency metrics. Create a counter, increment it as events occur, and send the aggregated total when you are ready. Counters support auto-flush so you do not have to manage send timing manually.

## Prerequisites

Before using In-App Analytics:

1. Register your app on the [Developer Dashboard](/manage).
2. Set up the Meta Quest Platform SDK in your Unity project. For more information, see [Getting started](/documentation/unity/unity-getting-started).
3. Initialize the Platform SDK in your app by calling `Core.AsyncInitialize()`.

## Segments

Segments represent timed phases of user activity. Open a segment at the start of an activity and close it when the activity ends.

### Open a segment

```csharp
using Oculus.Platform;
using Oculus.Platform.Models;

Message<SegmentEvent> msg = await InAppAnalytics.OpenSegment("test_segment");
if (msg.IsError)
{
    Debug.LogError($"OpenSegment failed: {msg.data}");
    return;
}
SegmentEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Close a segment

Closing a segment records the elapsed duration automatically.

```csharp
Message<SegmentEvent> msg = await InAppAnalytics.CloseSegment("test_segment");
if (msg.IsError)
{
    Debug.LogError($"CloseSegment failed: {msg.data}");
    return;
}
SegmentEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Close all open segments

```csharp
Message<SegmentEvent[]> msg = await InAppAnalytics.CloseAllOpenSegments();
if (msg.IsError)
{
    Debug.LogError($"CloseAllOpenSegments failed: {msg.data}");
    return;
}
foreach (var segment in msg.Data)
{
    Debug.Log(segment.ToJson());
}
```

### List active segments

```csharp
Message<SegmentEvent[]> msg = await InAppAnalytics.GetAllSegments();
if (msg.IsError)
{
    Debug.LogError($"GetAllSegments failed: {msg.data}");
    return;
}
foreach (var segment in msg.Data)
{
    Debug.Log(segment.ToJson());
}
```

## Metric events

### Send a one-off metric event

For simple measurements, use `SendEvent` with a metric name and a numeric value:

```csharp
string metricName = "test_event";
float value = 1.0f;

Message msg = await InAppAnalytics.SendEvent(metricName, value);
if (msg.IsError)
{
    Debug.LogError($"SendEvent failed: {msg.data}");
    return;
}
Debug.Log(msg.data);
```

### Queue metric events for batch processing

For events that should be batched before sending, use `QueueMetricEvent` with a `MetricEventInput`:

```csharp
using Oculus.Platform;
using Oculus.Platform.Models;

MetricEventInput metricEvent = new MetricEventInput()
{
    MetricName = "queued_metric",
    Value = 1.0f,
    MetricType = MetricType.Action,
    Timestamp = 1773088361
};

Message msg = await InAppAnalytics.QueueMetricEvent(metricEvent);
if (msg.IsError)
{
    Debug.LogError($"QueueMetricEvent failed: {msg.data}");
    return;
}
Debug.Log(msg.data);
```

### Queue segment events for batch processing

You can queue detailed segment events with custom metadata using `SegmentEventInput`:

```csharp
SegmentEventInput segEvent = new SegmentEventInput()
{
    SegName = "queued_segment"
};

Message msg = await InAppAnalytics.QueueSegmentEvent(segEvent);
if (msg.IsError)
{
    Debug.LogError($"QueueSegmentEvent failed: {msg.data}");
    return;
}
Debug.Log(msg.data);
```

## Event counters

Event counters are ideal for high-frequency metrics where you want to accumulate a total before sending. By default, counters auto-flush after a timeout so you do not need to manage send timing.

### Create a counter

Create a counter with an initial value of 1:

```csharp
string counterName = "test_counter";
bool? manualFlush = false;

Message<MetricEvent> msg = await InAppAnalytics.CreateEventCounter(counterName, manualFlush);
if (msg.IsError)
{
    Debug.LogError($"CreateEventCounter failed: {msg.data}");
    return;
}
MetricEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Increment a counter

```csharp
string counterName = "test_counter";
float amount = 1.0f;

Message<MetricEvent> msg = await InAppAnalytics.IncrementEventCounter(counterName, amount);
if (msg.IsError)
{
    Debug.LogError($"IncrementEventCounter failed: {msg.data}");
    return;
}
MetricEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Get counter value

Check the current value of a counter without sending it:

```csharp
Message<MetricEvent> msg = await InAppAnalytics.GetEventCounter("test_counter");
if (msg.IsError)
{
    Debug.LogError($"GetEventCounter failed: {msg.data}");
    return;
}
MetricEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Send a counter

Send the counter as a metric event and remove it from tracking:

```csharp
Message<MetricEvent> msg = await InAppAnalytics.SendEventCounter("test_counter");
if (msg.IsError)
{
    Debug.LogError($"SendEventCounter failed: {msg.data}");
    return;
}
MetricEvent model = msg.Data;
Debug.Log(model.ToJson());
```

### Manual flush mode

Pass `manualFlush: true` when creating a counter to disable auto-flush. You are then responsible for calling `SendEventCounter` explicitly:

```csharp
await InAppAnalytics.CreateEventCounter("coins_collected", manualFlush: true);
```

### List tracked counters

```csharp
Message<EventCounterNames> msg = await InAppAnalytics.GetAllEventCounterNames();
if (msg.IsError)
{
    Debug.LogError($"GetAllEventCounterNames failed: {msg.data}");
    return;
}
EventCounterNames model = msg.Data;
Debug.Log(model.ToJson());
```

## Using callbacks

All methods also support the callback pattern with `OnComplete`:

```csharp
InAppAnalytics.OpenSegment("test_segment").OnComplete((Message<SegmentEvent> msg) =>
{
    if (msg.IsError)
    {
        Debug.LogError($"OpenSegment failed: {msg.data}");
        return;
    }
    SegmentEvent model = msg.Data;
    Debug.Log(model.ToJson());
});
```

## API reference

| Method | Description | Returns |
|--------|-------------|---------|
| `OpenSegment(segName)` | Opens a named segment and records a START event. | `Request<SegmentEvent>` |
| `CloseSegment(segName)` | Closes a named segment and records an END event with duration. | `Request<SegmentEvent>` |
| `CloseAllOpenSegments()` | Closes all active segments. | `Request<SegmentEvent[]>` |
| `GetAllSegments()` | Returns all currently active segments without closing them. | `Request<SegmentEvent[]>` |
| `SendEvent(metricName, value)` | Sends a single metric event immediately. | `Request` |
| `QueueMetricEvent(event)` | Queues a metric event for batch processing. | `Request` |
| `QueueSegmentEvent(event)` | Queues a segment event for batch processing. | `Request` |
| `CreateEventCounter(counterName, manualFlush?)` | Creates a counter with an initial value of 1. | `Request<MetricEvent>` |
| `IncrementEventCounter(counterName, amount)` | Increments an existing counter by the given amount. | `Request<MetricEvent>` |
| `SendEventCounter(counterName)` | Sends the counter value and removes the counter. | `Request<MetricEvent>` |
| `GetEventCounter(counterName)` | Gets the current counter value without sending. | `Request<MetricEvent>` |
| `GetAllEventCounterNames()` | Returns the names of all tracked counters. | `Request<EventCounterNames>` |

## Data models

### MetricEvent

Returned by counter and metric event operations.

| Property | Type | Description |
|----------|------|-------------|
| `MetricName` | `string` | Name of the metric. |
| `Value` | `float` | Numeric value of the metric. |
| `SegId` | `string` | Segment ID the metric is attributed to. |
| `Timestamp` | `long` | Unix timestamp in seconds when the event was recorded. |
| `SeqNum` | `long` | Sequence number for ordering events. |
| `MetricType` | `MetricType` | Category of the metric. |
| `PositionOptional` | `Position` | Optional 3D position (x, y, z) associated with the event. |

### SegmentEvent

Returned by segment operations (open, close, list).

| Property | Type | Description |
|----------|------|-------------|
| `SegName` | `string` | Name of the segment. |
| `SegId` | `string` | Unique identifier for the segment instance. |
| `EventType` | `SegmentEventType` | Type of segment event. |
| `SegType` | `SegmentType` | Category of the segment. |
| `Timestamp` | `long` | Unix timestamp in seconds when the event occurred. |
| `DurationS` | `float` | Duration of the segment in seconds (populated on close). |
| `SeqNum` | `long` | Sequence number for ordering events. |
| `MatchId` | `string` | Optional match identifier for multiplayer sessions. |
| `PositionOptional` | `Position` | Optional 3D position associated with the event. |
| `SettingsOptional` | `SegmentSettings` | Optional segment settings (cosmetics, difficulty, game mode). |

### MetricEventInput

Used with `QueueMetricEvent` to provide detailed metric event data.

| Field | Type | Description |
|-------|------|-------------|
| `MetricName` | `string` | Name of the metric (required). |
| `Value` | `float` | Numeric value (required). |
| `SegId` | `string` | Segment ID to attribute the event to. |
| `Timestamp` | `long` | Custom Unix timestamp in seconds. |
| `SeqNum` | `long` | Sequence number for ordering. |
| `MetricType` | `MetricType` | Category of the metric. |
| `Position` | `PositionInput` | 3D position (x, y, z). |

### SegmentEventInput

Used with `QueueSegmentEvent` to provide detailed segment event data.

| Field | Type | Description |
|-------|------|-------------|
| `SegName` | `string` | Name of the segment (required). |
| `SegId` | `string` | Segment ID. |
| `EventType` | `SegmentEventType` | Type of segment event. |
| `SegType` | `SegmentType` | Category of the segment. |
| `Timestamp` | `long` | Custom Unix timestamp in seconds. |
| `DurationS` | `float` | Duration in seconds. |
| `SeqNum` | `long` | Sequence number for ordering. |
| `MatchId` | `string` | Match identifier for multiplayer sessions. |
| `Position` | `PositionInput` | 3D position (x, y, z). |
| `Settings` | `SegmentSettingsInput` | Segment settings (cosmetics, difficulty, game mode). |

### EventCounterNames

Returned by `GetAllEventCounterNames`.

| Property | Type | Description |
|----------|------|-------------|
| `Names` | `string[]` | Array of tracked counter names. |