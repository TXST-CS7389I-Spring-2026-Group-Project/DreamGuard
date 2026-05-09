"""
DreamGuard Study — Session Summary
===================================
Prints per-room and per-condition stats for one or more study sessions.

Usage:
    uv run summary.py <experiments_dir>          # all study_N folders
    uv run summary.py <experiments_dir>/study_3  # single session
    uv run summary.py experiments/ --csv         # also emit summary.csv
"""

import argparse
import sys
from datetime import datetime
from pathlib import Path

import pandas as pd
from tabulate import tabulate


# ── helpers ──────────────────────────────────────────────────────────────────

def parse_ts(ts: str) -> datetime:
    return datetime.fromisoformat(ts)


def load_session(session_dir: Path) -> dict:
    """Load all CSV files for a single study_N folder."""
    def read(name: str) -> pd.DataFrame | None:
        p = session_dir / name
        if not p.exists():
            return None
        return pd.read_csv(p, parse_dates=["timestamp_iso"])

    return {
        "study":      read("study.csv"),
        "rooms":      read("rooms.csv"),
        "collection": read("collection.csv"),
    }


def trigger_latencies(study: pd.DataFrame) -> pd.Series:
    """
    Returns a Series of latency values (seconds) indexed by room_id.
    Latency = TRIGGER.timestamp − ROOM_HALFWAY.timestamp
    """
    halfway = study[study.event_type == "ROOM_HALFWAY"].copy()
    triggers = study[study.event_type == "TRIGGER"].copy()

    if halfway.empty or triggers.empty:
        return pd.Series(dtype=float)

    # Extract room_id from detail column ("room_id=X ...")
    halfway["room_id"] = halfway["detail"].str.extract(r"room_id=(\S+)")

    rows = []
    for _, hw in halfway.iterrows():
        room = hw["room_id"]
        # First trigger after this halfway mark
        later = triggers[triggers.timestamp_iso > hw.timestamp_iso]
        if later.empty:
            continue
        first_trigger = later.iloc[0]
        latency = (first_trigger.timestamp_iso - hw.timestamp_iso).total_seconds()
        rows.append({"room_id": room, "latency_s": latency,
                     "technique": _extract(first_trigger["detail"], "technique")})

    return pd.DataFrame(rows) if rows else pd.DataFrame(columns=["room_id", "latency_s", "technique"])


def room_durations(rooms: pd.DataFrame) -> pd.DataFrame:
    if rooms is None or rooms.empty:
        return pd.DataFrame(columns=["room_id", "duration_s"])

    enters = rooms[rooms.event == "ENTER"].set_index("room_id")["timestamp_iso"]
    exits  = rooms[rooms.event == "EXIT"].set_index("room_id")["timestamp_iso"]
    shared = enters.index.intersection(exits.index)

    rows = [
        {"room_id": r, "duration_s": (exits[r] - enters[r]).total_seconds()}
        for r in shared
    ]
    return pd.DataFrame(rows)


def _extract(detail: str, key: str) -> str:
    """Extract key=value from a detail string."""
    if pd.isna(detail):
        return ""
    for part in str(detail).split():
        if part.startswith(f"{key}="):
            return part[len(key) + 1:]
    return ""


# ── per-session report ────────────────────────────────────────────────────────

def report_session(session_dir: Path) -> dict:
    data  = load_session(session_dir)
    study = data["study"]
    rooms = data["rooms"]

    if study is None or study.empty:
        print(f"  [skip] no study.csv in {session_dir.name}")
        return {}

    meta = study[study.event_type == "SESSION_START"]
    participant = study["participant_id"].dropna().iloc[0] if not study.empty else "?"
    condition   = study["condition"].dropna().iloc[0] if not study.empty else "?"

    print(f"\n{'='*60}")
    print(f"Session : {session_dir.name}")
    print(f"Participant : {participant}   Condition : {condition}")
    if not meta.empty:
        detail = meta.iloc[0]["detail"]
        print(f"Device  : {_extract(detail, 'device')}   Unity : {_extract(detail, 'unity')}")

    # Session wall-clock duration
    start_rows = study[study.event_type == "SESSION_START"]
    end_rows   = study[study.event_type == "SESSION_END"]
    if not start_rows.empty and not end_rows.empty:
        duration = (end_rows.iloc[0].timestamp_iso - start_rows.iloc[0].timestamp_iso).total_seconds()
        print(f"Duration: {duration:.1f}s ({duration/60:.1f} min)")

    # Room durations
    durations = room_durations(rooms)
    if not durations.empty:
        print("\n--- Room Durations ---")
        print(tabulate(durations, headers="keys", tablefmt="simple", showindex=False, floatfmt=".1f"))

    # Trigger latencies
    latencies = trigger_latencies(study)
    if not latencies.empty:
        print("\n--- Trigger Latencies (TRIGGER − ROOM_HALFWAY) ---")
        print(tabulate(latencies, headers="keys", tablefmt="simple", showindex=False, floatfmt=".3f"))
        print(f"  mean={latencies['latency_s'].mean():.3f}s  "
              f"min={latencies['latency_s'].min():.3f}s  "
              f"max={latencies['latency_s'].max():.3f}s")

    # Counts
    fp_count       = (study.event_type == "FALSE_POSITIVE").sum()
    intrusion_count = (study.event_type == "INTRUSION_MARK").sum()
    orb_count      = len(data["collection"]) if data["collection"] is not None else "n/a"

    print(f"\n--- Counts ---")
    print(f"  Orbs collected  : {orb_count}")
    print(f"  Intrusion marks : {intrusion_count}")
    print(f"  False positives : {fp_count}")

    # Technique changes
    changes = study[study.event_type == "TECHNIQUE_CHANGE"]
    if not changes.empty:
        print("\n--- Technique Changes ---")
        for _, row in changes.iterrows():
            print(f"  {row.timestamp_iso.strftime('%H:%M:%S')}  →  {_extract(row['detail'], 'technique')}")

    return {
        "session":     session_dir.name,
        "participant": participant,
        "condition":   condition,
        "orbs":        orb_count,
        "intrusions":  intrusion_count,
        "false_pos":   fp_count,
        "mean_latency_s": latencies["latency_s"].mean() if not latencies.empty else None,
    }


# ── entry point ───────────────────────────────────────────────────────────────

def main():
    parser = argparse.ArgumentParser(description="DreamGuard session summary")
    parser.add_argument("path", type=Path, help="experiments/ dir or a single study_N folder")
    parser.add_argument("--csv", action="store_true", help="save aggregate CSV alongside input path")
    args = parser.parse_args()

    path: Path = args.path.resolve()
    if not path.exists():
        sys.exit(f"Path not found: {path}")

    # Determine session dirs to process
    if (path / "study.csv").exists():
        session_dirs = [path]
    else:
        session_dirs = sorted(path.glob("study_*"), key=lambda p: int(p.name.split("_")[1]))

    if not session_dirs:
        sys.exit(f"No study_* folders found under {path}")

    rows = []
    for sd in session_dirs:
        row = report_session(sd)
        if row:
            rows.append(row)

    if rows and args.csv:
        out = path / "summary.csv"
        pd.DataFrame(rows).to_csv(out, index=False)
        print(f"\n[summary] Written → {out}")


if __name__ == "__main__":
    main()
