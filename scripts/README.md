# DreamGuard Scripts

Analysis scripts for DreamGuard study data, managed with [uv](https://docs.astral.sh/uv/).

## Prerequisites

Install uv if you don't have it:

```bash
pip install uv
# or: winget install astral-sh.uv
```

## Setup

```bash
cd scripts
uv sync
```

This creates a `.venv` and installs all dependencies (pandas, matplotlib, jupyterlab, etc.).

## Pull data from the Quest

```bash
adb pull /sdcard/Android/data/com.DefaultCompany.DreamGuard/files/experiments ../data
```

Expected layout after pulling:

```
data/
  study_0/
    study.csv
    rooms.csv
    collection.csv
    position.csv
    headset.csv
    r_controller.csv
    l_controller.csv
  study_1/
    ...
```

## Scripts

### `analysis.ipynb` — Full analysis notebook

Opens in JupyterLab with all visualisations: trajectory minimaps, heatmaps, trigger latency, room durations, event timeline, and CSV exports.

```bash
uv run jupyter lab
```

Then open `analysis.ipynb`. Outputs are saved to `../data/analysis/`.

---

### `summary.py` — Session summary (terminal)

Prints per-room durations, trigger latencies, and event counts.

```bash
uv run summary.py ../data              # all study_N folders
uv run summary.py ../data/study_0      # single session
uv run summary.py ../data --csv        # also write summary.csv
```

---

### `heatmap.py` — Player position heatmap

Overlays position data on the dungeon top-down image.

```bash
uv run heatmap.py ../data              # combined heatmap, all sessions
uv run heatmap.py ../data/study_0      # single session
uv run heatmap.py ../data --per-session  # one PNG per session
uv run heatmap.py ../data --per-room     # one subplot per room
```

---

### `timeline.py` — Event timeline

Renders a horizontal timeline with room spans and event markers.

```bash
uv run timeline.py ../data             # all sessions on one plot
uv run timeline.py ../data/study_0     # single session
uv run timeline.py ../data --out ../data/analysis/timeline.png
```
