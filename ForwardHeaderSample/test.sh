#!/bin/bash

curl -i -k -X GET --location "https://localhost:5001/.well-known/openid-configuration" \
    -H "X-Forwarded-For: 127.0.0.1" \
    -H "X-Forwarded-Proto: http" \
    -H "X-Forwarded-Host: yourdomain.com"
    

