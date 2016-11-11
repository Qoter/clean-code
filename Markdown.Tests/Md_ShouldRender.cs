using System;
using NUnit.Framework;

namespace Markdown.Tests
{
    [TestFixture]
    public class Md_ShouldRender
    {
        [TestCase("Текст _окруженный сдвух сторон_ бла бла бла", TestName = "01")]
        [TestCase("Можно \\_экранировать\\_ любой символ", TestName = "02")]
        [TestCase("__Два подчеркивания__ - делают текст жирным", TestName = "03")]
        [TestCase("Внутри __двойного выделения _одинарное_ тоже__ работает.", TestName = "04")]
        [TestCase("Но не наоборот — внутри _одинарного __двойное__ не работает_.", TestName = "05")]
        [TestCase("Подчерки внутри текста c цифрами _12_3 __12__3 не считаются выделением", TestName = "06")]
        [TestCase("Подчерки внутри слов не считаются выделением _h_e_l_l_o_world", TestName = "07")]
        [TestCase("__непарные _символы не считаются выделением.", TestName = "08")]
        [TestCase("Непробел после Иначе эти_ подчерки_ не считаются выделением ", TestName = "09")]
        [TestCase("Непробел до Иначе эти _подчерки _не считаются_ окончанием выделения и остаются просто символами подчерка", TestName = "10")]
        public void DoSomething_WhenSomething(string str)
        {
            Console.WriteLine(str);
            var a = new Md();

            Console.WriteLine(a.RenderToHtml(str));
        }
    }
}