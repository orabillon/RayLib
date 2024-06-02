#include "orn_utils.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/**
* Fonction pour vider le cache de saisie
*/
void orn_utils_clear_buffer() {
    int c;
    while ((c = getchar()) != '\n' && c != EOF) {
        // on boucle jusqu'à trouver un caractère de nouvelle ligne ou la fin de fichier
    }
}

/**
* Fonction pour la saisie sécurisé
* @param *str pointeur sur la variable de recuperation
* @param size nombre de caractere desirer 
*/
char *orn_utils_input_read(char *str, int size){
    char *data;

    data = fgets(str, size, stdin);

    if(data){
        size_t len = strlen(str) - 1;

        if(str[len] == '\n'){
            str[len] = '\0';
        } 
        else {
            // vide tempons de lecture
            fscanf(stdin, "%*[^\n]");
            fgetc(stdin);
        }
    
    }

    return data;
}
