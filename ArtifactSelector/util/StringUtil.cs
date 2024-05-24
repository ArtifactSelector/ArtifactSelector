using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ArtifactSelector
{
    internal static class StringUtil
    {

        public static bool isWhitespace(char c)
        {
            return Constants.WHITESPACES.Contains(c);
        }

        public static bool isAlphabets(string s)
        {
            return Regex.IsMatch(s, @"^[a-zA-Z]*$");
        }

        public static bool isAlphaPercent(string s)
        {
            return Regex.IsMatch(s, @"^[a-zA-Z%]*$");
        }

        public static bool isCompoundOperator(string s)
        {
            return Constants.COMPOUND_OPERATORS.Contains(s);
        }

        public static string FindClosestInList(string source, HashSet<string> targets, double minConfidence)
        {
            if (targets.Contains(source))
            {
                return source;
            }

            if (string.IsNullOrWhiteSpace(source))
            {
                return "";
            }

            string mostSimilarString = "";
            double mostSimilarValue = 0;

            foreach (var target in targets)
            {
                double similarityValue = StringSimilarity(source, target);

                if (similarityValue > minConfidence && similarityValue > mostSimilarValue)
                {
                    mostSimilarValue = similarityValue;
                    mostSimilarString = target;
                }
            }

            if (!string.IsNullOrWhiteSpace(mostSimilarString))
            {
                Logger.Notice(NoticeMessages.FoundMostSimilarString, source, mostSimilarString, mostSimilarValue);
            }
            else
            {
                throw new SelectorException(ErrorMessages.CouldNotRecognizeWord, source);
            }

            return mostSimilarString;
        }

        private static double StringSimilarity(string s1, string s2)
        {
            int distance = LevenshteinDistance(s1, s2);
            int maxLength = Math.Max(s1.Length, s2.Length);
            double similarity = 1.0 - (distance / (double)maxLength);
            return similarity * 100.0;
        }

        private static int LevenshteinDistance(string s1, string s2)
        {
            int m = s1.Length;
            int n = s2.Length;
            int[,] dp = new int[m + 1, n + 1];

            for (int i = 0; i <= m; i++)
            {
                for (int j = 0; j <= n; j++)
                {
                    if (i == 0)
                    {
                        dp[i, j] = j;
                    }
                    else if (j == 0)
                    {
                        dp[i, j] = i;
                    }
                    else if (s1[i - 1] == s2[j - 1])
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else
                    {
                        dp[i, j] = 1 + Math.Min(Math.Min(dp[i - 1, j], dp[i, j - 1]), dp[i - 1, j - 1]);
                    }
                }
            }

            return dp[m, n];
        }
    }
}
