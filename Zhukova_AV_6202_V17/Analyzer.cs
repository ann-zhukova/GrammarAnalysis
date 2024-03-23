using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhukova_AV_6202_V17
{
    class Analyzer
    {
        private enum States { S, A, B, C, D, H, G, I, J, K, L, M, N, O, P, Q, R, F, E} // перечесление состояний графа 
        public static bool CheckConst(string chain, out string value, out int errorPos) // реализация графа состояний константы 
        {
            States state = States.S;
            errorPos = -1; //позиция ошибки
            chain += ';'; //добавление к исходной строке завершающего символа
            value = ""; // константа или сообщение об ошибке
            int position = 0; // текущая позиция в строке
            while ((state != States.F) && (state != States.E) && (chain.Length > position))
            {
                char symbol = chain[position]; // просмотр символа в строке 
                switch (state) // переход к текущему состоянию 
                {
                    case States.S: 
                        {
                            switch(symbol) // просмотр символа 
                            {
                                case '1': case '2':
                                case '3': case '4':
                                case '5': case '6':
                                case '7': case '8':
                                case '9':
                                    {
                                        state = States.A; // перереход к следующему состоянию
                                        value += symbol; // добавления символа к результату
                                        position++; // переход к следующему символу 
                                        break;
                                    }
                                case '+':
                                case '-':
                                    {
                                        state = States.B; // переход к следующему состоянию
                                        value += symbol; // добавления символа к результату
                                        position++; // переход к следующему символу 
                                        break; 
                                    }
                                case '0':
                                    {
                                        if (position < chain.Length - 2) // проверка на незначащие нули
                                        {
                                            value = "Ошибка! Невозможен ввод незначаших нулей!"; // вывод ошибки 
                                            state = States.E; // переход к ошибочному состоянию
                                            errorPos = position; // присваивание позиции ошибки текущей позиции
                                        }
                                        else
                                        {
                                            value += symbol; // добавление символа к результату 
                                            position++; // переход к следующему символу 
                                            state = States.C; // переход к следующему состоянию
                                        }
                                        break;
                                    }
                                case ';':
                                    {
                                        if (position < chain.Length - 1) // проверка позиции завершающего символа 
                                        {
                                            value = "Ошибка! Ожидается число или знак (+, -)!"; // вывод ошибки 
                                            state = States.E; // переход к ошибочному состоянию
                                            errorPos = position; // присваение позиции ошибки текущей позиции
                                        }
                                        else
                                        {
                                            state = States.F; // перход к конечному сотоянию 
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        errorPos = position; // присваение позиции ошибки текущей позиции
                                        value = "Ошибка! Ожидается число или знак +, -!"; // вывод ошибки
                                        state = States.E; // переход к ошибочному сотоянию 
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.A):
                        {
                            switch (symbol) // просмотр символа 
                            {
                                case '1':  case '2':
                                case '3':  case '4':
                                case '5':  case '6':
                                case '7':  case '8':
                                case '9':  case '0':
                                    {
                                        state = States.A; // переход к следующему состоянию 
                                        value += symbol; // добавление символа к результату 
                                        position++; // переход к следующему символу 
                                        break;
                                    }
                                case ';':
                                    {
                                        if (position < chain.Length - 1) // проверка позиции заверщающего символа 
                                        {
                                            value = "Ошибка! Ожидается число!"; // вывод ошибки 
                                            state = States.E; // переход к ошибочному состоянию 
                                            errorPos = position; // присваение позиции ошибки текущей позиции
                                        }
                                        else
                                        {
                                            state = States.F; // переход к конечному состоянию 
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        errorPos = position; // присваение позиции ошибки текущей позиции
                                        value = "Ошибка! Ожидается число!"; // вывод ошибки 
                                        state = States.E; // переход к ошибочному состоянию 
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.B):
                        {
                            switch(symbol) // просмотр символа 
                            {
                                case '1': case '2':
                                case '3': case '4':
                                case '5': case '6':
                                case '7': case '8':
                                case '9':
                                    {
                                        state = States.A; // переход к слудующему состоянию 
                                        value += symbol; // добавление смивола к результату 
                                        position++; // переход к следующему символу
                                        break;
                                    }
                                default:
                                    {
                                        errorPos = position; // присваение позиции ошибки текущей позиции 
                                        value = "Ошибка! Ожидается число!"; // вывод ошибки 
                                        state = States.E; // переход к ошибочному состоянию 
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.C):
                        {
                            switch(symbol) // просмотр текущего символа 
                            {
                                case ';':
                                    {
                                        if (position < chain.Length - 1) // проверка позиции завершающего символа 
                                        {
                                            value = "Ошибка! Не ожидается никаких символов!"; // вывод ошибки 
                                            state = States.E; // переход к ошибочному состоянию 
                                            errorPos = position; // присваение позиции ошибки текущей позиции 
                                        }
                                        else
                                        {
                                            state = States.F; // переход к конечному состоянию 
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        errorPos = position; // присваение позиции ошибки текущей позиции 
                                        value = "Ошибка! Не ожидается никаких символов!"; // вывод ошибки 
                                        state = States.E; // переход к ошибочному состоянию 
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.F):
                        {
                            break;
                        }
                    case (States.E):
                        {
                            break;
                        }

                }

            }
            return state == States.F;
        }
        public static bool CheckID(string chain, out string value, out int errorPos, out LinkedList<Entry> idConsts, out LinkedList<Entry> numeralsConsts) // реализация графа состояний параметра
        {
            States state = States.S; // начальное состояние
            errorPos = -1; // позиция ошибки 
            chain += ';'; // добавление конечного символа к текущей строке 
            value = ""; // идентификатор или сообщение об ошибке
            int lenID = 0; // длина идентификатора 
            string constValue = ""; // константа 
            int position = 0; // позиция в строке
            idConsts = new LinkedList<Entry>(); // список идентификаторов 
            numeralsConsts = new LinkedList<Entry>(); // список констант 
            while ((state != States.F) && (state != States.E) && (chain.Length > position)&& lenID <= 8) 
            {
                char symbol = chain[position]; // просмотр символа в строке
                switch(state) // просмотр текущего состояния 
                {
                    case (States.S): 
                    {
                            if(symbol == 'a' || symbol == 'b' || symbol == 'c' ||
                                symbol == 'd' || symbol == 'e' || symbol == 'f' || symbol == 'g' ||
                                symbol == 'h' || symbol == 'i' || symbol == 'j' || symbol == 'k' ||
                                symbol == 'l' || symbol == 'm' || symbol == 'n' || symbol == 'o' ||
                                symbol == 'p' || symbol == 'q' || symbol == 'r' || symbol == 's' ||
                                symbol == 't' || symbol == 'u' || symbol == 'v' || symbol == 'w' ||
                                symbol == 'x' || symbol == 'y' || symbol == 'z')
                            {
                                state = States.A; // перход к текущему состоянию 
                                value += symbol; // добавление символа к счетчику 
                                lenID++; // длина идентификатора 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход к ошибочному состоянию
                                value += "Ошибка! Ожидается буква английского алфавита"; // вывод ошибки 
                                errorPos = position; // присваение позиции ошибки текущей позиции 
                            }
                        break;
                    }
                    case (States.A):
                        {
                            if (symbol == 'a' || symbol == 'b' || symbol == 'c' ||
                                symbol == 'd' || symbol == 'e' || symbol == 'f' ||
                                symbol == 'g' || symbol == 'h' || symbol == 'i' ||
                                symbol == 'j' || symbol == 'k' || symbol == 'l' ||
                                symbol == 'm' || symbol == 'n' || symbol == 'o' ||
                                symbol == 'p' || symbol == 'q' || symbol == 'r' ||
                                symbol == 's' || symbol == 't' || symbol == 'u' ||
                                symbol == 'v' || symbol == 'w' || symbol == 'x' ||
                                symbol == 'y' || symbol == 'z' || symbol == '0' ||
                                symbol == '1' || symbol == '2' || symbol == '3' || 
                                symbol == '4' || symbol == '5' || symbol == '6' ||
                                symbol == '7' || symbol == '8' || symbol == '9')
                            {
                                state = States.A; // перход к следующему состоянию 
                                value += symbol; // добавление символа к счетчику 
                                lenID++; // увелечение стечика идентификатора 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                if(symbol == ' ')
                                {
                                    if (value == "for" || value == "to" || value == "do" || value == "by")
                                    {
                                        state = States.E; // переход к ошибочному символу 
                                        value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                        errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                    }
                                    else
                                    {
                                        state = States.B; // переход к следующему состоянию 
                                        Entry entry = new Entry(); // создание новой записи 
                                        entry.value = value; // присваение значения массива 
                                        entry.type = "Массив"; // присваение типа 
                                        idConsts.AddLast(entry); // добавления записи к списку иентификатору 
                                        position++; // переход к следующему символу 
                                    }
                                    
                                }
                                else
                                {
                                    if(symbol == ';' && position == chain.Length -1)
                                    {
                                        
                                        if(value == "for"|| value == "to"|| value == "do" || value == "by")
                                        {
                                            state = States.E; // переход к ошибочному состоянию 
                                            value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        }
                                        else
                                        {
                                            Entry entry = new Entry(); // создание новой записи 
                                            entry.value = value; // присваение значению записи значения идентификатора 
                                            entry.type = "Счетчик"; // присваение типа
                                            bool check = true; 
                                            foreach (Entry  en in idConsts) // проверка на существание записи в списке 
                                            {
                                                if(en.type == entry.type && en.value == entry.value)
                                                {
                                                    check = false;
                                                }
                                            }
                                            if(check)
                                            {
                                                idConsts.AddLast(entry); // добавление записи в список 
                                            }
                                            position++; // переход к следующему символу 
                                            state = States.F; // переход к завершающему символу 
                                        }
                                        
                                    }
                                    else
                                    {
                                        if(symbol == '[')
                                        {
                                            if (value == "for" || value == "to" || value == "do" || value == "by")
                                            {
                                                state = States.E; // переход к ошибочному состоянию 
                                                value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            }
                                            else
                                            {
                                                state = States.C; // переход к следующему состоянию 
                                                Entry entry = new Entry(); // создание новой записи 
                                                entry.value = value; 
                                                entry.type = "Массив";
                                                idConsts.AddLast(entry); // добавление новой записи в список идентификаторов
                                                value += symbol; // добавление символа к результату 
                                                position++; // переход к новому символу 
                                            }
                                               
                                        }
                                        else
                                        {
                                            state = States.E; // переход к ошибочному символу 
                                            value = "Ошибка! Ожидается ["; // вывод ошибки 
                                            errorPos = position; // присваение позиции ошибки значения текущего символа 
                                        }
                                        
                                    }
                                }
                            }
                            break;
                        }
                    case (States.B):
                        {
                            
                            if(symbol == '[')
                            {
                                state = States.C; // переход к следующему состоянию 
                                value += symbol; // добавление символа к результату 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                if(symbol == ' ')
                                {
                                    position++; // переход к следующему символу 
                                    state = States.B; // переход к следующему состоянию 
                                }
                                else
                                {
                                    state = States.E; // переход к ошибочному состоянию 
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                    value = "Ошибка! Ожидается ["; // вывод ошибки 
                                }
                            }
                            break;
                        }
                    case (States.C):
                        {
                            
                            switch(symbol) // просмотр символа 
                            {
                                case ' ':
                                    {
                                        state = States.C; // переход к следующему состоянию 
                                        position++; // переход к следующей позиции 
                                        break;
                                    }
                                case '+':
                                case '-':
                                    {
                                        state = States.H; // переход к слеующему состоянию 
                                        position++; // переход к следующей позиции 
                                        value += symbol; // добавления символа к результату  
                                        constValue += symbol; // добавления символа к результату
                                        break;
                                    }
                                case '0': case '1':
                                case '2': case '3':
                                case '4': case '5':
                                case '6': case '7':
                                case '8': case '9':
                                    {
                                        state = States.D; // переход к следующему состоянию 
                                        position++; // переход к следущему символу 
                                        value += symbol; // добавления символа к счетчику 
                                        constValue += symbol; // добавление символа к константе
                                        break;
                                    }
                                case 'a': case 'b': case 'c':
                                case 'd': case 'e': case 'f':
                                case 'g': case 'h': case 'i':
                                case 'j': case 'k': case 'l':
                                case 'm': case 'n': case 'o':
                                case 'p': case 'q': case 'r':
                                case 's': case 't': case 'u':
                                case 'v': case 'w': case 'x':
                                case 'y': case 'z':
                                    {
                                        lenID = 1; // обновление длины идентификатора  
                                        state = States.G; // переход на новое состояние
                                        value += symbol; // добавление символа к счетчику 
                                        constValue += symbol; // добавление символа к иденификатору 
                                        position++; // переход к следующей позиции 
                                        break;
                                    }
                                default:
                                    {
                                        state = States.E; // переход на ошибочное состояние 
                                        value = "Ошибка! Ожидается знак, число или буква английского алфавита"; // вывод ошибки 
                                        break;
                                    }
                            }                      
                            break;
                        }
                    case (States.H):
                        {
                            switch(symbol) // просмотр символа 
                            {
                                case '0': case '1':
                                case '2': case '3':
                                case '4': case '5':
                                case '6': case '7':
                                case '8': case '9':
                                    {
                                        state = States.D; // переход на новое состояние 
                                        position++; // переход к следующему символу 
                                        constValue += symbol; // добавление символа к константе 
                                        value += symbol; // добавление символа к счетчику 
                                        break;
                                    }
                                default:
                                    {
                                        state = States.E; // перход на ошибочное состояние
                                        value = "Ошибка! Ожидается число"; // вывод ошибки 
                                        errorPos = position; // присваение позиции ошибки значения текущей позиции
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.G):
                        {
                            switch(symbol)
                            {
                                case 'a': case 'b':
                                case 'c': case 'd':
                                case 'e': case 'f':
                                case 'g': case 'h':
                                case 'i': case 'j':
                                case 'k': case 'l':
                                case 'm': case 'n':
                                case 'o': case 'p':
                                case 'q': case 'r':
                                case 's': case 't':
                                case 'u': case 'v':
                                case 'w': case 'x':
                                case 'y': case 'z':
                                case '0': case '1':
                                case '2': case '3':
                                case '4': case '5':
                                case '6': case '7':
                                case '8': case '9':
                                    {
                                        state = States.G; // переход к следующему состоянию 
                                        value += symbol; // добавление символа к счетчику 
                                        constValue += symbol; // добавление символа к констане 
                                        position++; // переход к следующей позиции 
                                        lenID++; // увеличение счетчика идентификатора 
                                        break;
                                    }
                                case ']':
                                    {
                                        if (constValue == "for" || constValue == "to" || constValue == "do" || constValue == "by")
                                        {
                                            state = States.E; // переход к ошибочному состоянию 
                                            value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        }
                                        else
                                        {
                                            if(constValue == idConsts.First.Value.value)
                                            {
                                                state = States.E; // переход к ошибочному состоянию 
                                                value = "Ошибка! Идентификатор не может содержать сам себя как индекс!";// вывод ошибки 
                                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            }
                                            else
                                            {
                                                Entry entry = new Entry(); // создание записи 
                                                entry.value = constValue; // присваение записи значения константы 
                                                entry.type = "Индекс"; // присвоения типа 
                                                bool check = true; 
                                                foreach (Entry en in idConsts) // проверка на наличие записи в списке
                                                {
                                                    if (en.type == entry.type && en.value == entry.value)
                                                    {
                                                        check = false;
                                                    }
                                                }
                                                if (check) 
                                                {
                                                    idConsts.AddLast(entry); // добавление записи в список констант 
                                                } 
                                                constValue = ""; // очищение переменной для константы или идентификатора 
                                                state = States.J; //переход на новое состояние
                                                value += symbol; // добавления нового символа к счетчику 
                                                position++; // переход к следующему символу 
                                            }
                                            
                                        }
                                        break;
                                    }
                                case ',':
                                    {
                                        if (constValue == "for" || constValue == "to" || constValue == "do" || constValue == "by")
                                        {
                                            state = States.E; // переход к ошибочному состоянию 
                                            value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        }
                                        else
                                        {
                                            if (constValue == idConsts.First.Value.value)
                                            {
                                                state = States.E; // переход к ошибочному состоянию 
                                                value = "Ошибка! Идентификатор не может содержать сам себя как индекс!"; // вывод ошибки 
                                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            }
                                            else
                                            {
                                                Entry entry = new Entry(); // создание записи 
                                                entry.value = constValue; // присваение записи значения константы 
                                                entry.type = "Индекс"; // присвоения типа 
                                                bool check = true;
                                                foreach (Entry en in idConsts) // проверка на наличие записи в списке
                                                {
                                                    if (en.type == entry.type && en.value == entry.value)
                                                    {
                                                        check = false;
                                                    }
                                                }
                                                if (check)
                                                {
                                                    idConsts.AddLast(entry); // добавление записи в список констант 
                                                }
                                               
                                                constValue = ""; // очищение переменной для константы или идентификатора 
                                                state = States.C; //переход на новое состояние
                                                value += symbol; // добавления нового символа к счетчику 
                                                position++; // переход к следующему символу 
                                            }
                                               
                                        }
                                        break;
                                    }
                                case ' ':
                                    {
                                        if (constValue == "for" || constValue == "to" || constValue == "do" || constValue == "by")
                                        {
                                            state = States.E; // переход к ошибочному состоянию 
                                            value = "Ошибка! Идентификатор не может быть ключевым словом!"; // вывод ошибки 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        }
                                        else
                                        {
                                            if (constValue == idConsts.First.Value.value)
                                            {
                                                state = States.E; // переход к ошибочному состоянию 
                                                value = "Ошибка! Идентификатор не может содержать сам себя как индекс!"; // вывод ошибки 
                                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            }
                                            else
                                            {
                                                Entry entry = new Entry(); // создание новой записи 
                                                entry.value = constValue;
                                                entry.type = "Индекс";
                                                bool check = true; // флаг наличия записи в списке 
                                                foreach (Entry en in idConsts) // проверка на наличие записи в списке 
                                                {
                                                    if (en.type == entry.type && en.value == entry.value)
                                                    {
                                                        check = false; // изменение флага 
                                                    }
                                                }
                                                if (check) // проверка флага 
                                                {
                                                    idConsts.AddLast(entry); // добавление записи в список 
                                                }
                                                constValue = ""; // очищение переменной для константы
                                                state = States.I; // переход в следующее состояние 
                                                value += symbol; // добавления символа к счетчику 
                                                position++; // переход к следующему символу 
                                            }
                                           
                                        }
                                        
                                        break;
                                    }
                                default:
                                    {
                                        state = States.E; //переход в ошибочное состояние 
                                        value = "Ошибка! Ожидается символ ']', ',', пробел, буква или цифра!"; // вывод ошибки 
                                        errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.D):
                        {
                            switch (symbol)
                            {
                                case '0': case '1': case '2': case '3':
                                case '4': case '5': case '6': case '7':
                                case '8': case '9':
                                    {
                                        state = States.D; 
                                        value += symbol; // добавления символа к счетчику
                                        constValue += symbol; // добавления счетчика к константе
                                        position++; // переход к следующему символу 
                                        break;
                                    }
                                case ']':
                                    {
                                        if (Convert.ToInt32(constValue) < 32768 && Convert.ToInt32(constValue) > -32767)
                                        {
                                            Entry entry = new Entry(); // создание новой записи о константе 
                                            entry.value = constValue; 
                                            entry.type = "Индекс";
                                            bool check = true; // флаг наличия константы в списке констант 
                                            foreach (Entry en in numeralsConsts) // проверка 
                                            {
                                                if (en.type == entry.type && en.value == entry.value)
                                                {
                                                    check = false; // изменение флага
                                                }
                                            }
                                            if (check) // проверка флага 
                                            {
                                                numeralsConsts.AddLast(entry); // добавление константы в список 
                                            }
                                            constValue = ""; // очищение переменной для константы 
                                            state = States.J; // переход в новое состояние
                                            value += symbol; // добавления символа к счетчику 
                                            position++; // переход в следующему символу 
                                        }
                                        else
                                        {
                                            state = States.E; // переход в ошибочное состояние 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            value = "Ошибка! Число должно находиться в диапазоне от -32767 до 32768"; // вывод ошибки 

                                        }
                                           
                                        break;
                                    }
                                case ',':
                                    {
                                        if (Convert.ToInt32(constValue) < 32768 && Convert.ToInt32(constValue) > -32767) // проверка семантики
                                        {
                                            Entry entry = new Entry(); // создание записи 
                                            entry.value = constValue; 
                                            entry.type = "Индекс";
                                            bool check = true; // флаг проверки наличия записи в списке 
                                            foreach (Entry en in numeralsConsts) // проверка 
                                            {
                                                if (en.type == entry.type && en.value == entry.value)
                                                {
                                                    check = false; // изменение флага 
                                                }
                                            }
                                            if (check) // проверка флага 
                                            {
                                                numeralsConsts.AddLast(entry); // добавление записи в список констант 
                                            }

                                            constValue = ""; // очишение переменной для константы
                                            state = States.C; // переход к следующему состоянию 
                                            value += symbol; // добавление символа к счетчику
                                            position++; // перход к следующему символу 
                                        }
                                        else 
                                        {
                                            state = States.E; // переход к ошибочному стостоянию 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            value = "Ошибка! Число должно находиться в диапазоне от -32767 до 32768"; // вывод ошибки

                                        }
                                        break;
                                    }
                                case ' ':
                                    {
                                        if (Convert.ToInt32(constValue) < 32768 && Convert.ToInt32(constValue) > -32767) // проверка семантики
                                        {
                                            state = States.I; // переход к следующему состоянию 
                                            Entry entry = new Entry(); // создание записи 
                                            entry.value = constValue;
                                            entry.type = "Индекс";
                                            bool check = true; // флаг проверки наличия записи в списке 
                                            foreach (Entry en in numeralsConsts) // проверка 
                                            {
                                                if (en.type == entry.type && en.value == entry.value)
                                                {
                                                    check = false; // изменение флага 
                                                }
                                            }
                                            if (check) // проверка флага 
                                            {
                                                numeralsConsts.AddLast(entry); // добавление записи в список констант 
                                            }
                                            constValue = ""; // очищения переменной для константы
                                            position++; // переход к следующему символу
                                        }
                                        else
                                        {
                                            state = States.E; // переход к ошибочному сосоянию 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            value = "Ошибка! Число должно находиться в диапазоне от -32767 до 32678"; // вывод ошибки 
                                        }
                                        break;
                                    }
                                default:
                                    {
                                        state = States.E; // переход в ошибочное состояние 
                                        value = "Ошибка! Ожидается символ ']', ',', пробел или цифра "; // вывод ошибки 
                                        errorPos = position; //присваение позиции ошибки значения текущей позиции
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.I):
                        {
                            switch (symbol) // просмотр символа 
                            {
                                case ']':
                                    {
                                        state = States.J; // переход в новое состояние 
                                        value += symbol; // добавление символа к счетчику 
                                        position++; // переход к следующей позиции 
                                        break;
                                    }
                                case ',':
                                    {
                                        state = States.C; // переход в новое состояние
                                        value += symbol; // добавление символа к счетчику 
                                        position++; // переход к следующей позиции 
                                        break;
                                    }
                                case ' ':
                                    {
                                        state = States.I; //переход в новое состояние 
                                        position++; // переход к следующему символу 
                                        break;
                                    }
                                default:
                                    {
                                        state = States.E; // переход в ошибочное состояние 
                                        value = "Ошибка!"; // вывод ошибки 
                                        errorPos = position; // присваение позиции ошибки значения текущей позиции
                                        break;
                                    }
                            }
                            break;
                        }
                    case (States.J):
                        {
                            if(symbol == ';' && position == chain.Length -1)
                            {
                                state = States.F; // переход в конечное состояние
                            }
                            else
                            {
                                state = States.E; //переход в ошибочное состояние 
                                value = "Ошибка! Не ожидается никаких символов"; // вывод ошибки 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                break;
                            }
                            break;
                        }
                    case (States.F):
                        {
                            break;
                        }
                    case (States.E):
                        {
                            break;
                        }
                }

            }
            if (lenID > 8) // проверка семантики 
            {
                value = "Ошибка! Превышена длина идентификатора"; // вывод ошибки 
                state = States.E; // переход в ошибочное состояние 
            }
            return state == States.F;
        }
        public static bool CheckLoopFor(string chain, out string value, out int errorPos, out string startConst, out string endConst, out string step, out LinkedList<Entry> idConsts, out LinkedList<Entry> numeralsConsts) // реализация графа состояний цикла
        {
            States state = States.S; 
            errorPos = -1; // позиция ошибки 
            value = ""; // идентификатор или сообщение об ошибке
            startConst = ""; // начальное значение цикла 
            endConst = ""; // конечное значение
            step = ""; // шаг 
            idConsts = new LinkedList<Entry>(); // список идентификаторов
            numeralsConsts = new LinkedList<Entry>(); // список констант 
            int position = 0; // позиция в строке
            while((state != States.F) && (state != States.E) && (chain.Length > position))
            {
                switch (state) //переход на текущее состояние
                {
                    case States.S:
                        {
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++; 
                            }
                            if (chain.IndexOf("for") != position) 
                            {
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается ключевое слово for"; // вывод ошибки 
                                state = States.E; // переход в ошибочное состояние
                            }
                            else
                            {
                                position += 3; // переход  к следующему символу 
                                state = States.A; // переход в следующее состояние 
                            }
                            break;
                        }
                    case States.A:
                        {
                            if (chain[position] == ' ')
                            {
                                position++; // переход к следующему символу
                                state = States.B; // переход в следующее состояние 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.B:
                        {
                            string strID; // значение параметра 
                            string valueID; // значение параметра или ошибка 
                            int errorPosID; // позиция ошибки в параметре 
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++; 
                            }
                            if (chain.IndexOf('[') != -1 && chain.IndexOf(']') != -1) 
                            {
                                strID = chain.Substring(position, chain.IndexOf(']') - position + 1); // получение подстроки содержащей параметр 
                            }
                            else
                            {
                                strID = chain.Substring(position); //получение подстроки содержащей счетчик идентификатор 
                                if(strID.IndexOf(' ')!= -1)
                                {
                                    strID = strID.Substring(0, strID.IndexOf(' '));
                                }
                                
                            }
                            if (CheckID(strID, out valueID, out errorPosID, out idConsts, out numeralsConsts)) // проверка синтаксиса параметра 
                            {
                                state = States.C; // переход в следующее состояние 
                                position += strID.Length; // переход к следующему символу 
                                if(idConsts.First.Value.type != "Счетчик") // добавление счетчика к списку идентификаторов 
                                {
                                    Entry entry = new Entry();
                                    entry.type = "Счетчик";
                                    entry.value = valueID;
                                    idConsts.AddFirst(entry);
                                }
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position + errorPosID; // присваение позиции ошибки значения текущей позиции 
                                value = valueID; // вывод ошибки 
                            }
                            break;
                        }
                    case States.C:
                        {
                            if (chain[position] == ' ')
                            {
                                state = States.D; // переход в следующее состояние 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.D:
                        {
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++;
                            }
                            if (chain.IndexOf(":=") == position) 
                            {
                                state = States.G; // переход в следующее состояние 
                                position += 2; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается :="; //вывод ошибки 
                            }
                            break;
                        }
                    case States.G:
                        {
                            if (chain[position] == ' ') 
                            {
                                state = States.H; // переход в следующее состояние 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции
                                value += "Ошибка! Ожидается пробел"; //вывод ошибки 
                            }
                            break;
                        }
                    case States.H:
                        {
                            string valueConst; // переменная для константы 
                            int errorPosConst; // позиция ошибки внутри константы 
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++;
                            }
                            string strStartConst = chain.Substring(position); // получение подстроки начального значения 
                            if(strStartConst.IndexOf(' ')!= -1)
                            {
                                strStartConst = strStartConst.Substring(0, strStartConst.IndexOf(' ')); 
                            }
                            if(CheckConst(strStartConst, out valueConst, out errorPosConst )) // проверка синтаксиса константы 
                            {
                                if(Convert.ToInt32(valueConst) < 30000 && Convert.ToInt32(valueConst) > - 30000) // проверка семантики 
                                {
                                    position += strStartConst.Length; //переход к следующему символу 
                                    Entry entry = new Entry(); // создание новой записи 
                                    entry.value = strStartConst; 
                                    entry.type = "начальное значение";
                                    numeralsConsts.AddLast(entry); // добавление записи к списку констант 
                                    startConst = strStartConst; // вывод начального значения 
                                    state = States.I; // переход в новое состояние 
                                }
                                else
                                {
                                    state = States.E; // переход в ошибочное состояние 
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции
                                    value += "Ошибка! Число должно находиться в диапазоне от до" ; // вывод ошибки 
                                }
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position + errorPosConst; // // присваение позиции ошибки значения текущей позиции
                                value += valueConst; // вывод ошибки 
                            }
                            break;
                        }
                    case States.I:
                        {
                            if (chain[position] == ' ') 
                            {
                                state = States.J; // переход в следующее состояние 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции
                                value = "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.J:
                        {
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++; 
                            }
                            if (chain.LastIndexOf("to") == position)
                            {
                                state = States.K; // переход в следующее состояние 
                                position += 2; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции
                                value += "Ошибка! Ожидается to"; // вывод ошибки 
                            }
                            break;
                        }
                     case States.K:
                        {
                            if (chain[position] == ' ')
                            {
                                state = States.L; // переход в слеующее состояние  
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position; // // присваение позиции ошибки значения текущей позиции
                                value = "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.L:
                        {
                            string valueConst; // значение константы 
                            int errorPosConst; // позиция ошибки в константе 
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++;
                            }
                            string strEndConst = chain.Substring(position); // получение подстроки с константой 
                            if (strEndConst.IndexOf(' ') != -1)
                            {
                                strEndConst = strEndConst.Substring(0, strEndConst.IndexOf(' ')); 
                            }
                            if (CheckConst(strEndConst, out valueConst, out errorPosConst)) // проверка константы 
                            {
                                if (Convert.ToInt32(valueConst) < 32768 && Convert.ToInt32(valueConst) > -32767) // проверка сематники константы
                                {
                                    position += strEndConst.Length; // переход к следующему символу 
                                    state = States.M; // переход в следующее состояние 
                                    Entry entry = new Entry(); // создание новой записи 
                                    entry.value = strEndConst;
                                    entry.type = "конечное значение";
                                    numeralsConsts.AddLast(entry); // добавление записи в список констант 
                                    endConst = strEndConst; // присваение конечного значения цикла 
                                }
                                else
                                {
                                    state = States.E; // переход в ошибочное состояние 
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции
                                    value += "Ошибка! Число должно находиться в диапазоне от -32767 до 32768"; // вывод ошибки 
                                }
                                   
                            }
                            else
                            {
                                state = States.E; //переход в ошибочное сотояние
                                errorPos = position + errorPosConst; // присваение позиции ошибки значения текущей позиции
                                value += valueConst; // вывод ошибки 
                            }
                            break;
                        }
                    case States.M:
                        {
                            if (chain[position] == ' ')
                            {
                                state = States.N; //переход в новое сотояние
                                position++; // переход к следующему символу
                            }
                            else
                            {
                                state = States.E; //переход в ошибочное сотояние
                                errorPos = position; // присваение позиции ошибки значения текущей позиции
                                value += "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.N:
                        {
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++; // переход к следующему символу
                            }
                            if(chain.LastIndexOf("do")== position)
                            {
                                if(Convert.ToInt32(startConst)>Convert.ToInt32(endConst))
                                {
                                    state = States.E; //переход в ошибочное сотояние
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции
                                    value = "Ошибка! Начальное значение не может быть больше конечного при положительном шаге"; // вывод ошибки 
                                }
                                else
                                {
                                    position += 2; // переход к следующему символу
                                    state = States.F; // переход в заверщающее состояние
                                }
                                
                            }
                            else
                            {
                                if (chain.LastIndexOf("by") == position)
                                {
                                    position += 2; // переход к следующему символу
                                    state = States.O; // переход в новое состояние 
                                }
                                else
                                {
                                    state = States.E; //переход в ошибочное сотояние
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции
                                    value += "Ошибка! Ожидается do или by"; // вывод ошибки 
                                }
                            }
                            break;
                        }
                    case States.O:
                        {
                            if (chain[position] == ' ')
                            {
                                state = States.P; // переход в новое состояние 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; //переход в ошибочное сотояние
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.P:
                        {
                            string valueConst; // переменная для шага 
                            int errorPosConst; // переменная для ошибки в шаге 
                            while (chain[position] == ' ') // пропуск незначащих пробелов 
                            {
                                position++; // перезод к следующему символу 
                            }
                            string strStartConst = chain.Substring(position); // получение подстроки начиная с текущего символа 
                            strStartConst = strStartConst.Substring(0, strStartConst.IndexOf(' ')); // получения подстроки содержащей шаг
                            if (CheckConst(strStartConst, out valueConst, out errorPosConst)) // проверка шага 
                            {
                                if (Convert.ToInt32(valueConst) < 32768 && Convert.ToInt32(valueConst) > -32767)
                                {
                                    if(Convert.ToInt32(valueConst) == 0)
                                    {
                                        state = States.E; // переход в ошибочное состояние 
                                        errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                        value += "Ошибка! Бесконечный цикл"; // вывод ошибки
                                    }
                                    else
                                    {
                                        if(Convert.ToInt32(valueConst) >0 && Convert.ToInt32(startConst) > Convert.ToInt32(endConst))
                                        {
                                            state = States.E; // переход в ошибочное состояние 
                                            errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                            value = "Ошибка! Для положительного шага начальное значение должно быть меньше конечного!"; // вывод ошибки
                                        }
                                        else
                                        {
                                            if(Convert.ToInt32(valueConst) < 0 && Convert.ToInt32(startConst) < Convert.ToInt32(endConst))
                                            {
                                                state = States.E; // переход в ошибочное состояние 
                                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                                value = "Ошибка! Для отрицательного шага начальное значение должно быть больше конечного!"; // вывод ошибки
                                            }
                                            else
                                            {
                                                position += strStartConst.Length; // переход к следующему символу 
                                                step = strStartConst; // присваение значения шага 
                                                state = States.Q; // переход к новому состоянию 
                                                Entry entry = new Entry(); // создание новой записи 
                                                entry.value = step; // присваения значения 
                                                entry.type = "шаг цикла";  // присваение типа 
                                                numeralsConsts.AddLast(entry); // добавление в список констант
                                            }
                                        }
                                        
                                    }         
                                }
                                else
                                {
                                    state = States.E; // переход в ошибочное состояние
                                    errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                    value += "Ошибка! Число должно находиться в диапазоне от -32767 до 32768"; // вывод ошибки
                                }
                                   
                            }
                            else
                            {
                                state = States.E; // переход в ошибочное состояние 
                                errorPos = position + errorPosConst; // присваение позиции ошибки значения текущей позиции 
                                value += valueConst; // вывод ошибки из анализатора константы 
                            }
                            break;
                        }
                    case States.Q:
                        {
                            if (chain[position] == ' ')
                            {
                                state = States.R; // переход к новому стостоянию 
                                position++; // переход к следующему символу 
                            }
                            else
                            {
                                state = States.E; // переход к ошибочному состоянию 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается пробел"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.R:
                        {
                            while (chain[position] == ' ')
                            {
                                position++; // переход к следующему символу 
                            }
                            if (chain.LastIndexOf("do") == position)
                            {
                                position += 2;
                                state = States.F; // переход в завершающее состояние 
                            }
                            else
                            {
                                state = States.E; // переход к ошибочному состоянию 
                                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                                value += "Ошибка! Ожидается только do"; // вывод ошибки 
                            }
                            break;
                        }
                    case States.E:
                        {
                            break;
                        }
                    case States.F:
                        {
                            break;
                        }
                }
            }
            if(state != States.F && position == chain.Length)
            {
                errorPos = position; // присваение позиции ошибки значения текущей позиции 
                value += "Ошибка! Строка введена не полностью"; // вывод ошибки 
            }
            return state == States.F;
        }

    }
}
