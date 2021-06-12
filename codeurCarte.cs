using System;
using System.IO;

namespace tpRhum{
  class CodeurCarte{

    public void affichageInstructions(){

      /**
                               * \brief Permet d'afficher les instructions du codage
                               */

      Console.Clear();
      Console.WriteLine("Vous avez choisi le codage d'une carte .clair vers .chiffre. \n \n Pour continuer veuillez m'indiquer le chemin d'accès faire le fichier clair \n Laisser vide pour annuler");
    }

    public string[] lireCheminAcces(string cheminAcces){
      /**
                               * \brief lis le fichier au chemin donnee et renvoie le tableau des donnees
                               */
      string[] carteTableauRetour = new string[10];

      if (!File.Exists(cheminAcces)) //si le chemin n'existe pas...
      {
          Console.WriteLine("\n Erreur critique : le fichier indiqué n'éxiste pas"); //...on indique qu'il y a une erreur
          System.Environment.Exit(0);
          return carteTableauRetour;
      }
      else{

        try
        {
          using (StreamReader sr = File.OpenText(cheminAcces))
          {

              string s;
              int i = 0;
              while ((s = sr.ReadLine()) != null)
              {
                carteTableauRetour[i] = s;
                i++;
              }
          }
          return carteTableauRetour;
        }

        catch (Exception e){
          Console.WriteLine("Erreur : {0}", e.Message);
          System.Environment.Exit(0);
          return carteTableauRetour;
        }
      }

    }

    public void afficherCarte(string[] carteAffiche){
      /**
                               * \brief Affiche la carte en clair
                               */
      Console.Clear();
      Console.WriteLine("Voici la carte en clair : \n");
      for (int i = 0; i < 10; i++) {
        Console.WriteLine("{0}", carteAffiche[i]);
      }
    }

  public string chiffrementCarte(string[] carteAChiffrer){
    /**
                             * \brief Cette fonction prends en option la carte en clair et renvoie la version chiffree
                             */
      string chaineChiffree = "";
      for (int indexChiffre = 0; indexChiffre < 10; indexChiffre++) {
        for (int indexLettre = 0; indexLettre < 10; indexLettre++) {
          chaineChiffree+=calculValeur(indexChiffre, indexLettre, carteAChiffrer);
          if (indexLettre < 9) {
            chaineChiffree += ":";
          }
        }
        chaineChiffree += "|";
      }
      Console.WriteLine("\nVoici le resultat chiffré : \n\n{0}", chaineChiffree);
      return chaineChiffree;
    }



    int calculValeur(int c, int l, string[] carteTableau){
      int valeurRetour = 0;
      char typeUnite = carteTableau[c][l];

      // Nord
      if (c == 0) {
        valeurRetour+=1;
      }
      else if (typeUnite != carteTableau[c-1][l]) {
        valeurRetour+=1;
      }

      // Ouest
      if (l == 0) {
        valeurRetour+=2;
      }
      else if (typeUnite != carteTableau[c][l-1]) {
        valeurRetour+=2;
      }

      // Sud
      if (c == 9) {
        valeurRetour+=4;
      }
      else if (typeUnite != carteTableau[c+1][l]) {
        valeurRetour+=4;
      }

      // Est
      if (l == 9) {
        valeurRetour+=8;
      }
      else if (typeUnite != carteTableau[c][l+1]) {
        valeurRetour+=8;
      }

      // type
      if (typeUnite == 'M') {
        valeurRetour+=64;
      }
      else if (typeUnite == 'F') {
        valeurRetour+=32;
      }

      return valeurRetour;

    }

    public void envoieVersChiffre(string valeurAEcrire, string nomFichier){
      /**
                               * \brief envoie vers un fichier .chiffree dans le dossier assets sous le meme nom du fichier
                               */
      try{
        using (StreamWriter writer = new StreamWriter("assets/" + nomFichier)){
          writer.WriteLine(valeurAEcrire);
        }
      }
      catch(Exception e) {
        Console.WriteLine("{0}", e.Message);
      }
    }

  }
}
