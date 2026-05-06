# Ts Ovrmetricstool

**Documentation Index:** Learn about ts ovrmetricstool in this documentation.

---

---
title: "Monitor Performance with OVR Metrics Tool"
description: "Track frame rate, CPU/GPU usage, thermal throttling, and other performance metrics on Meta Quest."
last_updated: "2025-12-05"
---

OVR Metrics Tool is a performance monitoring solution for Meta Quest headsets that provides [a wide range of performance metrics](/documentation/unity/ts-ovrstats/), including frame rate, heat, GPU and CPU throttling, and the number of screen tears and stale frames per second. In addition to built-in system metrics, app developers can define custom metrics to track application-specific performance or debugging information. Metrics can be visualized in real time through the onscreen overlay or automatically recorded in CSV reports for later analysis. OVR Metrics Tool also offers remote device management functionality and is available on the [Meta Horizon Store](https://www.meta.com/experiences/ovr-metrics-tool/2372625889463779/).

Additional information on OVR Metrics Tool can be found in [OVR Metrics Tool - Best Practices for Non-Engineers](/documentation/unity/ts-ovr-best-practices/).

## Overview

OVR Metrics Tool is a Meta Quest tool that can provide performance information about a running app. Much of this performance information is similar to that provided by `VrApi` Logcat logs. OVR Metrics Tools provides access to that information from an on-device app rather than the command line. See the [Logcat](/documentation/unity/ts-logcat/) topic for information on using Logcat.

OVR Metrics Tool has two modes. In **Report Mode**, the tool records a performance report about a VR session that can be read after it has concluded. Report data can be easily exported as a CSV with PNG images. In **Performance HUD Mode**, the tool displays a HUD overlay over running apps that provides real-time performance graphs and information. The information displayed on the performance HUD can be customized to preference.

App developers can define and report custom metrics directly from their applications, providing targeted insights for debugging and optimization during development. These metrics can be monitored in real time on the HUD overlay or recorded in CSV files for later analysis. By tracking application-specific performance indicators alongside standard system metrics, developers gain deeper visibility into app behavior, enabling faster identification and resolution of issues.

OVR Metrics Tool has a number of extra GPU statistics that require the profiling tool `ovrgpuprofiler` to be enabled from a connected shell. See the [ovrgpuprofiler](/documentation/unity/ts-ovrgpuprofiler/) for information on enabling the tool.

See the [OVR Metrics Tool and VrApi Stats Guide](/documentation/unity/ts-ovrstats/) for information on all of the statistics OVR Metrics Tool can track.

## Collect performance data with OVR Metrics Tool

The following sections describe how to install OVR Metrics Tool and use it to capture performance information for an app in **Report Mode** and **Performance HUD Mode**.

### Installation

Please install the latest version from the [Meta Horizon Store](https://www.meta.com/experiences/ovr-metrics-tool/2372625889463779/). Or search for **ovr metrics tool** and download the app from the App **Store** on headset.

### OVR Metrics Tool usage

The OVR Metrics Tool app can be launched from the headset's app **Library**. It can also be launched by connecting the headset to a computer via ADB and issuing the following command:

```
adb shell am start omms://app
```

When OVR Metrics Tool launches on the headset, the main screen looks like this:

From the main screen you can enable and configure the features of the tool.

#### Report mode

Report mode records performance data from a VR session. Data from the reports can be retrieved from the headset or viewed while in the headset.

To enable report mode, enable the toggle labeled **Record all captured metrics to csv files**. You can also enable report mode by issuing the following ADB command:

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_CSV
```

Once enabled, you must manually select the metrics to be collected on the **Stats** tab or by using one of the preset option buttons:

* **Basic** includes the following metrics:
  * Battery level
  * CPU level
  * GPU level
  * Average FPS
  * Stale Frame Count
  * CPU Utilization
  * GPU Utilization
  * App GPU Time

* **Advanced** includes everything in **Basic** as  well as the following metrics:
  * Foveation level
  * Early Frame count
  * Eye buffer width
  * Eye buffer height
  * Timewarp GPU Time
  * VrShell+Boundary GPU Time
  * Spacewarp FPS
  * Max Consecutive Stale Frames

OVR Metrics Tool provides access to a wide range of performance metrics for HUD display. Users can manually select which metrics to show in the HUD overlay using the Stats tab for real-time text readout, and the Graphs tab to visualize short-term historical trends for those metrics. This allows you to tailor the HUD to display the specific performance data most relevant to your application or analysis needs. Some advanced GPU statistics require enabling the profiling tool `ovrgpuprofiler` from a connected shell before they can be selected. For more information, see [GPU Profiling with ovrgpuprofiler](/documentation/unity/ts-ovrgpuprofiler/).

These statistics include the following:

* Boundary GPU Time
* CPU Utilization Core 0-7
* Application VSS, RSS, and Dalvik PSS

##### `ovrgpuprofiler` metrics

* Average Vertices Per Frame
* Average Fill Percentage per Eye
* Average Instructions per Fragment
* Average Instructions per Vertex
* Average Textures per Fragment
* Percentage Time Shading Fragments
* Percentage Time Shading Vertices
* Vertex Fetch Stall Percentage
* Texture Fetch Stall Percentage
* L1 Texture Miss Percentage
* L2 Texture Miss Percentage
* Texture Sample Percentage Using Nearest Filtering
* Texture Sample Percentage Using Linear Filtering
* Texture Sample Percentage Using Anisotropic Filtering

See [Statistics enabled with ovrgpuprofiler](/documentation/unity/ts-ovrstats#statistics-enabled-with-ovrgpuprofiler) for more information about these data points.

After choosing metrics, run an app and conduct a session to collect data. Note that data is logged for every app run. After the session, open OVR Metrics Tool, click the drop-down menu in the upper-right corner, and select **View Recorded Sessions**. Select the entry that corresponds to your session to see a series of graphs describing performance. Recorded sessions can be retrieved from `/sdcard/Android/data/com.oculus.ovrmonitormetricsservice/files/CapturedMetrics/` as a CSV file when the device is connected to a computer. [Meta Quest Developer Hub](/documentation/unity/ts-mqdh-file-manager) can be used for retrieving metrics from the **File Manager**.

#### Performance HUD mode

Performance HUD mode displays a real-time graph showing selected metrics over running apps. To enable performance HUD mode, from the main screen, turn on **Enable Persistent Overlay (may require reboot)**. You can also enable the HUD by issuing the ADB command:

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_OVERLAY
```

After enabling the HUD, you may have to reboot your headset for it to appear. The HUD resembles the following depending on the chosen metrics:

By default, no metrics are displayed on the HUD, although the FPS graph is present. You must manually select the displayed metrics on the **Stats** tab or by using the **Basic** and **Advanced** buttons described in the **Report Mode** section. The **Graphs** tab is used to configure which graphs will be shown on the overlay.

Below the buttons are more options for the overlay. Stats and metrics on the graph can be toggled (enabled by default). The **Render Overlay on GPU** option toggles hardware rendering of the overlay and is enabled by default.

**Lock Overlay to Head** is enabled by default. Disabling this unlocks the HUD from view and positions it in space, but this can be unpredictable and is not recommended. Below this toggle are options for the scale and position of the HUD.

The **Screenshot on Dropped Frames** option is at the bottom of the main screen. Enabling this option takes a screenshot if the number of dropped frames exceeds the set limit for the specified time.

Many of the HUD's options can be controlled from the command line:

#### Enable overlay

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_OVERLAY
```

This command can take the following optional parameters:

```
--eb headlocked (true|false) // whether the overlay should be locked to view
--ef pitch (-90.0 to 90.0) // the pitch of the overlay (negative is down)
--ef yaw (-180.0 to 180.0) // the yaw of the overlay (negative is left)
--ei scale (1, 2, or 3) // the scale of the overlay
--ef distance (0.1+) // the distance the overlay appears (headlocked only)
```

#### Disable overlay

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_OVERLAY
```

#### Enable/disable all graphs or stats

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_GRAPH // enable all graphs
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_STATS // enable all stats
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_GRAPH // disable all graphs
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_STATS // disable all stats
```

#### Enable/disable individual graphs or stats

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_GRAPH --es stat <stat> // add graph for <stat> to overlay
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_STAT --es stat <stat> // add <stat> to overlay
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_GRAPH --es stat <stat> // disable graph for <stat>
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_STATS --es stat <stat> // remove <stat> from overlay
```

Valid values for `<stat>` are listed below. The statistics they represent are described in the [OVR Metrics Tool and VrApi Stats Guide](/documentation/unity/ts-ovrstats/).

```
"available_memory_MB",
"app_pss_MB",
"battery_level_percentage",
"battery_temperature_celcius",
"battery_current_now_milliamps",
"sensor_temperature_celcius",
"power_current",
"power_level_state",
"power_voltage",
"power_wattage",
"cpu_level",
"gpu_level",
"cpu_frequency_MHz",
"gpu_frequency_MHz",
"mem_frequency_MHz",
"minimum_vsyncs",
"extra_latency_mode",
"average_frame_rate",
"display_refresh_rate",
"average_prediction_milliseconds",
"screen_tear_count",
"early_frame_count",
"stale_frame_count",
"maximum_rotational_speed_degrees_per_second",
"foveation_level",
"eye_buffer_width",
"eye_buffer_height",
"app_gpu_time_microseconds",
"timewarp_gpu_time_microseconds",
"guardian_gpu_time_microseconds",
"cpu_utilization_percentage",
"cpu_utilization_percentage_core0",
"cpu_utilization_percentage_core1",
"cpu_utilization_percentage_core2",
"cpu_utilization_percentage_core3",
"cpu_utilization_percentage_core4",
"cpu_utilization_percentage_core5",
"cpu_utilization_percentage_core6",
"cpu_utilization_percentage_core7",
"gpu_utilization_percentage",
"spacewarp_motion_vector_type",
"spacewarped_frames_per_second",
"app_vss_MB",
"app_rss_MB",
"app_dalvik_pss_MB",
"app_private_dirty_MB",
"app_private_clean_MB",
"app_uss_MB",
"stale_frames_consecutive",
"avg_vertices_per_frame",
"avg_fill_percentage",
"avg_inst_per_frag",
"avg_inst_per_vert",
"avg_textures_per_frag",
"percent_time_shading_frags",
"percent_time_shading_verts",
"percent_time_compute",
"percent_vertex_fetch_stall",
"percent_texture_fetch_stall",
"percent_texture_l1_miss",
"percent_texture_l2_miss",
"percent_texture_nearest_filtered",
"percent_texture_linear_filtered",
"percent_texture_anisotropic_filtered",
"vrshell_average_frame_rate",
"vrshell_gpu_time_microseconds",
"vrshell_and_guardian_gpu_time_microseconds"
```

#### Additional adb configuration options

```
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_CSV // records all metrics to CSV files in /sdcard/Android/data/com.oculus.ovrmonitormetricsservice/files/CapturedMetrics/
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_CSV // disables writing metrics to disk
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.ENABLE_DROPPED_FRAME_SCREENSHOT --ei count <count> --ei time <time> // enables functionality that will take a screenshot if <count> frames are missed within a window of <time>
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.DISABLE_DROPPED_FRAME_SCREENSHOT // disables automatic screenshot functionality
adb shell am broadcast -n com.oculus.ovrmonitormetricsservice/.SettingsBroadcastReceiver -a com.oculus.ovrmonitormetricsservice.LOG_STATE // prints a json blob to logcat with the current configuration state of OVR Metrics Tool, useful for automated tooling
```

## OVR Metrics Tool toolkit for Unity

The OVR Metrics Tool toolkit exposes functionality to integrate OVR Metrics data into your application, and allows your application to add data to OVR Metrics Tools' reports and overlays.

### Download the toolkit

* Download the toolkit package from <a href="/downloads/package/ovr-metrics-tool-sdk">this page</a>.
* Extract the `Unity/OVRMetricsToolSDK.unitypackage` file from the downloaded package.
* In Unity, go to **Assets** > **Import Package** > **Custom Package** and import the package.

### Accessing metrics data

Metrics data can be accessed as an `OVRMetricsToolSDK.MetricsSnapshot` struct, which contains a timestamp and fields for all the values for `<stat>` listed above.

The following snippet shows one possible method to display OVR Metrics data in your Unity app:

```
    void Update()
    {
        var metrics = OVRMetricsToolSDK.Instance.GetLatestMetricsSnapshot();
        GetComponent<TMP_Text>().text = metrics?.cpu_utilization_percentage.ToString() ?? "";
    }
```

### Append CSV debug string

`AppendCSVDebugString` is used to append custom developer data into the CSV that is produced when the **Record To CSV** option is selected. The debug data will be placed in the last column of the CSV, if data is entered at a higher frequency than 1 Hz, data will be placed on a new line in the CSV with empty metric columns. The suggested use for this is to mark scene transitions to aid in CSV analysis.

This method can be accessed by calling:

```
OVRMetricsToolSDK.Instance.AppendCsvDebugString(debugString);
```

### Set overlay debug string

`SetOverlayDebugString` is used to display custom information on the OVR Metrics Tool overlay. To make this data visible, make sure **Show Debug Data** is enabled. The data appears at the bottom of the overlay. The overlay debug string can be updated once per frame and is not persisted. By default the overlay debug string is white, but the color can be customized by including `<color=#{hex value}>...</color>` tags in the string.

The following method sets the string in the native Mobile SDK:

```
OVRMetricsToolSDK.Instance.SetOverlayDebugString(debugString);
```

### Define and submit app metrics

Define custom metrics to track application-specific data, for example, player health, enemy count, or any game-specific performance indicators. Call this once during initialization, typically in `Start()`:

```csharp
void Start()
{
    // Player Health Metric
    OVRMetricsToolSDK.Instance.DefineAppMetric(
        name: "player_health",
        displayName: "Player Health",
        group: "Player",
        rangeMin: 0,
        rangeMax: 100,
        graphMin: 0,
        graphMax: 100,
        redPercent: 0.2f,
        greenPercent: 0.7f,
        showGraph: true,
        showStat: true
    );

    // Active Enemies Metric
    OVRMetricsToolSDK.Instance.DefineAppMetric(
        name: "active_enemies",
        displayName: "Active Enemies",
        group: "Gameplay",
        rangeMin: 0,
        rangeMax: 50,
        graphMin: 0,
        graphMax: 50,
        redPercent: 0.8f,
        greenPercent: 0.5f,
        showGraph: true,
        showStat: true
    );
}
```

**Metric Parameters:**
* `name`: Unique identifier for the metric
* `displayName`: Human-readable name shown in OVR Metrics Tool
* `group`: Category for organizing metrics (for example, "Player", "Gameplay")
* `rangeMin`/`rangeMax`: Valid value range for the metric
* `graphMin`/`graphMax`: Range for graph display
* `redPercent`: Threshold for red warning indicator (0.0 to 1.0)
* `greenPercent`: Threshold for green indicator (0.0 to 1.0)
* `showGraph`: Display metric in graph view
* `showStat`: Display metric as text statistic

After defining your custom metrics, update them periodically (typically in `Update()` or when values change):

```csharp
void Update()
{
    // Update metrics with current values
    OVRMetricsToolSDK.Instance.UpdateAppMetric("player_health", currentHealth);
    OVRMetricsToolSDK.Instance.UpdateAppMetric("active_enemies", enemyCount);
}
```

**Best Practice:** Don't submit metrics every frame if values haven't changed. Update only when necessary to reduce overhead.

### Core Metrics Reporting

Unity built-in metrics can be automatically tracked and visualized in OVR Metrics Tool through the Core Metrics Reporting component. This feature provides real-time monitoring of critical Unity performance data including memory usage and rendering statistics, making it easier to identify performance bottlenecks during development.

To enable Core Metrics Reporting in your Unity project:

1. Select the **Camera Rig** GameObject in your scene hierarchy.
2. In the Inspector, click **Add Component** and search for **OVR Metrics Manager**.
3. Once added, the component UI provides options to configure which metrics to monitor:
   - **Memory Metrics**: Track memory allocation and usage patterns
   - **Render Metrics**: Monitor rendering performance data

For each metric category, you can choose the visualization format:
* **Stats**: Display metrics as numerical text values in the overlay
* **Graphs**: Visualize metrics as real-time graphs showing trends over time

These Unity metrics integrate seamlessly with the OVR Metrics Tool overlay and CSV recording features, appearing alongside system-level performance data. This unified view allows you to correlate Unity-specific metrics with device performance metrics for comprehensive analysis.

## See also

* [OVR Metrics Tool and VrApi Stats Guide](/documentation/unity/ts-ovrstats/)
* [Developer Tools for Meta Quest](/resources/developer-tools/)