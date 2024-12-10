using ChocolateFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChocolateFactory.Data
{
    public class SeedData
    {
        public static List<Item> GetItems()
        {
            return new List<Item>
            {
                new Item("Шоколадный батончик Twix", 55, 2.22m, "twix.jpg", new NutritionalInfo(3.5, 24.4, 64.8)),
                new Item("Конфета «Коммунарка» Столичные", 17, 0.51m, "stolichny.png", new NutritionalInfo(3.4, 14.2, 69.0)),
                new Item("Шоколадный батончик «Mars» с нугой и карамелью", 50, 2.22m, "mars.jpg", new NutritionalInfo(3.9, 17.7, 70.0)),
                new Item("Конфета «Коммунарка» Любимая Аленка", 35, 1.43m, "lyubimaya_alyonka.png", new NutritionalInfo(6.0, 30.0, 57.0)),
                new Item("Конфета глазированная «Choko Nut»", 38, 0.32m, "choco_nut.png", new NutritionalInfo(5.5, 23.0, 65.0)),
                new Item("Карамель «Chupa Chups»", 12, 0.49m, "chupa_chups.png", new NutritionalInfo(0.0, 0.0, 95.0)),
                new Item("Конфета «Хрутка» Криспи с хрустящей вафлей", 22, 1.13m, "hrutka.jpg", new NutritionalInfo(5.9, 29.0, 59.0)),
                new Item("Конфета «РотФронт» батончик №1", 19, 0.25m, "rotfront.png", new NutritionalInfo(10.0, 29.0, 55.0)),
                new Item("Конфета «Сладуница» Самый умный", 13, 0.29m, "samiy_umniy.png", new NutritionalInfo(4.0, 33.0, 58.0)),
                new Item("Конфета «Коммунарка» Батончик сливочный", 26, 0.54m, "kommunarka_batonchik.png", new NutritionalInfo(10.0, 34.0, 52.0)),
                new Item("Карамель глазированная «Москвичка»", 17, 0.23m, "moskvichka.png", new NutritionalInfo(3.0, 9.0, 78.0)),
                new Item("Конфета «Коммунарка» Стандарт 1969", 47, 0.86m, "standart_1969.png", new NutritionalInfo(3.5, 18.1, 65.0)),
                new Item("Конфета «Goodmix» со вкусом соленой карамели", 44, 1.99m, "goodmix_solyonaya_karamel.png", new NutritionalInfo(6.1, 28.0, 61.0)),
                new Item("Nougat", 30, 1.50m, "nougat.png", new NutritionalInfo(2.0, 5.0, 40.0)),
                new Item("Taffy", 25, 1.30m, "taffy.png", new NutritionalInfo(1.0, 0.5, 30.0)),
            };
        }

        public static List<Gift> GetGifts()
        {
            List<Item> items = GetItems();

            Gift gift1 = new Gift("Gift Box 1", "gift_box_1.png");
            gift1.AddItem(new GiftItemModel(items.First(x => x.Name == "Шоколадный батончик Twix")));
            gift1.AddItem(new GiftItemModel(items.First(x => x.Name == "Карамель «Chupa Chups»")));
            gift1.AddItem(new GiftItemModel(items.First(x => x.Name == "Шоколадный батончик «Mars» с нугой и карамелью")));

            Gift gift2 = new Gift("Gift Box 2", "gift_box_2.png");
            gift2.AddItem(new GiftItemModel(items.First(x => x.Name == "Taffy")));
            gift2.AddItem(new GiftItemModel(items.First(x => x.Name == "Конфета «Коммунарка» Любимая Аленка")));
            gift2.AddItem(new GiftItemModel(items.First(x => x.Name == "Конфета «Коммунарка» Батончик сливочный")));
            gift2.AddItem(new GiftItemModel(items.First(x => x.Name == "Конфета «Коммунарка» Столичные")));

            return new List<Gift> { gift1, gift2 };
        }
    }
}
