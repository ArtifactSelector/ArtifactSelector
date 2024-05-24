using ArtifactSelector.artifact_evaluator;
using ArtifactSelector.model;
using System;
using System.Collections.Generic;

namespace ArtifactSelector.source_processor.test_parser
{
    internal class TestParser
    {
        private Tokenizer tokenizer = new Tokenizer();

        public List<ArtifactTestCase> Parse(string testSource)
        {
            string[] lines = testSource.Split(new[] { Constants.NEW_LINE, Constants.CARRIAGE_RETURN });

            List<ArtifactTestCase> testcases = new List<ArtifactTestCase>();

            string name = "";
            ArtifactSet set = ArtifactSet.None;
            GearSlot slot = GearSlot.None;
            MainStat mainstat = MainStat.None;
            List<SubStat> substats = new List<SubStat>();
            ArtifactAction action = ArtifactAction.None;
            int newLineCount = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                int lineNum = (i - newLineCount) % 9;
                string line = lines[i];

                if (line == "")
                {
                    newLineCount++;
                    continue;
                }

                int actualLineNum = (i / 2) + 1;

                if (lineNum == 0)
                {
                    name = line;
                    if (String.IsNullOrEmpty(name))
                    {
                        throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }
                }

                if (lineNum == 1)
                {
                    TokenType setToken = tokenizer.MakeToken(line, actualLineNum).TokenType;
                    set = ArtifactSetDictionary.FromToken(setToken);
                    if (set == ArtifactSet.None)
                    {
                        throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }
                }

                if (lineNum == 2)
                {
                    TokenType slotToken = tokenizer.MakeToken(line, actualLineNum).TokenType;
                    slot = GearSlotDictionary.FromToken(slotToken);
                    if (slot == GearSlot.None)
                    {
                        throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }
                }

                if (lineNum == 3)
                {
                    TokenType mainstatToken = tokenizer.MakeToken(line, actualLineNum).TokenType;
                    mainstat = MainStatDictionary.FromToken(mainstatToken);
                    if (mainstat == MainStat.None)
                    {
                        throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }
                }

                if (lineNum > 3 && lineNum < 8)
                {
                    TokenType substatToken = tokenizer.MakeToken(line, actualLineNum).TokenType;
                    SubStat substat = SubStatDictionary.FromToken(substatToken);
                    if (substat == SubStat.None && (lineNum != 7 || line != Constants.NONE_KWORD))
                    {
                        throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }

                    if (substat != SubStat.None)
                    {
                        substats.Add(substat);
                    }
                }

                if (lineNum == 8)
                {
                    TokenType actionToken = tokenizer.MakeToken(line, actualLineNum).TokenType;

                    switch (actionToken)
                    {
                        case TokenType.ACTION_LOCK:
                            action = ArtifactAction.Lock;
                            break;
                        case TokenType.ACTION_KEEP:
                            action = ArtifactAction.Keep;
                            break;
                        case TokenType.ACTION_TRASH:
                            action = ArtifactAction.Trash;
                            break;
                        default:
                            throw new ParsingException(ErrorMessages.ParsingError, actualLineNum);
                    }

                    Artifact artifact = new Artifact(slot, set, mainstat, substats);
                    substats = new List<SubStat>();
                    testcases.Add(new ArtifactTestCase(artifact, action, name));
                }

            }

            return testcases;
        }
    }
}
