# Ts Ovrgpuprofiler

**Documentation Index:** Learn about ts ovrgpuprofiler in this documentation.

---

---
title: "Use ovrgpuprofiler for GPU Profiling"
description: "Access real-time GPU metrics and perform render stage traces on Meta Quest using ovrgpuprofiler CLI."
---

`ovrgpuprofiler` is a performance monitoring CLI tool for Meta Quest headsets that developers can use to access a range of real-time GPU metrics and perform render stage traces. `ovrgpuprofiler` is included with the Meta Quest runtime and does not need to be manually installed.

## Use ovrgpuprofiler to Retrieve Real-time Metrics

Open a shell via [ADB](/documentation/unity/ts-adb/) on a connected Meta Quest before using `ovrgpuprofiler`. If not using a shell, precede all commands in this topic with `adb shell <command>`.

### Get Metrics List

To list all supported real-time metrics and their ID number, enter the following from the command line when a Meta Quest is connected via ADB:

```
ovrgpuprofiler -m
```

The beginning of the output for this command looks like the following:

```
    47 metrics supported:
    1       Clocks / Second
    2       GPU % Bus Busy
    3       % Vertex Fetch Stall
    4       % Texture Fetch Stall
    5       L1 Texture Cache Miss Per Pixel
    6       % Texture L1 Miss
    7       % Texture L2 Miss
    8       % Stalled on System Memory
    9       Pre-clipped Polygons/Second
    10      % Prims Trivially Rejected
    11      % Prims Clipped
```

**Note:** The metric count shown in this example output (47) is specific to the device and runtime version used when the output was captured. The actual number of supported metrics varies by device and runtime version.

Alternatively, `ovrgpuprofiler -m -v` provides the same list with more verbose descriptions for each metric.

### Get Metric Data

To retrieve data for a metric, the command takes the following format:

```
ovrgpuprofiler -r<metric ID number>
```

For example, to retrieve the metric **Texture Fetch Stall** (ID number 4), enter `ovrgpuprofiler -r4` and the console prints data every second until you press **Ctrl-C**.

#### Get Data for Multiple Metrics

You can also request multiple metrics at once by separating ID numbers with commas in a string, such as `ovrgpuprofiler -r"4,5,6"`. The following shows output from `ovrgpuprofiler -r"4,5,6"`:

```
$ ovrgpuprofiler -r"4,5,6"
% Texture Fetch Stall                      :           2.449
L1 Texture Cache Miss Per Pixel            :           0.124
% Texture L1 Miss                          :          20.338

% Texture Fetch Stall                      :           2.369
L1 Texture Cache Miss Per Pixel            :           0.122
% Texture L1 Miss                          :          20.130

% Texture Fetch Stall                      :           2.580
L1 Texture Cache Miss Per Pixel            :           0.127
% Texture L1 Miss
...
```

**Note:** Do not request more than 30 real-time metrics simultaneously.

## Use ovrgpuprofiler for Render Stage Tracing

`ovrgpuprofiler` supports render stage GPU tracing on a tile-per-tile level. Unlike direct-mode GPUs, which execute draw calls sequentially, tile-based renderers batch draw calls for an entire surface. That surface is then split into tiles that are computed sequentially, where each tile executes all the draw calls that touched that tile. `ovrgpuprofiler` can tell you how much time was spent in each rendering stage for each surface rendered during a trace's duration.

### Prepare for Render Stage Tracing

Tracing on a tile-per-tile level requires the app's GPU context to be in detailed GPU profiling mode. To set the OS to start subsequent apps in detailed GPU profiling mode, enter the following command:

```
ovrgpuprofiler -e
```

If an app is running when the command is entered, it must be restarted for the change to take effect.

`ovrgpuprofiler -i` shows whether detailed GPU profiling mode is currently enabled. Use `ovrgpuprofiler -d` to disable it.

In addition, apps being used with `ovrgpuprofiler` may require the `<uses-permission android:name="android.permission.INTERNET" />` permission in their manifest.

**Note:** Detailed GPU profiling incurs an approximately 10% overhead in GPU rendering times. Keep this overhead in mind when reading trace output.

**Note:** The INTERNET permission requirement may vary by runtime version. Verify that this permission is required for your specific setup and target runtime.

### Execute a Trace

To execute a 100 ms trace on the currently running app, enter the following:

```
ovrgpuprofiler -t
```

Trace length can be specified in seconds by including a number with the `-t` argument. For example, `ovrgpuprofiler -t1.2` would run a trace for 1.2 seconds.

The output of the trace is printed to the console, listing the surfaces rendered during the trace along with render stage information.

### Read a Trace

Lines from the trace output look like the following:

