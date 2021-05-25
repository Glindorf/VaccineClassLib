using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using VaccineClassLib;

namespace RestConsumerVaccine
{
    class Program
    {

        private const string ServerUrl = "http://localhost:1954/"; // det er der vi vores consumer henne. Kig i mappe VaccineApí --> properties -->  launchSettings.json, find application URL'en og sæt ind

        static void Main(string[] args)
        {
            // vi skal have vores liste af vacciner - husk "add --> project reference" (vaccineClassLib)
            // den skal consume api'en, derfor skal den ikke have en reference til selve api'en, den skal bare lave forespørgsler til den
            List<Vaccine> vList = new List<Vaccine>();

            // Så skal vi have HTTP-request ind: - Husk "Manage Nuget Packages" - søg "web api client"
            // En klasse, der håndterer vores kalp til Api'en

            HttpClientHandler handler = new HttpClientHandler();

            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                // opsætning af http clienten, så den ved hvilken adresse api'en ligger på
                client.BaseAddress = new Uri(ServerUrl); // sætter BaseAddress til at være lig med localhost i l. 12

                // her fjerner vi default headers så vi selv kan sætte dem op
                client.DefaultRequestHeaders.Clear();

                // her fortæller vi, at vores response skal være i json-format -> det er de eneste oplysninger der behøver at være i headeren
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    // http client prøver at lave et GET kald til {BaseUrl}/vaccines og venter på svar
                    var response = client.GetAsync($"vaccines").Result; // tager fat i vacciner - vi laver et kald
                    if (response.IsSuccessStatusCode) // hvis kaldet er ok
                    {
                        // hvis statuskoden på svaret var fint, så læser Json svaret som en liste af vacciner
                        vList = response.Content.ReadAsAsync<List<Vaccine>>().Result; // tager vi fat i den her - vælger at læse det her response som en liste af vacciner (vi får det ud i Json, valgt i l. 33
                    }
                    else
                    {
                        vList = null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }


            Console.WriteLine("Alle vacciner: \n");
            // sætter listen fra l. 18 til at være den liste vi får ud:

            foreach (Vaccine vaccine in vList)
            {
                Console.WriteLine(vaccine + "\n"); //  - \n (ny linje efter hver gang)
            }

            // så tager vi fat i de enkelte vacciner: // handler ændres til handler2

            // vi laver en vaccinevariabel (når nedenstående er lavet):

            Vaccine v = new Vaccine();

            HttpClientHandler handler2 = new HttpClientHandler();

            handler2.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler2))
            {
                client.BaseAddress = new Uri(ServerUrl); // sætter BaseAddress til at være lig med localhost i l. 12

                client.DefaultRequestHeaders.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var response = client.GetAsync($"vaccines/1").Result; // vi tager fat i /1
                    if (response.IsSuccessStatusCode) // hvis kaldet er ok
                    {
                        v = response.Content.ReadAsAsync<Vaccine>().Result; // ikke længere liste, men bare enkelt vaccine
                    }
                    else
                    {
                        v = null; // ændres til v
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            Console.WriteLine("Vaccine nr. 1: \n");

            Console.WriteLine(v);

            // Så sætter vi start-up projects:
            // VaccineApi "Start" (klik den øverst)
            // RestConsumerVaccine "Start" bagefter den

        }
    }
}
