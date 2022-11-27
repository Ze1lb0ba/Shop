using System;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            UserConnect.UserHello();
        }
    }
    class UserConnect
    {

        public static void UserHello()
        {
            Console.WriteLine("Добро пожаловать в Семерочку, выберите за чем вы пришли");
            Console.WriteLine("1-Овощи");
            Console.WriteLine("2-Фрукты");
            string hello = Console.ReadLine();
            Catalog.Cat(Check(hello));
        }
        public static int Check(string i)
        {
            if (int.TryParse(i, out int sel) == false || sel != 1 && sel != 2)
            {
                Console.WriteLine("Некорректно введены данные");
                UserHello();
                return sel;
            }
            else return sel;
        }
        
    }

    class Catalog
    {
        public int sel;
        Vegetables veg = new Vegetables();
        Catalog cat =  new Catalog();
        public static void Cat(int sel)
        {
            if (sel == 1) Vegetables.PrintPrice();
            else Fruit.PrintPrice();
        }

    }

    class Vegetables : Catalog
    {
        public static string[] description = new string[] { "Красные спелые", "Зеленые в пупырку", "В земле и глазках" };
        public static string[] name = new string[] { "Томаты", "Огурцы", "Картофель" };
        public static int[] count = new int[] { 3, 2, 5 };
        public static int[] price = new int[] { 50, 25, 10 };
        

        public static void PrintPrice()
        {
            Programs.GiveInfoAbout(description, name, count, price);
           
        }

    }

    class Fruit : Catalog
    {
        public static string[] description = new string[] { "Будь как Ньютон", "Спелая садовая", "Нельзя скушать" };
        public static string[] name = new string[] { "Яблоки", "Слива", "Груша" };
        public static int[] count = new int[] { 7, 15, 10 };
        public static int[] price = new int[] { 50, 75, 60 };

        public static void PrintPrice()
        {
            Programs.GiveInfoAbout(description, name, count, price);
        }
    }

    class Programs
    {
        public static string[] pdes;
        public static string[] pname;
        public static int[] pcount;
        public static int[] pprice;
        public static int selectOfUser;
        public static int kg;
        public static void GiveInfoAbout(string[] des, string[] name, int[] count, int[] price)
        {
            pdes = des;
            pname = name;
            pcount = count;
            pprice = price;
            for(int i = 0; i <name.Length; i++)
            {
                Console.WriteLine("{0} - {1}", i + 1, name[i]);
            }
            Console.WriteLine("4 - Назад");
            selectOfUser = Checker(Console.ReadLine());
            PrintInfo(selectOfUser);
            Console.WriteLine("Сколько вы хотите купить данного товара? Вес товара в киллограммах указывайте целыми числами");
            kg = UserAdressInfo.CheckInt();
            if (RemainStore(kg, selectOfUser) == true)
            { 
                Console.WriteLine("Товар успешно добавлен в корзину");
                Cart.AddCart();
            }

        }
        public static int Checker(string str)
        {
            if (int.TryParse(str, out int select) == false || select <0 || select > pname.Length+1)
            {
                Console.WriteLine("Ошибочно указаны значения");
                GiveInfoAbout(pdes, pname, pcount, pprice);
            }
            else if(select == 4)
                 UserConnect.UserHello();  
           
            return select;
        }

        public static void PrintInfo(int select)
        {
            Console.WriteLine("");
            Console.WriteLine("Товар: " + pname[select-1]);
            Console.WriteLine(pdes[select - 1]);
            Console.WriteLine("Остаток: " + pcount[select - 1]+" кг");
            Console.WriteLine("Цена: " + pprice[select - 1]);
            
        }

        public static bool RemainStore(int kg, int select)
        {
            if (pcount[select - 1] >= kg)
                return true;
            else
            {
                Console.WriteLine("Товара на складе не достаточно");
                GiveInfoAbout(pdes,pname,pcount,pprice);
                return false;
            }
        }

        public static void CouriersOnFire(int personSelect)
        {
            if(personSelect == 1)  CouriersWolk.ZP();
            if (personSelect == 2) CouriersOnCar.ZP();
        }

        public static void EnterPhoneNumber()
        {
            string number =Console.ReadLine();
            int[] numberletters = new int[number.Length];
            char[] PhoneNumber = new char[number.Length];
            if (number.Length == 11)
            {
                for (int i = 0; i < number.Length; i++)
                {
                    string j = Convert.ToString(number[i]);
                    if (int.TryParse(j, out int x) == true)
                    {
                        numberletters[i] = x;
                        
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неправильный номер, повторите попытку.");
                        EnterPhoneNumber();
                    } 
                }
            }
            else 
            {
                Console.WriteLine("Вы ввели неправильный номер, повторите попытку. Номер должен состоять из 11 цифр, без знаков");
                EnterPhoneNumber();
            }
            
            Console.WriteLine("Ваш номер телефона:");

            for (int i = 0; i < numberletters.Length; i++)
            {
                
                Console.Write(numberletters[i]);
                
            }
            Console.WriteLine("");
        }
    }

    abstract class Delivery
    {
        
        private int countOfCourier = 5;
        public int CountOfCourier
        {
            get { return countOfCourier; }
            set
            {
                if (countOfCourier > 0)
                {
                    Console.WriteLine("Курьер выехал,ожидайте свой заказ");
                }
                else
                {
                    Console.WriteLine("К сожалению все курьеры сейчас заняты, заберите заказ из магазина");
                }
            }
        }
        public virtual void Deliv() 
        {
            Console.WriteLine("Спасибо за Ваш выбор!");
        }
    }
    class Courier : Delivery
    {
        (string street, int house, int floor, int room) adress;
        public static string phoneNumber = "8(999)999-99-99";
        public static string shopAdress = "Сусликовый переулок дом 13";
        public override void Deliv()
        {
            base.Deliv();
            Console.WriteLine("Ищем свободного курьера...");
            adress = UserAdressInfo.EnterAdress();
            Console.WriteLine("В ближайшее время мы отправим вам курьера по адресу");
            Console.WriteLine("{0}, дом {1}, квартира {2}, этаж {3}", adress.street, adress.house, adress.room, adress.floor);
            DateTime curentDateTime = DateTime.Now;
            curentDateTime.Print();
            Console.WriteLine("По всем вопросам обращайтесь по номеру: " + phoneNumber);
            CountOfCourier--;
        }
    }
    class InShop : Delivery
    {
        public override void Deliv()
        {
            base.Deliv();
            Console.WriteLine("Ваш заказ формируется, введите номер телефона на который будет доставлена СМС о готовности заказа");
            Programs.EnterPhoneNumber();
            Console.WriteLine("Ваш заказ ожидает вас по адрессу {0}", Courier.shopAdress);
            Console.WriteLine("По всем вопросам обращайтесь по номеру: " + Courier.phoneNumber);
        }
    }
    class UserAdressInfo : Courier
    {

        static Random  rnd = new Random();
        public static (string street, int house, int floor, int room) EnterAdress()
        {
            (string street, int house, int floor, int room) adres;
            Console.WriteLine("Для того чтобы курьер нашел вас, необходимо указать некоторую информацию");
            Console.WriteLine("Насколько срочно необходимо доставить ваш заказ? (1 - Не срочно / 2 - срочно)");
            Programs.CouriersOnFire(int.Parse(Console.ReadLine()));
            Console.WriteLine("Введите название улицы");
            adres.street = Console.ReadLine();
            Console.WriteLine("Введите номер дома");
            adres.house = CheckInt();
            Console.WriteLine("Введите этаж");
            adres.floor = CheckInt();
            Console.WriteLine("Введите квартиру");
            adres.room = CheckInt();
            Console.WriteLine("Введите Номер телефона");
            Programs.EnterPhoneNumber();
            NumberOrder<int>.OrderName = rnd.Next(1, 100);
            return adres;
        }

        public static int CheckInt()
        {
            string myint = Console.ReadLine();
            if (int.TryParse(myint, out int rdyint)==true && rdyint >0)
            {
                return rdyint;
            }
            else
            {
                Console.WriteLine("Данные введены с ошибкой, попробуйте ещё раз");
                CheckInt();
                return rdyint;
            }
        }
    }

    class NumberOrder<T>
    {
        
        private static T orderName;
        public static T OrderName
        {
            set { orderName = value; }
            get { return orderName; }   
        }
    }
    abstract class Couriers
    {
        public static int payForDelivery = 300;
    }

    class CouriersWolk: Couriers
    {
       private static int numberOfDelivery = 0;
       private static int coef = 1;
       public static int zp = 0;

        public int NumberOfDelivery
        {
            get { return numberOfDelivery++; }         
        }

        public static void ZP()
        {
            zp = numberOfDelivery * coef * payForDelivery;
            Console.WriteLine("Пеший курьер прибудет в течении часа");
        }

  
    }
    class CouriersOnCar:Couriers
    {
        private static int numberOfDelivery = 0;
        private static int coef = 2;
        public static int zp = 0;
        public  int NumberOfDelivery
        {
            get { return numberOfDelivery++; }
        }
        public static void ZP()
        {
            zp = numberOfDelivery * coef * payForDelivery;
            Console.WriteLine("Экспресс доставка на автомобиле будет у вас в течении 15 минут");
        }

    }
    static class MyExtention
    {
        public static void Print(this DateTime dateTime)
        {
            Console.WriteLine(dateTime);
        } 
    }
    class Cart : Programs
    {
        static string[] name;
        static string[] reservname;
        static int[] price;
        static int[] reservprice;
        static int[] count;
        static int[] reservcount;
        static Courier cour = new Courier();
        static InShop inshop = new InShop();
        static int x = 0;
        static int summ = 0;

        public static void AddCart()
        {
            if (x == 0)
            {
                name = new string[] { pname[selectOfUser-1] } ;
                price = new int[] { pprice[selectOfUser - 1] };
                count = new int[] { kg };
                x++;
            }
            else if (x != 0)
            {
                
                name = (string[])LongerForMassive(name, reservname, pname);
                price = (int[])LongerForMassive(price, reservprice, pprice);
                reservcount = count;
                count = new int[count.Length + 1];
                for(int i = 0; i<reservcount.Length; i++)
                {
                    count[i] = reservcount[i];
                }
                count[count.Length - 1] = kg;
                pcount[selectOfUser - 1] -= kg;
            }
            CartState();
        }

        public static TCollection[] LongerForMassive<TCollection>(TCollection[] lname, TCollection[] lreservname, TCollection[] pname)
        {
            lreservname = lname;
            lname = new TCollection[lname.Length+1];
            for (int i = 0; i < lname.Length - 1; i++)
            {
                lname[i] = lreservname[i];
            }
            lname[lname.Length-1] = pname[selectOfUser-1];
            return lname;
            
        }
        public static void CartState()
        {
            Console.WriteLine("");
            Console.WriteLine("В вашей корзине сейчас:");
            Console.WriteLine("");
            Printcart(name, count);
            Console.WriteLine("");
            Console.WriteLine("На общую сумму:");
            Console.WriteLine(Printcard(price, count));
            Console.WriteLine("Хотите ли вы продолжить покупки? 1 - да/ 0 - нет");
            WhatToDo(int.Parse(Console.ReadLine()));
        }

        static void Printcart(string[]names, int[] counts)
        {
            for (int i = 0; i < names.Length; i++)
                Console.Write(counts[i]+ " кг - "+ names[i] + ", ");
        }

        static int Printcard(int[] rcount, int[] rprice)
        {
            for(int i = 0; i<rcount.Length; i++)
            {
                summ = summ + (rcount[i] * rprice[i]);
            }
            return summ;
        }
        static void WhatToDo(int answer)
        {
            if (answer == 0)
            {
                Console.WriteLine("Вы желаете чтобы ваш заказ доставили на дом, или придете за ним лично? (0-на дом/1-заберу в магазине)");
                int answer2 = int.Parse(Console.ReadLine());
                if (answer2 == 0)
                    cour.Deliv();
                else
                    inshop.Deliv();
            }
            else
            {
                UserConnect.UserHello();
            }
        }
    }
}
