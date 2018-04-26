#!/bin/sh
set -e

docfx docfx.json

echo "Push the new docs to the remote branch"
git add . -A
git commit -m "Update generated documentation"
git push origin master