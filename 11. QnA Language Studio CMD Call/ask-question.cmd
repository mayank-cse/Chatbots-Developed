@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

rem Set variables
set prediction_url="https://jdbots-d-language-service.cognitiveservices.azure.com/language/:query-knowledgebases?projectName=Connect-EchoBot&api-version=2021-10-01&deploymentName=production"
set key="a0f2e692cee347208905478f89096400"

curl -X POST !prediction_url! -H "Ocp-Apim-Subscription-Key: !key!" -H "Content-Type: application/json" -d "{'question': 'I am tired?' }"