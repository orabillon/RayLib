#/usr/bin/env bash

rm ./bin/MonProg
rm ./bin/assets/images/*.*
rm ./bin/assets/fonts/*.*
rm ./bin/assets/sons/*.*

cp ./src/assets/images/*.* ./bin/assets/images/
cp ./src/assets/fonts/*.* ./bin/assets/fonts/
cp ./src/assets/sons/*.* ./bin/assets/sons/

gcc -Wall -Werror -pedantic -g src/main.c tools/*.c -o bin/MonProg -lraylib -lGL -lm -lpthread -ldl -lrt -lX11 

if [ $? -eq 0 ]; then
    echo "Compilation OK"
    cd bin
    ./MonProg
else
    echo "Erreur de compilation"
fi