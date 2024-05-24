using ArtifactSelector.artifact_evaluator;
using ArtifactSelector.model;
using ArtifactSelector.source_processor;
using ArtifactSelector.source_processor.test_parser;
using System;
using System.Collections.Generic;
using System.IO;

namespace ArtifactSelector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    string source = ReadSource(args[0]);
                    RunSelector(source);
                }
                else if (args.Length == 3 && args[1] == "-t")
                {
                    string source = ReadSource(args[0]);
                    string test = ReadSource(args[2]);
                    RunTest(source, test);
                }
                else
                {
                    throw new SelectorException(ErrorMessages.Usage);
                }
            }
            catch (SelectorException e)
            {
                Logger.Error(e.Message);
            }
            catch (Exception e)
            {
                Logger.Error(e.StackTrace);
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

        }

        private static void RunSelector(string source)
        {
            Tokenizer tokenizer = new Tokenizer();
            TokenIterator iterator = new TokenIterator(tokenizer.Tokenize(source));
            Parser parser = new Parser();
            ArtifactEvaluator evaluator = parser.Parse(iterator);

            Scanner.ArtifactScanner scanner = new Scanner.ArtifactScanner();

            int trashCount = 0;
            while (trashCount < 39)
            {
                Artifact artifact = scanner.Next();
                ArtifactAction action = evaluator.Evaluate(artifact);

                switch (action)
                {
                    case ArtifactAction.Trash:
                        trashCount++;
                        break;
                    case ArtifactAction.Keep:
                        scanner.DeselectArtifact();
                        break;
                    case ArtifactAction.Lock:
                        scanner.LockArtifact();
                        break;
                    default:
                        throw new SelectorException(ErrorMessages.ReturnedNoneAction);
                }
            }
            Logger.Notice("Done");
        }

        private static void RunTest(string source, string test)
        {
            Tokenizer tokenizer = new Tokenizer();
            TokenIterator iterator = new TokenIterator(tokenizer.Tokenize(source));
            Parser parser = new Parser();
            ArtifactEvaluator evaluator = parser.Parse(iterator);
            TestParser testParser = new TestParser();
            List<ArtifactTestCase> testcases = testParser.Parse(test);

            int failedCount = 0;
            foreach (ArtifactTestCase testcase in testcases)
            {
                ArtifactAction expected = testcase.Action;
                ArtifactAction actual = evaluator.Evaluate(testcase.Artifact);

                if (expected != actual)
                {
                    failedCount++;
                    Logger.Notice(NoticeMessages.FailedTestcase, testcase.Name, expected, actual);
                }
            }

            if (failedCount > 0)
            {
                Logger.Notice(NoticeMessages.FailedTestcaseOverview, failedCount, testcases.Count);
            }
            else
            {
                Logger.Notice(NoticeMessages.PassedAllTestcases);

            }
        }

        private static string ReadSource(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        return sr.ReadToEnd();
                    }
                }
                catch (Exception)
                {
                    throw new SelectorException(ErrorMessages.CouldNotReadFile, filePath);
                }
            }
            throw new SelectorException(ErrorMessages.FileDoesNotExist, filePath);
        }
    }
}