```
    Surface 1    | 1216x1344 | color 32bit, depth 24bit, stencil 0 bit, MSAA 4 | 60  128x224 bins | 5.08 ms | 130 stages :  Binning : 0.623ms Render : 1.877ms StoreColor : 0.309ms Blit : 0.002ms Preempt : 1.286ms
```

This shows that Surface 1 has a resolution of 1216x1344, 32-bit color, 24-bit depth, and uses MSAA 4. The surface was broken down into 60 tiles/bins with a size of 128x224, and it took 5.08 ms to render in total. There were 130 render stage executions in the process, and the remaining fields show how much time was spent in each render stage. Not every render stage appears for each surface.

#### Multiview Surface Output by Device

On Meta Quest, `ovrgpuprofiler` outputs one surface line per slice for multiview apps. This means that there is one surface for each eye. You must add the render times of two eye surfaces for the total frame time.

On Meta Quest 2, however, `ovrgpuprofiler` outputs one surface line for both views of the surface, due to how the Adreno 650v3 GPU processes multiview commands (Hardware Multiview). On Meta Quest 2, bins of multiview surfaces are shared between both views. Therefore, the following output:

```
135 96x176 bins
```

should be interpreted as:

```
135 96x176x2 bins
```

Meta Quest 3 and Meta Quest 3S both use the Adreno 740v3 GPU (Meta Quest 3 at 690 MHz, Meta Quest 3S at 492 MHz). Whether `ovrgpuprofiler` outputs one surface line per eye (as on Meta Quest) or one surface line for both views (as on Meta Quest 2 with Hardware Multiview) has not been verified for these devices. Refer to the release notes for your runtime version or contact developer support for confirmation.

Render stages that appear include the following:

* **Binning** - The Meta Quest's GPU uses a tiled architecture, meaning that all draw calls for a frame are executed in two stages. The first stage is the binning phase, where triangle vertex positions for all draw calls are calculated and assigned to bins that correspond to a partition of the drawing surface.
* **Render** - This is the second stage of the draw call that began with binning. One chunk of this represents the total cost of all vertex and fragment operations for one bin. A simplified version of the vertex shaders is executed during binning for the purpose of finding a triangle's position. The full version of the vertex shaders is re-executed to compute the interpolants used by the fragment shader during this stage.
* **LoadColor** - Loads the color from slow memory into fast memory. This can happen when starting to render into a surface without clearing it.
* **StoreColor** - After an entire bin of pixel and fragment operations are done executing, the calculated color value is copied from fast memory (dedicated for the bin's rendering operations) to slow memory.
* **Blit** - Copying between slow memory regions. This can happen for various operations, such as mipmap generation and when clearing a surface without rendering anything.
* **Preempt** - The compositor is an OS-level service that executes at regular intervals to present the image submitted by the application to the screen. To deliver the image at the proper cadence, the GPU preempts the application's workload so the compositor can complete its work on time.

## Command-Line Argument Reference

The following command-line arguments are available for `ovrgpuprofiler`:

<table>
  <tr>
   <td><strong>Argument</strong>
   </td>
   <td><strong>Description</strong>
   </td>
  </tr>
  <tr>
   <td>-r/--realtime
   </td>
   <td>Prints the value of the real-time metrics every second. Accepts an optional comma-separated list of metrics IDs to track.
   </td>
  </tr>
  <tr>
   <td>-m/--metrics
   </td>
   <td>Prints the list of available real-time metrics IDs, their name, and their description.
   </td>
  </tr>
  <tr>
   <td>-v/--verbose
   </td>
   <td>Adds more detailed information to most other commands.
   </td>
  </tr>
  <tr>
   <td>-e/--enable-detailed
   </td>
   <td>Enables detailed profiling mode on the GPU driver; required for render stage tracing. Only applies to applications started after this mode is started.
   </td>
  </tr>
  <tr>
   <td>-d/--disable-detailed
   </td>
   <td>Disables detailed profiling mode on the GPU driver.
   </td>
  </tr>
  <tr>
   <td>-i/--is-detailed
   </td>
   <td>Queries if the GPU driver is in detailed profiling mode.
   </td>
  </tr>
  <tr>
   <td>-t/--trace
   </td>
   <td>Executes a render stage trace, with an optional trace length as argument in seconds.
   </td>
  </tr>
   <tr>
   <td>-c/--continuous
   </td>
   <td>If you specify this along with -t/--trace, the results of the render stage trace are polled periodically to reduce memory pressure.
   </td>
  </tr>
  <tr>
   <td>-l/--low-overhead
   </td>
   <td>If you specify this along with -t/--trace, the render stage trace is performed in low-overhead mode, which omits many details in exchange for a more accurate measurement.
   </td>
  </tr>
</table>

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)