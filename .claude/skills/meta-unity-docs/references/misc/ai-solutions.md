# Ai Solutions

**Documentation Index:** Learn about ai solutions in this documentation.

---

---
title: "LLM resources for AI"
description: "Explore LLM-powered AI tools and resources available for Meta Quest app development."
last_updated: "2026-04-08"
---

Meta provides LLM-optimized documentation for all Meta Horizon OS development platforms. These resources are designed to be consumed by AI assistants and large language models, enabling accurate and up-to-date responses when you use AI tools for Meta Quest development.

## LLM-optimized documentation

Meta publishes developer documentation in a machine-readable format optimized for LLM consumption, providing clean text without navigation chrome, ads, or other page elements that reduce accuracy.

### Your documentation index

The documentation index lists all available pages for your platform:

[**Documentation index**](https://developers.meta.com/horizon/llmstxt/documentation/unity/llms.txt/) — `https://developers.meta.com/horizon/llmstxt/documentation/unity/llms.txt/`

Start with this index to discover available pages.

### Other platform indices
- [Unreal documentation index](https://developers.meta.com/horizon/llmstxt/documentation/unreal/llms.txt/)
- [Spatial SDK documentation index](https://developers.meta.com/horizon/llmstxt/documentation/spatial-sdk/llms.txt/)
- [Android Apps documentation index](https://developers.meta.com/horizon/llmstxt/documentation/android-apps/llms.txt/)
- [Native documentation index](https://developers.meta.com/horizon/llmstxt/documentation/native/llms.txt/)
- [Web documentation index](https://developers.meta.com/horizon/llmstxt/documentation/web/llms.txt/)

### Additional indices

- [Design guidelines and patterns](https://developers.meta.com/horizon/llmstxt/design/llms.txt/)
- [Developer policies and guidelines](https://developers.meta.com/horizon/llmstxt/policy/llms.txt/)
- [Developer resources and tools](https://developers.meta.com/horizon/llmstxt/resources/llms.txt/)

## Accessing LLM resources

### Through the MCP server

[Hzdb](/documentation/unity/ts-mqdh-mcp/) provides tools that retrieve documentation automatically:

- **`search_doc`** — Searches across Meta Horizon OS documentation for relevant pages.
- **`fetch_meta_quest_doc`** — Fetches the full content of a specific documentation page by URL.

When you ask your AI assistant a question about Meta Quest development, it uses these tools to find and retrieve the relevant documentation pages.

### Direct access

You can also point any LLM or AI tool at the documentation URLs directly. The base URL pattern is:

```
https://developers.meta.com/horizon/llmstxt/documentation/unity/{page-slug}.md/
```

Start with the [documentation index for your platform](https://developers.meta.com/horizon/llmstxt/documentation/unity/llms.txt/) to discover available pages.