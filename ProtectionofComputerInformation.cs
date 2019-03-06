using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ZKI_LR2
{
    class EncryptionCaesar
    {
        public EncryptionCaesar()
        {

        }
        protected void SimplyEncryption(string text, int key)//Простая шировка Цезаря
        {
            Console.WriteLine("Шифруемый текст:\n" + text);
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                temp += Convert.ToChar(text[i] + key);
            }
            Console.WriteLine(temp);
        }
        protected void SimlpyUnencryption(string text, int key)
        {
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                temp += Convert.ToChar(text[i] - key);

            }
            Console.WriteLine("Расшифрованный текст" + temp);
        }
        protected void AffinnEncryption(string text, int keyA, int keyB)//Аффинная шифровка Цезаря
        {
            Console.WriteLine("Аффинная шифровка...\n" +
                "Шифруемый текст:\n" + text);
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 32)
                {
                    temp += text[i];
                    continue;
                }
                temp += Convert.ToChar((keyA * (text[i] - 1040) + keyB + 1040));
            }
            Console.WriteLine(temp);
            AffinnUnencryption(temp, keyA, keyB);//
        }
        protected void AffinnUnencryption(string text, int keyA, int keyB)
        {
            string temp = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == 32)
                {
                    temp += text[i];
                    continue;
                }
                temp += Convert.ToChar((text[i] - keyB - 1040) / 4 + 1040);
            }
            Console.WriteLine("Расшифрованный текст: " + temp);
        }

        protected void CaesorWordEncryption(string text, string word, int key)// шифровка Цезаря с ключевым словом
        {
            string temp = "";
            char[] tempchar = new char[64];
            for (int i = 0; i < 64; i++)
            {
                tempchar[i] = Convert.ToChar(1040 + i);
                // Console.WriteLine(tempchar[i]);
            }
            Console.WriteLine("Шифровка Цезаря с словом...\n" +
                "Шифруемый текст:\n" + text);
            temp += word;
            foreach (char k in word)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (k == tempchar[i])
                    {
                        tempchar[i] = '8';
                    }

                }
            }

            for (int i = 0; i < 64 - word.Length; i++)
            {
                if (tempchar[i] == '8')
                    continue;
                temp += tempchar[i];
            }
            string transcript = "";
            string alfavit = "";
            foreach (char k in tempchar)
            {
                alfavit += k;
            }

            int num = 0;
            for (int i = 0; i < text.Length; i++)
            {
                for (int j = 1040; j < 1072; j++)
                {
                    num = alfavit.IndexOf(text[i]);
                }
                num += key;
                if (text[i] == ' ' || text[i] == '-')
                {
                    transcript += '_';
                    continue;
                }
                transcript += temp[num];
                num = 0;
            }
            Console.WriteLine("Шифрованный текст:\n" + transcript);
            Console.ReadKey();
        }
        protected void CaesorWordUnencryption(string text, int keyA, int keyB)
        {

            Console.WriteLine("Расшифрованный текст:\n "  /*+temp*/);
        }

        protected void Transcript(string text, int key)
        {
            Console.WriteLine("Выберите вид расшифровки\n" +
                "1 - Простая расшифровка цезаря по ключу\n" +
                "2 - Аффинная расшифровка цезаря по двум ключам");
            int swt = Convert.ToInt32(Console.ReadLine());
            switch (swt)
            {
                case 1:
                    {
                        SimlpyUnencryption(text, key);

                    }
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
        protected void Treesemus(string text, string key)
        {
            char[,] worksheet = new char[4, 8];
            string word = text;

            string tempText = "";
            Console.WriteLine("Text:{0}\nElements from key only <=8", text);
            int k1 = 0;
            foreach (char k in key)
            {
                Console.Write(key[k1] + " ");
                worksheet[0, k1] = key[k1];
                k1++;
            }
            int alf = 1040;
            for (int i = 0; i < 4; i++)
            {
                for (int j = k1; j < 8; j++)
                {
                    if (key.IndexOf(Convert.ToString((char)alf)) != -1)
                    {
                        alf++;
                        j--;
                        continue;
                    }
                    //Console.WriteLine(key.IndexOf(Convert.ToString(alf)));
                    worksheet[i, j] = (char)alf;
                    alf++;
                    Console.Write(worksheet[i, j] + " ");
                }
                k1 = 0;
                Console.WriteLine();
            }
            char tempText1;
            word = "";
            bool check = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    word += " ";
                    continue;
                }
                if (text[i] == '–')
                {
                    word += "–";
                    continue;
                }
                tempText1 = text[i];
                check = false;
                for (int j = 0; j < 4; j++)
                {
                    for (int l = 0; l < 8; l++)
                    {

                        if (worksheet[j, l] == tempText1)
                        {
                            if (j + 1 < 4)
                                word += worksheet[j + 1, l];
                            else
                                word += worksheet[0, l];
                            check = true;
                            break;
                        }
                    }
                    if (check == true)
                        break;
                }

            }
            Console.WriteLine("Encripting text: " + word);
        }
        public void SwtitchMethod(string text, int key)
        {
            Console.WriteLine("ВЫ ИСПОЛЬЗУЕТЕ ШИФРОВАНИЕ ЦЕЗАРЯ\n" +
                "1 - для простого шифравания с ключом\n" +
                "2 - для аффинной шифровки с двумя ключами\n" +
                "3 - для шифрования Цезаря с ключом и ключевым словом" +
                "4 - для шифрования Трисемуса");
            int swt = Convert.ToInt32(Console.ReadLine());
            switch (swt)
            {
                case 1:
                    SimplyEncryption(text, key);
                    break;
                case 2:
                    {
                        Console.Write("!!!Для этого метода потребуется 2 ключа\n" +
                            "a= ");
                        key = Convert.ToInt32(Console.ReadLine());

                        Console.Write("b= ");
                        int key1 = Convert.ToInt32(Console.ReadLine());
                        AffinnEncryption(text, key, key1);
                    }
                    break;
                case 3:
                    {
                        Console.Write("Введите ключевое слово =");
                        string WordKey = Convert.ToString(Console.ReadLine());
                        //  Console.Write("Введите ключ ="); key = Convert.ToInt32(Console.ReadLine());
                        CaesorWordEncryption(text, WordKey, key);
                    }
                    break;
                case 4:
                    Treesemus("УСПЕХ – ЭТО КОГДА ТЫ ДЕВЯТЬ РАЗ УПАЛ, НО ДЕСЯТЬ РАЗ ПОДНЯЛСЯ", "РУЧКА");
                    break;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }
    class Plaifer
    {
        string word = "";
        string text;
        string key;
        string alfavit = "";
        public Plaifer(string _text, string _key)
        {
            text = _text;
            key = _key;
        }
        public void Encryption()
        {
            int amount = 0;
            foreach (char k in key)
            {
                if (alfavit.IndexOf(k) != -1)
                    continue;
                else
                {
                    alfavit += k;
                    amount++;
                }
            }


            for (int i = 32; i < 64; i++)
            {
                if (alfavit.IndexOf(Convert.ToChar((char)1040 + i)) != -1)
                    continue;
                else
                    alfavit += Convert.ToChar((char)1040 + i);
            }
            Console.WriteLine(alfavit.Length + " " + alfavit);

            char[,] alfavitArr = new char[4, 8];
            Console.WriteLine("Вспомогательный массив");
            for (int i = 0, b = 0; i < 4; i++)
            {

                for (int j = 0; j < 8; j++, b++)
                {
                    alfavitArr[i, j] = alfavit[b];
                    Console.Write(alfavitArr[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Исходный текст........\n" + text);
            Console.WriteLine("Зашифрованный текст...");
            int X1 = 0, Y1 = 0, X2 = 0, Y2 = 0;
            //Console.WriteLine((int)alfavitArr[2,2]+"|||"+(int)text[0]);
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',' || text[i] == ' ')
                    text = text.Remove(i, 1);
            }
            for (int tempI = 0; tempI < text.Length - 2; tempI += 2)
            {
                //if (text[tempI + 1] == ',' || text[tempI + 1] == ' ' || text[tempI] == ',' || text[tempI] == ' ')
                //{
                //    Console.Write(" "); tempI++;
                //}

                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        // Console.Write(text[tempI]+""+text[tempI]);
                        if (text[tempI] == alfavitArr[i, j])
                        {
                            X1 = j;
                            Y1 = i;// Console.Write("X1Y1=" + X1 + "" + Y1 + "Для буквы {0}", text[tempI]);
                        }
                        if (text[tempI + 1] == alfavitArr[i, j])
                        {

                            X2 = j;
                            Y2 = i;// Console.Write("X2Y2=" + X2 + "" + Y2 + "Для буквы {0}", text[tempI + 1]);
                        }
                    }

                }
                //  Console.WriteLine();
                ///
                if (Y1 == Y2)
                {
                    if (X1 + 1 == 8)

                        X1 = -1;
                    Console.Write(alfavitArr[Y1, X1 + 1]);

                    if (X2 + 1 == 8)

                        X2 = -1;
                    Console.Write(alfavitArr[Y2, X2 + 1]);
                    continue;

                }
                else if (X1 == X2)
                {
                    if (Y1 + 1 == 4)

                        Y1 = -1;
                    Console.Write(alfavitArr[Y1 + 1, X1]);

                    if (Y2 + 1 == 4)

                        Y2 = -1;
                    Console.Write(alfavitArr[Y2 + 1, X2]);
                    continue;
                }
                else
                {
                    if (X1 < X2)
                    {
                        Console.Write(alfavitArr[Y2, X1] + "" + alfavitArr[Y1, X2]);
                    }
                    else
                    {
                        // Console.Write("{0} {1}", Y1, X2);
                        if (Y1 == -1)
                            Y1++;
                        Console.Write(alfavitArr[Y1, X2] + "" + alfavitArr[Y2, X1]);
                    }
                }
                Y1 = 0;
                Y2 = 0;
                X1 = 0;
                X2 = 0;
                /// тут нужно свапнуть
                ///
            }
        }
    }
    class Whitson
    {

        public static string DeleteReplic(string text)
        {
            return new string(text.ToCharArray().Distinct().ToArray());
        }
        string KeyFirst;
        string KeySecond;
        string text;
        string alf = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЬЫЪЭЮЯ";
        public Whitson(string text, string key1arg, string key2arg)
        {
            KeyFirst = key1arg.ToUpper();
            KeySecond = key2arg.ToUpper();
            this.text = text;
        }
        public void WhitsonConsider()
        {
            string[] str1 = text.ToUpper().Split(',', ' ', ':', '.', '?', '!');
            string str = "";
            for (int i = 0; i < str1.Length; i++)
            {
                str += str1[i];
            }
            if (str.Length % 2 != 0)
            {
                str = str.PadRight((str.Length + 1), 'Ъ');
            }
            int length = str.Length / 2;
            int rows = 4;
            int columns = str.Length / rows;

            char[,] array1 = new char[rows, columns];
            char[,] array2 = new char[rows, columns];
            int count = 0;
            int count1 = 0;
            string trans1 = DeleteReplic(KeyFirst + alf);
            string trans2 = DeleteReplic(KeySecond + alf);
            Console.WriteLine("Table 1:" + "                       " + "Table 2");
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < alf.Length / 4; j++)
                {
                    array1[i, j] = trans1[count++];
                    Console.Write(array1[i, j] + " ");
                }
                Console.Write("               ");
                for (int j = 0; j < alf.Length / 4; j++)
                {
                    array2[i, j] = trans2[count1++];
                    Console.Write(array2[i, j] + " ");
                }
                Console.WriteLine();
            }
            count = 0;
            count1 = 0;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\n\nText: " + text);
            int l = -1;
            string[] str2 = new string[length];
            for (int i = 0; i < str.Length; i += 2)
            {
                l++;
                if (l < length)
                {
                    str2[l] = Convert.ToString(str[i]) + Convert.ToString(str[i + 1]);
                }
            }
            int i_1 = 0, j_1 = 0;
            int i_2 = 0, j_2 = 0;
            string buffer_string1 = "", buffer_string2 = "";
            string encodetString = "";
            columns = 8;
            foreach (string both in str2)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < columns; j++)
                    {

                        if (both[0] == (array1[i, j]))
                        {
                            i_1 = i;
                            j_1 = j;

                        }
                        if (both[1] == (array2[i, j]))
                        {
                            i_2 = i;
                            j_2 = j;

                        }
                    }
                }
                //  Console.Write(columns);

                if (i_1 == i_2)
                {
                    if (j_1 == columns - 1)
                    {
                        buffer_string2 = Convert.ToString(array1[i_1, 0]);
                    }
                    else
                    {
                        buffer_string2 = Convert.ToString(array1[i_1, j_1 + 1]);
                    }
                    if (j_2 == columns - 1)
                    {
                        buffer_string1 = Convert.ToString(array2[i_2, 0]);
                    }
                    else
                    {
                        buffer_string1 = Convert.ToString(array2[i_2, j_2 + 1]);
                    }
                }
                if (i_1 != i_2)
                {

                    buffer_string2 = Convert.ToString(array1[i_2, j_1]);
                    buffer_string1 = Convert.ToString(array2[i_1, j_2]);
                }
                if (buffer_string1 == buffer_string2)
                {
                    encodetString += buffer_string1 + "Ъ" + buffer_string2;
                }
                else
                {
                    encodetString += buffer_string1 + buffer_string2;
                }
            }
            Console.WriteLine("До перестановки:");
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write(str[i]);
                if ((i + 1) % 2 == 0)
                    Console.Write(" ");
            }
            Console.WriteLine();
            Console.WriteLine("После перестановки:");
            for (int i = 0; i < encodetString.Length; i++)
            {
                Console.Write(encodetString[i]);
                if ((i + 1) % 2 == 0)
                    Console.Write(" ");
            }
            Console.ReadKey();
        }
    }
    class Vernam
    {
        private int option;


        public Vernam()
        {
        metka:
            Console.WriteLine("1 для шифрования,2 для расшифровки");
            try
            {

                int temp = Convert.ToInt32(Console.ReadLine());
                if (temp == 1 || temp == 2)
                {
                    if (temp == 2 && File.Exists("vernam.txt") == false)
                    {
                        throw new Exception("Вы еще ничего не шифровали!Выберите 1");
                    }
                    option = temp;
                }
                else
                {
                    throw new Exception("Недопустимый выбор");

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                goto metka;
            }

        }
        public void EncodyngChilper()
        {
           
            //encrypt
            if (option == 1)
            {
                string openText = File.ReadAllText("ZKI5.txt");
                string key = GetPass(openText.Length);
                Console.WriteLine("Используйте этот случайный ключ для расшифровки!\n" + key);
                if (key?.Length != openText.Length)
                    return;
                string chiper_txt = "";
                for (int i = 0; i < openText.Length; i++)
                    chiper_txt += (char)(openText[i] ^ key[i]);//openSymbol XOR key
                File.WriteAllText("vernam.txt", chiper_txt);
            }
            //decrypt
            if (option == 2)
            {
                string chiper_txt = File.ReadAllText("vernam.txt");
                Console.WriteLine("Ваш ключ?...");
                string key = Console.ReadLine();

                if (key?.Length != chiper_txt.Length)
                    return;

                string openText = "";
                for (int i = 0; i < chiper_txt.Length; i++)
                    openText += (char)(chiper_txt[i] ^ key[i]);//chipersymbol XOR key
                Console.WriteLine("Decrypt:   " + openText);
                File.WriteAllText("ZKI5decrypt.txt", openText);
            }
        }
        static string GetPass(int size)
        {
            int[] arr = new int[size]; // сделаем длину пароля в 16 символов
            Random rnd = new Random();
            string Password = "";

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rnd.Next(33, 125);
                Password += (char)arr[i];
            }
            return Password;
        }

    }

    class ProtectionOfComputerInformation
    {
        static void Main(string[] args)
        {
            int KeyForTasck = 16;
            //<ZKI_1zadanie>
            //string TxtForTasck = "МЫ ДОЛЖНЫ ПРИЗНАТЬ ОЧЕВИДНОЕ: ПОНИМАЮТ ЛИШЬ ТЕ, КТО ХОЧЕТ ПОНЯТЬ";
            //</ZKI_1zadanie>

            //<ZKI_2zadanie>
            // string TxtForTasck = "СМЫСЛ ЖИЗНИ НАШЕЙ – НЕПРЕРЫВНОЕ ДВИЖЕНИЕ";
            //</ZKI_2zadanie>

            //<ZKI_2zadanie>
            //  string TxtForTasck = "РАЗУМА ЛИШАЕТ НЕ СОМНЕНИЕ, А УВЕРЕННОСТЬ";
            //</ZKI_2zadanie>

            // EncryptionCaesar caesar = new EncryptionCaesar();
            //caesar.SwtitchMethod(TxtForTasck, KeyForTasck);

            // Plaifer lr3 = new Plaifer("системы второго типа являются утилитами шифрования, которые необходимо специально вызывать ", "перемены");
            // lr3.Encryption();
            //   Whitson ob = new Whitson("системы второго типа являются утилитами шифрования, которые необходимо специально вызывать", "Мисюченко", "Владислав");
            //   ob.WhitsonConsider();
            Vernam on = new Vernam();
            on.EncodyngChilper();
            Console.ReadKey();
        }
    }
}
