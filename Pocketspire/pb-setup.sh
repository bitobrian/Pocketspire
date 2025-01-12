#!/bin/sh

# Start PocketBase in the background
./pb/pocketbase serve --http=0.0.0.0:8080 &

# Wait for a moment to ensure PocketBase is up and running
sleep 5

# Run superuser command only if PB_SU is not empty
if [ -n "$PB_SU" ]; then
  ./pb/pocketbase superuser create $PB_SU $PB_SU_PW
fi

# Keep the container running (wait indefinitely)
tail -f /dev/null