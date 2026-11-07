#!/usr/bin/env bash
# Runs every repro: restore, unit tests, then Stryker; reports land in <repro>/out/<config>.
set -u
here="$(cd "$(dirname "$0")" && pwd)"
for d in "$here"/*/; do
  name=$(basename "$d")
  echo "=== $name"
  cd "$d" && dotnet tool restore >/dev/null && dotnet test Lib.Tests 2>&1 | tail -3
  for cfg in Lib/stryker-config*.json; do
    tag=$(basename "$cfg" .json)
    mkdir -p "$d/tmp/$tag"
    (cd "$d/Lib" && TEMP="$d/tmp/$tag" TMP="$d/tmp/$tag" TMPDIR="$d/tmp/$tag" \
      dotnet stryker --config-file "$(basename "$cfg")" --output "$d/out/$tag" --verbosity info --skip-version-check > "$d/out-$tag.log" 2>&1)
    echo "  $tag exit=$?"
  done
done
