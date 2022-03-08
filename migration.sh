#!/bin/bash
 
echo "Enter Migration Name: "
read x

cd ./src/Negotiations.Infrastructure
dotnet ef migrations add "${x}" --startup-project ../Negotiations.WebApi --output-dir ./DatabaseContext/Migrations
