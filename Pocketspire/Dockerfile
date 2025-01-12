FROM alpine:3

ARG PB_VERSION=0.24.1
ARG CPU_ARCH=amd64

RUN apk add --no-cache \
    unzip \
    ca-certificates

ADD https://github.com/pocketbase/pocketbase/releases/download/v${PB_VERSION}/pocketbase_${PB_VERSION}_linux_${CPU_ARCH}.zip /tmp/pb.zip
RUN unzip /tmp/pb.zip -d /pb/

# uncomment to copy the local pb_migrations dir into the image
# COPY ./pb_migrations /pb/pb_migrations

# uncomment to copy the local pb_hooks dir into the image
# COPY ./pb_hooks /pb/pb_hooks

COPY pb-setup.sh .

RUN chmod +x pb-setup.sh

EXPOSE 8080

CMD ["/bin/sh", "-c", "./pb-setup.sh"]