@ECHO OFF
SETLOCAL ENABLEEXTENSIONS

ECHO Creating Boticario.docker.mssql

ECHO.
ECHO Checking image
docker rmi Boticario.docker.mssql --force

ECHO.
ECHO Creating image
docker build --tag Boticario.docker.mssql --no-cache --rm .

:End
