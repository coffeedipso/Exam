using System.Text.RegularExpressions;
using Exam;

int choice = 0;
string typeOfDictionary = "";
int menu()
{
    Console.WriteLine("\n" +
        //   "\n Создать словарь (Engl->Rus) - 1 " +
        //     "\n Создать словарь (Rus->Engl) - 2 " +
        " Добавить слово и его перевод - 1 " +
        "\n Заменить перевод - 2 " +
        "\n Заменить слово - 3" +
        "\n Искать перевод - 4" +
        "\n Удалить слово - 5" +
        "\n Удалить перевод - 6" +
        "\n Записать словарь в файл - 7" +
        "\n Добавить доп.перевод - 8" +
        "\n Выход - 9 ");
    Console.WriteLine();
    Console.WriteLine("Введите значение 1-8: ");
    try
    {
        choice = Int32.Parse(Console.ReadLine());
        if (choice < 1 || choice > 9)
        {
            Console.WriteLine("Некорректный ввод, введите еще раз:");
            return menu();
        }
        return choice;
    }
    catch (FormatException)
    {
        Console.WriteLine("Некорректный ввод, введите еще раз:");
        return menu();
    }
}
int counter = 0;
Dictionary<string, string> EnglRusDictionary = new Dictionary<string, string>();
while (true)
{
    int ChoiceForSwitch = menu();
    switch (ChoiceForSwitch)
    {
        case 1:

            Console.WriteLine("Введите слово:");
            string word = Console.ReadLine();
            Console.WriteLine("Введите перевод:");
            string translation = Console.ReadLine();
            void ExeptFunction()
            {
                Console.WriteLine($"\nВаш словарь {typeOfDictionary}");
                try
                {
                    counter++;
                    EnglRusDictionary = MyDictionary.AddWordAndTranslation(EnglRusDictionary, word, translation);
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Ошибка при добавлении слова и перевода: " + ex.Message);
                }
            }
            if (MyDictionary.IsEnglLettersOnly(word) && MyDictionary.IsRussianLettersOnly(translation) || MyDictionary.IsEnglLettersOnly(translation) && MyDictionary.IsRussianLettersOnly(word))
            {

                if (!EnglRusDictionary.Any())
                {
                    typeOfDictionary = MyDictionary.IsEnglLettersOnly(word) && MyDictionary.IsRussianLettersOnly(translation) ? "Engl-Rus" : "Rus-Engl";
                    ExeptFunction();
                }
                else
                {
                    bool isFirstKeyEnglLettersOnly = MyDictionary.IsEnglLettersOnly(EnglRusDictionary.First().Key);
                    bool isFirstValueRussianLettersOnly = MyDictionary.IsRussianLettersOnly(EnglRusDictionary.First().Value);

                    if ((isFirstKeyEnglLettersOnly && isFirstValueRussianLettersOnly) && (MyDictionary.IsEnglLettersOnly(word) && MyDictionary.IsRussianLettersOnly(translation)))
                    {
                        typeOfDictionary = "Engl-Rus";
                        ExeptFunction();
                    }
                    else if ((!isFirstKeyEnglLettersOnly && !isFirstValueRussianLettersOnly) && (MyDictionary.IsRussianLettersOnly(word) && MyDictionary.IsEnglLettersOnly(translation)))
                    {
                        typeOfDictionary = "Rus-Engl";
                        ExeptFunction();
                    }
                    else
                    {
                        Console.WriteLine($"\nНекорректный ввод. Ваш словарь {typeOfDictionary}");
                    }
                }

                Console.WriteLine();
                MyDictionary.ShowDictionary(EnglRusDictionary);
            }
            else
            {
                Console.WriteLine("\nНекорректный ввод");
            }
            break;
        case 2:
            Console.WriteLine("Введите слово(ключ),перевод которого хотите заменить: ");
            string oldWord = Console.ReadLine();
            Console.WriteLine("Введите новый перевод(значение), на который заменяете старый: ");
            string newWord = Console.ReadLine();

            if (EnglRusDictionary.ContainsKey(oldWord) && newWord is string)
            {
                if (MyDictionary.IsRussianLettersOnly(newWord) && MyDictionary.IsEnglLettersOnly(oldWord) || MyDictionary.IsRussianLettersOnly(oldWord) && MyDictionary.IsEnglLettersOnly(newWord))
                {
                    EnglRusDictionary = MyDictionary.ChangeTranslation(EnglRusDictionary, oldWord, newWord);
                    Console.WriteLine();
                    MyDictionary.ShowDictionary(EnglRusDictionary);
                }
                else
                {
                    Console.WriteLine($"Ваш словарь {typeOfDictionary}, некорректный ввод");
                }
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 3:
            Console.WriteLine("Введите слово(ключ), которое хотите заменить: ");
            string oldWord2 = Console.ReadLine();
            Console.WriteLine("Введите новое слово, на которое заменяете старое: ");
            string newWord2 = Console.ReadLine();
            if (EnglRusDictionary.ContainsKey(oldWord2) && newWord2 is string)
            {
                if (MyDictionary.IsRussianLettersOnly(newWord2) && MyDictionary.IsRussianLettersOnly(oldWord2) || MyDictionary.IsEnglLettersOnly(oldWord2) && MyDictionary.IsEnglLettersOnly(newWord2))
                {
                    EnglRusDictionary = MyDictionary.ChangeWord(EnglRusDictionary, oldWord2, newWord2);
                    Console.WriteLine();
                    MyDictionary.ShowDictionary(EnglRusDictionary);
                }
                else
                {
                    Console.WriteLine($"Ваш словарь {typeOfDictionary}, некорректный ввод");
                }
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 4:
            Console.WriteLine("Напоминание:" + $"Ваш словарь {typeOfDictionary}");
            Console.WriteLine("Введите слово, перевод которого хотите узнать: ");
            string wordForTranslate = Console.ReadLine();
            if (EnglRusDictionary.ContainsKey(wordForTranslate) && wordForTranslate is string)
            {
                MyDictionary.FindWordFromDictionary(EnglRusDictionary, wordForTranslate);
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 5:
            Console.WriteLine("Введите слово, которое хотите удалить: ");
            string wordDelete = Console.ReadLine();
            if (EnglRusDictionary.ContainsKey(wordDelete) && wordDelete is string)
            {
                EnglRusDictionary = MyDictionary.DeleteWord(EnglRusDictionary, wordDelete, typeOfDictionary, counter);
                Console.WriteLine();
                MyDictionary.ShowDictionary(EnglRusDictionary);
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 6:
            Console.WriteLine("Введите слово, перевод которого хотите удалить: ");
            string TranslationDelete = Console.ReadLine();
            if (EnglRusDictionary.ContainsKey(TranslationDelete) && TranslationDelete is string)
            {
                EnglRusDictionary = MyDictionary.DeleteTranslation(EnglRusDictionary, TranslationDelete, typeOfDictionary, counter);
                Console.WriteLine();
                MyDictionary.ShowDictionary(EnglRusDictionary);
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 7:
            MyDictionary.GetFile(EnglRusDictionary);
            Console.WriteLine("Текстовый файл получен");
            break;
        case 8:
            Console.WriteLine("Введите слово, которому хотите добавить перевод: ");
            string additionalTranslation = Console.ReadLine();
            Console.WriteLine("Введите перевод:");
            string translationForWord = Console.ReadLine();
            if (EnglRusDictionary.ContainsKey(additionalTranslation) && additionalTranslation is string)
            {
                EnglRusDictionary = MyDictionary.AddAdditionalTranslation(EnglRusDictionary, additionalTranslation, translationForWord);
                Console.WriteLine();
                MyDictionary.ShowDictionary(EnglRusDictionary);
            }
            else
            {
                Console.WriteLine("Слово в словаре отсутствует/некорректный ввод");
            }
            break;
        case 9:
            return;
    }
}