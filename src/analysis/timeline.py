"""
DreamGuard Study — Event Timeline
===================================
Renders a horizontal event timeline for one or more study sessions.

Each row = a session.  Events are plotted as coloured markers/spans along
the session's wall-clock duration.  Room spans are shaded in the background.

Usage:
    uv run timeline.py <experiments_dir>          # all sessions on one plot
    uv run timeline.py <experiments_dir>/study_3  # single session
    uv run timeline.py experiments/ --out plots/timeline.png
"""

import argparse
import sys
from pathlib import Path

import matplotlib.pyplot as plt
import matplotlib.patches as mpatches
import pandas as pd

plt.rcParams.update({
    'axes.facecolor':    '#D8D8D8',
    'figure.facecolor':  '#E4E4E4',
    'font.size':         14,
    'font.weight':       'bold',
    'axes.titlesize':    16,
    'axes.titleweight':  'bold',
    'axes.labelsize':    14,
    'axes.labelweight':  'bold',
    'xtick.labelsize':   12,
    'ytick.labelsize':   12,
    'legend.fontsize':   12,
    'lines.linewidth':   2.5,
    'axes.linewidth':    1.5,
    'patch.linewidth':   1.5,
    'grid.linewidth':    1.2,
    'xtick.major.width': 1.5,
    'ytick.major.width': 1.5,
})


# ── colour / marker config ────────────────────────────────────────────────────

EVENT_STYLE = {
    "SESSION_START":         dict(color="#4CAF50", marker="D", zorder=5, ms=7),
    "SESSION_END":           dict(color="#F44336", marker="D", zorder=5, ms=7),
    "ROOM_ENTER":            dict(color="#2196F3", marker="|", zorder=4, ms=14, lw=2),
    "ROOM_COMPLETE":         dict(color="#1565C0", marker="|", zorder=4, ms=14, lw=2),
    "ROOM_HALFWAY":          dict(color="#9C27B0", marker="v", zorder=4, ms=8),
    "CONDITION_BLOCK_START": dict(color="#FF9800", marker="s", zorder=3, ms=7),
    "INTRUSION_MARK":        dict(color="#F44336", marker="^", zorder=5, ms=9),
    "TRIGGER":               dict(color="#E91E63", marker="*", zorder=5, ms=12),
    "FALSE_POSITIVE":        dict(color="#FF5722", marker="x", zorder=5, ms=9, lw=2),
    "CONFEDERATE_EXIT":      dict(color="#795548", marker="v", zorder=4, ms=8),
    "TECHNIQUE_CHANGE":      dict(color="#607D8B", marker="P", zorder=3, ms=8),
}

ROOM_COLORS = ["#E3F2FD", "#F3E5F5", "#E8F5E9", "#FFF8E1"]


# ── data loading ──────────────────────────────────────────────────────────────

def load_session(session_dir: Path) -> dict:
    def read(name):
        p = session_dir / name
        return pd.read_csv(p, parse_dates=["timestamp_iso"]) if p.exists() else None
    return {"study": read("study.csv"), "rooms": read("rooms.csv")}


def session_label(session_dir: Path, study: pd.DataFrame) -> str:
    pid  = study["participant_id"].dropna().iloc[0] if not study.empty else "?"
    cond = study["condition"].dropna().iloc[0]      if not study.empty else "?"
    return f"{session_dir.name}\nP={pid} cond={cond}"


# ── per-session row ───────────────────────────────────────────────────────────

