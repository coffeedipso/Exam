using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Exam
{
    internal class MyDictionary
    {
         public static Func<string, bool> IsRussianLettersOnly = r => Regex.IsMatch(r, "^[А-Яа-я,]+$");
         public static Func<string, bool> IsEnglLettersOnly = e => Regex.IsMatch(e, "^[A-Za-z,]+$");
        public static string GenerateWordEngl(int counter)
        {
            string result = Regex.Replace("a", @"[a-zA-Z]", m => ((char)(m.Value[0] + counter - 1)).ToString().ToUpper());
            return result;
        }
        public static string GenerateWordRus(int counter)
        {
            string result = Regex.Replace("а", @"[А-Яа-я]", m => ((char)(m.Value[0] + counter - 1)).ToString().ToUpper());
            return result;
        }

        public static void ShowDictionary(Dictionary<string, string> EnglRusDictionary)
        {
            foreach (var word in EnglRusDictionary)
            {
                Console.WriteLine($"key: {word.Key}  value: {word.Value}");
            }
        }
        public static Dictionary<string, string> AddWordAndTranslation(Dictionary<string, string> EnglRusDictionary, string word, string translation)
        {
            EnglRusDictionary[word] = translation;
            return EnglRusDictionary;
        }
        public static Dictionary<string, string> ChangeTranslation(Dictionary<string, string> EnglRusDictionary, string Keys, string newWord)
        {
            EnglRusDictionary[Keys] = newWord;
            return EnglRusDictionary;
        }
        public static Dictionary<string, string> ChangeWord(Dictionary<string, string> EnglRusDictionary, string Keys,string newWord)
        {
            var value = EnglRusDictionary[Keys];
            EnglRusDictionary.Remove(Keys);
            EnglRusDictionary.Add(newWord, value);
            return EnglRusDictionary;
        }
        public static void FindWordFromDictionary(Dictionary<string, string> EnglRusDictionary, string wordForTranslate)
        {
            var translation = EnglRusDictionary.FirstOrDefault(x => x.Key == wordForTranslate);
            if (translation.Key != null)
            {
                Console.WriteLine($"Перевод слова {translation.Key} - {translation.Value}");
            }
        }
        public static Dictionary<string, string> DeleteWord(Dictionary<string, string> EnglRusDictionary, string DeleteWord, string typeOfDictionary,int counter)
        {
                string EnglWord = $"none{GenerateWordEngl(counter)}";
                string RusWord = $"Пусто{GenerateWordRus(counter)}";
                var value = EnglRusDictionary[DeleteWord];
                EnglRusDictionary.Remove(DeleteWord);
                EnglRusDictionary.Add(typeOfDictionary == "Rus-Engl" ? RusWord : EnglWord, value);
                return EnglRusDictionary;
        }
        public static Dictionary<string, string> DeleteTranslation(Dictionary<string, string> EnglRusDictionary, string TranslationDelete, string typeOfDictionary, int counter)
        {
            string EnglWord = $"none{GenerateWordEngl(counter)}";
            string RusWord = $"Пусто{GenerateWordRus(counter)}";
            var value = EnglRusDictionary[TranslationDelete];
            EnglRusDictionary.Remove(TranslationDelete);
            EnglRusDictionary.Add(TranslationDelete, typeOfDictionary == "Rus-Engl" ? EnglWord : RusWord);
            return EnglRusDictionary;
        }
        public static void GetFile(Dictionary<string, string> EnglRusDictionary)
        {
            using (StreamWriter writer = new StreamWriter("словарь.txt"))
            {
                
                foreach (var pair in EnglRusDictionary)
                {
                    writer.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
        }
        public static Dictionary<string, string> AddAdditionalTranslation(Dictionary<string, string> EnglRusDictionary,string additionalTranslation,string translationForWord)
        {
            EnglRusDictionary[additionalTranslation] += "," + translationForWord;
            return EnglRusDictionary;
        }
    }
}
