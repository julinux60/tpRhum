using System;
using System.IO;
using System.Text;

namespace tpRhum{
  class DecodeurCarte{

    public void affichageInstructions(){
      /**
                               * \brief Permet d'afficher les instructions du decodage
                               */
      Console.Clear();
      Console.WriteLine("Vous avez choisi le décodage d'une carte .chiffre vers .clair. \n \n Pour continuer veuillez m'indiquer le chemin d'accès faire le fichier chiffre \n Laisser vide pour annuler");
    }

    public string lireCheminAcces(string cheminAcces){
      /**
                               * \brief lis le fichier au chemin donnee et renvoie le tableau des donnees
                               */
      string carteChiffreRetour = "";

      if (!File.Exists(cheminAcces)) //si le chemin n'existe pas...
      {
          Console.WriteLine("\n Erreur critique : le fichier indiqué n'éxiste pas"); //...on indique qu'il y a une erreur
          System.Environment.Exit(0);
          return carteChiffreRetour;
      }
      else{

        try
        {
          using (StreamReader sr = File.OpenText(cheminAcces))
          {

              string s;
              while ((s = sr.ReadLine()) != null)
              {
                carteChiffreRetour = s;
              }
          }
          return carteChiffreRetour;
        }

        catch (Exception e){
          Console.WriteLine("Erreur : {0}", e.Message);
          System.Environment.Exit(0);
          return carteChiffreRetour;
        }
      }

    }

    public void afficherCarte(string carteAAfficher){
      /**
                               * \brief affiche la carte chiffree
                               */
      Console.WriteLine("Carte chiffre : \n {0}", carteAAfficher);
    }


    public string[] decode(string carteChiffrePourDecodage){

      /**
                               * \brief permis de decoder la carte chiffrer
                               */

      // Variables systeme

      string[] carteDechiffre = new string[10];
      string chiffreActuelle = "";
      int currentLigne = 0;
      int chiffreIndex = 0;
      int indexLigne = 0;
      int[] carteValeurs = new int[100];
      char maxChar = 'a';
      int[,] parcellesRef = new int[100,100]; // le premier correspond  au numero de parcelles et le deuxieme au numero de case


      // Lecture carte

      for (int i = 0; i < carteChiffrePourDecodage.Length; i++) {
        if (carteChiffrePourDecodage[i] != '|') {
          if (carteChiffrePourDecodage[i] != ':') {
            chiffreActuelle += carteChiffrePourDecodage[i];
          }

          else{
            if (searchTypeOf(chiffreActuelle) == "z") {
              carteDechiffre[currentLigne] += "0";
            }
            else{
              carteDechiffre[currentLigne] += searchTypeOf(chiffreActuelle);
            }
            // ne se declenche que si un : est declenche, ainsi le chiffre est affichageInstructions
            carteValeurs[chiffreIndex] = Int16.Parse(chiffreActuelle);
            chiffreActuelle = "";
            chiffreIndex++;
            indexLigne++;
          }
        }

        else{
          // ne se declenche que si | est declenche ainsi la ligne est fini

          if (searchTypeOf(chiffreActuelle) == "z") {
            carteDechiffre[currentLigne] += "0";
          }
          else{
            carteDechiffre[currentLigne] += searchTypeOf(chiffreActuelle);
          }
          carteValeurs[chiffreIndex] = Int16.Parse(chiffreActuelle);
          chiffreActuelle = "";
          chiffreIndex++;
          indexLigne = 0;
          currentLigne++;
        }
      }


      for (int i = 0;i < carteDechiffre.Length-1; i++) {
        StringBuilder sb = new StringBuilder(100);
        sb.Append(carteDechiffre[i]);
        string ligneActuelle = sb.ToString();
        string ligneDessous = carteDechiffre[i+1];

        for (int j = 0; j < 10; j++) {
          if (ligneActuelle[j] == '0') {
            // ligneActuelle[j] = "a";
            bool[] frontier = this.analyzeFrontier(carteValeurs[i*10 + j]);
            Console.WriteLine("{0}, {1} : {2}", i, j, frontier[0]);
          }
        }

      }

      return carteDechiffre;
    }

    private string searchTypeOf(string numberToTest){
      int number = Int16.Parse(numberToTest);

      if (number < 32) {
        return "z";
      }

      else if(number < 64 && number >= 32){
        return "F";
      }

      else if(number >= 64){
        return "M";
      }

      else{
        return "0";
      }
    }

    private bool[] analyzeFrontier(int value){
      int number = value;
      bool[] tableauRetour = new bool[3];
      tableauRetour[0] = false;
      tableauRetour[1] = false;
      tableauRetour[2] = false;


      if(number >= 8){
        tableauRetour[0] = true;
        number-=8;
      }
      if(number >= 4){
        tableauRetour[1] = true;
        number-=4;
      }
      if(number >= 2){
        tableauRetour[2] = true;
        number-=2;
      }

      return tableauRetour;

    }

  }
}
