using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CommandParser;

namespace UnitTestCommandParser
{
    [TestClass]
    public class CommandParserTests
    {
        [TestMethod]
        public void TestGetNumberParams_i0()
        {
            // arrange
            List<string> listcommands = new List<string>(new string[] { "-PING", "-PRINT", "-K", "/?", "/HELP", "-HELP", "-QUIT" });
            int i = 0;
            string[] words = ("-k key1 val1 key2 -print param1 param2 param3").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int actual_countparams;
            // act
            actual_countparams = CommandParser.Program.GetNumberParams(i, words, listcommands);

            // assert
            int expected_countparams = 3;
            Assert.AreEqual(expected_countparams, actual_countparams, "Extracting parameters error (i=0)");
                                
        }
        [TestMethod]
        public void TestGetNumberParams_inot0()
        {
            // arrange
            List<string> listcommands = new List<string>(new string[] { "-PING", "-PRINT", "-K", "/?", "/HELP", "-HELP", "-QUIT" });
            int i = 4;
            string[] words = ("-k key1 val1 key2 -print param1 param2").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int actual_countparams;
      
            // act
            actual_countparams = CommandParser.Program.GetNumberParams(i, words, listcommands);

            // assert
            int expected_countparams = 2;
            Assert.AreEqual(expected_countparams, actual_countparams, "Extracting parameters error (i<>0)");


        }
         [TestMethod]
        public void TestGetParams_i0_K_params3()
        {
            // arrange
            int i = 0;
            string[] words = ("-k key1 val1 key2 -print param1 param2 param3").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int countparams=3;
            string cmd_key="-K";
            string actual_parameters;
            // act
            actual_parameters = CommandParser.Program.GetParams(i, countparams,words,cmd_key);

            // assert
            string expected_parameters = "key1 - val1\nkey2 - null";
            Assert.AreEqual(expected_parameters, actual_parameters, "Extracting parameters error (cmd_key:-k,counparams:3,i=0)");

        }
         [TestMethod]
         public void TestGetParams_i0_K_params0()
         {
             // arrange
             int i = 0;
             string[] words = ("-k -print param1 param2").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
             int countparams = 0;
             string cmd_key = "-K";
             string actual_parameters;
             // act
             actual_parameters = CommandParser.Program.GetParams(i, countparams, words, cmd_key);

             // assert
             string expected_parameters = "-K: Nothing to print! You don't input parameters.";
             Assert.AreEqual(expected_parameters, actual_parameters, "Extracting parameters error (cmd_key:-k,counparams:0,i=0)");

         }
         [TestMethod]
         public void TestGetParams_inot0_PRINT_params5()
         {
             // arrange
             int i = 1;
             string[] words = ("-k -print param1 param2 param3 param4 param5").Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
             int countparams = 5;
             string cmd_key = "-PRINT";
             string actual_parameters;
             // act
             actual_parameters = CommandParser.Program.GetParams(i, countparams, words, cmd_key);

             // assert
             string expected_parameters = "param1 param2 param3 param4 param5";
             Assert.AreEqual(expected_parameters, actual_parameters, "Extracting parameters error (cmd_key:-print,counparams:5,i<>0)");

         }

    }
}
