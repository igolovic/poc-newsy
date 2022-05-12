#!/usr/bin/env bash

# exit when any command fails
set -e

# trust dev root CA
openssl x509 -inform DER -in /https-root/root.cer -out /https-root/root.crt
cp /https-root/root.crt /usr/local/share/ca-certificates/
update-ca-certificates

# start the app
dotnet watch run