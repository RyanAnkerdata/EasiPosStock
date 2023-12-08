#!/bin/bash

if [[ ! -d certs ]]
then
    mkdir certs
    cd certs/
    if [[ ! -f localhost.pfx ]]
    then
        dotnet dev-certs https -v -ep localhost.pfx -p 5b4a91d4-b5e3-4bac-9b32-5f9ad3ffc319 -t
    fi
    cd ../
fi

docker-compose up -d
