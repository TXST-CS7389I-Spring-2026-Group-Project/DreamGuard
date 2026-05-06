# Voice Sdk Composer

**Documentation Index:** Learn about voice sdk composer in this documentation.

---

---
title: "Using Composer with Voice SDK"
description: "Integrate Wit.ai Composer with Voice SDK to design interactive, graph-based dialogue experiences in your app."
---

Voice SDK provides support for Composer, a new feature in [Wit.ai](https://wit.ai/) for designing interactive conversations. Composer is a graph-based dialogue designer that can understand Intents, trigger client-side actions, and provide static or dynamic responses based upon the flow of the conversation.

Data received from intents and entities is stored in a key-value JSON object, the context_map. From the client side, this context map can receive values from Composer as well as be updated to provide data to Composer.

Composer consists of four types of modules:
- **Input modules**, which represent information that comes from your end user or from another system.
- **Response modules**, which provide the user with a text or voice response, or trigger a client-side action.
- **Decision modules**, which provide logic to control the flow of the conversation.
- **Context modules**, which modify the data in the context map.

Wit.ai provides a number of [recipes](https://wit.ai/docs/recipes#composer) that will give you additional information about how to use Composer.

Composer is available on Wit.ai, and Voice SDK support for Composer is available through the [Composer Support download](https://github.com/wit-ai/wit-unity/releases/tag/Composer) page on GitHub.

## This section consists of the following topics:
- [Key Unity Components for Composer](/documentation/unity/voice-sdk-composer-key/)
- [Creating a Composer-based scene](/documentation/unity/voice-sdk-composer-creating/)
- [Demo Scenes](/documentation/unity/voice-sdk-composer-demo)