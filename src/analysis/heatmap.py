"""
DreamGuard Study — Player Position Heatmap
===========================================
Overlays player position data on the dungeon top-down image.

Usage:
    uv run heatmap.py <experiments_dir>          # all sessions, combined heatmap
    uv run heatmap.py <experiments_dir>/study_3  # single session
    uv run heatmap.py experiments/ --per-session # one PNG per session
    uv run heatmap.py experiments/ --per-room    # one subplot per room

World-space bounds are derived from the position data by default.
Override with --xmin, --xmax, --zmin, --zmax if needed (Unity uses Z for forward).

The dungeon image path defaults to docs/assets/DungeonTopDown.png relative to the
project root (two levels up from this script). Override with --dungeon-img.
"""

import argparse
import sys
from pathlib import Path

import matplotlib.pyplot as plt
import matplotlib.colors as mcolors
import numpy as np
import pandas as pd
from PIL import Image


# ── helpers ──────────────────────────────────────────────────────────────────

def find_project_root(start: Path) -> Path:
    """Walk up until we find a directory that contains 'Assets' or CLAUDE.md."""
    for p in [start, *start.parents]:
        if (p / "CLAUDE.md").exists() or (p / "Assets").exists():
            return p
    return start


def load_positions(session_dir: Path) -> pd.DataFrame | None:
    p = session_dir / "position.csv"
    if not p.exists():
        return None
    df = pd.read_csv(p, parse_dates=["timestamp_iso"])
    return df


def load_rooms_events(session_dir: Path) -> pd.DataFrame | None:
    p = session_dir / "rooms.csv"
    if not p.exists():
        return None
    return pd.read_csv(p, parse_dates=["timestamp_iso"])


def load_study_events(session_dir: Path) -> pd.DataFrame | None:
    p = session_dir / "study.csv"
    if not p.exists():
        return None
    return pd.read_csv(p, parse_dates=["timestamp_iso"])


def world_to_img(x: np.ndarray, z: np.ndarray,
                 xmin: float, xmax: float, zmin: float, zmax: float,
                 img_w: int, img_h: int) -> tuple[np.ndarray, np.ndarray]:
    """
    Map Unity world-space (x, z) → image pixel (px, py).
    Unity +Z is typically "up" in a top-down view (north).
    The dungeon image has its top at high-Z, bottom at low-Z.
    """
    px = ((x - xmin) / (xmax - xmin) * img_w).astype(int)
    py = ((1 - (z - zmin) / (zmax - zmin)) * img_h).astype(int)
    return px, py


def compute_heatmap(positions: list[pd.DataFrame],
                    xmin: float, xmax: float, zmin: float, zmax: float,
                    resolution: int = 256) -> np.ndarray:
    """Bin all (x, z) samples into a 2-D histogram."""
    xs = np.concatenate([df["x"].values for df in positions])
    zs = np.concatenate([df["z"].values for df in positions])

    heatmap, _, _ = np.histogram2d(
        xs, zs,
        bins=resolution,
        range=[[xmin, xmax], [zmin, zmax]],
    )
    # Transpose so rows=Z, cols=X and flip Z so high-Z is at image top
    return np.flipud(heatmap.T)


# ── plotting ──────────────────────────────────────────────────────────────────

def plot_heatmap(heatmap: np.ndarray, dungeon_img: Image.Image | None,
                 xmin: float, xmax: float, zmin: float, zmax: float,
                 title: str, out_path: Path,
                 event_markers: list[dict] | None = None):

    fig, ax = plt.subplots(figsize=(5, 14))

    extent = [xmin, xmax, zmin, zmax]

    if dungeon_img is not None:
        ax.imshow(dungeon_img, extent=extent, origin="lower", aspect="auto", zorder=0)

    # Mask zero bins so they're transparent
    masked = np.ma.masked_where(heatmap == 0, heatmap)
    cmap = plt.cm.hot_r
    cmap.set_bad(alpha=0.0)

    ax.imshow(masked, extent=extent, origin="lower", aspect="auto",
              cmap=cmap, alpha=0.6, zorder=1,
              norm=mcolors.PowerNorm(gamma=0.4, vmin=1, vmax=heatmap.max()))

    # Optional event markers (triggers, intrusions)
    if event_markers:
        for m in event_markers:
            ax.plot(m["x"], m["z"], marker=m.get("marker", "o"),
                    color=m.get("color", "cyan"), markersize=8, zorder=3,
                    label=m.get("label", ""), linestyle="None",
                    markeredgecolor="black", markeredgewidth=0.5)

    ax.set_xlim(xmin, xmax)
    ax.set_ylim(zmin, zmax)
    ax.set_xlabel("X (world)")
    ax.set_ylabel("Z (world)")
    ax.set_title(title)

    # Colourbar
    sm = plt.cm.ScalarMappable(cmap=cmap,
                                norm=mcolors.PowerNorm(gamma=0.4, vmin=0, vmax=heatmap.max()))
    sm.set_array([])
    fig.colorbar(sm, ax=ax, label="Dwell time (samples)", fraction=0.03, pad=0.04)

    plt.tight_layout()
    plt.savefig(out_path, dpi=150, bbox_inches="tight")
    plt.close()
    print(f"[heatmap] Saved → {out_path}")


