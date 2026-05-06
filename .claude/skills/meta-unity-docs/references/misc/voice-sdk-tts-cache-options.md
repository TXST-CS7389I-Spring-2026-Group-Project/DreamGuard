# Voice Sdk Tts Cache Options

**Documentation Index:** Learn about voice sdk tts cache options in this documentation.

---

---
title: "TTS Cache Options"
description: "Configure TTSRuntimeCache and TTSDiskCache to manage text-to-speech clip storage, memory limits, and persistence."
---

Two options can be used for TTS caching with the Voice SDK TTS package:
* **TTSRuntimeCache**: The TTSRuntimeCache holds loaded TTS clips and returns them immediately if a load request is made for the same clip. The runtime cache will keep all files in memory indefinitely unless a LRU (Least Recently Used) clip limit or RAM limit is placed on it.  When a limit has been passed, the TTSRuntimeCache will automatically unload the least recently used TTS clips. The TTSRuntimeCache uses the following parameters:

    a. **ClipLimit**: Whether a clip total should be limited.

    b. **ClipCapacity**: The maximum number of clips allowed when limited.

    c. **RamLimit**: Whether the runtime cache should limit clips loaded based on RAM.

    d. **RamCapacity**: Capacity of the runtime cache when limited (in KB).

{:width="624px"}

* **TTSDiskCache**: The disk cache used for handling storage and streaming of files from disk. This cache allows for downloading TTS files to disk so that they will load quicker on subsequent use.  While the TTSDiskCache contains default settings to be used with TTS requests, these can be overridden on a per request basis. TTSService.instance.Load also accepts custom TTSDiskCacheSettings for storing specific TTS requests. The TTSDiskCache using the following parameters:

    a.  **DiskPath**: The path for the disk cache relative to the disk cache location.

    b. **Disk Cache Location**: Available options for the disk cache location.

    * **Stream**: No disk caching is used.
    * **Preload**: Files are loaded from the StreamingAssets directory. This only applies to files that have been loaded into the application. Any that have not been cached will be streamed.
    * **Persistent**: Uses the application’s on device persistent directory, which will remain on disk until deleted. This should only be used for a very limited number of clips.
    * **Temporary**:  Uses the application’s on device temporary cache directory which will remain on disk until the device decides it should be deleted.

{:width="864px"}