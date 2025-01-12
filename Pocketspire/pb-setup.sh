#!/bin/sh

# Start PocketBase in the background
./pb/pocketbase serve --http=0.0.0.0:8080 &

# Wait for a moment to ensure PocketBase is up and running
sleep 5

# Assign provided values or use defaults if not provided
SUPERUSER_NAME=${2:-""}
SUPERUSER_PASSWORD=${4:-""}

# Check if both SUPERUSER_NAME and SUPERUSER_PASSWORD are non-empty before running the command
if [ -n "$SUPERUSER_NAME" ] && [ -n "$SUPERUSER_PASSWORD" ]; then
./pb/pocketbase superuser create "$SUPERUSER_NAME" "$SUPERUSER_PASSWORD"
fi

# Keep the container running (wait indefinitely)
tail -f /dev/null