using ChocolateFactory.Data;
using ChocolateFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Repository
{
    public class SeedData
    {
        public static List<Item> GetItems()
        {
            return new List<Item>
            {
                new Item("Шоколадный батончик Twix", 55, 2.22m, "twix.jpg", new NutritionalInfo(3.5m, 24.4m, 64.8m)),
                new Item("Конфета «Коммунарка» Столичные", 17, 0.51m, "stolichny.png", new NutritionalInfo(3.4m, 14.2m, 69.0m)),
                new Item("Шоколадный батончик «Mars» с нугой и карамелью", 50, 2.22m, "mars.jpg", new NutritionalInfo(3.9m, 17.7m, 70.0m)),
                new Item("Конфета «Коммунарка» Любимая Аленка", 35, 1.43m, "lyubimaya_alyonka.png", new NutritionalInfo(6.0m, 30.0m, 57.0m)),
                new Item("Конфета глазированная «Choko Nut»", 38, 0.32m, "choco_nut.png", new NutritionalInfo(5.5m, 23.0m, 65.0m)),
                new Item("Карамель «Chupa Chups»", 12, 0.49m, "chupa_chups.png", new NutritionalInfo(0.0m, 0.0m, 95.0m)),
                new Item("Конфета «Хрутка» Криспи с хрустящей вафлей", 22, 1.13m, "hrutka.jpg", new NutritionalInfo(5.9m, 29.0m, 59.0m)),
                new Item("Конфета «РотФронт» батончик №1", 19, 0.25m, "rotfront.png", new NutritionalInfo(10.0m, 29.0m, 55.0m)),
                new Item("Конфета «Сладуница» Самый умный", 13, 0.29m, "samiy_umniy.png", new NutritionalInfo(4.0m, 33.0m, 58.0m)),
                new Item("Конфета «Коммунарка» Батончик сливочный", 26, 0.54m, "kommunarka_batonchik.png", new NutritionalInfo(10.0m, 34.0m, 52.0m)),
                new Item("Карамель глазированная «Москвичка»", 17, 0.23m, "moskvichka.png", new NutritionalInfo(3.0m, 9.0m, 78.0m)),
                new Item("Конфета «Коммунарка» Стандарт 1969", 47, 0.86m, "standart_1969.png", new NutritionalInfo(3.5m, 18.1m, 65.0m)),
                new Item("Конфета «Goodmix» со вкусом соленой карамели", 44, 1.99m, "goodmix_solyonaya_karamel.png", new NutritionalInfo(6.1m, 28.0m, 61.0m))
            };
        }

        public static List<Gift> GetGifts()
        {
            List<Item> items = GetItems();

            Gift gift1 = new Gift("Gift Box 1", "gift_box_1.png");
            gift1.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Шоколадный батончик Twix")));
            gift1.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Карамель «Chupa Chups»")));
            gift1.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Шоколадный батончик «Mars» с нугой и карамелью")));

            Gift gift2 = new Gift("Gift Box 2", "gift_box_2.png");
            gift2.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Конфета «Коммунарка» Любимая Аленка")));
            gift2.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Конфета «Коммунарка» Батончик сливочный")));
            gift2.GiftItems.Add(new GiftItem(items.First(x => x.Name == "Конфета «Коммунарка» Столичные")));

            return new List<Gift> { gift1, gift2 };
        }
    }
}
