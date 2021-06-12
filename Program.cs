/**
 * \file main.c
 * \brief Programme de tests.
 * \author Dujardin julien
 * \version 0.1
 * \date 10 Juin 2021
 *
 * Programme de cryptage decryptage de carte
 *
 */

﻿using System;
using System.IO;

namespace tpRhum
{
    class Program
    {
        static void Main(string[] args)
        {
            initialisation();
        }

        // La fonction initialisation à pour but de gérer tout ce qui va se passer au début du programme, comme dire bonjour

        static void initialisation(){

          // Partie choix

            string userchoice = "z"; //valeur impossible pour l'initialisation de la variable
            Console.Clear();

            Console.WriteLine("Bonjour mon capitaine !");

            while(!(userchoice == "a" || userchoice == "b" || userchoice == "c")){ //tant que l'utilisateur n'a pas propose un choix valide
              Console.WriteLine("Que puis-je faire pour vous ? \n a : Coder une carte claire \n b : Décoder une carte chiffrer \n c : Lire une carte");
              userchoice = Console.ReadLine();
            }

            // traitement du choix

            switch(userchoice){
              case "a":
                CodeurCarte codeur = new CodeurCarte(); //Je créé l'objet pour avoir accès à toutes ces fonctions
                codeur.affichageInstructions();
                string cheminAcces = Console.ReadLine();
                if (cheminAcces == "") {
                  System.Environment.Exit(0);
                }
                string[] tableauCarte = new string[10];
                tableauCarte = codeur.lireCheminAcces(cheminAcces);
                codeur.afficherCarte(tableauCarte);
                string carteChiffre = codeur.chiffrementCarte(tableauCarte);
                codeur.envoieVersChiffre(carteChiffre, Path.GetFileName(cheminAcces).Replace("clair", "chiffre"));
                break;

              case "b":
                DecodeurCarte decodeur = new DecodeurCarte();
                decodeur.affichageInstructions();
                string cheminAccesDecodeur = Console.ReadLine();
                if (cheminAccesDecodeur == "") {
                  System.Environment.Exit(0);
                }
                string carteChiffrePourDecodage = "";

                cheminAccesDecodeur = "assets/Alabasta.chiffre";

                carteChiffrePourDecodage = decodeur.lireCheminAcces(cheminAccesDecodeur);
                decodeur.afficherCarte(carteChiffrePourDecodage);
                // decodeur.afficherCarteClair(carteChiffrePourDecodage);

                string[] carteDechiffre = new string[10];
                CodeurCarte codeur2 = new CodeurCarte(); //Je créé l'objet pour avoir accès à toutes ces fonctions
                carteDechiffre = decodeur.decode(carteChiffrePourDecodage);
                codeur2.afficherCarte(carteDechiffre);
                break;

              case "c":
                break;
            }


        }
    }
}
