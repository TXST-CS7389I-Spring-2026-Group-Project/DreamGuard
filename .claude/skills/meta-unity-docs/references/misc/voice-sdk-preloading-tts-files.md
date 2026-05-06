# Voice Sdk Preloading Tts Files

**Documentation Index:** Learn about voice sdk preloading tts files in this documentation.

---

---
title: "Preloading TTS Files"
description: "Cache text-to-speech audio files locally using TTS Preload Settings and the built-in TTSDiskCache in Voice SDK."
---

You can also stream TTS files directly from a preloaded TTS cache using Voice SDK’s built-in TTSDiskCache feature.  To preload the files to a TTS cache, use a TTS Preload Settings asset.

## To create and implement a TTS Preload Settings asset:

1. Set up the TTS Preload settings

    a. Navigate to **Assets** > **Create** > **Voice SDK** > **TTS Preload Settings** to generate a **TTS Preload Settings** asset.

    **Note**: A **TTS Preload Settings** asset can only be used in a scene with a TTSService. The service itself should automatically be found and linked at the top of the **Inspector** window.

    b. Select the generated asset and view in the **Inspector** window. Preload or delete clips in the TTS cache.

    {:width="624px"}

2. Add any voices and phrases desired.

    a. Click **Add Voice**.

    b. Expand the voice displayed and for **Voice ID**, select the voice you want to use from the list.

    c. Click **Add Phrase**.

    d. Expand **Phrases** and enter the **Phrase** and **Clip ID** as appropriate.

    e. Add additional voices or phrases as needed.

    {:width="624px"}

3. To speed the import of TTS preload data, click **Import JSON** to load a JSON file with phrases. The file itself contains dictionaries which have arrays of phrases for each voice.

    For more complex data imports, use the `TTSPreloadUtility.ImportData` method to import a dictionary of phrases for each voice directly into a TTSPreloadSettings file.

    **Note**: Importing data does not allow for duplication of the same phrase. Any duplicated phrase using the same voice is ignored.

    {:width="624px"}

4. After all phrases have been imported into a TTSPreloadSetting asset, click **Preload Cache**. This loads all TTS files into the `TTS StreamingAssets` directory.

5. To remove the files from the TTS preload disk cache, click **Delete Cache**.

6. To update the download state for each clip, click **Refresh Data**.

**Note**: The Wit.ai service only permits a limited number of TTS requests per minute and if you are preloading a large number of clips, you may need to click **Preload Cache** multiple times in order to download all files required.