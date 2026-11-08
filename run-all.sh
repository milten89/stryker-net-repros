#!/usr/bin/env bash
# Runs every repro: restore, build, tests, then Stryker with each config; reports land in <repro>/out/<config>.
# A repro with Repro.sln (.NET Framework) passes it with --solution and runs its test apps directly,
# because dotnet test on the .NET 10 SDK refuses VSTest mode for xUnit v3 projects.
set -u
here="$(cd "$(dirname "$0")" && pwd)"
for d in "$here"/*/; do
  name=$(basename "$d")
  echo "=== $name"
  cd "$d" && dotnet tool restore >/dev/null
  solution=()
  if [ -f Repro.sln ]; then
    solution=(--solution ../Repro.sln)
    dotnet build Repro.sln >/dev/null 2>&1
    for exe in Lib.Tests/bin/Debug/*/Lib.Tests.exe; do "$exe" 2>&1 | grep "Total:"; done
  else
    dotnet test Lib.Tests 2>&1 | tail -3
  fi
  for cfg in Lib/stryker-*.json; do
    tag=$(basename "$cfg" .json)
    mkdir -p "$d/tmp/$tag"
    (cd "$d/Lib" && TEMP="$d/tmp/$tag" TMP="$d/tmp/$tag" TMPDIR="$d/tmp/$tag" \
      dotnet stryker --config-file "$(basename "$cfg")" "${solution[@]}" --output "$d/out/$tag" --verbosity info --skip-version-check > "$d/out-$tag.log" 2>&1)
    echo "  $tag exit=$?"
  done
done