def draw_session_row(ax, y: float, study: pd.DataFrame, rooms: pd.DataFrame | None,
                     t0: pd.Timestamp, legend_handles: dict):
    """Draw one horizontal timeline row at vertical position y."""

    # Background room spans
    if rooms is not None and not rooms.empty:
        enters = rooms[rooms.event == "ENTER"].set_index("room_id")["timestamp_iso"]
        exits  = rooms[rooms.event == "EXIT"].set_index("room_id")["timestamp_iso"]
        shared = enters.index.intersection(exits.index)
        for i, room_id in enumerate(sorted(shared)):
            x0 = (enters[room_id] - t0).total_seconds()
            x1 = (exits[room_id]  - t0).total_seconds()
            color = ROOM_COLORS[i % len(ROOM_COLORS)]
            ax.axvspan(x0, x1, ymin=(y - 0.4) / ax.get_ylim()[1],
                       ymax=(y + 0.4) / ax.get_ylim()[1],
                       color=color, alpha=0.5, zorder=1)

    # Event markers
    for _, row in study.iterrows():
        etype = row["event_type"]
        if etype not in EVENT_STYLE:
            continue
        t = (row["timestamp_iso"] - t0).total_seconds()
        style = EVENT_STYLE[etype]
        h = ax.plot(t, y,
                    marker=style["marker"],
                    color=style["color"],
                    markersize=style.get("ms", 8),
                    markeredgewidth=style.get("lw", 1),
                    markeredgecolor="black",
                    linestyle="None",
                    zorder=style.get("zorder", 3))
        if etype not in legend_handles:
            legend_handles[etype] = mpatches.Patch(color=style["color"], label=etype)

    # Baseline
    if not study.empty:
        duration = (study["timestamp_iso"].max() - t0).total_seconds()
        ax.hlines(y, 0, duration, colors="#BDBDBD", linewidths=1, zorder=0)


# ── main plot ─────────────────────────────────────────────────────────────────

def plot_timeline(session_dirs: list[Path], out_path: Path):
    sessions = []
    for sd in session_dirs:
        data = load_session(sd)
        if data["study"] is None or data["study"].empty:
            print(f"  [skip] no study.csv in {sd.name}")
            continue
        sessions.append((sd, data["study"], data["rooms"]))

    if not sessions:
        sys.exit("No sessions with data found.")

    n = len(sessions)
    fig_height = max(4, n * 1.6 + 2)
    fig, ax = plt.subplots(figsize=(16, fig_height))

    # y positions: top session at highest y
    ys = list(range(n, 0, -1))
    labels = []
    legend_handles: dict = {}

    # Use each session's own SESSION_START as t0
    for (sd, study, rooms), y in zip(sessions, ys):
        starts = study[study.event_type == "SESSION_START"]
        t0 = starts.iloc[0]["timestamp_iso"] if not starts.empty else study["timestamp_iso"].min()
        # Temporarily set ylim so axvspan scaling works
        ax.set_ylim(0, n + 1)
        draw_session_row(ax, y, study, rooms, t0, legend_handles)
        labels.append(session_label(sd, study))

    ax.set_yticks(ys)
    ax.set_yticklabels(labels)
    ax.set_xlabel("Time since SESSION_START (s)")
    ax.set_title("DreamGuard — Event Timeline")
    ax.set_ylim(0, n + 1)
    ax.grid(axis="x", linestyle="--", alpha=0.4, zorder=0)

    if legend_handles:
        ax.legend(handles=list(legend_handles.values()),
                  loc="upper left", bbox_to_anchor=(1.01, 1),
                  borderaxespad=0, ncol=1, framealpha=0.9)

    plt.tight_layout()
    plt.savefig(out_path, dpi=150, bbox_inches="tight")
    plt.close()
    print(f"[timeline] Saved → {out_path}")


# ── entry point ───────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(description="DreamGuard event timeline")
    parser.add_argument("path", type=Path)
    parser.add_argument("--out", type=Path, default=None,
                        help="Output PNG path (default: next to experiments dir)")
    args = parser.parse_args()

    path: Path = args.path.resolve()
    if not path.exists():
        sys.exit(f"Path not found: {path}")

    if (path / "study.csv").exists():
        session_dirs = [path]
    else:
        session_dirs = sorted(path.glob("study_*"), key=lambda p: int(p.name.split("_")[1]))

    if not session_dirs:
        sys.exit(f"No study_* folders found under {path}")

    out = args.out or (path / "timeline.png")
    plot_timeline(session_dirs, out)


if __name__ == "__main__":
    main()