# ── entry point ───────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(description="DreamGuard player heatmap")
    parser.add_argument("path", type=Path)
    parser.add_argument("--dungeon-img", type=Path, default=None,
                        help="Path to DungeonTopDown.png")
    parser.add_argument("--xmin", type=float, default=None)
    parser.add_argument("--xmax", type=float, default=None)
    parser.add_argument("--zmin", type=float, default=None)
    parser.add_argument("--zmax", type=float, default=None)
    parser.add_argument("--resolution", type=int, default=200,
                        help="Heatmap bin resolution (default 200)")
    parser.add_argument("--per-session", action="store_true",
                        help="Emit one PNG per session instead of a combined heatmap")
    parser.add_argument("--out", type=Path, default=None,
                        help="Output PNG path (default: next to experiments dir)")
    args = parser.parse_args()

    path: Path = args.path.resolve()
    if not path.exists():
        sys.exit(f"Path not found: {path}")

    # Locate dungeon image
    if args.dungeon_img:
        dungeon_img_path = args.dungeon_img
    else:
        project_root = find_project_root(Path(__file__).resolve())
        dungeon_img_path = project_root / "docs" / "assets" / "DungeonTopDown.png"

    dungeon_img = None
    if dungeon_img_path.exists():
        img_raw = Image.open(dungeon_img_path).convert("RGBA")
        dungeon_img = img_raw
        print(f"[heatmap] Loaded dungeon image: {dungeon_img_path}")
    else:
        print(f"[heatmap] Dungeon image not found at {dungeon_img_path} — rendering without background")

    # Collect session dirs
    if (path / "position.csv").exists():
        session_dirs = [path]
    else:
        session_dirs = sorted(path.glob("study_*"), key=lambda p: int(p.name.split("_")[1]))

    if not session_dirs:
        sys.exit(f"No study_* folders found under {path}")

    # Load all position data
    all_positions: dict[str, pd.DataFrame] = {}
    for sd in session_dirs:
        df = load_positions(sd)
        if df is not None and not df.empty:
            all_positions[sd.name] = df

    if not all_positions:
        sys.exit("No position.csv data found.")

    # Determine world-space bounds
    all_df = pd.concat(all_positions.values())
    xmin = args.xmin if args.xmin is not None else float(all_df["x"].min())
    xmax = args.xmax if args.xmax is not None else float(all_df["x"].max())
    zmin = args.zmin if args.zmin is not None else float(all_df["z"].min())
    zmax = args.zmax if args.zmax is not None else float(all_df["z"].max())

    # Add a small margin
    xpad = (xmax - xmin) * 0.05
    zpad = (zmax - zmin) * 0.05
    xmin -= xpad; xmax += xpad
    zmin -= zpad; zmax += zpad

    out_dir = args.out.parent if args.out else path

    if args.per_session:
        for name, df in all_positions.items():
            hm = compute_heatmap([df], xmin, xmax, zmin, zmax, args.resolution)
            out = out_dir / f"heatmap_{name}.png"
            plot_heatmap(hm, dungeon_img, xmin, xmax, zmin, zmax,
                         title=f"Player Heatmap — {name}", out_path=out)
    else:
        hm = compute_heatmap(list(all_positions.values()), xmin, xmax, zmin, zmax, args.resolution)
        label = list(all_positions.keys())[0] if len(all_positions) == 1 else f"{len(all_positions)} sessions"
        out = args.out or (out_dir / "heatmap_combined.png")
        plot_heatmap(hm, dungeon_img, xmin, xmax, zmin, zmax,
                     title=f"Player Position Heatmap ({label})", out_path=out)


if __name__ == "__main__":
    main()
