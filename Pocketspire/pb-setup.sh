#!/bin/sh
./pb/pocketbase serve --http=0.0.0.0:8080 &
# wait 5 seconds
sleep 5

echo "PB_SU" $PB_SU
echo "PB_SU_PW" $PB_SU_PW

# if $PB_SU is not empty, then run  ./pb/pocketbase superuser upsert $PB_SU $PB_SU_PW
if [ -n "$PB_SU" ]; then
	./pb/pocketbase superuser upsert $PB_SU $PB_SU_PW
fi

tail -f /dev/null