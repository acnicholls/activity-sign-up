docker-machine --debug \
    create \
    --engine-storage-driver vfs \
    --driver generic \
    --generic-ip-address 192.168.1.211 \
    --generic-engine-port 2376 \
    --generic-ssh-port 22 \
    --generic-ssh-user root \
    --generic-ssh-key ~/.ssh/docker-host_rsa \
    dockernano
