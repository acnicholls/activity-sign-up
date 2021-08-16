START=$(date -u +%s%N)

# echo "starting at $START"

docker builder prune -af

END=$(date +%s%N)

# echo "ending at $END"

# borrowed from https://serverfault.com/questions/151109/how-do-i-get-the-current-unix-time-in-milliseconds-in-bash
echo "total build time $(echo "scale=3;($(date +%s%N) - ${START})/(1*10^09)" | bc) seconds"