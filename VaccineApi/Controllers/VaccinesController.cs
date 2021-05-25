using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VaccineClassLib;

namespace VaccineApi.Controllers
{
    [Route("[controller]")] // fjern "api/" for så bruger den kun Vaccines - i stedet for man skal skrive /api/vaccines, skal man bare skrive /vaccines
    [ApiController]
    public class VaccinesController : ControllerBase 
    { // 2. a: List vaccinerne fra opgaven og returner alle vaccinetyper

        private static int nextId = 4; //variabel til at holde styr på det næste Id i rækken - laves først efter vi har lavet listen lige nedenunder denne linje 

        // Nedenstående svarer til opg. c - vi lavede den bare først, da vi alligevel skulle bruge den til de andre opgaver

        private static List<Vaccine> vaccines = new List<Vaccine>()
        {
            new Vaccine(){Id = 1, Producer = "AstraZeneca", Efficiency = 75, Price = 25}, 
            new Vaccine(){Id = 2, Producer = "Moderna", Efficiency = 95, Price = 165}, 
            new Vaccine(){Id = 3, Producer = "Pfizer", Efficiency = 95, Price = 165}
        };

        [HttpGet] // Get-metode, der henter alle vaccinerne
        [Route("")] // behøver ikke være her i dette tilfælde, da den alligevel er tom
        public IEnumerable<Vaccine> GetAll() // IEnumerable - et interface, der sørger for at have (en liste eller) et array af objekter eller andet, fx integers, det betyder bare, at man kan loope igennem dem (lave en løkke) som fx et foreach loop - det er en simpel liste
        {
            return vaccines; // Den smider vores vacciner ud til os
        }

        // 2. b Returnerer vaccinen med det angivne id

        [HttpGet] // Vi har en statisk liste, det skal være en HttpGet
        [Route("{Id}")] // Vi sætter route på og kalder den Id, da vi gerne vil have et tal med på den
        public Vaccine GetById(int id) // Vi skriver Vaccine, da det er en vaccine vi gerne vil returnerer - og GetById, da vi gerne vil få den frem via Id (int id)
        {

            Vaccine v = vaccines.Find(e =>e.Id == id); // Vi opretter en ny variabel, som vi kalder v - men hvordan finder vi den specifikke vaccine? På lister findes der lambda-udtryk. Vi kan sige vaccines dot find. Vi ved at Id er unikke, men vi skal have et lambda.udtryk, der identificerer hvad er det vi skal bruge til at finde med. Vi siger: e (det er den du er nået til) skal være det samme som det id vi sender med til den.

            // man kan teste om v = null, hvis man fx søger på id = 4, men der kun er 3 med på listen

            if (v == null)
            {
                Response.StatusCode = 404; // statuskoden for NotFound.
            }

            return v; // og returnerer v
        }

        // 2. c Her tilføjer vi en ny vaccine, som skal returnere det id den er blevet tildelt

        [HttpPost] // Her skal vi have en HttpPost, da vi skal kunne tilføje
        [Route("")] // Behøves ikke her

        public int Add(Vaccine vacc) // Den skal godtage en vaccine, som vi kalder vac
        // Så får vi en vaccine, men vi vil også have, at dens Id passer ind i listen som det næste Id, derfor:

        {
            vacc.Id = nextId++; // her siger vi, at vaccinen skal være lig med vores næste Id (sat i toppen af denne side), og efter det plusser vi en oven i

            vaccines.Add(vacc); // Her tager vi fat i vores liste og add'er den vaccine vi har lavet (den der er sendt til routen) EFTER den har fået et nyt Id

            return vacc.Id; // Vi returnerer det Id den er blevet tildelt
        }

        // 2. d Sletter vaccinen med det angivne Id og returnerer True, hvis det pågældende Id findes - ellers False

        [HttpDelete]
        [Route("{Id}")] // den skal indeholde ID, så vi kan vide hvilken vi skal slette
        public bool Delete(int id)
        {
            // her finder vi den vaccine, der skal slettes
            Vaccine v = vaccines.Find(vac =>vac.Id == id);// Vi skal have fat i en vaccine, som har det her id. Vi skal finde vaccinen - med en lambda. "Find den her, der er lig med det id vi sender med til den".
            // Vi skal returnere True eller False
            return vaccines.Remove(v);  // sender resultatet af når man holder musen over, kan man se hvad den returnerer - true hvis succes, false hvis ikke + hvis det ikke findes i listen (.Remove er en indbygget funktion i lister, der returnerer true eller false, alt efter om den får slettet noget eller ej)
        }

        // Vi prøvekører projektet ved at ændre StartUp til vores API

    }
}
