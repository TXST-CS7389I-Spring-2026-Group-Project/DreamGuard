# Known Issues

## "DreamGuard Not Responding" on Build and Run

**Symptom:** After using Unity's *Build and Run* to deploy to the Meta Quest, the app launches but immediately shows a "DreamGuard not responding" error.

**Cause:** The app requires the headset to be worn (sensors detect the user) to initialize correctly. If the Quest is sitting on a desk when the app launches, it may not fully start up, triggering the not-responding dialog.

**Workaround:** Put the headset on before initiating *Build and Run*, or quit and then relaunch the app from your MetaQuest library.
