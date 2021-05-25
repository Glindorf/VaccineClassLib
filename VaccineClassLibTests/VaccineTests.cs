using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaccineClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaccineClassLib.Tests
{
    [TestClass()]
    public class VaccineTests
    {
        [TestMethod()]
        public void TestForSuccesfulCreationOfObject()
        {
            try
            {
                // Arrange - vi starter med at arrangere testen, det gør vi ved at sige:
                // Act

                Vaccine v = new Vaccine(){Id = 1, Producer = "Test", Efficiency = 75, Price = 10}; // vi opretter et nyt objekt af den. Og så propper vi noget data ind i den - derfor kommer "act" med ind i her - fordi vi opsætter testen og tester om den virker som den skal på samme tid.

                // Assert - her tester vi på, om den rent faktisk kan oprette det her

                Assert.AreEqual("Id: 1, Producer: Test, Price: 10, Efficiency: 75", v.ToString());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message); // hvis vi rammer i catch'en skal vi have en Assert.Fail, hvor vi sender e dot message med, så vi kan se hvor/hvorfor den fejler.
            }

            // HUSK AT BUILDE FØR DU KØRER TESTEN
        }

        [TestMethod]
        public void TestForFailInProducerName()
        {
            try // vi går ud fra try-fail-catch metoden
            { // Vi sætter den op til at fejle ved kun at sige Producer = "Tes", da den minimum skal være fore lang
                Vaccine vaccine = new Vaccine() {Producer = "Tes"};
                Assert.Fail(); // hvis den ikke kaster en exception, så er den her failed - den skal jo kaste en exception, da navnet ikke kun må være på tre bostaver, men skal være på fire.
            }
            catch (Exception e)
            {

                // Vi tester om beskeden i exception er korrekt
                Assert.IsTrue(e.Message.Contains("Producer name too short"));
            }
        } // Hvis vi kører testen her er der "succeed", fordi vi har sat den til at fejle! Laver vi Producer = "Test", så fejler testen, fordi den ikke fejler (= den må jo gerne være fire lang!)

        [TestMethod]
        public void TestForFailInPrice()

        {
            try
            {
                Vaccine vaccine = new Vaccine() {Price = -1};
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Price must be 0 or higher"));
            }
        }

        [TestMethod]
        public void TestForFailBelowEfficiency()
        {
            try
            {
                Vaccine vaccine = new Vaccine() {Efficiency = 49};
                Assert.Fail();

            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Efficiency must be at least 50"));
            }
        }

        [TestMethod]
        public void TestForFailAboveEfficiency()
        {
            try
            {
                Vaccine vaccine = new Vaccine() {Efficiency = 101};
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("Efficiency must be 100 or below"));
            }
        }

    }
}