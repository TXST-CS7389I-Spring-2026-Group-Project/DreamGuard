# Ts Gpumeminfo

**Documentation Index:** Learn about ts gpumeminfo in this documentation.

---

---
title: "Track GPU Memory Usage with gpumeminfo"
description: "Monitor real-time GPU memory allocation and usage on Meta Quest headsets using gpumeminfo."
---

Gpumeminfo is a performance monitoring CLI tool for Meta Quest headsets that tracks GPU memory usage per process. It provides a more in-depth view of graphics memory than `dumpsys meminfo`. It is included with the Meta Quest runtime and does not need to be manually installed.

## Use gpumeminfo to Track GPU Memory Usage on a Process

You can use `gpumeminfo` to track the GPU memory usage on a process with the following command:

```
adb shell gpumeminfo -p $(pidof <process-name>)
```

To list GPU memory statistics for all running processes, use the following command:

```
adb shell gpumeminfo -l
```

`gpumeminfo` will continuously run and print the list of allocations and sizes in MB. The output for the `-p` command looks like the following:

## Command-Line Argument Reference

The following command-line arguments are available for use with `gpumeminfo`:

**Argument**|**Description**
:-:|:--
-h|Prints help message.
-m|Prints the memory information in machine-readable format.
-o|Prints the memory information once and exits.
-p|Specifies which application to attach to.
-s|Specifies the sampling interval.
-d|Dump system overall statistics.
-l|Lists GPU memory statistics of all running processes.
-t|Sort the list with memory usage type in the following order: any(0), arraybuffer, cl, cl_image_nomap, cl_kernel_stack, command, egl_image, egl_surface, gl, texture, vk_cmdbuffer, vk_devicememory.

## See Also

* [Developer Tools for Meta Quest](/resources/developer-tools/)